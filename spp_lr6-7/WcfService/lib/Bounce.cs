﻿using System;
using System.Threading;

namespace WcfService
{
    class Bounce
    {
        private Action callback;
        private int delay;
        private Thread thread;
        private object lockcallback = new object();
        private object lockthread = new object();

        public Bounce(Action callback, int delay)
        {
            this.callback = callback;
            this.delay = delay;
            thread = new Thread(new ThreadStart(TryToCallCallbackThread));
        }

        public void CallBounce()
        {
            lock (lockthread)
            {
                thread.Abort();
                thread = new Thread(new ThreadStart(TryToCallCallbackThread));
                thread.Start();
            }
        }

        public void CancelBounce()
        {
            lock(lockthread)
            {
                thread.Abort();
            }
        }

        public void CallImmidiate()
        {
            new Thread(new ThreadStart(CallImmidiateThread)).Start();
        }

        private void TryToCallCallbackThread()
        {
            Thread.Sleep(delay);
            lock (lockcallback)
            {
                callback();
            }
        }


        private void CallImmidiateThread()
        {
            lock (lockcallback)
            {
                lock (lockthread)
                {
                    thread.Abort();
                }
                new Thread(new ThreadStart(callback)).Start();
            }
        }
    }
}
