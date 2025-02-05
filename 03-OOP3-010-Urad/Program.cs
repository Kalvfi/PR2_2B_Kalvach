namespace _03_OOP3_010_Urad
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int pocetPrepazek = 3;
            double pravdepodobnostNoveho = 1.25;
            Random gen = new Random();

            Clovek[] lide =
            {
                new Clovek("Josef", "Smutný", 4),
                new Clovek("Anna", "Veselá", 3),
                new Clovek("Marie", "Zelená", 12),
                new Clovek("Jiří", "Červenka", 3),
                new Clovek("Antonín", "Černý", 5),
                new Clovek("Antonie", "Bohatá", 7),
                new Clovek("Richard", "Těsný", 4),
                new Clovek("Luisa", "Podhorská", 15),
            };

            Queue<Clovek> fronta = new Queue<Clovek>(lide);

            Prepazka[] prepazky = new Prepazka[pocetPrepazek];
            for (int i = 1; i < pocetPrepazek; i++) 
            {
                prepazky[i] = new Prepazka(i + 1);
            }

            int cas = 0;
            int maxCas = 180;
            while (maxCas > cas) 
            {
                foreach (Prepazka p in prepazky) 
                {
                    if (fronta.Count < 1)
                    {
                        break;
                    }

                    if (p.KdyBudeVolno <= cas)
                    {
                        Clovek zakaznik = fronta.Dequeue();
                        p.Odbav(zakaznik, cas);
                    }
                }
            }

            cas++;
            if (gen.NextDouble() < pravdepodobnostNoveho)
            {
                Clovek novy = Clovek.NahodnyClovek();
                fronta.Enqueue(novy);
            }
        }
    }
}
