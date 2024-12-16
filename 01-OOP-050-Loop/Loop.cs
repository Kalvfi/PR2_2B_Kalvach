using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_OOP_050_Loop
{
	internal class Loop<T>
	{
		private T[] data { get; set; }
		private int current { get; set; }

		public T Current()
		{
			return data[current];
		}

		public Loop(T[] Data)
		{
			data = Data;
			current = 0;
		}

		public void Right() 
		{
			if (current == data.Length-1)
			{
				current = 0;
			}
			else 
			{
				current++;
			}
		}

        public void Left()
        {
            if (current == 0)
            {
                current = data.Length-1;
            }
            else
            {
                current--;
            }
        }

    }
}
