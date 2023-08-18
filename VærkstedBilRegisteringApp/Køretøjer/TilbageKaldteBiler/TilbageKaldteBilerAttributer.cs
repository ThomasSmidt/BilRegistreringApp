namespace VærkstedBilRegisteringApp.Køretøjer.TilbageKaldteBiler;

internal class TilbageKaldteBilerAttributer : Attribute
{
    public string? Mærke { get; set; }
    public string? Model { get; set; }
    public int Årgang { get; set; }
    public string? Fabriksfejl { get; set; }
}
