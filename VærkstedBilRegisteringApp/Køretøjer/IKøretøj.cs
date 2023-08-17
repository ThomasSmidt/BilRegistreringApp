namespace VærkstedBilRegisteringApp.Køretøjer
{
    internal class IKøretøj
    {
        public string? Nummerplade { get; set; }
        public string? Mærke { get; set; }
        public string? Model { get; set; }
        public string? FabriksFejl { get; set; }
        public string? Årgang { get; set; }
        public DateTime FørsteRegistrering { get; set; }
        public DateTime SidsteSynsDato { get; set; }
    }
}
