using System;
using System.Collections.Generic;
using System.Linq;

namespace core
{
    public static class Algorithms
    {
        public static bool IsZero(this double left)
        {
            //return false;
            return HasMinimalDifference(left, 0, 10);
        }

        public static bool HasMinimalDifference(double value1, double value2, int units)
        {
            long lValue1 = BitConverter.DoubleToInt64Bits(value1);
            long lValue2 = BitConverter.DoubleToInt64Bits(value2);

            // If the signs are different, return false except for +0 and -0.
            if ((lValue1 >> 63) != (lValue2 >> 63))
            {
                if (value1 == value2)
                    return true;

                return false;
            }

            long diff = Math.Abs(lValue1 - lValue2);

            if (diff <= (long)units)
                return true;

            return false;
        }

        private static List<string> SplitMultiDelims(string text, string delimiters)
        {
            var chars = delimiters.Split(',', StringSplitOptions.RemoveEmptyEntries);
            return text
                .Split(chars, StringSplitOptions.RemoveEmptyEntries)
                .ToList();
        }

        private static int Minimum(int a, int b) => a < b ? a : b;

        private static int Minimum(int a, int b, int c) => (a = a < b ? a : b) < c ? a : c;

        /// <summary>
        /// Damerau Levenshtein Distance Algorithm
        /// </summary>
        /// <param name="firstText"></param>
        /// <param name="secondText"></param>
        /// <returns></returns>
        public static int DlDistance(string firstText, string secondText)
        {
            var n = firstText.Length + 1;
            var m = secondText.Length + 1;
            var arrayD = new int[n, m];

            for (var i = 0; i < n; i++)
            {
                arrayD[i, 0] = i;
            }

            for (var j = 0; j < m; j++)
            {
                arrayD[0, j] = j;
            }

            for (var i = 1; i < n; i++)
            {
                for (var j = 1; j < m; j++)
                {
                    var cost = firstText[i - 1] == secondText[j - 1] ? 0 : 1;

                    arrayD[i, j] = Minimum(arrayD[i - 1, j] + 1, // delete
                        arrayD[i, j - 1] + 1, // insert
                        arrayD[i - 1, j - 1] + cost); // replacement

                    if (i > 1 && j > 1
                              && firstText[i - 1] == secondText[j - 2]
                              && firstText[i - 2] == secondText[j - 1])
                    {
                        arrayD[i, j] = Minimum(arrayD[i, j],
                            arrayD[i - 2, j - 2] + cost); // permutation
                    }
                }
            }

            return arrayD[n - 1, m - 1];
        }

        public static long Power(long x, int n)
        {
            if (n == 0)
            {
                return 1;
            }

            if (n % 2 == 0)
            {
                var p = Power(x, n / 2);
                return p * p;
            }
            else
            {
                return x * Power(x, n - 1);
            }
        }

        public static double WordSimilarity(string one, string two)
        {
            var wordsOne = SplitMultiDelims(one, " ");
            var wordsTwo = SplitMultiDelims(two, " ");
            var total = 0.00d;

            foreach (var word1 in wordsOne)
            {
                var best = two.Length * 1.00d;

                foreach (var word2 in wordsTwo)
                {
                    var distance = Similarity(word1, word2);
                    if (distance < best) best = distance;
                    if (distance == 0.00d) break;
                }

                total += best;
            }

            return total;
        }

        public static double Value(string one, string two)
        {
            //=MIN(D2,E2)*0.8+MAX(D2,E2)*0.2
            //var distance = Similarity(one, two);
            var distance = Distance(one, two);
            var word = WordSimilarity(one, two);
            var min = Math.Min(distance, word);
            var max = Math.Max(distance, word);
            var value = (min * 0.8d) + (max * 0.2d);
            return value;
        }

        public static bool IsSimilarTo(this string one, string two)
        {
            var i = Value(one, two);
            return i < 14.50d;
        }

        public static bool IsSimilarTo(this string one, IEnumerable<string> strings)
        {
            foreach (var s in strings)
            {
                if (one.IsSimilarTo(s)) return true;
            }

            return false;
        }

        public static float Distance(string firstWord, string secondWord)
        {
            var n = firstWord.Length + 1;
            var m = secondWord.Length + 1;
            var matrixD = new float[n, m];

            const float deletionCost = 1.00f;
            const float insertionCost = 1.00f;

            for (var i = 0; i < n; i++)
            {
                matrixD[i, 0] = i;
            }

            for (var j = 0; j < m; j++)
            {
                matrixD[0, j] = j;
            }

            for (var i = 1; i < n; i++)
            {
                for (var j = 1; j < m; j++)
                {
                    var substitutionCost = firstWord[i - 1] == secondWord[j - 1] ? 0 : 1;

                    matrixD[i, j] = Minimum(matrixD[i - 1, j] + deletionCost, // delete
                        matrixD[i, j - 1] + insertionCost, // insert
                        matrixD[i - 1, j - 1] + substitutionCost); // replacement
                }
            }

            return matrixD[n - 1, m - 1];
        }

        private static float Minimum(float a, float b, float c) => (a = a < b ? a : b) < c ? a : c;

        /// <summary>
        /// Calculate the difference between 2 strings using the Levenshtein distance algorithm
        /// </summary>
        /// <param name="one"></param>
        /// <param name="two"></param>
        /// <returns></returns>
        public static double Similarity(string one, string two)
        {
            var source1Length = one.Length;
            var source2Length = two.Length;

            var matrix = new double[source1Length + 1, source2Length + 1];

            // First calculation, if one entry is empty return full length
            if (source1Length == 0)
                return source2Length;

            if (source2Length == 0)
                return source1Length;

            // Initialization of matrix with row size source1Length and columns size source2Length
            for (var i = 0; i <= source1Length; matrix[i, 0] = i++) { }
            for (var j = 0; j <= source2Length; matrix[0, j] = j++) { }

            // Calculate rows and columns distances
            for (var i = 1; i <= source1Length; i++)
            {
                for (var j = 1; j <= source2Length; j++)
                {
                    var cost = (two[j - 1] == one[i - 1]) ? 0 : 1;

                    matrix[i, j] = Math.Min(
                        Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
                        matrix[i - 1, j - 1] + cost);
                }
            }

            return matrix[source1Length, source2Length];
        }
    }
}