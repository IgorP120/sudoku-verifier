using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace SudokuVerifier
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath = args[0];
            if ((filePath ?? "").Trim().Length == 0)
            {
                throw new Exception("File path is required as a first argiument");
            }

            filePath = filePath.Trim();

            Console.WriteLine($"Verifying Sudoku numbers from {filePath}...{Environment.NewLine}");

            const int n = 3; // it's always 3, but just in case; it's better to use a variable anyway
            int m = n * n;

            var sudokuMatrix = new int[m, m];

            var lines = File.ReadLines(filePath).ToArray();
            if (lines.Length < m)
            {
                throw new Exception($"Invalid input format: file must contain at least {m} records");
            }

            // more than "m" records is okay, just ignore the rest:

            // fill the Sudoku matrix
            int lineNum = 0;
            foreach (var line in lines)
            {
                // skip/ignore empty lines:
                if (line == null || line.Trim().Length == 0)
                {
                    continue;
                }

                if (line.Length != m)
                {
                    throw new Exception($"Invalid inpout format: each line must contain exactly {m} digits");
                }

                Console.WriteLine(line);

                int charNum = 0;
                foreach(char c in line)
                {
                    int digit;
                    if (!int.TryParse(c.ToString(), out digit)) {
                        throw new Exception($"Invalid input format: {c} is not a number in {line}, line {lineNum+1}");
                    }
                    sudokuMatrix[lineNum, charNum++] = digit;
                }

                if (++lineNum >= m)
                {
                    break; // ignore the rest, if there are more than "m" lines
                }
            }

            Console.WriteLine();

            // Now verify all Sudoku conditions, as per https://en.wikipedia.org/wiki/Sudoku:

            var sudokuSession = new SudokuSession(sudokuMatrix);

            if (sudokuSession.VerifySolution())
            {
                Console.WriteLine("Congratulations! These numbers compose a valid Sudoku solution.");
            } else
            {
                Console.WriteLine("Sorry, these numbers do not represent a valid Sudoku solution.");
            }

            Console.WriteLine("Press any key to exit...");

            Console.ReadKey();

        }


       
    }
}