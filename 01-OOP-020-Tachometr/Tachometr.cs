using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_OOP_020_Tachometr
{
    internal class Tachometr
    {
		private int stav = 0;

		public int Stav
		{
			get { return stav; }
			set
			{
				if (value < 0)
					throw new ArgumentOutOfRangeException();

				stav = value;
			}
		}
		
		/// <summary>
		/// Zvýší stav tachometru
		/// </summary>
		/// <param name="kilometry">O kolik se má zvýšit</param>
		/// <exception cref="ArgumentOutOfRangeException">Eror</exception>
		public void Ujed(int kilometry) 
		{
            if (kilometry < 0)
                throw new ArgumentOutOfRangeException();

			stav += kilometry;
        }
	}
}
