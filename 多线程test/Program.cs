using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace 多线程test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("start");
            test t = new test();
            t.SubDeTransaction();
            Console.WriteLine("end!");
        }
    }
}