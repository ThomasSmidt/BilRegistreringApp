﻿using System.Globalization;
using System.Reflection;
using VærkstedBilRegisteringApp.Køretøjer.TilbageKaldteBiler;

namespace VærkstedBilRegisteringApp
{
    internal class Menu
    {
        static List<object>? bilRegister;

        public static void MenuSetup()
        {
            //Opsætning af startside
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            CommonMethods cm = new();
            GenererBanner();

            //Clear bilRegister for at undgå duplikation af objekter
            bilRegister?.Clear();

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
            //Opsætning af spørgsmål > brugerinput > validering loops
            Validering val = new Validering();
            string fornavn = val.ValiderKunBogstaver("Indtast kundens fornavn: ");
            string efternavn = val.ValiderKunBogstaver("Indtast kundens efternavn: ");
            string telefonnummer = val.ValiderTelefonnummer("Indtast kundens telefonnummer: ");
            string mærke = val.ValiderKunBogstaver("Indtast kundens bils mærke: ");
            string model = val.MenuNullCheck("Indtast bilens model: ");
            string nummerplade = val.MenuNullCheck("Indtast kundens nummerplade: ");
            (double motorStørrelse, bool erBenzin) = val.CheckMotorStørrelse("Indtast bilens motor størrelse: ");
            int årgang = val.ValiderÅrgang("Indtast bilens årgang: ");
            DateTime førsteRegistrering = val.ValiderDato("Indtast bilens første registrerings dato: ");
            

            //Check om bilen skal synes
            DateTime sidsteSynsDato = CheckOmBilenSkalTilSyn(førsteRegistrering);

            //Instantierer et nyt køretøj afhængig af, om det skal være float eller double og tilføjer det til bilRegister
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

        private static DateTime CheckOmBilenSkalTilSyn(DateTime førsteRegistrering)
        {
            const int _førsteGangSyn = 5;
            const int _intervalSyn = 2;
            DateTime currentDate = DateTime.Now;
            DateTime sidsteSynsDato = new();
            Validering val = new();

            //Hvis bilens registreringsdato er ældre end 5 år, gå videre til næste check
            if (førsteRegistrering.Year <= DateTime.Now.Year - _førsteGangSyn)
            {
                sidsteSynsDato = val.ValiderDato("Indtast bilens sidste syns dato: ");

                //Hvis bilens sidst blev synet for over 2 år siden, skal bilen til syn
                if (sidsteSynsDato <= currentDate.AddYears(-_intervalSyn))
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
            return sidsteSynsDato;
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

        private static void CheckEfterFabriksFejl(string mærke, string model, int årgang)
        {
            //Udksriver fabriksfejl hvis mærke, model, og årgang matcher en bil i "TilbageKaldteBiler"
            foreach (TilbageKaldteBilerEnum tilbagekaldtBil in Enum.GetValues(typeof(TilbageKaldteBilerEnum)))
            {
                FieldInfo field = tilbagekaldtBil.GetType().GetField(tilbagekaldtBil.ToString());
                TilbageKaldteBilerAttributer attr = field.GetCustomAttribute<TilbageKaldteBilerAttributer>();
                if (attr.Mærke.ToLower() == mærke.ToLower() && attr.Model.ToLower() == model.ToLower() && årgang <= attr.Årgang)
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
