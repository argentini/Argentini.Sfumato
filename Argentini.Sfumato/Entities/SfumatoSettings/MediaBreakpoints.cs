namespace Argentini.Sfumato.Entities.SfumatoSettings;

public sealed class MediaBreakpoints
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
