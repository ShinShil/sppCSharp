using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LrThreadPool
{
    class Program
    {
        static int number = 0;

        static void Main(string[] args)
        {
            for (int i = 0; i < 10; ++i) {
                LrThreadPool.Add(new Thread(new ThreadStart(ThreadProc)));
            }
        }

        static void ThreadProc()
        {
            int inNumber = number++;
            Console.WriteLine("Start thread: " + inNumber);
            Thread.Sleep(2000);
            Console.WriteLine("End thread: " + inNumber);
        }
    }
}
