using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spp_lr5
{
    class CmdArgs
    {
        public string DestFile { get; set; }
        public string SrcFile { get; set; }
        public uint nThreadsAmount { get; set; } = 1;

        public CmdArgs(string[] args)
        {
            if(!InputArgsAreValid(args))
            {
                PrintUsage();
                throw new Exception("Cmd checker exception");
            } else
            {
                SrcFile = args[0];
                DestFile = args[1];
                nThreadsAmount = UInt32.Parse(args[2]);
            }
        }

        public bool InputArgsAreValid(string[] args)
        {
            bool res = true;
            if (args.Length < 3)
            {
                PrintUsage();
                res = false;
            }
            else
            {
                uint nThreads = 0;
                if (!UInt32.TryParse(args[2], out nThreads))
                {
                    Console.WriteLine("3rd arg should be UInt32");
                    PrintUsage();
                    res = false;
                }
            }
            return res;
        }

        public void PrintUsage()
        {
            Console.WriteLine("Wrong input, usage:\n\tspp_lr5 <src_file> <dest_file> <thread_amount>\n");
        }
    }
}
