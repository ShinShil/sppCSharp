using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace spp_lr5
{
    class Md5Checker
    {
        public static bool CheckFiles(string file1, string file2)
        {
            using(var md5 = MD5.Create())
            {
                using (var f1Stream = File.OpenRead(file1))
                {
                    using (var f2Stream = File.OpenRead(file2))
                    {
                        var hash1 = md5.ComputeHash(f1Stream);
                        var hash2 = md5.ComputeHash(f2Stream);
                        return hash1.SequenceEqual(hash2);
                    }
                }
            }
        }
    }
}
