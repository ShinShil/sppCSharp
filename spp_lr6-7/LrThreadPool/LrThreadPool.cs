using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LrThreadPool
{
    public class LrThreadPool
    {
        private static Queue<Thread> Threads { get; set; }
        
        public static void Add(Thread thread)
        {
            Threads.Enqueue(thread);
            InvokeThread(thread);
        }

        private static void InvokeThread(Thread thread)
        {
            thread.Start();
            if(Threads.Count != 0)
            {
                InvokeThread(Threads.Dequeue());
            }
        }
    }
}
