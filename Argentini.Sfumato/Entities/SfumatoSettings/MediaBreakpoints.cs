namespace Argentini.Sfumato.Entities.SfumatoSettings;

public sealed class MediaBreakpoints
{
    private string _zero = "0em";
    public string Zero
    {
        get => _zero;
        set
        {
            _zero = value switch
            {
                "" => "0em",
                _ => value
            };
        }
    }
    private string _phab = "25em"; // 400px
    public string Phab
    {
        get => _phab;
        set
        {
            _phab = value switch
            {
                "" => "25em",
                _ => value
            };
        }
    }
    private string _tabp = "33.75em"; // 540px
    public string Tabp
    {
        get => _tabp;
        set
        {
            _tabp = value switch
            {
                "" => "33.75em",
                _ => value
            };
        }
    }
    private string _tabl = "50em"; // 800px
    public string Tabl
    {
        get => _tabl;
        set
        {
            _tabl = value switch
            {
                "" => "50em",
                _ => value
            };
        }
    }
    private string _note = "80em"; // 1280px
    public string Note
    {
        get => _note;
        set
        {
            _note = value switch
            {
                "" => "80em",
                _ => value
            };
        }
    }
    private string _desk = "90em"; // 1440px
    public string Desk
    {
        get => _desk;
        set
        {
            _desk = value switch
            {
                "" => "90em",
                _ => value
            };
        }
    }
    private string _elas = "100em"; // 1600px
    public string Elas
    {
        get => _elas;
        set
        {
            _elas = value switch
            {
                "" => "100em",
                _ => value
            };
        }
    }
}
