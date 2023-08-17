using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VærkstedBilRegisteringApp
{
    internal class Validering
    {
        CommonMethods cm;
        public string ValiderKunBogstaver(string prompt)
        {
            string navn;

            do
            {
                Console.Write(prompt);
                navn = Console.ReadLine();

                if (ErBogstavEllerMellemrum(navn) && navn.Length != 0)
                {
                    return navn;
                }
                else
                {
                    cm.VisFejlBesked("UgyldigtInput");
                }
            } while (true);
        }
        static bool ErBogstavEllerMellemrum(string str)
        {
            foreach (char c in str)
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                {
                    return false;
                }
            }
            return true;
        }
        public string ValiderTelefonnummer(string prompt)
        {
            string telefonnummer;

            do
            {
                Console.Write(prompt);
                telefonnummer = Console.ReadLine();

                if (ErGyldigtTelefonnummer(telefonnummer))
                {
                return telefonnummer;

                }
                else
                {
                    cm.VisFejlBesked("UgyldigtInput");
                }
            } while (true);
        }
        public static bool ErGyldigtTelefonnummer(string telefonnummer)
        {
            telefonnummer = string.Join("", telefonnummer.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
            if (telefonnummer.Length == 8 && telefonnummer.All(char.IsDigit))
            {
                return true;
            }

            if (telefonnummer.Length == 11 && telefonnummer.StartsWith("+45") && telefonnummer.Substring(3).All(char.IsDigit))
            {
                return true;
            }

            return false;
        }
        public string MenuNullCheck(string prompt)
        {
            string input;

            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();

                if (input != null && input.Length != 0)
                {
                    return input;
                }
                else
                {
                    cm.VisFejlBesked("UgyldigtInput");

                }
            } while (true);
        }
        public (double, bool) CheckMotorStørrelse(string prompt)
        {
            string motorStørrelse;
            bool erBenzin;
            do
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                //Sætter motorstørrelse til float eller double, afhlængig af om der er brug for en double
                if (float.TryParse(input, out var floatValue))
                {
                    erBenzin = true;
                    return (floatValue, erBenzin);
                }
                else if (double.TryParse(input, out var doubleValue))
                {
                    erBenzin = false;
                    return (doubleValue, erBenzin);
                }
                else
                {
                    cm.VisFejlBesked("UgyldigtInput");
                }
            } while (true);
        }
        public DateOnly ValiderDato(string prompt)
        {
            DateOnly dateTime;

            do
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (DateOnly.TryParse(input, out dateTime))
                {
                    return dateTime;
                }
                else
                {
                    cm.VisFejlBesked("UgyldigtInput");
                }
            } while (true);
        }
        public string ValiderÅrgang(string prompt)
        {
            string? årgang;

            do
            {
                Console.Write(prompt);
                årgang = Console.ReadLine();

                if (årgang != null && årgang.Length == 4)
                {
                    return årgang;
                }
                else
                {
                    cm.VisFejlBesked("UgyldigtInput");
                }
            } while (true);
        }
    }
}
