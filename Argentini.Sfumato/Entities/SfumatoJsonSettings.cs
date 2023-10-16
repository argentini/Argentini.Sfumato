namespace Argentini.Sfumato.Entities;

public class SfumatoJsonSettings
{
    public string CssOutputPath { get; set; } = string.Empty;

    public List<ProjectPath> ProjectPaths { get; set;  } = new();

    public string ThemeMode { get; set; } = "system";
    public bool UseAutoTheme { get; set; }

    public Breakpoints? Breakpoints { get; set; } = new();
    public FontSizeUnits? FontSizeUnits { get; set; } = new();
}

public sealed class ProjectPath
{
    public string Path { get; set; } = string.Empty;
    public string FileSpec { get; set; } = "*.html";
    public bool Recurse { get; set; } = false;
    public bool IsFilePath { get; set; }
}

public sealed class Breakpoints
{
    private int _zero;
    public int Zero
    {
        get => _zero;
        set
        {
            _zero = value switch
            {
                < 0 => 0,
                _ => value
            };
        }
    }
    private int _phab = 400;
    public int Phab
    {
        get => _phab;
        set
        {
            _phab = value switch
            {
                < 0 => 400,
                _ => value
            };
        }
    }
    private int _tabp = 540;
    public int Tabp
    {
        get => _tabp;
        set
        {
            _tabp = value switch
            {
                < 0 => 540,
                _ => value
            };
        }
    }
    private int _tabl = 800;
    public int Tabl
    {
        get => _tabl;
        set
        {
            _tabl = value switch
            {
                < 0 => 800,
                _ => value
            };
        }
    }
    private int _note = 1280;
    public int Note
    {
        get => _note;
        set
        {
            _note = value switch
            {
                < 0 => 1280,
                _ => value
            };
        }
    }
    private int _desk = 1440;
    public int Desk
    {
        get => _desk;
        set
        {
            _desk = value switch
            {
                < 0 => 1440,
                _ => value
            };
        }
    }
    private int _elas = 1600;
    public int Elas
    {
        get => _elas;
        set
        {
            _elas = value switch
            {
                < 0 => 1600,
                _ => value
            };
        }
    }
}

public sealed class FontSizeUnits
{
    public string Zero { get; set; } = "4.35vw";
    public string Phab { get; set; } = "4vw";
    public string Tabp { get; set; } = "1.6vw";
    public string Tabl { get; set; } = "1vw";
    public string Note { get; set; } = "1vw";
    public string Desk { get; set; } = "1vw";
    public string Elas { get; set; } = "1vw";
}
