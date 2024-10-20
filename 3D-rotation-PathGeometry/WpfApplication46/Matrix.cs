using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication46
{
    internal static class Matrix
    {
        public static double[,] Mult(double[,] matrixA, double[,] matrixB)
        {
            int length = matrixA.GetLength(0);
            int aCols = matrixA.GetLength(1);
            int length2 = matrixB.GetLength(0);
            int bCols = matrixB.GetLength(1);
            if (aCols != length2)
            {
                throw new Exception("Non-conformable matrices in MatrixProduct");
            }

            double[,] result = Create(length, bCols);
            Parallel.For(0, length, delegate (int i)
            {
                for (int j = 0; j < bCols; j++)
                {
                    for (int k = 0; k < aCols; k++)
                    {
                        result[i, j] += matrixA[i, k] * matrixB[k, j];
                    }
                }
            });
            return result;
        }

        private static double[,] Create(int rows, int cols)
        {
            double[,] array = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    array[i, j] = 0.0;
                }
            }

            return array;
        }
    }
}
