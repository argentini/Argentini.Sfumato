namespace Argentini.Sfumato.Entities.SfumatoSettings;

public sealed class MediaBreakpoints
{
    private string _sm = "640px"; // 640px
    public string Sm
    {
        get => _sm;
        set
        {
            _sm = value switch
            {
                "" => "640px",
                _ => value
            };
        }
    }
    private string _md = "768px"; // 768px
    public string Md
    {
        get => _md;
        set
        {
            _md = value switch
            {
                "" => "768px",
                _ => value
            };
        }
    }
    private string _lg = "1024px"; // 1024px
    public string Lg
    {
        get => _lg;
        set
        {
            _lg = value switch
            {
                "" => "1024px",
                _ => value
            };
        }
    }
    private string _xl = "1280px"; // 1280px
    public string Xl
    {
        get => _xl;
        set
        {
            _xl = value switch
            {
                "" => "1280px",
                _ => value
            };
        }
    }
    private string _xxl = "1536px"; // 1536px
    public string Xxl
    {
        get => _xxl;
        set
        {
            _xxl = value switch
            {
                "" => "1536px",
                _ => value
            };
        }
    }
}
