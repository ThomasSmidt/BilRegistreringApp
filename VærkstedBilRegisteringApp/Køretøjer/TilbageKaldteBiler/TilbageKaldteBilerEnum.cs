namespace VærkstedBilRegisteringApp.Køretøjer.TilbageKaldteBiler;

internal enum TilbageKaldteBilerEnum
{
    [TilbageKaldteBilerAttributer(Mærke = "Fiat", Model = "Punto", Årgang = 2010, Fabriksfejl = "Udstødning")]
    FiatPunto,
    [TilbageKaldteBilerAttributer(Mærke = "Alfa Romeo", Model = "Giulia", Årgang = 2019, Fabriksfejl = "Styrtøjet")]
    AlfaRomeo
}
