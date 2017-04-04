using System;
using AtomGraph;

namespace ConsoleApp1
{
/*
OUTPUT:
-------------------------------
-1
3
77
-------------------------------
2
3
*/
    class Program
    {
        static void Main(string[] args)
        {
            AtomicList<Atomic<int>> list = new AtomicList<Atomic<int>> { 2, 3 };

            using (var scope = new AmbientAtomicScope())
            {
                list[0].Value = -1;
                list.Add(77);
                Print(list);
            }
            Print(list);
        }

        private static void Print(AtomicList<Atomic<int>> list)
        {
            Console.WriteLine("---");
            foreach (var value in list)
            {
                Console.WriteLine(value);
            }
        }
    }
}