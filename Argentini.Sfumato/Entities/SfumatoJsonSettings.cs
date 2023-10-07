namespace Argentini.Sfumato.Entities;

public class SfumatoJsonSettings
{
    public string CssOutputPath { get; set; } = string.Empty;

    public List<ProjectPath> ProjectPaths { get; set;  } = new();

    public string ThemeMode { get; set; } = "system";

    public Breakpoints? Breakpoints { get; set; } = new();
    public FontSizeViewportUnits? FontSizeViewportUnits { get; set; } = new();
}

public sealed class ProjectPath
{
    public string Path { get; set; } = string.Empty;
    public string FileSpec { get; set; } = "*.html";
    public bool Recurse { get; set; } = false;
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

public sealed class FontSizeViewportUnits
{
    private double _zero = 4.35;
    public double Zero
    {
        get => _zero;
        set
        {
            _zero = value switch
            {
                < 0 => 4.35,
                _ => value
            };
        }
    }
    private double _phab = 4;
    public double Phab
    {
        get => _phab;
        set
        {
            _phab = value switch
            {
                < 0 => 4,
                _ => value
            };
        }
    }
    private double _tabp = 1.6;
    public double Tabp
    {
        get => _tabp;
        set
        {
            _tabp = value switch
            {
                < 0 => 1.6,
                _ => value
            };
        }
    }
    private double _tabl = 1;
    public double Tabl
    {
        get => _tabl;
        set
        {
            _tabl = value switch
            {
                < 0 => 1,
                _ => value
            };
        }
    }
    private double _note = 1;
    public double Note
    {
        get => _note;
        set
        {
            _note = value switch
            {
                < 0 => 1,
                _ => value
            };
        }
    }
    private double _desk = 1;
    public double Desk
    {
        get => _desk;
        set
        {
            _desk = value switch
            {
                < 0 => 1,
                _ => value
            };
        }
    }
    private double _elas = 1;
    public double Elas
    {
        get => _elas;
        set
        {
            _elas = value switch
            {
                < 0 => 1,
                _ => value
            };
        }
    }
}
