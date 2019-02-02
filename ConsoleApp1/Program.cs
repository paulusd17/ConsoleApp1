using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace ConsoleApp1
{
    /// <summary>
    /// This is the main script for the program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// This is the start method for the program. It wil read in the corpus
        /// and pass it to the indexer one at a time
        /// </summary>
        /// <param name="args">Not used</param>
        public static void Main(string[] args)
        {

            Console.WriteLine("Program Started");
            DateTime start = DateTime.Now;
            try
            {
                DirectoryInfo CorpusPath = null;
                if (args is null || args.Length == 0)
                {
                    Console.WriteLine("Do you want to use the default path location for the corpus (current location where running exe is located)? Y or N");
                    string answer = Console.ReadLine();
                    if (answer.ToUpper() == "Y")
                    {
                        CorpusPath = new DirectoryInfo(Directory.GetCurrentDirectory() + "/corpus");
                    }
                    else
                    {
                        GetPath(CorpusPath);
                    }
                }
                else
                {
                    if (Directory.Exists(args[0]))
                    {
                        CorpusPath = new DirectoryInfo(args[0]);
                    }
                    else
                    {
                        GetPath(CorpusPath);
                    }
                }

                Index index = new Index();

                foreach (FileInfo path in CorpusPath.EnumerateFiles())
                {
                    Console.WriteLine("Processing Index file :" + path.Name);
                    Console.WriteLine("Run Time:" + DateTime.Now.Subtract(start).ToString());
                    try
                    {
                        index.IndexFile(path.FullName);
                    }
                    catch (Exception ex)
                    {
                        SetError(start, "Error processing " + path.FullName + ". " + ex.Message);
                    }
                }

                Console.WriteLine("Index Complete");
                Console.WriteLine("Writing Index To File");

                try
                {
                    index.WriteToFile();
                }
                catch (Exception ex)
                {
                    SetError(start, "Error writing file to disk. " + ex.Message);
                }

                Console.WriteLine("Run Time:" + DateTime.Now.Subtract(start).ToString());
                Console.WriteLine("Program Complete. This window will close in 5 seconds.");
                Thread.Sleep(5000);
            }
            catch (Exception ex)
            {
                SetError(start, "Unknow error occured. " + ex.Message);
            }
        }

        /// <summary>
        /// This will set the exception message and exit the program
        /// </summary>
        /// <param name="start">The datatime that the program began</param>
        /// <param name="msg">The error message to display</param>
        public static void SetError(DateTime start, string msg)
        {
            Console.WriteLine("Run Time:" + DateTime.Now.Subtract(start).ToString());
            Console.WriteLine(msg);
            Console.WriteLine("Program Error. This window will close in 5 seconds.");
            Thread.Sleep(5000);
            Environment.Exit(1);
        }

        //This will handle getting the path to the corpus if the user does not wish to use the default path
        private static void GetPath(DirectoryInfo CorpusPath)
        {
            while (CorpusPath is null)
            {
                Console.WriteLine("Enter the fully qualified path to the root corpus folder");
                string answer = Console.ReadLine();
                if (Directory.Exists(answer))
                    CorpusPath = new DirectoryInfo(answer);
                else
                {
                    Console.WriteLine("The path entered does not exist.");
                    CorpusPath = null;
                }
            }
        }
    }
}
