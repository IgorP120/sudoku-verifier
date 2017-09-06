using System;
using System.Linq;

namespace SudokuVerifier
{
    public class SudokuSession
    {
        private const int blockSize = 3;
        private readonly int matrixSize = blockSize * 3;

        public int[,] matrix;

        public SudokuSession(int[,] matrix) {
            if (matrix == null)
            {
                throw new ArgumentNullException("matrix array is required");
            }

            if (matrix.GetLength(0) != this.matrixSize || matrix.GetLength(1) != this.matrixSize)
            {
                throw new ArgumentException($"{this.matrixSize} by {this.matrixSize} matrix is required");
            }

            this.matrix = matrix;
        }

        /// <summary>
        ///  Returns true if array contains all digits from 1 to 9.
        ///  Otherwise returns false
        /// </summary>
        /// <param name="digits"></param>
        /// <returns>
        /// true if array contains all digits from 1 to 9
        /// </returns>
        public bool Verify9DigitSet(int[] digits)
        {
            if (digits.Length != 9)
            {
                throw new Exception("Array must contain exactly 9 digits");
            }
            var sortedArray = digits.OrderBy(d => d).ToArray();
            for (int i = 0; i < 9; i++)
            {
                if (sortedArray[i] != i + 1)
                {
                    return false;
                }
            }

            return true;
        }

        public bool VerifySolution()
        {
            // 1. Each column contains all digits from 1 to 9:

            for (int colIndex = 0; colIndex < matrixSize; colIndex++)
            {
                var col = this.matrix.GetColumn(colIndex);
                if (!Verify9DigitSet(col))
                {
                    return false;
                }
            }

            // 2. Each row contains all digits from 1 to 9.

            for (int rowIndex = 0; rowIndex < matrixSize; rowIndex++)
            {
                var row = matrix.GetRow(rowIndex);
                if (!Verify9DigitSet(row))
                {
                    return false;
                }
            }

            // 3. Each of the nine 3×3 subgrids that compose the grid (also called "boxes", "blocks", or "regions") contains all digits from 1 to 9.
            for (int blockLeftIndex = 0; blockLeftIndex <= matrixSize - blockSize; blockLeftIndex += blockSize)
            {
                for (int blockTopIndex = 0; blockTopIndex <= matrixSize - blockSize; blockTopIndex += blockSize)
                {
                    // convert block to array
                    var block = new int[matrixSize];
                    for (int i = 0; i < blockSize; i++)
                    {
                        for (int j = 0; j < blockSize; j++)
                        {
                            block[i * blockSize + j] = matrix[blockTopIndex + i, blockLeftIndex + j];
                        }
                    }
                    if (!Verify9DigitSet(block))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
