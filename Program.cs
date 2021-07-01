/*
 * Given a Sudoku data structure with size NxN, N > 0 and vN == integer, write a method to validate if it has been filled out correctly.
 * The data structure is a multi-dimensional Array, ie:

    [
      [7,8,4,  1,5,9,  3,2,6],
      [5,3,9,  6,7,2,  8,4,1],
      [6,1,2,  4,3,8,  7,5,9],

      [9,2,8,  7,1,5,  4,6,3],
      [3,5,7,  8,4,6,  1,9,2],
      [4,6,1,  9,2,3,  5,8,7],

      [8,7,6,  3,9,4,  2,1,5],
      [2,4,3,  5,6,1,  9,7,8],
      [1,9,5,  2,8,7,  6,3,4]
    ]


 * Rules for validation

 * Data structure dimension: NxN where N > 0 and vN == integer
 * Rows may only contain integers: 1..N (N included)
 * Columns may only contain integers: 1..N (N included)
 * 'Little squares' (3x3 in example above) may also only contain integers: 1..N (N included)

 * taken from http://www.codewars.com/kata/540afbe2dc9f615d5e000425/train/javascript
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuTest
{
    class SudokuPuzzleValidator
    {
        static void Main(string[] args)
        {
            int[][] goodSudoku1 = {
                new int[] {7,8,4,  1,5,9,  3,2,6},
                new int[] {5,3,9,  6,7,2,  8,4,1},
                new int[] {6,1,2,  4,3,8,  7,5,9},

                new int[] {9,2,8,  7,1,5,  4,6,3},
                new int[] {3,5,7,  8,4,6,  1,9,2},
                new int[] {4,6,1,  9,2,3,  5,8,7},

                new int[] {8,7,6,  3,9,4,  2,1,5},
                new int[] {2,4,3,  5,6,1,  9,7,8},
                new int[] {1,9,5,  2,8,7,  6,3,4}
            };


            int[][] goodSudoku2 = {
                new int[] {1,4, 2,3},
                new int[] {3,2, 4,1},

                new int[] {4,1, 3,2},
                new int[] {2,3, 1,4}
            };

            int[][] badSudoku1 =  {
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},

                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},

                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9}
            };

            int[][] badSudoku2 = {
                new int[] {1,2,3,4,5},
                new int[] {1,2,3,4},
                new int[] {1,2,3,4},
                new int[] {1}
            };

            Debug.Assert(ValidateSudoku(goodSudoku1), "This is supposed to validate! It's a good sudoku!");
            Debug.Assert(ValidateSudoku(goodSudoku2), "This is supposed to validate! It's a good sudoku!");
            Debug.Assert(!ValidateSudoku(badSudoku1), "This isn't supposed to validate! It's a bad sudoku!");
            Debug.Assert(!ValidateSudoku(badSudoku2), "This isn't supposed to validate! It's a bad sudoku!");
        }

        static bool ValidateSudoku(int[][] puzzle)
        {
            //check N > 0
            int N = puzzle.Length;
            if (puzzle.Length > 0)
            {
                //check √N == integer
                double result = Math.Sqrt(N);
                bool isSquare = result % 1 == 0;
                if (isSquare == true)
                {
                    //taking sum from 1...N
                    int[] arrayToN= Enumerable.Range(1, N).ToArray();
                    int sum = arrayToN.Sum();
                    int rowSum = 0;
                    int colSum = 0;
                    int squareSum = 0;
                    
                    //check if array is a perfect square
                    for (int row = 0; row < puzzle.Length; row++)
                    {
                        for (int col = 0; col < puzzle[row].Length; col++)
                        {
                            if (puzzle.Length != puzzle[row].Length)
                            {
                                Console.WriteLine("Not Valid");
                                return false;
                            }
                        }
                    }
                    //checking row sum
                    for (int i = 0; i < N; i++)
                    {
                        rowSum = 0;
                        colSum = 0;
                        for (int j = 0; j < N; j++)
                        {
                            rowSum= rowSum+ puzzle[i][j];
                            colSum = colSum + puzzle[j][i];
                        }
                    }
                    //checking row sum
                    if (rowSum != sum)
                    {
                        Console.WriteLine("Not Valid");
                        return false;
                    }
                    //checking column sum
                    if (colSum != sum)
                    {
                        Console.WriteLine("Not Valid");
                        return false;
                    }
                    //checking sum of squares
                    for (int i = 0; i< N - 2; i += Convert.ToInt32(result))
                    {
                        for (int j = 0; j < N - 2; j += Convert.ToInt32(result))
                        {
                            squareSum = 0;
                            for (int sqrRow = 0; sqrRow < Convert.ToInt32(result); sqrRow++)
                            {
                                for (int sqlCol = 0; sqlCol < Convert.ToInt32(result); sqlCol++)
                                {
                                    int X = i + sqrRow;
                                    int Y = j + sqlCol;
                                    squareSum = squareSum + puzzle[X][Y];
                                }
                            }
                            if (squareSum != sum)
                            {
                                Console.WriteLine("Not Valid");
                                return false;
                            }
                        }
                    }
                    Console.WriteLine("Valid");
                    return true;

                }
                else
                {
                    Console.WriteLine("Not Valid");
                    return false;

                }
            }
            else
            {
                Console.WriteLine("Not Valid");
                return false;
            }
        }
    }
}