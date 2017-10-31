using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace spp_lr3
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcurrentAccumulator accumulator = new ConcurrentAccumulator(10, 4000, (List<object> list) => {
                list.ForEach(num => Console.WriteLine("Main thread: {0}, {1}", Thread.CurrentThread.ManagedThreadId, num));
            });
            accumulator.Add(1);
            Thread.Sleep(1000);
            accumulator.Add(2);
            accumulator.Add(3);
        }
    }
}
