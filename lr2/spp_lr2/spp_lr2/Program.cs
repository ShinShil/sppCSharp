using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace spp_lr2
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Count() < 2)
            {
                Console.WriteLine("Wrong arguments, usage: spp_lr2 <src_folder> <dest_folder>");
            } else
            {
                string src = GetFullPath(args[0]);
                string dest = GetFullPath(args[1]);
                var fullFileNames = Directory.GetFiles(src);
                int copiedFiles = 0;
                using (var countdownEvent = new CountdownEvent(fullFileNames.Length))
                {
                    foreach (var fullFileName in fullFileNames)
                    {
                        ThreadPool.QueueUserWorkItem(copy =>
                        {
                            string destFullFileName = dest + "\\" + Path.GetFileName(fullFileName);
                            try
                            {
                                File.Copy(fullFileName, destFullFileName);
                                Interlocked.Increment(ref copiedFiles);
                            }
                            catch
                            {
                                Console.WriteLine("Fail to copy file\nfrom {0}\nto {1}", fullFileName, destFullFileName);
                            }
                            countdownEvent.Signal();
                        });
                    }
                    countdownEvent.Wait();
                }
                Console.WriteLine("\nCopied files amount: {0}\n", copiedFiles);
            }
        }

        static string GetFullPath(string path)
        {
            return Directory.GetCurrentDirectory() + "\\" + path;
        }
    }
}
