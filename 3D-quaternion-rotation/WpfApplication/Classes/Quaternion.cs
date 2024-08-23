using System;


namespace WpfApplication
{
    public class Quaternion
    {
        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }

        public double W { get; set; }

        public Quaternion(double w = 0.0, double x = 0.0, double y = 0.0, double z = 0.0)
        {
            W = w;
            X = x;
            Y = y;
            Z = z;
        }

        public Quaternion(double w, Vector3D vec)
        {
            W = w;
            X = vec.X;
            Y = vec.Y;
            Z = vec.Z;
        }

        public bool isEqual(Quaternion q)
        {
            return q == this;
        }

        public static double ToRadians(double angle)
        {
            return angle * Math.PI / 180.0;
        }

        private Quaternion Copy()
        {
            return new Quaternion(W, X, Y, Z);
        }

        public override string ToString()
        {
            return $"Quaternion [w, x, y, z]: [{W}, {X}, {Y}, {Z}]";
        }

        //
        // Summary:
        //     Магнитуда (длина) кватерниона
        //
        // Returns:
        //     Возвращает скаляр длины
        private double Mag()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z + W * W);
        }

        //
        // Summary:
        //     Нормализация текущего кватерниона
        //
        // Returns:
        //     Возвращает новый кватернион и изменяет текущий
        private Quaternion Normalize()
        {
            double num = Mag();
            X /= num;
            Y /= num;
            Z /= num;
            W /= num;
            return Copy();
        }

        //
        // Summary:
        //     Нормализация кватерниона
        //
        // Parameters:
        //   quat:
        //
        // Returns:
        //     Возвращает новый кватернион
        public static Quaternion Normalize(Quaternion quat)
        {
            return quat.Normalize().Copy();
        }

        //
        // Summary:
        //     Проверка на нормализацию кватерниона
        //
        // Returns:
        //     True - нормализован, False - не нормализован
        private bool isNormalized()
        {
            if (Mag() == 1.0)
            {
                return true;
            }

            return false;
        }

        //
        // Summary:
        //     Инвертирование текущего кватерниона
        //
        // Returns:
        //     Возвращает новый кватернион и изменяет текущий
        private Quaternion Invert()
        {
            Normalize();
            X *= -1.0;
            Y *= -1.0;
            Z *= -1.0;
            return Copy();
        }

        //
        // Summary:
        //     Конвертация кватерниона в матрицу
        //
        // Returns:
        //     Двумерный массив вида [,]
        public double[,] ToMatrix()
        {
            if (!isNormalized())
            {
                return Normalize().ToMatrix();
            }

            double num = X * X;
            double num2 = X * Y;
            double num3 = X * Z;
            double num4 = X * W;
            double num5 = Y * Y;
            double num6 = Y * Z;
            double num7 = Y * W;
            double num8 = Z * Z;
            double num9 = Z * W;
            return new double[4, 4]
            {
            {
                1.0 - 2.0 * (num5 + num8),
                2.0 * (num2 - num9),
                2.0 * (num3 + num7),
                0.0
            },
            {
                2.0 * (num2 + num9),
                1.0 - 2.0 * (num + num8),
                2.0 * (num6 - num4),
                0.0
            },
            {
                2.0 * (num3 - num7),
                2.0 * (num6 + num4),
                1.0 - 2.0 * (num + num5),
                0.0
            },
            { 0.0, 0.0, 0.0, 1.0 }
            };
        }

        //
        // Summary:
        //     Конвертация матрицы в кватернион
        //
        // Parameters:
        //   m:
        //     Двумерный массив
        //
        // Returns:
        //     Новый кватернион
        public Quaternion MatrixToQuaternion(double[,] m)
        {
            double[] array = new double[4];
            int[] array2 = new int[3] { 1, 2, 0 };
            double num = m[0, 0] + m[1, 1] + m[2, 2];
            if (num > 0.0)
            {
                double num2 = Math.Sqrt(num + 1.0);
                W = num2 / 2.0;
                num2 = 0.5 / num2;
                X = (m[1, 2] - m[2, 1]) * num2;
                Y = (m[2, 0] - m[0, 2]) * num2;
                Z = (m[0, 1] - m[1, 0]) * num2;
            }
            else
            {
                int num3 = 0;
                if (m[1, 1] > m[0, 0])
                {
                    num3 = 1;
                }

                if (m[2, 2] > m[num3, num3])
                {
                    num3 = 2;
                }

                int num4 = array2[num3];
                int num5 = array2[num4];
                double num2 = Math.Sqrt(m[num3, num3] - (m[num4, num4] + m[num5, num5]) + 1.0);
                array[num3] = num2 * 0.5;
                if (num2 != 0.0)
                {
                    num2 = 0.5 / num2;
                }

                array[3] = (m[num4, num5] - m[num5, num4]) * num2;
                array[num4] = (m[num3, num4] + m[num4, num3]) * num2;
                array[num5] = (m[num3, num5] + m[num5, num3]) * num2;
                X = array[0];
                Y = array[1];
                Z = array[2];
                W = array[3];
            }

            return Copy();
        }

        //
        // Summary:
        //     Умножение кватерниона на вектор. Позволяет повернуть вектор на вращение заданное,
        //     кватернионом
        //
        // Parameters:
        //   q:
        //     Кватернион
        //
        //   v:
        //     Вектор
        //
        // Returns:
        //     Новый вектор
        public static Vector3D Rotate(Quaternion q, Vector3D v)
        {
            return q.Rotate(v);
        }

        public Vector3D Rotate(Vector3D v)
        {
            Quaternion q0 = new Quaternion(0.0, v.X, v.Y, v.Z);
            Quaternion q1 = new Quaternion(W, X, Y, Z);
            q1.Invert();
            Quaternion q2 = Multiply(q0, q1);
            q2 = Multiply(this, q2);
            v.X = q2.X;
            v.Y = q2.Y;
            v.Z = q2.Z;
            return new Vector3D
            {
                X = q2.X,
                Y = q2.Y,
                Z = q2.Z
            };
        }

        //
        // Summary:
        //     Умножение на кватернион
        //
        // Parameters:
        //   q:
        public void Multiply(Quaternion q)
        {
            Quaternion quaternion = this * q;
            W = quaternion.W;
            X = quaternion.X;
            Y = quaternion.Y;
            Z = quaternion.Z;
        }

        public Quaternion Multiply(double n)
        {
            W *= n;
            X *= n;
            Y *= n;
            Z *= n;
            return Copy();
        }

        //
        // Summary:
        //     Умножение двух кватернионов
        //
        // Parameters:
        //   q1:
        //     Кватернион 1
        //
        //   q2:
        //     Кватернион 2
        //
        // Returns:
        //     Новый кватернион
        public static Quaternion Multiply(Quaternion q1, Quaternion q2)
        {
            Vector3D vector3D = new Vector3D
            {
                X = q1.X,
                Y = q1.Y,
                Z = q1.Z
            };
            Vector3D vector3D2 = new Vector3D
            {
                X = q2.X,
                Y = q2.Y,
                Z = q2.Z
            };
            Quaternion quaternion = new Quaternion();
            double w = q1.W * q2.W - Vector3D.Dot(vector3D, vector3D2);
            Vector3D vector3D3 = Vector3D.Cross(vector3D, vector3D2);
            vector3D2 = Vector3D.Mult(vector3D2, q1.W);
            vector3D = Vector3D.Mult(vector3D, q2.W);
            quaternion.X = vector3D.X + vector3D2.X + vector3D3.X;
            quaternion.Y = vector3D.Y + vector3D2.Y + vector3D3.Y;
            quaternion.Z = vector3D.Z + vector3D2.Z + vector3D3.Z;
            quaternion.W = w;
            return quaternion;
        }

        public static Quaternion Multiply(Quaternion q, double n)
        {
            return q.Multiply(n).Copy();
        }

        public static Quaternion MultiplyQuick(Quaternion q1, Quaternion q2)
        {
            Quaternion quaternion = new Quaternion();
            double num = (q1.W + q1.X) * (q2.W + q2.X);
            double num2 = (q1.Z - q1.Y) * (q2.Y - q2.Z);
            double num3 = (q1.X - q1.W) * (q2.Y + q2.Z);
            double num4 = (q1.Y + q1.Z) * (q2.X - q2.W);
            double num5 = (q1.X + q1.Z) * (q2.X + q2.Y);
            double num6 = (q1.X - q1.Z) * (q2.X - q2.Y);
            double num7 = (q1.W + q1.Y) * (q2.W - q2.Z);
            double num8 = (q1.W - q1.Y) * (q2.W + q2.Z);
            quaternion.W = num2 + (0.0 - num5 - num6 + num7 + num8) * 0.5;
            quaternion.X = num - (num5 + num6 + num7 + num8) * 0.5;
            quaternion.Y = 0.0 - num3 + (num5 - num6 + num7 - num8) * 0.5;
            quaternion.Z = 0.0 - num4 + (num5 - num6 - num7 + num8) * 0.5;
            return quaternion;
        }

        //
        // Summary:
        //     Получение кватерниона из углов Эйлера для одной оси
        //
        // Parameters:
        //   v:
        //     Вектор, вокруг которого происходит вращение
        //
        //   angle:
        //     Угол
        public static Quaternion FromAngle(Vector3D v, double angle)
        {
            Quaternion quaternion = new Quaternion();
            angle *= 0.5;
            v.Normalize();
            double num = Math.Sin(angle);
            quaternion.X = v.X * num;
            quaternion.Y = v.Y * num;
            quaternion.Z = v.Z * num;
            quaternion.W = Math.Cos(angle);
            return quaternion;
        }

        //
        // Summary:
        //     Получение кватерниона из углов Эйлера для трех осей
        //
        // Parameters:
        //   roll:
        //     X угол в рад.
        //
        //   pitch:
        //     Y угол в рад.
        //
        //   yaw:
        //     Z угол в рад.
        //
        // Returns:
        //     Новый кватернион
        public static Quaternion FromEulerFull(double roll, double pitch, double yaw)
        {
            Vector3D v = new Vector3D(1.0);
            Vector3D v2 = new Vector3D(0.0, 1.0);
            Vector3D v3 = new Vector3D(0.0, 0.0, 1.0);
            Quaternion q = FromAngle(v, roll);
            Quaternion q2 = FromAngle(v2, pitch);
            Quaternion q3 = FromAngle(v3, yaw);
            Quaternion q4 = Multiply(q, q2);
            return Multiply(q4, q3);
        }

        //
        // Summary:
        //     Умножение кватернионов
        //
        // Parameters:
        //   q1:
        //
        //   q2:
        //
        // Returns:
        //     Новый кватернион
        public static Quaternion operator *(Quaternion q1, Quaternion q2)
        {
            double w = q1.W * q2.W - q1.X * q2.X - q1.Y * q2.Y - q1.Z * q2.Z;
            double x = q1.W * q2.X + q1.X * q2.W + q1.Y * q2.Z - q1.Z * q2.Y;
            double y = q1.W * q2.Y + q1.Y * q2.W + q1.Z * q2.X - q1.X * q2.Z;
            double z = q1.W * q2.Z + q1.Z * q2.W + q1.X * q2.Y - q1.Y * q2.X;
            return new Quaternion(w, x, y, z);
        }

        public static Quaternion operator -(Quaternion q)
        {
            return new Quaternion(0.0 - q.W, 0.0 - q.X, 0.0 - q.Y, 0.0 - q.Z);
        }

        public static Quaternion operator +(Quaternion q1, Quaternion q2)
        {
            return new Quaternion(q1.W + q2.W, q1.X + q2.X, q1.Y + q2.Y, q1.Z + q2.Z);
        }

        public static bool operator ==(Quaternion q1, Quaternion q2)
        {
            return q1.W == q2.W && q1.X == q2.X && q1.Y == q2.Y && q1.Z == q2.Z;
        }

        public static bool operator !=(Quaternion q1, Quaternion q2)
        {
            return !(q1 == q2);
        }

        public Vector3D ToEuler()
        {
            Vector3D vector3D = new Vector3D();
            Normalize();
            double num = X * X;
            double num2 = Y * Y;
            double num3 = Z * Z;
            vector3D.X = Math.Atan2(2.0 * (X * W + Y * Z), 1.0 - 2.0 * (num + num2));
            vector3D.Y = Math.Asin(2.0 * (Y * W - Z * X));
            vector3D.Z = Math.Atan2(2.0 * (Z * W + X * Y), 1.0 - 2.0 * (num2 + num3));
            return vector3D;
        }
    }
}
