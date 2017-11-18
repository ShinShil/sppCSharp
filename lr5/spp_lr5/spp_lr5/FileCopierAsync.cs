using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace spp_lr5
{
    class FileCopierAsync
    {
        private string destFilePath;
        private string srcFilePath;
        private uint maxThreadsAmount = 0;
        private uint copiedBytesAmountPerTime = 1;

        private FileStream srcStream;
        private FileStream destStream;

        private object lockobject = new object();
        private Thread copyThread = null;

        public int TotalSize { get; private set; } = 0;

        public event Action OnStart;
        public event Action OnSuccess;
        public event Action OnAbort;

        public FileCopierAsync(string destFilePath, string srcFilePath, uint maxThreadsAmount)
        {
            this.destFilePath = destFilePath;
            this.srcFilePath = srcFilePath;
            this.maxThreadsAmount = maxThreadsAmount;
            if (maxThreadsAmount == 0) throw new Exception("Bad value for maxThreadsAmount, should greater than zero");
        }

        public void StartCopyAsync()
        {
            srcStream = new FileStream(srcFilePath, FileMode.Open, FileAccess.Read);
            destStream = new FileStream(destFilePath, FileMode.Create, FileAccess.Write);

            long needCopyingThreadsAmount = (long)Math.Floor(srcStream.Length / (double)copiedBytesAmountPerTime);

            copyThread = new Thread(new ThreadStart(() =>
            {
                using (var countdownEvent = new CountdownEvent((int)needCopyingThreadsAmount))
                {
                    for (int i = 0; i < needCopyingThreadsAmount; ++i)
                    {
                        ThreadPool.QueueUserWorkItem(copy =>
                        {
                            lock (lockobject)
                            {
                                Thread.Sleep(500);
                                byte[] readenBytes = new byte[copiedBytesAmountPerTime];
                                int readenBytesAmount = srcStream.Read(readenBytes, 0, readenBytes.Length);
                                if (readenBytesAmount == 0)
                                {
                                    destStream.Close();
                                    srcStream.Close();
                                }
                                else
                                {
                                    destStream.Write(readenBytes, 0, readenBytesAmount);
                                }
                                countdownEvent.Signal();
                            }
                        });
                    }
                    countdownEvent.Wait();
                    OnSuccess();
                }
            }));

            OnStart();
            copyThread.Start();
        }

        public void AbortCopy()
        {
            if (copyThread != null && copyThread.IsAlive)
            {
                copyThread.Abort();
                OnAbort();
            }
        }
    }
}
