namespace _03_OOP3_030_Voziky
{
    internal class Program
    {
        static Random random = new Random(123456);
        static void Main(string[] args)
        {
            int pocetVoziku = 50;
            int casStart = 0;
            int casKonec = 12 * 60;
            int maxZakaznikuZaMinutu = 3;
            int minNakup = 5;
            int maxNakup = 45;

            Stack<Vozik> dostupneVoziky = new Stack<Vozik>(Enumerable.Range(0, pocetVoziku).Select(id => new Vozik(id)));
            List<(int casVraceni, Vozik vozik)> pouzivaneVoziky = new List<(int, Vozik)>();
            Queue<int> frontaNaVoziky = new Queue<int>();

            for (int cas = casStart; cas <= casKonec; cas++)
            {
                pouzivaneVoziky.Sort((a,b) => a.casVraceni.CompareTo(b.casVraceni));
                while (pouzivaneVoziky.Count > 0 && pouzivaneVoziky[0].casVraceni == cas)
                {
                    dostupneVoziky.Push(pouzivaneVoziky[0].vozik);
                    pouzivaneVoziky.RemoveAt(0);
                }

                int pocetZakazniku = random.Next(0, maxZakaznikuZaMinutu + 1);
                for (int i = 0; i < pocetZakazniku; i++)
                {
                    int nakup = random.Next(minNakup, maxNakup + 1);
                    if (dostupneVoziky.Count > 0)
                    {
                        Vozik vozik = dostupneVoziky.Pop();
                        vozik.PridejCas(nakup);
                        pouzivaneVoziky.Add((cas + nakup, vozik));
                    }
                    else 
                    {
                        frontaNaVoziky.Enqueue(nakup);
                    }
                }

                while (frontaNaVoziky.Count > 0 && dostupneVoziky.Count > 0)
                {
                    int nakup = frontaNaVoziky.Dequeue();
                    Vozik vozik = dostupneVoziky.Pop();
                    vozik.PridejCas(nakup);
                    pouzivaneVoziky.Add((cas + nakup, vozik));
                }
            }

            var srovnaneVoziky = dostupneVoziky.Concat(pouzivaneVoziky.Select(ac => ac.vozik)).OrderByDescending(cart => cart.CasPouziti);
            Console.WriteLine("Vozík | Doba v provozu (minuty)");
            Console.WriteLine("------------------------------");
            foreach (Vozik vozik in srovnaneVoziky)
            {
                Console.WriteLine($"{vozik.Id,5} | {vozik.CasPouziti,4} min");
            }
        }
    }
}
