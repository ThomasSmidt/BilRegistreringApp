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
            //Dette program bruger kun "ugyldigt input" men metoden er lavet på denne måde,
            //for at víse hvordan man kan lave en metode som kan udskriver flere fejlbeskeder.
            if (errorType == "UgyldigtInput")
            {
                Console.Write("Ugyldig indtastning. Tryk på en vilkårlig tast for at prøve igen.");
                Console.ReadKey();
                RydLinje(-1);
            }
        }
    }
}
