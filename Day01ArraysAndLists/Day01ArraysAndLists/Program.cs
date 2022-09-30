using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01ArraysAndLists
{
    internal class Program
    {
        static void Main(string[] args)
        {

            try
            {
                // possibly a jagged array, only the first dimension is allocated
                //int[][] twoDimInt = new int[4][];
                // rectangular array
                //int[,] twoDimIntRectangular = new int[4, 3];
                //int[,,] threedimintrect = new int[4, 3, 2]; // 3d: 4 x 3 x 2 size

                int[,] data2dInts = { { 1, 2, 3 }, { 3, 4, 1 }, { 5, 6, 10 } };

                // TASK: compute the avearage value (as floating point) of all values in data2dInts
                { // using two loops
                    double sum = 0;
                    for (int row = 0; row < data2dInts.GetLength(0); row++)
                    {
                        for (int col = 0; col < data2dInts.GetLength(1); col++)
                        {
                            sum += data2dInts[row, col];
                        }
                    }
                    double avg = sum / data2dInts.Length;
                    Console.WriteLine($"The average of all values is {avg:0.0##}");
                }
                { // using a for loop
                    double sum = 0;
                    foreach (int val in data2dInts)
                    {
                        sum += val;
                    }
                    double avg = sum / data2dInts.Length;
                    Console.WriteLine($"The avarege of all values is {avg:0.###}");
                }

            }
            finally
            {
                Console.WriteLine("Press Any Key to Finish");
                Console.ReadKey();
            }
        }
    }
}
