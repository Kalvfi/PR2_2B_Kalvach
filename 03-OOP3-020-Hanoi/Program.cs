namespace _03_OOP3_020_Hanoi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int discCount = 15;
            Puzzle puzzle = new Puzzle(discCount);
            puzzle.Render();
            puzzle.Solve();

        }
    }
}
