using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_OOP_040_Paginator
{
    class Paginator
    {
        private string[] Data { get; set; }
        private int Limit { get; set; }
        public int ItemCount { get; set; }
        public int PageCount { get; set; }

        public Paginator(string[] data, int limit)
        {
            Data = data;
            Limit = limit;
            ItemCount = data.Length;
            PageCount = (int)Math.Ceiling((double)ItemCount / limit);
        }

        public int GetPageItemCount(int n)
        {
            if (n < 1 || n > PageCount-1)
            {
                return 0;
            }
            else if (n == PageCount-1 && ItemCount % Limit != 0)
            {
                return ItemCount % Limit;
            }
            else 
            {
                return Limit;
            }
        }

        public string[] GetPage(int n)
        {
            if (n < 1 || n > PageCount-1)
            {
                return [];
            }

            int startIndex = n * Limit;
            string[] Page = new string[GetPageItemCount(n)];

            for (int i = 0; i < Page.Length; i++, startIndex++)
            {
                Page[i] = Data[startIndex];
            }

            return Page;
        }

        public int FindPage(string s)
        {
            int index = -1;

            for (int i = 0; i < Data.Length; i++)
            {
                if (Data[i] == s) 
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
            {
                return -1;
            }
            else 
            {
                return index / Limit;
            }
        }
    }
}
