using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CavityMap
{
    class CavityMap
    {
        static void Main(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine());
            char[,] grid = new char[n, n];                  // <----------------------  2D array *allocated*
            for (int i = 0; i < n; i++)
            {
                string line = Console.ReadLine();
                char[] chars = line.ToCharArray();

                // TODO;  Any array copy ?
                // System.Buffer.BlockCopy(src,srcOffset, dest, destOffset, byteCount)  is CRUDE
                for (int j = 0; j < n; j++)
                {
                    grid[i, j] = chars[j];
                }
            }


            for (int i = 1; i < n - 1; i++)
            {
                for (int j = 1; j < n - 1; j++)
                {

                    char ch = grid[i, j];
                    if (ch == 'X')
                    {
                        continue;
                    }

                    // If any neighbor is X, this cell cannot be X
                    if ((grid[i - 1, j] == 'X') ||
                          (grid[i + 1, j] == 'X') ||
                          (grid[i, j - 1] == 'X') ||
                          (grid[i, j + 1] == 'X')
                        )
                    {
                        continue;
                    }

                    int v = Convert.ToInt32(ch);
                    int a = Convert.ToInt32(grid[i - 1, j]);
                    int b = Convert.ToInt32(grid[i + 1, j]);
                    int c = Convert.ToInt32(grid[i, j - 1]);
                    int d = Convert.ToInt32(grid[i, j + 1]);

                    if ((v > a) && (v > b) && (v > c) && (v > d))
                    {
                        grid[i, j] = 'X';
                    }
                }
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(grid[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
