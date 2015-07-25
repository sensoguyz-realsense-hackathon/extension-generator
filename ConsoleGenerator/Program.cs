using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Give config filename");
                return;
            }
            if (!File.Exists(args[0]))
            {
                Console.WriteLine("Given file is not exist");
                return;
            }
            var json = File.ReadAllText(args[0]);
            var config = JsonConvert.DeserializeObject(json);
            Console.WriteLine(config.ToString());
        }
    }
}
