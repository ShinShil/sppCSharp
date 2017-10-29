using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace spp_lr1
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Count() == 0)
            {
                Console.WriteLine("missing argument: assembly name, usage: spp_lr1 <assembly_name>");
            }
            else
            {
                PrintPublicTypes(GetAssembly(args[0]));
            }
        }

        static void PrintPublicTypes(Assembly assembly)
        {
            if (assembly == null) return;

            var types = assembly.GetTypes()
                .Where(t => t.IsPublic)
                .OrderBy(t => t.Namespace)
                .ThenBy(t => t.Name);

            if(types.Count() == 0)
            {
                Console.WriteLine("There ara no public types in {0}", assembly.Location);
            } else
            {
                foreach(var type in types)
                {
                    Console.WriteLine(type.FullName);
                }
            }
        }

        static Assembly GetAssembly(string assemblyName)
        {
            string assemblyPath = Directory.GetCurrentDirectory() + "\\" + assemblyName;
            try
            {
                Assembly a = Assembly.LoadFile(assemblyPath);
                return a;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Can't find file: {0}", assemblyPath);
                return null;
            }
            catch (BadImageFormatException)
            {
                Console.WriteLine("{0} not allowed assembly or it was compiled in newer CLR version that current({1})", assemblyPath, System.Environment.Version);
                return null;
            }
        }
    }
}
