using System;


namespace WpfApplication
{
    public class Vector3D
    {
        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }

        public Vector3D(double x = 0.0, double y = 0.0, double z = 0.0)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3D CopyToVector()
        {
            return new Vector3D(X, Y, Z);
        }

        public double[] CopyToArray()
        {
            return new double[3] { X, Y, Z };
        }

        //
        // Summary:
        //     Присвоение скалярных значений вектору
        //
        // Parameters:
        //   x:
        //
        //   y:
        //
        //   z:
        public void Set(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        //
        // Summary:
        //     Текстовое представление вектора
        //
        // Returns:
        //     Строка вида "[x, y, z]"
        public override string ToString()
        {
            return $"Vector3D Object: [{X}, {Y}, {Z}]";
        }

        //
        // Summary:
        //     Вывод значений вектора на консоль вида "[x, y, z]"
        public void ToConsole()
        {
            Console.WriteLine(ToString());
        }

        //
        // Summary:
        //     Сложение вектора со скалярами
        //
        // Parameters:
        //   vector:
        //
        // Returns:
        //     Возвращает новый вектор и изменяет текущий
        public Vector3D Add(double x, double y, double z)
        {
            X += x;
            Y += y;
            Z += z;
            return CopyToVector();
        }

        //
        // Summary:
        //     Сложение двух векторов
        //
        // Parameters:
        //   v:
        //
        // Returns:
        //     Возвращает новый вектор и изменяет текущий
        public Vector3D Add(Vector3D v)
        {
            X += v.X;
            Y += v.Y;
            Z += v.Z;
            return CopyToVector();
        }

        //
        // Summary:
        //     Сложение двух векторов
        //
        // Parameters:
        //   v1:
        //
        //   v2:
        //
        // Returns:
        //     Возвращает новый вектор
        public static Vector3D Add(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        //
        // Summary:
        //     Вычитание из вектора скаляров
        //
        // Parameters:
        //   x:
        //
        //   y:
        //
        // Returns:
        //     Возвращает новый вектор и изменяет текущий
        public Vector3D Sub(double x, double y, double z)
        {
            X -= x;
            Y -= y;
            Z -= z;
            return CopyToVector();
        }

        //
        // Summary:
        //     Вычитание из вектора другого вектора
        //
        // Parameters:
        //   v:
        //
        // Returns:
        //     Возвращает новый вектор и изменяет текущий
        public Vector3D Sub(Vector3D v)
        {
            X -= v.X;
            Y -= v.Y;
            Z -= v.Z;
            return CopyToVector();
        }

        //
        // Summary:
        //     Вычитание двух векторов
        //
        // Parameters:
        //   v1:
        //
        //   v2:
        //
        // Returns:
        //     Возвращает новый вектор
        public static Vector3D Sub(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        //
        // Summary:
        //     Деление вектора на скаляр
        //
        // Parameters:
        //   n:
        //
        // Returns:
        //     Возвращает новый вектор и изменяет текущий
        public Vector3D Div(double n)
        {
            X /= n;
            Y /= n;
            Z /= n;
            return CopyToVector();
        }

        //
        // Summary:
        //     Деление вектора на другой вектор
        //
        // Parameters:
        //   v:
        //
        // Returns:
        //     Возвращает новый вектор и изменяет текущий
        public Vector3D Div(Vector3D v)
        {
            X /= v.X;
            Y /= v.Y;
            Z /= v.Z;
            return CopyToVector();
        }

        //
        // Summary:
        //     Divide (деление) двух векторов
        //
        // Parameters:
        //   val:
        //
        // Returns:
        //     Возвращает новый вектор
        public static Vector3D Div(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1.X / v2.X, v1.Y / v2.Y, v1.Z / v2.Z);
        }

        //
        // Summary:
        //     Деление вектора на скаляр
        //
        // Parameters:
        //   v:
        //     Вектор
        //
        //   n:
        //     Скаляр
        //
        // Returns:
        //     Возвращает новый вектор
        public static Vector3D Div(Vector3D v, double n)
        {
            return new Vector3D(v.X / n, v.Y / n, v.Z / n);
        }

        //
        // Summary:
        //     Multiply (умножение) вектора на число
        //
        // Parameters:
        //   n:
        //
        // Returns:
        //     Возвращает новый вектор и изменяет текущий
        public Vector3D Mult(double n)
        {
            X *= n;
            Y *= n;
            Z *= n;
            return CopyToVector();
        }

        //
        // Summary:
        //     Multiply (умножение) вектора на число
        //
        // Parameters:
        //   v:
        //
        //   n:
        //
        // Returns:
        //     Возвращает новый вектор
        public static Vector3D Mult(Vector3D v, double n)
        {
            return new Vector3D(v.X * n, v.Y * n, v.Z * n);
        }

        //
        // Summary:
        //     Скалярное (Dot) умножение векторов
        //
        // Parameters:
        //   v:
        //
        // Returns:
        //     Скаляр
        public double Dot(Vector3D v)
        {
            return X * v.X + Y * v.Y + Z * v.Z;
        }

        //
        // Summary:
        //     Скалярное произведение векторов
        //
        // Parameters:
        //   v1:
        //     Вектор 1
        //
        //   v2:
        //     Вектор 2
        //
        // Returns:
        //     Скаляр
        public static double Dot(Vector3D v1, Vector3D v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        //
        // Summary:
        //     Векторное произведение двух векторов
        //
        // Parameters:
        //   v1:
        //     Вектор 1
        //
        //   v2:
        //     Вектор 2
        //
        // Returns:
        //     Новый вектор
        public static Vector3D Cross(Vector3D v1, Vector3D v2)
        {
            return new Vector3D(v1.Y * v2.Z - v1.Z * v2.Y, v1.Z * v2.X - v1.X * v2.Z, v1.X * v2.Y - v1.Y * v2.X);
        }

        //
        // Parameters:
        //   v:
        //
        //   onto:
        public static Vector3D Project(Vector3D v, Vector3D onto)
        {
            double num = Dot(v, onto);
            double num2 = Dot(onto, onto);
            return num / num2 * onto;
        }

        //
        // Summary:
        //     Интерполяция вектора к другому вектору
        //
        // Parameters:
        //   x:
        //
        //   y:
        //
        //   amt:
        //     Value between 0.0 (old vector) and 1.0 (new vector). 0.9 is very near the new
        //     vector. 0.5 is halfway in between
        //
        // Returns:
        //     Возвращает новый вектор и изменяет текущий
        public Vector3D Lerp(double x, double y, double z, double amt)
        {
            return new Vector3D(X + (x - X) * amt, Y + (y - Y) * amt, Z + (z - Z) * amt);
        }

        //
        // Summary:
        //     Интерполяция вектора к другому вектору
        //
        // Parameters:
        //   v:
        //
        //   amt:
        //     Value between 0.0 (old vector) and 1.0 (new vector). 0.9 is very near the new
        //     vector. 0.5 is halfway in between
        public Vector3D Lerp(Vector3D v, double amt)
        {
            return new Vector3D(X + (v.X - X) * amt, Y + (v.Y - Y) * amt, Z + (v.Z - Z) * amt);
        }

        //
        // Summary:
        //     Интерполяция вектора к другому вектору
        //
        // Parameters:
        //   v1:
        //
        //   v2:
        //
        //   amt:
        //     Value between 0.0 (old vector) and 1.0 (new vector). 0.9 is very near the new
        //     vector. 0.5 is halfway in between
        //
        // Returns:
        //     Новый вектор
        public static Vector3D Lerp(Vector3D v1, Vector3D v2, double amt)
        {
            Vector3D vector3D = new Vector3D();
            vector3D = v1.CopyToVector();
            return vector3D.Lerp(v2, amt);
        }

        //
        // Summary:
        //     Вычисление угла между двумя векторами
        //
        // Parameters:
        //   v:
        //
        // Returns:
        //     Возвращает угол в радианах
        public double angleBetween(Vector3D v)
        {
            double val = Dot(v) / (Mag() * v.Mag());
            return Math.Acos(Math.Min(1.0, Math.Max(-1.0, val)));
        }

        //
        // Summary:
        //     Получение квадрата длины вектора
        //
        // Returns:
        //     Скаляр
        public double MagSq()
        {
            return X * X + Y * Y + Z * Z;
        }

        //
        // Summary:
        //     Ограничение (Limit) длины вектора до max значения
        //
        // Parameters:
        //   max:
        //     Требуемая максимальная длина вектора
        //
        // Returns:
        //     Вектор с лимитированной длиной
        public Vector3D Limit(double max)
        {
            double num = MagSq();
            if (num > max * max)
            {
                Div(Math.Sqrt(num)).Mult(max);
            }

            return CopyToVector();
        }

        //
        // Summary:
        //     Вычисление длины вектора
        //
        // Returns:
        //     Скаляр
        public double Mag()
        {
            return Math.Sqrt(MagSq());
        }

        //
        // Summary:
        //     Нормализация вектора
        //
        // Returns:
        //     Вектор единичной длины
        public Vector3D Normalize()
        {
            double num = Mag();
            if (num != 0.0)
            {
                Mult(1.0 / num);
            }

            return CopyToVector();
        }

        //
        // Summary:
        //     Задание длины вектора
        //
        // Parameters:
        //   n:
        //     целочисленная длина
        public void SetMag(int n)
        {
            Normalize().Mult(n);
        }

        //
        // Summary:
        //     Задание длины вектора
        //
        // Parameters:
        //   n:
        //     вещественная длина
        public void SetMag(double n)
        {
            Normalize().Mult(n);
        }

        //
        // Summary:
        //     Создание 3D вектора по паре углов
        //
        // Parameters:
        //   theta:
        //     the polar angle, in radians
        //
        //   phi:
        //     the azimuthal angle, in radians
        //
        //   len:
        //     Длина вектора. По умолчанию длина = 1
        //
        // Returns:
        //     Возвращает новый вектор
        public static Vector3D FromAngles(double theta, double phi, double len = 1.0)
        {
            double num = Math.Cos(phi);
            double num2 = Math.Sin(phi);
            double num3 = Math.Cos(theta);
            double num4 = Math.Sin(theta);
            return new Vector3D(len * num4 * num2, (0.0 - len) * num3, len * num4 * num);
        }

        //
        // Summary:
        //     Создание единичного 3D вектора по случайному углу 2PI
        //
        // Parameters:
        //   rnd:
        public static Vector3D Random3D(Random rnd)
        {
            double num = rnd.NextDouble() * Math.PI * 2.0;
            double num2 = rnd.NextDouble() * 2.0 - 1.0;
            double num3 = Math.Sqrt(1.0 - num2 * num2);
            double x = num3 * Math.Cos(num);
            double y = num3 * Math.Sin(num);
            return new Vector3D(x, y, num2);
        }

        //
        // Summary:
        //     Вычисление расстояния между векторами
        //
        // Parameters:
        //   v:
        //
        // Returns:
        //     Cкаляр
        public double Dist(Vector3D v)
        {
            return Sub(this, v).Mag();
        }

        //
        // Summary:
        //     Вычисление расстояния между двумя векторами
        //
        // Parameters:
        //   v1:
        //     Вектор 1
        //
        //   v2:
        //     Вектор 2
        //
        // Returns:
        //     Cкаляр
        public static double Dist(Vector3D v1, Vector3D v2)
        {
            return v1.Dist(v2);
        }

        //
        // Summary:
        //     Adds two vectors together
        //
        // Parameters:
        //   left:
        //     Вектор 1
        //
        //   right:
        //     Вектор 2
        //
        // Returns:
        //     Новый суммированный вектор
        public static Vector3D operator +(Vector3D left, Vector3D right)
        {
            return Add(left, right);
        }

        //
        // Summary:
        //     Изменение вектора на негативный
        //
        // Parameters:
        //   v:
        //     Вектор
        //
        // Returns:
        //     Новый отрицательный вектор
        public static Vector3D operator -(Vector3D v)
        {
            return v.Mult(-1.0);
        }

        //
        // Summary:
        //     Разность векторов
        //
        // Parameters:
        //   left:
        //     Вектор 1
        //
        //   right:
        //     Вектор 2
        //
        // Returns:
        //     Новый вектор
        public static Vector3D operator -(Vector3D left, Vector3D right)
        {
            return Sub(left, right);
        }

        //
        // Summary:
        //     Умножение скаляра на вектор
        //
        // Parameters:
        //   left:
        //     Скаляр
        //
        //   right:
        //     Вектор
        //
        // Returns:
        //     Новый вектор
        public static Vector3D operator *(double left, Vector3D right)
        {
            return Mult(right, left);
        }

        //
        // Summary:
        //     Умножение пар элементов обоих векторов
        //
        // Parameters:
        //   left:
        //     Вектор 1
        //
        //   right:
        //     Вектор 2
        //
        // Returns:
        //     Новый вектор
        public static Vector3D operator *(Vector3D left, Vector3D right)
        {
            return new Vector3D(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
        }

        //
        // Summary:
        //     Умножение вектора на скаляр
        //
        // Parameters:
        //   left:
        //     Вектор
        //
        //   right:
        //     Скаляр
        //
        // Returns:
        //     Новый вектор
        public static Vector3D operator *(Vector3D left, double right)
        {
            return Mult(left, right);
        }

        //
        // Summary:
        //     Деление первого вектора на второй вектор
        //
        // Parameters:
        //   left:
        //     Вектор 1
        //
        //   right:
        //     Вектор 2
        //
        // Returns:
        //     Новый вектор
        public static Vector3D operator /(Vector3D left, Vector3D right)
        {
            return Div(left, right);
        }

        //
        // Summary:
        //     Деление вектора на скаляр
        //
        // Parameters:
        //   value1:
        //     Вектор
        //
        //   value2:
        //     Скаляр
        //
        // Returns:
        //     Новый вектор
        public static Vector3D operator /(Vector3D value1, double value2)
        {
            return Div(value1, value2);
        }

        //
        // Summary:
        //     Returns a value that indicates whether each pair of elements in two specified
        //     vectors is equal.
        //
        // Parameters:
        //   left:
        //     Вектор 1
        //
        //   right:
        //     Вектор 2
        //
        // Returns:
        //     true if left and right are equal; otherwise, false.
        public static bool operator ==(Vector3D left, Vector3D right)
        {
            if (left.X == right.X && left.Y == right.Y && left.Z == right.Z)
            {
                return true;
            }

            return false;
        }

        //
        // Summary:
        //     Returns a value that indicates whether two specified vectors are not equal.
        //
        // Parameters:
        //   left:
        //     Вектор 1
        //
        //   right:
        //     Вектор 2
        //
        // Returns:
        //     true if left and right are not equal; otherwise, false.
        public static bool operator !=(Vector3D left, Vector3D right)
        {
            if (left.X != right.X || left.Y != right.Y || left.Z == right.Z)
            {
                return true;
            }

            return false;
        }
    }
}
