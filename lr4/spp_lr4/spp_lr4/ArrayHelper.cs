using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spp_lr4
{
    class ArrayHelper
    {
        public unsafe static int Sum(int[] arr, int count)
        {
            unsafe
            {
                fixed (int* arrPointer = &arr[0])
                {
                    int res = 0;
                    for (int i = 0; i < count; ++i)
                    {
                        if (Int32.MaxValue - res > arrPointer[i])
                        {
                            res += arrPointer[i];
                        }
                        else
                        {
                            throw new OverflowException("Int overflow");
                        }
                    }
                    return res;
                }
            }
        }
    }
}
