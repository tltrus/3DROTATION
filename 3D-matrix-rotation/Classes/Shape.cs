using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace WpfApp
{
    // Координаты вершины
    class Vertex
    {
        public double x, y, z;
    }

    // Индексы соединяемых вершин
    class Edge
    {
        public int src, dest;
    }

    class Shape
    {
        public List<Vertex> vertices;    // Вершины
        public List<Edge> edges;         // Ребра
        public double xc, yc, zc = 200;  // Координаты центра объекта
        private int width, height, screen_depth;

        public Shape(int width, int height, int screen_depth)
        {
            vertices = new List<Vertex>();
            edges = new List<Edge>();

            this.width = width;
            this.height = height;
            this.screen_depth = screen_depth;
        }

        public void AddVertex(double x, double y, double z)
        {
            Vertex vertex = new Vertex();
            vertex.x = x;
            vertex.y = y;
            vertex.z = z;
            vertices.Add(vertex);
        }

        public void AddEdge(int src, int dest)
        {
            Edge edge = new Edge();
            edge.src = src;
            edge.dest = dest;
            edges.Add(edge);
        }

        public void Draw(WriteableBitmap wb)
        {
            double x = 0, y = 0, z = 0;
            int x1 = 0, x2 = 0, y1 = 0, y2 = 0;

            // Shape
            for (int i = 0; i < edges.Count; i++)
            {
                // Координаты первой точки ребра
                x = vertices[edges[i].src].x + xc;
                y = vertices[edges[i].src].y + yc;
                z = vertices[edges[i].src].z + zc;

                if (Math.Abs(z) < 0.01) z = 0.01; // иззбегаем деления на 0

                // Вычисляем экранные координаты
                x1 = (int)(width / 2 + screen_depth * x / z);
                y1 = (int)(height / 2 + screen_depth * y / z);

                // Координаты второй точки ребра
                x = vertices[edges[i].dest].x + xc;
                y = vertices[edges[i].dest].y + yc;
                z = vertices[edges[i].dest].z + zc;

                if (Math.Abs(z) < 0.01) z = 0.01; // иззбегаем деления на 0

                // Вычисляем экранные координаты
                x2 = (int)(width / 2 + screen_depth * x / z);
                y2 = (int)(height / 2 + screen_depth * y / z);

                wb.DrawLine(x1, y1, x2, y2, Colors.White);
            }
        }
    }
}
