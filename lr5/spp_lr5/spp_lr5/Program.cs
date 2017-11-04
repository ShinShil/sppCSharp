using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace spp_lr5
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!InputArgsAreValid(args)) return;
            CopyFile(args[0], args[1]);
        }

        static void CopyFile(string src, string dest)
        {
            CopyFile(src, dest);
        }

        static bool InputArgsAreValid(string[] args)
        {
            bool res = true;
            if (args.Length < 3)
            {
                PrintUsage();
                res = false;
            }
            else
            {
                int nThreads = 0;
                if (!Int32.TryParse(args[2], out nThreads))
                {
                    Console.WriteLine("3rd arg should an integer");
                    PrintUsage();
                    res = false;
                }
            }
            return res;
        }

        static void PrintUsage()
        {
            Console.WriteLine("Wrong input, usage:\n\tspp_lr5 <src_file> <dest_file> <thread_amount>\n");
        }
    }
}
