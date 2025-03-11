using System;
using System.Collections.Generic;
using System.IO;

namespace _03_OOP3_Projekt
{
    internal static class FileManager
    {
        public static string CurrentFileName { get; private set; }
        public static List<Salesman> FileContent { get; private set; }
        public static bool IsSaved { get; private set; }

        public static void CreateFile(Action refresher)
        {
            Console.Clear();
            Console.Write("Zadej název nového souboru: ");
            CurrentFileName = Console.ReadLine() + ".txt";
            File.WriteAllText(CurrentFileName, "");
            FileContent = new List<Salesman>();
            IsSaved = true;
            refresher.Invoke();
        }

        public static void LoadFile(Salesman root, Action refresher)
        {
            Console.Clear();
            Console.Write("Zadej název souboru, který chceš načíst: ");
            CurrentFileName = Console.ReadLine() + ".txt";
            if (File.Exists(CurrentFileName))
            {
                var content = File.ReadAllText(CurrentFileName);
                FileContent = DeserializeSalesmen(root, content);
                IsSaved = true;
                refresher.Invoke();
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Soubor '{CurrentFileName}' neexistuje.");
                CurrentFileName = null;
                FileContent = null;
                Console.ReadKey();
                refresher.Invoke();
            }
        }

        public static void SaveFile(Action refresher)
        {
            if (CurrentFileName == null)
            {
                Console.Clear();
                Console.WriteLine("Není načten žádný soubor.");
                Console.ReadKey();
            }
            else 
            {
                var content = SerializeSalesmen(FileContent);
                File.WriteAllText(CurrentFileName, content);
                Console.Clear();
                Console.WriteLine($"Soubor '{CurrentFileName}' byl uložen.");
                Console.ReadKey();
                IsSaved = true;
                refresher.Invoke();
            }  
        }

        public static void AddToFile(Salesman salesman)
        {
            if (FileContent == null)
            {
                Console.Clear();
                Console.WriteLine("Není načten žádný soubor.");
                Console.ReadKey();
            }
            else
            {
                FileContent.Add(salesman);
                IsSaved = false;
            }
        }

        public static void RemoveFromFile(Salesman salesman)
        {
            if (FileContent != null)
            {
                FileContent.Remove(salesman);
                IsSaved = false;
            }
        }

        private static List<Salesman> DeserializeSalesmen(Salesman root, string content)
        {
            int[] salesmenIDs;

            try
            {
                salesmenIDs = content.Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            }
            catch 
            {
                Console.Clear();
                Console.Write("Načtený soubor neobsahuje kompatibilní data.");
                Console.ReadKey();
                CurrentFileName = null;
                return null;
            }
            
            List<Salesman> salesmen = new List<Salesman>();

            foreach (int ID in salesmenIDs)
            {
                salesmen.Add(Salesman.FindSalesman(root, ID));
            }

            return salesmen;
        }

        private static string SerializeSalesmen(List<Salesman> salesmen)
        {
            string content = "";

            foreach (Salesman salesman in salesmen) 
            {
                if(salesman != null)
                    content += salesman.ID + "\n";
            }

            return content;
        }
    }
}
