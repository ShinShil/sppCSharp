using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace spp_lr3
{
    class Bounce
    {
        private Action callback;
        private int delay;
        private Thread thread;

        public Bounce(Action callback, int delay)
        {
            this.callback = callback;
            this.delay = delay;
            thread = new Thread(new ThreadStart(TryToCallCallback));
        }

        public void BounceCallback()
        {
            thread.Abort();
            thread = new Thread(new ThreadStart(TryToCallCallback));
            thread.Start();

        }

        private void TryToCallCallback()
        {
            Console.WriteLine("Callback thread: " + Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(delay);
            callback();
        }
    }
}
