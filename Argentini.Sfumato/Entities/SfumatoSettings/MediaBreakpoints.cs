namespace Argentini.Sfumato.Entities.SfumatoSettings;

public sealed class MediaBreakpoints
{
    private string _sm = "40em"; // 640px
    public string Sm
    {
        get => _sm;
        set
        {
            _sm = value switch
            {
                "" => "40em",
                _ => value
            };
        }
    }
    private string _md = "48em"; // 768px
    public string Md
    {
        get => _md;
        set
        {
            _md = value switch
            {
                "" => "48em",
                _ => value
            };
        }
    }
    private string _lg = "64em"; // 1024px
    public string Lg
    {
        get => _lg;
        set
        {
            _lg = value switch
            {
                "" => "64em",
                _ => value
            };
        }
    }
    private string _xl = "80em"; // 1280px
    public string Xl
    {
        get => _xl;
        set
        {
            _xl = value switch
            {
                "" => "80em",
                _ => value
            };
        }
    }
    private string _xxl = "96em"; // 1536px
    public string Xxl
    {
        get => _xxl;
        set
        {
            _xxl = value switch
            {
                "" => "96em",
                _ => value
            };
        }
    }
}
