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
            ConcurrentAccumulator accumulator = new ConcurrentAccumulator(2, 1000, (List<object> list) =>
            {
                if (list.Count == 0)
                {
                    Console.WriteLine("Empty List");
                }
                else
                {
                    list.ForEach(num => Console.Write(num + " "));
                    Console.WriteLine();
                }
            });
            accumulator.Add(1);
            accumulator.Add(2);
            accumulator.Add(3);
            accumulator.Add(4);
            accumulator.Start();
            accumulator.Add(5);
            Thread.Sleep(2000);
            accumulator.Stop();
        }
    }
}
