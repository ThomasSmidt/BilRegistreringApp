using VærkstedBilRegisteringApp.Køretøjer;

namespace VærkstedBilRegisteringApp.Codes;
[Serializable]
internal class Køretøj<T> : IKøretøj
{
    public T MotorStørrelse { get; set; }
    public record KundeKontaktInfoRecord(string? kundensFornavn, string? kundensEfternavn, string? kundensTlf);
    public KundeKontaktInfoRecord KundeKontaktInfo { get; set; }
    public Køretøj(
        string? kundensFornavn, string? kundensEfternavn, string? kundensTlf, 
        string? mærke, string? model, T størrelse, string nummerplade,
        string årgang, DateTime førsteRegistrering, DateTime sidsteSynsDato)
    {
        Mærke = mærke;
        Model = model;
        MotorStørrelse = størrelse;
        Nummerplade = nummerplade;
        Årgang = årgang;
        FørsteRegistrering = førsteRegistrering;
        SidsteSynsDato = sidsteSynsDato;
        KundeKontaktInfo = new(kundensFornavn, kundensEfternavn, kundensTlf);
    }
}

