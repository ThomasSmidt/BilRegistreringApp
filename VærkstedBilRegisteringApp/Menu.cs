using System.Reflection;
using VærkstedBilRegisteringApp.Køretøjer.TilbageKaldteBiler;

namespace VærkstedBilRegisteringApp
{
    internal class Menu
    {
        static List<object> bilRegister;
        public static void MenuSetup()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            CommonMethods cm = new();
            GenererBanner();

            //Henter bilregisteret fra en JSON fil og indsætter det i en liste
            bilRegister = JSON.ReadFromJsonFile();

            //Viser registrerede biler
            Console.WriteLine("Registrerede biler:");
            if (bilRegister.Any())
            {
                UdskrivKøretøjer(bilRegister);
            }
            else
            {
                Console.WriteLine("Der er ingen registrerede biler.");
            }

            //UI
            Console.WriteLine("\nValgmuligheder:");
            Console.WriteLine("    [1] Registrer Ny Kunde   [2] Vis Kunde Kontaktinformation    [3] Afslut");
            Console.WriteLine();
            ConsoleKeyInfo brugerInput = Console.ReadKey();

            switch (brugerInput.KeyChar)
            {
                case '1':
                    CommonMethods.RydLinje(0);
                    RegistrerKunde();
                    break;
                case '2':
                    CommonMethods.RydLinje(0);
                    VisKontaktInformation(bilRegister);
                    break;
                case '3':
                    Environment.Exit(0);
                    break;
                default:
                    CommonMethods.RydLinje(0);
                    cm.VisFejlBesked("UgyldigtInput");
                    MenuSetup();
                    break;
            }
        }

        private static void UdskrivKøretøjer(List<object> alleKøretøjer)
        {
            //Check om Køretøj er af type double eller string, da vi har brugt en generic type
            foreach (object item in alleKøretøjer)
            {
                if (item.GetType() == typeof(Køretøj<double>))
                {
                    Console.WriteLine($"  {((Køretøj<double>)item).Mærke} " +
                        $"{((Køretøj<double>)item).Model} " +
                        $"{((Køretøj<double>)item).Nummerplade}");
                }
                else if (item.GetType() == typeof(Køretøj<string>))
                {
                    Console.WriteLine($"  {((Køretøj<string>)item).Mærke} " +
                        $"{((Køretøj<string>)item).Model} " +
                        $"{((Køretøj<string>)item).Nummerplade}");
                }
            }
        }

        private static void RegistrerKunde()
        {
            //Opsætning af spørgsmål > brugerinput
            CommonMethods cm = new CommonMethods();
            string fornavn = cm.ValiderKunBogstaver("Indtast kundens fornavn: ");
            string efternavn = cm.ValiderKunBogstaver("Indtast kundens efternavn: ");
            string telefonnummer = cm.ValiderTelefonnummer("Indtast kundens telefonnummer: ");
            string mærke = cm.ValiderKunBogstaver("Indtast kundens bils mærke: ");
            string model = cm.MenuNullCheck("Indtast bilens model: ");
            string nummerplade = cm.MenuNullCheck("Indtast kundens nummerplade: ");
            (double motorStørrelse, bool erBenzin) = cm.CheckMotorStørrelse("Indtast bilens motor størrelse: ");
            string årgang = cm.ValiderÅrgang("Indtast bilens årgang: ");
            DateOnly førsteRegistrering = cm.ValiderDato("Indtast bilens første registrerings dato: ");
            DateOnly sidsteSynsDato = new();


            //Check om bilen skal synes
            CheckOmBilenSkalTilSyn(førsteRegistrering, sidsteSynsDato);

            //Instantierer et nyt køretøj afhængig af, om det skal være float eller double
            if (erBenzin)
            {
                Køretøj<float> benzinBil = new Køretøj<float>(fornavn, efternavn, telefonnummer, mærke, model,
                    (float)motorStørrelse, nummerplade, årgang, førsteRegistrering, sidsteSynsDato);
                bilRegister.Add(benzinBil);
                JSON.WriteToJsonFile(bilRegister);
            }
            else
            {
                Køretøj<double> elBil = new Køretøj<double>(fornavn, efternavn, telefonnummer, mærke, model,
                    (float)motorStørrelse, nummerplade, årgang, førsteRegistrering, sidsteSynsDato);
                bilRegister.Add(elBil);
                JSON.WriteToJsonFile(bilRegister);
            }

            CheckEfterFabriksFejl(mærke, model, årgang);

            //Reset
            Console.Write("\nTryk på en tast for at gå tilbage til startmenuen.");
            Console.ReadKey();
            MenuSetup();
        }

