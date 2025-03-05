using System.Transactions;
using System.IO;
using System.Text;

namespace _03_OOP3_Projekt
{
    internal class Program
    {
        private static Salesman boss;

        static void Main(string[] args)
        {
            string filename = "largetree.json";
            boss = Salesman.DeserializeTree(File.ReadAllText(filename));

            //DisplaySalesmenTree(boss);

            //------------------------| Můj Kód |------------------------

            DisplayExplorer(boss);
        }

        static void DisplaySalesmenTree(Salesman node, string indent = "")
        {
            Console.WriteLine($"{indent}{node.Name} {node.Surname} - Sales: {node.Sales}");

            foreach (var subordinate in node.Subordinates)
            {
                DisplaySalesmenTree(subordinate, indent + "    ");
            }
        }

        //------------------------| Můj Kód |------------------------

        static void DisplayExplorer(Salesman salesman)
        {
            
            Salesman superior = Salesman.FindSuperior(boss, salesman);
            int branchSales = salesman.BranchSales();

            List<Button> buttons = new List<Button>();

            buttons.Add(new NavigationButton("Přejít nahoru", () => DisplayExplorer(boss)));
            buttons.Add(new NavigationButton("Přejít na seznam", () => DisplayFile()));
            buttons.Add(new FileButton(() => AddToFile(salesman), true));

            if (superior != null)
            {
                buttons.Add(new SalesmanButton(superior.ToString(), () => DisplayExplorer(superior)));
            }
            foreach (var subordinate in salesman.Subordinates)
            {
                buttons.Add(new SalesmanButton(subordinate.ToString(), () => DisplayExplorer(subordinate)));
            }

            buttons[0].IsSelected = true;

            while (true)
            {
                Console.Clear();
                foreach (Button button in buttons)
                {
                    if (button.Label == "Přejít nahoru")
                        button.Draw();
                }
                Console.Write("  |  ");
                foreach (Button button in buttons)
                {
                    if (button.Label == "Přejít na seznam")
                        button.Draw();
                }
                Console.WriteLine();
                Console.WriteLine(new String('-', 34));
                Console.WriteLine();
                Console.Write("Obchodník: ");
                Console.Write(salesman);
                Console.Write("\t");
                foreach (Button button in buttons)
                {
                    if (button.Label == "Přidat" || button.Label == "Odebrat")
                        button.Draw();
                }
                Console.WriteLine();
                Console.WriteLine(new String('-', 23));
                Console.WriteLine($"Přímé prodeje: ${salesman.Sales}");
                Console.WriteLine($"Celkové prodeje sítě: ${salesman.BranchSales()}");
                Console.WriteLine();
                Console.Write("Nadřízený: ");
                if (superior != null)
                {
                    foreach (Button button in buttons)
                    {
                        if (button.Label == superior.ToString())
                            button.Draw();
                    }
                }
                Console.WriteLine("\n");
                Console.Write("Podřízení: ");
                foreach (Button button in buttons)
                {
                    foreach (Salesman subordinate in salesman.Subordinates)
                    {
                        if (button.Label == subordinate.ToString())
                        {
                            button.Draw();
                            Console.Write("\n           ");
                        }
                    }
                }

                HandleInput(buttons);
            }
        }

        static void DisplayFile()
        {
            StreamReader reader;
            StreamWriter writer;
            string filename = null;

            List<Button> buttons = new List<Button>();

            buttons.Add(new NavigationButton("Založit", () => CreateFile()));
            buttons.Add(new NavigationButton("Načíst", () => LoadFile()));
            buttons.Add(new NavigationButton("Uložit", () => SaveFile()));
            buttons.Add(new NavigationButton("Přejít na prohlížeč", () => DisplayExplorer(boss)));

            buttons[0].IsSelected = true;

            while (true)
            {
                Console.Clear();
                foreach (Button button in buttons)
                {
                    if (button.Label == "Založit")
                        button.Draw();
                }
                Console.Write("  |  ");
                foreach (Button button in buttons)
                {
                    if (button.Label == "Načíst")
                        button.Draw();
                }
                Console.Write("  |  ");
                foreach (Button button in buttons)
                {
                    if (button.Label == "Uložit")
                        button.Draw();
                }
                Console.Write("  |  ");
                foreach (Button button in buttons)
                {
                    if (button.Label == "Přejít na prohlížeč")
                        button.Draw();
                }
                Console.WriteLine();
                Console.WriteLine(new String('-', 34));
                Console.WriteLine();
                if(filename != null) 
                {
                    Console.Write(filename);
                    Console.WriteLine(new String('-', 20));
                    Console.WriteLine("\n");
                    DrawFile();
                }

                HandleInput(buttons);

            }
        }

        private static void CreateFile()
                {
                    throw new NotImplementedException();
                }

        private static void LoadFile()
                {
                    throw new NotImplementedException();
                }

        private static void SaveFile()
        {
            throw new NotImplementedException();
        }

        private static void DrawFile()
        {
            throw new NotImplementedException();
        }

        static void AddToFile(Salesman salesman) 
        {
            throw new NotImplementedException();
        }

        static void HandleInput(List<Button> buttons) 
        {
            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.RightArrow:
                    for (int i = 0; i < buttons.Count; i++)
                    {
                        if (buttons[i].IsSelected)
                        {
                            buttons[i].IsSelected = false;
                            if (i + 1 >= buttons.Count)
                                buttons[0].IsSelected = true;
                            else
                                buttons[i + 1].IsSelected = true;
                            break;
                        }
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    for (int i = 0; i < buttons.Count; i++)
                    {
                        if (buttons[i].IsSelected)
                        {
                            buttons[i].IsSelected = false;
                            if (i - 1 < 0)
                                buttons[buttons.Count - 1].IsSelected = true;
                            else
                                buttons[i - 1].IsSelected = true;
                            break;
                        }
                    }
                    break;
                case ConsoleKey.Spacebar:
                    foreach (Button button in buttons)
                    {
                        if (button.IsSelected)
                        {
                            button.ExecuteAction();
                            break;
                        }
                    }
                    break;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
