using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace _03_OOP3_Projekt
{
    abstract class Button
    {
        public string Label { get; protected set; }
        public bool IsSelected { get; set; }
        public Action Action { get; protected set; }

        public Button(string label, Action action)
        {
            Label = label;
            IsSelected = false;
            Action = action;
        }

        public virtual void ExecuteAction() 
        {
            Action.Invoke();
        }

        public virtual void Draw()
        {
            if (IsSelected)
                Console.BackgroundColor = ConsoleColor.Yellow;

            Console.Write(Label);
            Console.ResetColor();
        }
    }

    class SalesmanButton : Button
    {
        public SalesmanButton(string label, Action action) : base(label, action)
        {
        }

        public override void Draw()
        {
            if (IsSelected)
                Console.ForegroundColor = ConsoleColor.Black;
            base.Draw();
        }
    }

    class FileButton : Button
    {
        public bool Type { get; private set; }
        public Salesman Salesman { get; private set; }

        public FileButton(bool type, Salesman salesman) : base(null, null)
        {
            Type = type;
            Salesman = salesman;
            Action = Type ? () => FileManager.AddToFile(Salesman) : () => FileManager.RemoveFromFile(Salesman);
            Label = (Type) ? "Přidat" : "Odebrat";
        }

        public override void ExecuteAction()
        {
            base.ExecuteAction();
            if (FileManager.FileContent != null)
            {
                Type = FileManager.FileContent.Contains(Salesman) ? false : true;
            }
            else 
            {
                Type = true;
            }
            Action = Type ? () => FileManager.AddToFile(Salesman) : () => FileManager.RemoveFromFile(Salesman);
            Label = Type ? "Přidat" : "Odebrat";
        }

        public override void Draw()
        {
            Console.ForegroundColor = (Type) ? ConsoleColor.Green : ConsoleColor.Red;
            base.Draw();
        }
    }

    class NavigationButton : Button
    {
        public NavigationButton(string label, Action action) : base(label, action)
        {
        }

        public override void Draw()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            base.Draw();
        }
    }

    class BoolButton : Button
    {
        public bool Type { get; private set; }

        public BoolButton(bool type, string label, Action action) : base(label, action)
        {
            Type = type;
        }

        public override void Draw()
        {
            Console.ForegroundColor = (Type) ? ConsoleColor.Green : ConsoleColor.Red;
            base.Draw();
        }
    }
}
