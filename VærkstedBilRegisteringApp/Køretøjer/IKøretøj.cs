using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VærkstedBilRegisteringApp.Køretøjer
{
    internal class IKøretøj
    {
        public string? Nummerplade { get; set; }
        public string? Mærke { get; set; }
        public string? Model { get; set; }
        public string? FabriksFejl { get; set; }
        public string? Årgang { get; set; }
        public DateOnly FørsteRegistrering { get; set; }
        public DateOnly SidsteSynsDato { get; set; }
    }
}
