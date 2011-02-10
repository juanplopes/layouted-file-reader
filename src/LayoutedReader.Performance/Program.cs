using System;
using System.Linq;
using LayoutedReader.Layouts;
using System.Threading;
using System.IO;
using System.Collections.Generic;

namespace LayoutedReader.Performance
{
    class Program
    {

        static void Main(string[] args)
        {
            var loader = new FileLoader("Layouts/index.xml");

            DeployContext.PrintHeader();

            Test(loader.Read("Files/test.txt"));
            //Test(loader.Read("Files/C21C.txt"));
            Console.ReadLine();
        }


        public static void Test(IEnumerable<DeployContext> items)
        {
            foreach (var item in items)
                item.PrintInfo();

            Console.WriteLine();

        }


    }

}
