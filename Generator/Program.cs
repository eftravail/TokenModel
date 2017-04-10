using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            int count;
            int maxCount = 5;
            var length = 48;

            //count = 0;
            //Console.WriteLine("Random Key:");
            //while (count < maxCount)
            //{
            //    Console.WriteLine(Keys.CreateRandomKey(length).ToString());
            //    count++;
            //}

            count = 0;
            Console.WriteLine("Random Key String:");
            while (count < maxCount)
            {
                Console.WriteLine(Keys.CreateRandomKeyString(length));
                count++;
            }


            Console.ReadKey();
        }
    }
}
