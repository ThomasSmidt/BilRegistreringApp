namespace VærkstedBilRegisteringApp
{
    internal class CommonMethods
    {
        public static void RydLinje(int linje)
        {
            int nuværendeLinje = Console.CursorTop;

            Console.SetCursorPosition(0, nuværendeLinje);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, nuværendeLinje);

            //Sletter flere linjer hvis "linje" < 0
            if (linje < 0)
            {
                Console.SetCursorPosition(0, nuværendeLinje + linje);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, nuværendeLinje - 1);
            }
        }
        public void VisFejlBesked(string errorType)
        {
            if (errorType == "UgyldigtInput")
            {
                Console.Write("Ugyldig indtastning. Tryk på en vilkårlig tast for at prøve igen.");
                Console.ReadKey();
                RydLinje(-1);
            }
        }
        public string ValiderKunBogstaver(string prompt)
        {
            string navn;

            do
            {
                Console.Write(prompt);
                navn = Console.ReadLine();

                if (ErBogstavEllerMellemrum(navn) && navn.Length != 0)
                {
                    break;
                }
                else
                {
                    VisFejlBesked("UgyldigtInput");
                }
            } while (true);

            return navn;
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
                    break;
                }
                else
                {
                    VisFejlBesked("UgyldigtInput");
                }
            } while (true);

            return telefonnummer;
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
                    break;
                }
                else
                {
                    VisFejlBesked("UgyldigtInput");

                }
            } while (true);

            return input;
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
                    VisFejlBesked("UgyldigtInput");
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
                    break;
                }
                else
                {
                    VisFejlBesked("UgyldigtInput");
                }
            } while (true);

            return dateTime;
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
                    break;
                }
                else
                {
                    VisFejlBesked("UgyldigtInput");
                }
            } while (true);

            return årgang;
        }
    }
}
