using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_OOP2_060_IFace_IComparable_Body
{
    internal class Bod : IComparable
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Bod(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public override string ToString()
        {
            return string.Format("Bod [{0};{1}]", this.X, this.Y);
        }

        public double VzdalenostOdStredu()
        {
            return Math.Sqrt(this.X * this.X + this.Y * this.Y);
        }

        public int CompareTo(object? obj)
        {
            Bod druhyBod = obj as Bod;
            if (druhyBod is null)
                throw new ArgumentException();

            return this.VzdalenostOdStredu().CompareTo(druhyBod.VzdalenostOdStredu());
        }
    }
}
