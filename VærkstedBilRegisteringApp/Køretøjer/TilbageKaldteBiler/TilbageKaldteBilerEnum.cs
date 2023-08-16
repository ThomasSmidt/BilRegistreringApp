namespace VærkstedBilRegisteringApp.Køretøjer.TilbageKaldteBiler;

internal enum TilbageKaldteBilerEnum
{
    [TilbageKaldteBilerAttributer(Mærke = "Fiat", Model = "Punto", Årgang = "2010", Fabriksfejl = "Udstødning")]
    FiatPunto,
    [TilbageKaldteBilerAttributer(Mærke = "Alfa", Model = "Romeo", Årgang = "2019", Fabriksfejl = "Styrtøjet")]
    AlfaRomeo
}
