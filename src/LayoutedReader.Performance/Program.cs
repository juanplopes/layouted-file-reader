using System;
using System.Linq;
using LayoutedReader.Layouts;
using System.Threading;
using System.IO;

namespace LayoutedReader.Performance
{
    class Program
    {

        static void Main(string[] args)
        {
            var loader = new FileLoader("Layouts/index.xml");

            Console.Write("{0,-20} ", "filename");
            DeployContext.PrintHeader();

            Test(loader.Read("Files/C21.txt"));
            //Test(loader.Read("Files/C21C.txt"));
            Console.ReadLine();
        }


        public static void Test(RecordEnumeration items)
        {
            Console.Write("{0,-20} ", items.Filename);

            foreach (var item in items)
                item.PrintInfo();

            Console.WriteLine();

        }


    }

}
