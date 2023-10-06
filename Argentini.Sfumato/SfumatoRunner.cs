using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;

namespace Argentini.Sfumato;

public sealed class SfumatoRunner
{
	#region Properties
	
	public SfumatoAppState AppState { get; } = new();

	public static int IndentationSpaces => 4;

	public static int MaxConsoleWidth => GetMaxConsoleWidth();
	
	private static int GetMaxConsoleWidth()
	{
		try
		{
			return Console.WindowWidth - 1;
		}
		catch
		{
			return 78;
		}
	}
	
    //public List<FileSystemWatcher> ProjectFileSystemWatchers { get; } = new();

	#endregion

	public SfumatoRunner()
	{
		var timer = new Stopwatch();

		timer.Start();
		
		TypeAdapterConfig<ScssNode, ScssNode>.NewConfig()
			.PreserveReference(true)
			.AfterMapping((src, dest) => 
			{
				dest.Classes = src.Classes.Adapt<List<ScssClass>>();
				dest.Nodes = src.Nodes.Adapt<List<ScssNode>>();
			});
		
#if DEBUG
		AppState.DiagnosticMode = true;
#endif		

		AppState.DiagnosticOutput.Append($"Cold start in {timer.Elapsed.TotalSeconds:N3} seconds{Environment.NewLine}");
	}

	#region Entry Points
	
	/// <summary>
	/// Loads settings and app state.
	/// </summary>
	public async Task InitializeAsync(IEnumerable<string>? args = null)
	{
		await AppState.InitializeAsync(args ?? Enumerable.Empty<string>());
	}
	
	/// <summary>
	/// Build the project's CSS and write to storage.
	/// </summary>
	/// <param name="runner"></param>
	public async Task GenerateProjectCssAsync()
	{
		var timer = new Stopwatch();
		
		await AppState.GatherUsedScssCoreClassesAsync();

		timer.Start();

		Console.Write("Generating CSS...");
		
		var projectScss = AppState.StringBuilderPool.Get();

		projectScss.Append(AppState.ScssInjectableCore);
		projectScss.Append(await GenerateScssObjectTreeAsync());
		
		//await File.WriteAllTextAsync(Path.Combine(CssOutputPath, "sfumato.scss"), projectScss.ToString());

		AppState.DiagnosticOutput.Append($"Generated sfumato.scss ({projectScss.Length.FormatBytes()}) in {timer.Elapsed.TotalSeconds:N3} seconds{Environment.NewLine}");

		timer.Restart();

		var fileSize = await SfumatoScss.TranspileScss(projectScss, AppState, AppState.ReleaseMode);
		
		Console.WriteLine($" saved sfumato.css ({fileSize.FormatBytes()})");
		AppState.DiagnosticOutput.Append($"Transpiled sfumato.css ({fileSize.FormatBytes()}) in {timer.Elapsed.TotalSeconds:N3} seconds{Environment.NewLine}");

		AppState.StringBuilderPool.Return(projectScss);
	}
	
	#endregion
	
	#region Generation Methods
	
