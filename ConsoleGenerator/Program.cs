﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleGenerator
{
    class Program
    {
        private const string RealSenseExtensionString = "RealSense Extension";
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Give config filename");
                return;
            }
            if (args.Length !=2)
            {
                Console.WriteLine("Give config filename and target path");
                return;
            }
            if (!File.Exists(args[0]))
            {
                Console.WriteLine("Given file is not exist");
                return;
            }
            if (!Directory.Exists(args[1]))
            {
                Console.WriteLine("Given directory is not exist");
                Console.WriteLine("Do you want to create folder? Yes(y) or No(n)?");
                var answer = Console.ReadLine()?.ToLower() == "y";
                if (answer) Directory.CreateDirectory(args[1]);
                else return;
            }
            var json = File.ReadAllText(args[0]);
            dynamic config = JsonConvert.DeserializeObject(json);
//            Console.WriteLine(config.ToString());
            CopyDir(new DirectoryInfo(Environment.CurrentDirectory + "/project"),
                args[1] + "/" + config.name + " " + RealSenseExtensionString);
        }

        static void CopyDir(DirectoryInfo dir,string destinationPath)
        {
            var files = dir.GetFiles();
            Directory.CreateDirectory(destinationPath);
            foreach (var file in files)
                File.Copy(file.FullName, string.Concat(destinationPath, "/", file.Name));
            var dirs = dir.GetDirectories();
            if (dirs.Length == 0) return;
            foreach (var directory in dirs)
                CopyDir(directory, destinationPath + "/" + directory.Name);

        }

        void ParseLink(object json)
        {
            
        }
    }
}
