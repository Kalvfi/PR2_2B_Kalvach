namespace _00_Rev_01_pred_maximem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Dobrý den, zadávejte čísla. Pokud chcete přestat zadejte solvo dost");

            int max = -9999999;
            int predmax = 0;
            int x = 0;
            int i = 0;

            while (true) 
            {
                string vstup = Console.ReadLine();
                if (vstup == "dost")
                {
                    break;
                }
                else if (!int.TryParse(vstup, out int a))
                {

                }
                else 
                {
                    int cislo = int.Parse(vstup);
                    if (i == 1) 
                    {
                        predmax = cislo;
                    }

                    if (cislo > max)
                    {
                        max = cislo;
                        predmax = x;
                        x = cislo;
                        i++;
                    }
                    else 
                    {
                        x = cislo;
                    }
                    
                }
            }

            if (max == -9999999)
            {
                Console.WriteLine("Nebylo vloženo žádné číslo.");
            }
            else 
            {
                Console.WriteLine(predmax);
            }
        }
    }
}
