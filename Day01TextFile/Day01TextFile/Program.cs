using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * TASK
----

Create Day01TextFile console project.

Ask the user for their name.
Write that name to "data.txt" file 3 times on separate lines.

Then read back the contents of the file file (all lines, no matter how many) and display them back on the console.

Make sure to handle the usual exceptions related to file IO.
 */

namespace Day01TextFile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                const string filePath = @"..\..\data.txt";

                Console.Write("What is your name? ");
                string name = Console.ReadLine();

                try
                {
                    { // write an array of strings
                        string[] namesArray = { name, name, name };
                        File.WriteAllLines(filePath, namesArray);
                    }
                    /*
                    { // write 3 lines, delete then append - this is the worst solution
                        System.IO.File.Delete(filePath);
                        for (int i = 0; i< 3; i++)
                        {
                            System.IO.File.AppendAllText(filePath, name + Environment.NewLine);
                        }
                    }*/
                    {
                        string fileContents = $"{name}\n{name}\n{name}\n";
                        File.WriteAllText(filePath, fileContents);
                    }
                    // NOTE: Using StreamWriter is not recommened for small amounts of data
                    // https://docs.microsoft.com/en-us/dotnet/standard/io/how-to-write-text-to-a-file
                    // NOTE: Handle selectively many exceptions at the same time
                }
                catch (SystemException ex)
                {
                    Console.WriteLine("Error writing to file: " + ex.Message);
                }


                /*
                try
                {
                    using (sw = new StreamWriter(filePath)) {
                        //Write a line of text
                        sw.WriteLine(name);
                        sw.WriteLine(name);
                        sw.WriteLine(name);
                        //Close the file - automatic when leaving this block
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
                finally
                {
                    Console.WriteLine("Executing finally block.");
                    Console.ReadKey();
                }*/

                try
                {
                    { // most common way
                        string[] linesArray = File.ReadAllLines(filePath);
                        foreach (string line in linesArray)
                        {
                            Console.WriteLine(line);
                        }
                    }
                    {
                        string allContent = File.ReadAllText(filePath);
                        Console.WriteLine(allContent);
                    }
                }
                catch (SystemException ex) // (IOException ex)
                {
                    Console.WriteLine("Error writing to file: " + ex.Message);
                }

            }
            finally
            {
                Console.WriteLine("Press any key to finish");
                Console.ReadKey();
            }
        }
    }
}
