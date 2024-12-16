namespace _01_OOP_020_Tachometr
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //vytvořte nový tachometr
            Tachometr tachometr = new Tachometr();

            //zkuste jeho stav několikrát zvýšit a pak vypsat
            tachometr.Ujed(15);
            tachometr.Ujed(20);
            tachometr.Ujed(76);

            Console.WriteLine(tachometr.Stav);

            //zkuste také zvýšit o zápornou hodnotu
            tachometr.Ujed(-10);
        }
    }
}
