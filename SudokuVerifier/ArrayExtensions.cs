using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuVerifier
{
    public static class ArrayExtensions
    {
        public static T[] GetRow<T>(this T[,] twoDimArray, int rowIndex)
        {
            int n = twoDimArray.GetLength(0);
            int m = twoDimArray.GetLength(1);

            if (rowIndex >= m || rowIndex < 0) {
                throw new IndexOutOfRangeException($"Row index is out of Range: {rowIndex}");
            }

            var retVal = new T[n];
            for (int i = 0; i < n; i++)
            {
                retVal[i] = twoDimArray[rowIndex, i];
            }

            return retVal;
        }

        public static T[] GetColumn<T>(this T[,] twoDimArray, int columnIndex)
        {
            int n = twoDimArray.GetLength(0);
            int m = twoDimArray.GetLength(1);

            if (columnIndex >= n || columnIndex < 0)
            {
                throw new IndexOutOfRangeException($"Column index is out of Range: {columnIndex}");
            }

            var retVal = new T[n];
            for (int i = 0; i < m; i++)
            {
                retVal[i] = twoDimArray[i, columnIndex];
            }

            return retVal;
        }
    }
}
