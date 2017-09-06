using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuVerifier;

namespace SudokuUnitTests
{
    [TestClass]
    public class TestSet1
    {
        [TestMethod]
        public void Check9DigitsArray_Correct()
        {
            var ss = new SudokuSession(new int[9, 9]);
            var okSequence = new int[9] { 2, 1, 3, 4, 5, 9, 8, 7, 6 };
            var result = ss.Verify9DigitSet(okSequence);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Check9DigitsArray_Wrong()
        {
            var ss = new SudokuSession(new int[9, 9]);
            var okSequence = new int[9] { 0, 1, 3, 4, 5, 9, 8, 7, 6 };
            var result = ss.Verify9DigitSet(okSequence);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckInput_Wrong()
        {
            var matrix = new int[8, 9];
            var ss = new SudokuSession(matrix);
        }
    }
}