	/// <summary>
	/// Get the complete SCSS class from the cached file from storage.  
	/// </summary>
	/// <param name="scssClass"></param>
	/// <param name="excludeDeclaration"></param>
	/// <returns></returns>
	private string GetScssClassMarkupFromCachedFile(ScssClass scssClass, bool excludeDeclaration = false)
	{
		var startIndex = AppState.ScssFiles[scssClass.FilePath].StartsWith($".{scssClass.RootClassName}", StringComparison.Ordinal) ? 0 : AppState.ScssFiles[scssClass.FilePath].IndexOf($"\n.{scssClass.RootClassName}", StringComparison.Ordinal);
		var endIndex = -1;

		if (startIndex < 0)
			return string.Empty;

		endIndex = AppState.ScssFiles[scssClass.FilePath].IndexOf("\n.", startIndex + 1, StringComparison.Ordinal);

		if (endIndex < 0)
		{
			// End of file, no more classes; grab last closing brace
			endIndex = AppState.ScssFiles[scssClass.FilePath].LastIndexOf("}", startIndex + 1, StringComparison.Ordinal);
		}

		else
		{
			// More classes follow so use the next one as a marker for where the current class ends
			while (endIndex > startIndex)
			{
				if (AppState.ScssFiles[scssClass.FilePath][--endIndex] == '}')
					break;
			}
			
			if (AppState.ScssFiles[scssClass.FilePath][endIndex] != '}')
				endIndex = -1;
		}

		if (excludeDeclaration)
		{
			// Repoint start index to the opening brace
			startIndex = AppState.ScssFiles[scssClass.FilePath].IndexOf("{", startIndex, StringComparison.Ordinal);
			
			if (startIndex < 0)
				return string.Empty;

			startIndex++;
			
			// Repoint end index to the closing brace
			while (AppState.ScssFiles[scssClass.FilePath][endIndex] != '}')
			{
				endIndex--;
			}

			endIndex--;

			if (endIndex < 0)
				return string.Empty;
		}

		if (endIndex < 0 || endIndex <= startIndex)
			return string.Empty;

		var result = AppState.ScssFiles[scssClass.FilePath].Substring(startIndex, endIndex - startIndex + 1);

		result = result.TrimStart('\n').TrimEnd().TrimEnd('\n');
		
		return result;
	}

	/// <summary>
	/// Generate SCSS class from prefixed class name, including nesting.
	/// Only includes pseudoclasses, not media queries.
	/// </summary>
	/// <param name="scssClass"></param>
	/// <param name="stripPrefix"></param>
	/// <returns></returns>
	public async Task<string> GenerateScssClassMarkupAsync(ScssClass scssClass, string stripPrefix = "")
	{
		var scssResult = AppState.StringBuilderPool.Get();
		var level = 0;
		var className = scssClass.ClassName;
		var scssBody = string.Empty;

		if (string.IsNullOrEmpty(stripPrefix) == false && className.StartsWith(stripPrefix, StringComparison.Ordinal))
			className = className.TrimStart(stripPrefix) ?? className;
		
		var segments = className.Split(':', StringSplitOptions.RemoveEmptyEntries);

		if (segments.Length == 0)
			return string.Empty;
			
		if (segments.Length > 1)
		{
			var prefixes = new string[segments.Length - 1];
			var renderedClassName = false;

			Array.Copy(segments, prefixes, segments.Length - 1);
			
			scssBody = GetScssClassMarkupFromCachedFile(scssClass, true);

			foreach (var prefix in prefixes)
			{
				if (IsPseudoclassPrefix(prefix) == false)
					continue;
				
				if (renderedClassName == false)
				{
					scssResult.Append($"{Indent(level)}.{scssClass.EscapedClassName} {{\n");
					renderedClassName = true;
					level++;
				}

				var pseudoClass = AppState.PseudoclassPrefixes.First(p => p.Key.Equals(prefix, StringComparison.Ordinal));
					
				scssResult.Append($"{Indent(level)}{pseudoClass.Value}\n");
				level++;
			}
		}

		else
		{
			scssBody = GetScssClassMarkupFromCachedFile(scssClass, true);
			scssResult.Append($"{Indent(level)}.{scssClass.EscapedClassName} {{\n");
			level++;
		}
		
		var existingIndentation = scssBody.FirstNonSpaceCharacter();

		scssResult.Append($"{scssBody.Indent(level * IndentationSpaces - existingIndentation)}\n");

		while (level > 0)
		{
			level--;
			scssResult.Append($"{Indent(level)}}}\n");
		}
		
		var result = scssResult.ToString();
		
		AppState.StringBuilderPool.Return(scssResult);
		
		return await Task.FromResult(result);
	}
	