        private static void CheckOmBilenSkalTilSyn(DateOnly førsteRegistrering, DateOnly sidsteSynsDato)
        {
            const int _førsteGangSyn = 5;
            const int _intervalSyn = 2;
            DateTime currentDate = DateTime.Now.AddYears(_intervalSyn);
            DateOnly currentDateOnly = new(currentDate.Year, currentDate.Month, currentDate.Day);
            CommonMethods cm = new();

            if (førsteRegistrering.Year < DateTime.Now.Year - _førsteGangSyn)
            {
                sidsteSynsDato = cm.ValiderDato("Indtast bilens sidste syns dato: ");
                if (sidsteSynsDato <= currentDateOnly.AddYears(-_intervalSyn))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nKundens bil skal til syn.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nKundens bil skal ikke til syn.");
                    Console.ForegroundColor = ConsoleColor.White;
                };
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nKundens bil skal ikke til syn.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private static void VisKontaktInformation(List<object> alleKøretøjer)
        {
            Console.Write("Skriv kundens nummerplade: ");
            var input = Console.ReadLine();

            //Checker om Køretøj er af type double eller string, da vi har brugt en generic type
            foreach (object item in alleKøretøjer)
            {
                if (item.GetType() == typeof(Køretøj<double>))
                {
                    if (((Køretøj<double>)item).Nummerplade == input)
                    {
                        CommonMethods.RydLinje(0);
                        Console.WriteLine(
                            $"{((Køretøj<double>)item).KundeKontaktInfo.kundensFornavn} " +
                            $"{((Køretøj<double>)item).KundeKontaktInfo.kundensEfternavn} " +
                            $"{((Køretøj<double>)item).KundeKontaktInfo.kundensTlf}");
                    }
                }
                else if (item.GetType() == typeof(Køretøj<string>))
                {
                    if (((Køretøj<string>)item).Nummerplade == input)
                    {
                        CommonMethods.RydLinje(0);
                        Console.WriteLine(
                            $"{((Køretøj<string>)item).KundeKontaktInfo.kundensFornavn} " +
                            $"{((Køretøj<string>)item).KundeKontaktInfo.kundensEfternavn} " +
                            $"{((Køretøj<string>)item).KundeKontaktInfo.kundensTlf}");
                    }
                }
            }
            Console.Write("\nTryk på en tast for at gå tilbage til startmenuen.");
            Console.ReadKey();
            MenuSetup();
        }

        private static void CheckEfterFabriksFejl(string mærke, string model, string årgang)
        {
            //Udksriver fabriksfejl hvis mærke, model, og årgang matcher
            foreach (TilbageKaldteBilerEnum tilbagekaldtBil in Enum.GetValues(typeof(TilbageKaldteBilerEnum)))
            {
                FieldInfo field = tilbagekaldtBil.GetType().GetField(tilbagekaldtBil.ToString());
                TilbageKaldteBilerAttributer attr = field.GetCustomAttribute<TilbageKaldteBilerAttributer>();
                if (attr.Mærke == mærke && attr.Model == model && årgang == attr.Årgang)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Bilen har følgende fabriksfejl: {attr.Fabriksfejl}");
                    Console.ForegroundColor = ConsoleColor.White;
                    return;
                }
            }
        }

        private static void GenererBanner()
        {
            Console.WriteLine("\t    ____                                 ____  _ __         " +
                "\r\n\t   / __ )___  ____  ____  __  _______   / __ )(_) /__  _____" +
                "\r\n\t  / __  / _ \\/ __ \\/ __ \\/ / / / ___/  / __  / / / _ \\/ ___/" +
                "\r\n\t / /_/ /  __/ / / / / / / /_/ (__  )  / /_/ / / /  __/ /    " +
                "\r\n\t/_____/\\___/_/ /_/_/ /_/\\__, /____/  /_____/_/_/\\___/_/     " +
                "\r\n\t                       /____/                               \n");
        }

    }
}
