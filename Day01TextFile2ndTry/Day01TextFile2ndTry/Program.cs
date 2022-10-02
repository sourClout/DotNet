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

namespace Day01TextFile2ndTry
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                const string filePath = @"..\..\data2.txt";

                Console.Write("What is your name? ");
                string name = Console.ReadLine();

                try
                {
                    // write an array of strings
                    string[] namesArray = { name, name, name };
                    File.WriteAllLines(filePath, namesArray);
                }
                catch (SystemException ex)
                {
                    Console.WriteLine("Error writing to file: " + ex.Message);
                }

                // Reading from a file
                try
                {
                    string[] linesArray = File.ReadAllLines(filePath);
                    foreach (string line in linesArray)
                    {
                        Console.WriteLine(line);
                    }
                }
                catch (SystemException ex) // (IOException ex)
                {
                    Console.WriteLine("Error writing to file: " + ex.Message);
                }
            }
            finally
            {
                Console.WriteLine("Press any key ti finish");
                Console.ReadKey();
            }

            



        }
    }
}
