using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WpfApp
{
    class Matrix
    {
        public Matrix()
        {

        }

        public double[,] Rotate(double xa, double ya, double za)
        {
            double[,] xr = Create(4, 4);
            double[,] yr = Create(4, 4);
            double[,] zr = Create(4, 4);

            // Матрица поворота вокруг оси Ох
            xr[0, 0] = 1;   xr[0, 1] = 0;       xr[0, 2] = 0;           xr[0, 3] = 0;
            xr[1, 0] = 0;   xr[1, 1] = Cos(xa); xr[1, 2] = -Sin(xa);    xr[1, 3] = 0;
            xr[2, 0] = 0;   xr[2, 1] = Sin(xa); xr[2, 2] = Cos(xa);     xr[2, 3] = 0;
            xr[3, 0] = 0;   xr[3, 1] = 0;       xr[3, 2] = 0;           xr[3, 3] = 1;

            // Матрица поворота вокруг оси Оy
            yr[0, 0] = Cos(ya);   yr[0, 1] = 0;    yr[0, 2] = Sin(ya);  yr[0, 3] = 0;
            yr[1, 0] = 0;         yr[1, 1] = 1;    yr[1, 2] = 0;        yr[1, 3] = 0;
            yr[2, 0] = -Sin(ya);  yr[2, 1] = 0;    yr[2, 2] = Cos(ya);  yr[2, 3] = 0;
            yr[3, 0] = 0;         yr[3, 1] = 0;    yr[3, 2] = 0;        yr[3, 3] = 1;

            // Матрица поворота вокруг оси Оz
            zr[0, 0] = Cos(za); zr[0, 1] = -Sin(za);    zr[0, 2] = 0;   zr[0, 3] = 0;
            zr[1, 0] = Sin(za); zr[1, 1] = Cos(za);     zr[1, 2] = 0;   zr[1, 3] = 0;
            zr[2, 0] = 0;       zr[2, 1] = 0;           zr[2, 2] = 1;   zr[2, 3] = 0;
            zr[3, 0] = 0;       zr[3, 1] = 0;           zr[3, 2] = 0;   zr[3, 3] = 1;

            return Mult(Mult(xr, yr), zr); // Возвращаем результирующую матрицу
        }

        public void RotateShape(Shape shape, double xa, double ya, double za)
        {
            double[,] rm = Rotate(xa, ya, za); // сгенерировать матрицу вращения
            double[] cm = new double[4] {0, 0, 0, 1}; // вектор (матрица) "столбец"

            for (int i = 0; i < shape.vertices.Count; i++) // цикл по всем вершинам фигуры
            {
                // инициализация ветора матрицы "столбца" для умножения на матрицу вращения
                cm[0] = shape.vertices[i].x;
                cm[1] = shape.vertices[i].y;
                cm[2] = shape.vertices[i].z;

                cm = Mult(rm, cm); // вызов умножения матриц

                // внесение изменений после вращения
                shape.vertices[i].x = cm[0];
                shape.vertices[i].y = cm[1];
                shape.vertices[i].z = cm[2];
            }
        }

        public void RotateAxis(List<Vertex> axis, double xa, double ya, double za)
        {
            double[,] rm = Rotate(xa, ya, za); // сгенерировать матрицу вращения
            double[] cm = new double[4] { 0, 0, 0, 1 }; // вектор (матрица) "столбец"

            for (int i = 0; i < axis.Count; i++)
            {
                // инициализация ветора матрицы "столбца" для умножения на матрицу вращения
                cm[0] = axis[i].x;
                cm[1] = axis[i].y;
                cm[2] = axis[i].z;

                cm = Mult(rm, cm); // вызов умножения матриц

                // внесение изменений после вращения
                axis[i].x = cm[0];
                axis[i].y = cm[1];
                axis[i].z = cm[2];
            }
        }


        // Умножение двумерных матриц
        public double[,] Mult(double[,] matrixA, double[,] matrixB)
        {
            int aRows = matrixA.GetLength(0); int aCols = matrixA.GetLength(1);
            int bRows = matrixB.GetLength(0); int bCols = matrixB.GetLength(1);
            if (aCols != bRows)
                throw new Exception("Non-conformable matrices in MatrixProduct");
            double[,] result = Create(aRows, bCols);
            Parallel.For(0, aRows, i =>
            {
                for (int j = 0; j < bCols; ++j)
                    for (int k = 0; k < aCols; ++k)
                        result[i, j] += matrixA[i, k] * matrixB[k, j];
            }
            );
            return result;
        }

        // Умножение двумерной матрицы на матрицу-столбец
        public double[] Mult(double[,] matrixA, double[] matrixB)
        {
            int aRows = matrixA.GetLength(0); int aCols = matrixA.GetLength(1);
            int bRows = matrixB.GetLength(0); int bCols = 1;
            if (aCols != bRows)
                throw new Exception("Non-conformable matrices in MatrixProduct");
            double[] result = new double[4] {0, 0, 0, 0};
            Parallel.For(0, aRows, i =>
            {
                for (int j = 0; j < bCols; ++j)
                    for (int k = 0; k < aCols; ++k)
                        result[i] += matrixA[i, k] * matrixB[k];
            }
            );
            return result;
        }

        // Создание двумерной матрицы с нулями
        private double[,] Create(int rows, int cols)
        {
            // Создаем матрицу, полностью инициализированную
            // значениями 0.0. Проверка входных параметров опущена.
            double[,] result = new double[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    result[i, j] = 0;
            return result;
        }

        // Вспомогательные методы
        private double Sin(double x) => Math.Sin(x);
        private double Cos(double x) => Math.Cos(x);
    }
}
