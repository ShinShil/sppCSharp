using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace spp_lr3
{

    class ConcurrentAccumulator
    {
        public delegate void FlushCallback(List<object> items);

        private List<object> buffer = new List<object>();
        private int maxObjectsAmount;
        private Bounce bounce;
        private object lockAdd = new object();
        private object lockFlush = new object();

        public ConcurrentAccumulator(int maxObjectsAmount, int timeout, FlushCallback flushCallback)
        {
            this.maxObjectsAmount = maxObjectsAmount;
            bounce = new Bounce(() =>
            {
                lock (lockFlush)
                {
                    flushCallback(buffer);
                    buffer = new List<object>();
                    new Thread(new ThreadStart(() => bounce.CallBounce())).Start();
                    Monitor.PulseAll(lockFlush);
                }
            }, timeout);
            bounce.CallBounce();
        }

        public void Add(object item)
        {
            new Thread(new ParameterizedThreadStart(obj =>
            {
                lock (lockAdd)
                {
                    lock (lockFlush)
                    {
                        while (CanCallFlushCalback())
                        {
                            bounce.CallImmidiate();
                            Monitor.Wait(lockFlush);
                        }
                        buffer.Add(obj);
                    }
                }
            })).Start(item);
            bounce.CallBounce();
        }

        private bool CanCallFlushCalback()
        {
            return buffer.Count + 1 > maxObjectsAmount;
        }
    }
}
