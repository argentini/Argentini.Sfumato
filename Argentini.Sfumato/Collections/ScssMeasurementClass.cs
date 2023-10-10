namespace Argentini.Sfumato.Collections;

public sealed class ScssMeasurementClass : ScssBaseClass
{
    protected override void ProcessOptions()
    {
        AddNumberedRemUnits();
        AddPercentages();
    }
}