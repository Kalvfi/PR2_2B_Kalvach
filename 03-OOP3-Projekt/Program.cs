using System.Transactions;
using System.IO;
using System.Text;
using System.Globalization;

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

            List<Button> buttons =
            [
                new NavigationButton("Přejít nahoru", () => DisplayExplorer(boss)),
                new NavigationButton("Přejít na seznam", () => DisplayFile())
            ];

            if (FileManager.FileContent != null)
            {
                buttons.Add(FileManager.FileContent.Contains(salesman) ? new FileButton(false, salesman) : new FileButton(true, salesman));
            }
            else 
            {
                buttons.Add(new FileButton(true, salesman));
            }

            if (superior != null)
            {
                buttons.Add(new SalesmanButton(superior.ToString(), () => DisplayExplorer(superior)));
            }

            foreach (Salesman subordinate in salesman.Subordinates)
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
                    if (button is FileButton)
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
                    if (button is SalesmanButton && button.Label != superior?.ToString())
                    {
                        button.Draw();
                        Console.Write("\n           ");
                    }
                }

                HandleInput(buttons);
            }
        }

        static void DisplayFile()
        {
            List<Button> buttons =
            [
                new NavigationButton("Založit", () => FileManager.CreateFile(() => DisplayFile())),
                new NavigationButton("Načíst", () => FileManager.LoadFile(boss, () => DisplayFile())),
                new NavigationButton("Uložit", () => FileManager.SaveFile(() => DisplayFile())),
                new NavigationButton("Přejít na prohlížeč", () => DisplayExplorer(boss)),
            ];

            if (FileManager.FileContent != null && FileManager.FileContent.Count() != 0)
            {
                foreach (Salesman salesman in FileManager.FileContent)
                {
                    if (salesman != null)
                        buttons.Add(new SalesmanButton(salesman.ToString(), () => DisplayExplorer(salesman)));
                }
            }

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
                Console.WriteLine(new String('-', 53));
                Console.WriteLine();
                Console.WriteLine(FileManager.CurrentFileName);
                Console.WriteLine(new String('-', 20));
                foreach (Button button in buttons)
                {
                    if (button is SalesmanButton)
                    {
                        button.Draw();
                        Console.Write("\n");
                    }
                }

                HandleInput(buttons);

            }
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
                    CheckExit();
                    break;
            }
        }

        static void CheckExit() 
        {
            if (!FileManager.IsSaved) 
            {
                List<Button> buttons =
                [
                    new BoolButton(true, "ANO", () => FileManager.SaveFile(() => DisplayFile())),
                    new BoolButton(false, "NE", () => Environment.Exit(0))
                ];
                buttons[0].IsSelected = true;

                while (true) 
                {
                    Console.Clear();
                    Console.WriteLine("Máte neuložené změny. Chcete je uložit?\n");
                    foreach (Button button in buttons) 
                    {
                        button.Draw();
                        Console.Write("\t");
                    }

                    HandleInput(buttons);
                }
            }

            Environment.Exit(0);
        }
    }
}
