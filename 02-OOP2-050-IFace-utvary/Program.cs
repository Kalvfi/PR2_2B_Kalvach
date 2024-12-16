namespace _02_OOP2_050_IFace_utvary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Ctverec ctverec = new Ctverec(3);
            Trojuhelnik trojuhelnik = new Trojuhelnik(2, 3, 4);
            Kruh kruh = new Kruh(10);
            Ctverec ctverec1 = new Ctverec(1);

            IUtvar[] utvary = [ctverec, trojuhelnik, kruh, ctverec1];
            foreach (IUtvar utvar in utvary)
            {
                Console.WriteLine(utvar.Nazev);
                Console.WriteLine(utvar);
                Console.WriteLine();
            }

            PlechovkaBarvy plechovka = new PlechovkaBarvy(80, 2);
            foreach (IUtvar utvar in utvary)
            { 
                Console.WriteLine(utvar);
                Console.WriteLine(utvar.GetObsah());
                if (plechovka.Obarvi(utvar))
                    Console.WriteLine("Barvím");
                else
                    Console.WriteLine("Není dost barvy");
                Console.WriteLine(plechovka);
                Console.WriteLine();
            }
        }
    }
}
