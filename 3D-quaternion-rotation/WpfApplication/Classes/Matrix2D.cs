using System;

namespace WpfApplication
{
    internal class Matrix2D
    {
        public static double[] Mult(double[] vector, double[,] matrixA)
        {
            int aRows = matrixA.GetLength(0); int aCols = matrixA.GetLength(1);
            int vCols = vector.Length;
            if (aRows != vCols)
                throw new Exception("Non-conformable matrices in MatrixProduct");
            double[] result = new double[vCols];
            for (int i = 0; i < aCols; ++i)
            {
                for (int j = 0; j < vCols; ++j)
                {
                    result[i] += vector[j] * matrixA[j, i];
                }
            }
            return result;
        }
    }
}