	/// <summary>
	/// Iterate used classes and build a tree to consolidate descendants.
	/// Serializing this tree into SCSS markup will prevent
	/// duplication of media queries, etc. to keep the file size down.
	/// </summary>
	/// <param name="config"></param>
	/// <returns></returns>
	public async Task<string> GenerateScssObjectTreeAsync()
	{
		var usedClasses = AppState.UsedClasses.OrderBy(c => c.ClassName).ToList().Adapt<List<ScssClass>>();
		var hierarchy = new ScssNode
		{
			Prefix = string.Empty,
			PrefixPath = string.Empty
		};

		#region Build Hierarchy
		
		foreach (var scssClass in usedClasses)
		{
			// Handle base classes (no prefixes) or prefixes start with pseudoclass (no inheritance)

			if (scssClass.ClassName.Contains(':') == false || IsPseudoclassPrefix(scssClass.Prefixes[0]))
			{
				hierarchy.Classes.Add(scssClass);
			}

			else
			{
				var prefixPath = string.Empty;
				var node = hierarchy;

				foreach (var prefix in scssClass.Prefixes)
				{
					if (IsMediaQueryPrefix(prefix) == false)
						break;
					
					prefixPath += $"{prefix}:";
					
					var prefixNode = node.Nodes.FirstOrDefault(n => n.Prefix.Equals(prefix, StringComparison.Ordinal));

					if (prefixNode is null)
					{
						var newNode = new ScssNode
						{
							Prefix = prefix,
							PrefixPath = prefixPath
						};

						node.Nodes.Add(newNode);
						node = newNode;
					}

					else
					{
						node = prefixNode;
					}
				}
				
				node.Classes.Add(scssClass);
			}			
		}
		
		#endregion

		if (AppState.Settings.ThemeMode.Equals("class", StringComparison.OrdinalIgnoreCase))
		{
			// Search first level nodes for "dark" prefixes and add additional nodes to support "auto-theme";
			// Light mode will work without any CSS since removing "dark-theme" and "auto-theme" from the
			// <html> class list will disabled dark mode altogether.
			foreach (var node in hierarchy.Nodes.ToList())
			{
				if (node.Prefix.Equals("dark", StringComparison.Ordinal) == false)
					continue;
				
				var newNode = node.Adapt<ScssNode>();

				newNode.Prefix = "auto-dark";
					
				hierarchy.Nodes.Add(newNode);
			}
		} 
		
		var sb = AppState.StringBuilderPool.Get();
		
		await GenerateScssObjectTreeRecurseAsync(hierarchy, sb);
        
		var scss = sb.ToString();
		
		AppState.StringBuilderPool.Return(sb);
		
		return scss;
	}
	private async Task GenerateScssObjectTreeRecurseAsync(ScssNode scssNode, StringBuilder sb)
	{
		if (string.IsNullOrEmpty(scssNode.Prefix) == false)
		{
			var prefix = scssNode.Prefix;

			if (prefix.Equals("auto-dark", StringComparison.Ordinal))
				prefix = "dark";
			
			var mediaQueryPrefix = AppState.MediaQueryPrefixes.First(p => p.Key.Equals(prefix));

			if (AppState.Settings.ThemeMode.Equals("class", StringComparison.OrdinalIgnoreCase) && scssNode.Prefix == "dark")
			{
				sb.Append($"{Indent(scssNode.Level - 1)}html.dark-theme {{\n");
			}

			else if (AppState.Settings.ThemeMode.Equals("class", StringComparison.OrdinalIgnoreCase) && scssNode.Prefix == "auto-dark")
			{
				sb.Append($"{Indent(scssNode.Level - 1)}html.auto-theme {{ {mediaQueryPrefix.Value}\n");
			}
			
			else
			{
				sb.Append($"{Indent(scssNode.Level - 1)}{mediaQueryPrefix.Value}\n");
			}
		}
			
		if (scssNode.Classes.Count > 0)
		{
			foreach (var scssClass in scssNode.Classes)
			{
				var markup = await GenerateScssClassMarkupAsync(scssClass, scssNode.PrefixPath);
					
				sb.Append($"{markup.Indent(scssNode.Level * IndentationSpaces - markup.FirstNonSpaceCharacter()).TrimEnd('\n')}\n");
			}
		}
			
		if (scssNode.Nodes.Count > 0)
		{
			foreach (var node in scssNode.Nodes)
			{
				await GenerateScssObjectTreeRecurseAsync(node, sb);
			}
		}
			
		if (string.IsNullOrEmpty(scssNode.Prefix) == false)
		{
			sb.Append($"{Indent(scssNode.Level - 1)}}}\n");
		}

		if (scssNode.Prefix == "auto-dark")
		{
			sb.Append($"{Indent(scssNode.Level - 1)}}}\n");
		}
	}
	
