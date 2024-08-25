using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace WpfApp
{
    class Axis
    {
        public List<Vertex> verticesX = new List<Vertex>();
        public List<Vertex> verticesY = new List<Vertex>();
        public List<Vertex> verticesZ = new List<Vertex>();
        private int width, height, screen_depth;

        public Axis(int width, int height, int screen_depth)
        {
            this.width = width;
            this.height = height;
            this.screen_depth = screen_depth;
            
            verticesX.Add(new Vertex() { x = 0, y = 0, z = 0 });
            verticesX.Add(new Vertex() { x = 150, y = 0, z = 0 });

            verticesY.Add(new Vertex() { x = 0, y = 0, z = 0 });
            verticesY.Add(new Vertex() { x = 0, y = 150, z = 0 });

            verticesZ.Add(new Vertex() { x = 0, y = 0, z = 0  });
            verticesZ.Add(new Vertex() { x = 0, y = 0, z = 150 });
        }

        public void Draw(WriteableBitmap wb)
        {
            DrawAxis(wb, verticesX, Colors.Red);   // X
            DrawAxis(wb, verticesY, Colors.Green); // Y
            DrawAxis(wb, verticesZ, Colors.Blue);  // Z
        }

        private void DrawAxis(WriteableBitmap wb, List<Vertex> axis, Color color)
        {
            double x, y, z;
            int x1, x2, y1, y2;

            for (int i = 0; i < axis.Count - 1; i++)
            {
                x = axis[i].x;
                y = axis[i].y;
                z = axis[i].z + screen_depth;

                if (Math.Abs(z) < 0.01) z = 0.01; // иззбегаем деления на 0

                // Вычисляем экранные координаты
                x1 = (int)(width / 2 + screen_depth * x / z);
                y1 = (int)(height / 2 + screen_depth * y / z);

                x = axis[i + 1].x;
                y = axis[i + 1].y;
                z = axis[i + 1].z + screen_depth;

                if (Math.Abs(z) < 0.01) z = 0.01; // иззбегаем деления на 0

                // Вычисляем экранные координаты
                x2 = (int)(width / 2 + screen_depth * x / z);
                y2 = (int)(height / 2 + screen_depth * y / z);

                wb.DrawLine(x1, y1, x2, y2, color);
            }
        }
    }
}
