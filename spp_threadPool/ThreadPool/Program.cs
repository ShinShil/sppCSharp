using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPool
{
    class Program
    {
        static void Main(string[] args)
        {
            Lab_ThreadPool pool;
            System.Threading.ThreadPool.QueueUserWorkItem(obj => Console.WriteLine("test"));
        }
    }
}