	#endregion
	
	#region Helper Methods
	
	/// <summary>
	/// Create space indentation based on level of depth.
	/// </summary>
	/// <param name="level"></param>
	/// <returns></returns>
	private static string Indent(int level)
	{
		return new string(' ', level * IndentationSpaces);
	}

	/// <summary>
	/// Determine if a prefix is a media query prefix.
	/// </summary>
	/// <param name="prefix"></param>
	/// <returns></returns>
	private bool IsMediaQueryPrefix(string prefix)
	{
		var mediaQueryPrefix = AppState.MediaQueryPrefixes.FirstOrDefault(p => p.Key.Equals(prefix, StringComparison.Ordinal));
		return string.IsNullOrEmpty(mediaQueryPrefix.Key) == false;
	}

	/// <summary>
	/// Determine if a prefix is a pseudoclass prefix.
	/// </summary>
	/// <param name="prefix"></param>
	/// <returns></returns>
	private bool IsPseudoclassPrefix(string prefix)
	{
		var pseudoclassPrefix = AppState.PseudoclassPrefixes.FirstOrDefault(p => p.Key.Equals(prefix, StringComparison.Ordinal));
		return string.IsNullOrEmpty(pseudoclassPrefix.Key) == false;
	}
	
	#endregion
}

public sealed class ScssClass
{
	public string FilePath { get; set; } = string.Empty;

	private string _className = string.Empty;
	public string ClassName
	{
		get => _className;

		set
		{
			_className = value;

			if (_className.LastIndexOf(':') > -1 && _className.LastIndexOf(':') < _className.Length - 1)
			{
				var segments = _className.Split(':', StringSplitOptions.RemoveEmptyEntries);

				if (segments.Length == 0)
					Prefixes = Array.Empty<string>();
			
				if (segments.Length == 1)
				{
					RootClassName = segments[0];
				}

				Prefixes = new string[segments.Length - 1];

				Array.Copy(segments, Prefixes, segments.Length - 1);
				
				RootClassName = segments[^1];
			}
			
			else
			{
				RootClassName = _className;
			}
		}
	}

	public string RootClassName { get; private set; } = string.Empty;
	public string EscapedClassName => ClassName.Replace(":", "\\:");
	public string[] Prefixes { get; private set; } = Array.Empty<string>();

}

public sealed class ScssNode
{
	public string Prefix { get; set; } = string.Empty; // e.g. dark

	public string PrefixPathValue = string.Empty;
	public string PrefixPath // e.g. dark:tabp:
	{
		get => PrefixPathValue;

		set
		{
			PrefixPathValue = value;
			Level = 0;

			if (string.IsNullOrEmpty(PrefixPathValue))
				return;

			var segmentCount = PrefixPathValue.Split(':', StringSplitOptions.RemoveEmptyEntries).Length;

			Level = segmentCount;
		}
	}
	public int Level { get; set; }
	public List<ScssClass> Classes { get; set; } = new();
	public List<ScssNode> Nodes { get; set; } = new();
}
