using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spp_lr4
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arrOverflow = new int[] { int.MaxValue, 2 };
            int[] arrOk = new int[] { 1, 2 };

            try
            {
                Console.WriteLine(ArrayHelper.Sum(arrOk, arrOk.Length));
                Console.WriteLine(ArrayHelper.Sum(arrOk, arrOk.Length + 4));
                Console.WriteLine(ArrayHelper.Sum(arrOverflow, arrOk.Length + 2));
            }
            catch (OverflowException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
