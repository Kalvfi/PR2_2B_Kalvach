namespace _00_Rev_01_pod_limitem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double[] cisla = { 1.3, 2.2, -1.5, 1.4, -2.7 };
            Console.WriteLine(PrumerPodLimitem(cisla, 1.1));
            Console.WriteLine(PrumerPodLimitem(cisla, -2));
        }

        public static double PrumerPodLimitem(double[] cisla, double limit)
        {
            double soucet = 0;
            double x = 0;

            for (int i = 0; i < cisla.Length; i++)
            {
                if (cisla[i] < limit)
                {
                    soucet += cisla[i];
                    x++;
                }
            }

            return soucet / x;
        }
        
        //private static double PrumerPodLimitem(double[] cisla, double limit) => cisla.Where(x => x < limit).Average();
    }
}
