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
        private int maxObjectsAmount = 10;
        private Bounce bounce;

        public ConcurrentAccumulator(int maxObjectsAmount, int timeout, FlushCallback flushCallback)
        {
            this.maxObjectsAmount = maxObjectsAmount;
            bounce = new Bounce(() =>
            {
                flushCallback(buffer);
                buffer = new List<object>();
            }, timeout);
            bounce.BounceCallback();
        }

        public void Add(object item)
        {
            buffer.Add(item);
            bounce.BounceCallback();
        }
    }
}
