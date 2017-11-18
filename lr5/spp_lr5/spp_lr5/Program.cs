using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;

namespace spp_lr5
{
    class Program
    {
        static FileCopierAsync copierAsync = null;
        static CmdArgs cmdArgs;
        static InputBlockerAsync inputBlockerAsync;

        static void Main(string[] args)
        {
            try
            {
                cmdArgs = new CmdArgs(args);
                inputBlockerAsync = new InputBlockerAsync(asyncRes =>
                {
                    if(copierAsync != null)
                    {
                        copierAsync.AbortCopy();
                    }
                });
                copierAsync = new FileCopierAsync(cmdArgs.DestFile, cmdArgs.SrcFile, cmdArgs.nThreadsAmount);
                copierAsync.OnStart += CopierAsync_onStart;
                copierAsync.OnAbort += CopierAsync_onAbort;
                copierAsync.OnSuccess += CopierAsync_onSuccess;
                copierAsync.StartCopyAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fail to copy");
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
            }
        }

        private static void CopierAsync_onSuccess()
        {
            inputBlockerAsync.Continue();
            Console.WriteLine("File have been succesfully copied");
        }

        private static void CopierAsync_onAbort()
        {
            inputBlockerAsync.Continue();
            Console.WriteLine("Copying has been terminated by user");
        }

        private static void CopierAsync_onStart()
        {
            inputBlockerAsync.Wait();
            Console.WriteLine("Copying files, press any key to stop copying...");
        }
    }
}
