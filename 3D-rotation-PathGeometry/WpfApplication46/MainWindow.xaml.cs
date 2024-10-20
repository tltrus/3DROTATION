using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;


namespace WpfApplication46
{
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer timer;
        Random rnd = new Random();
        DrawingVisual visual;
        DrawingContext dc;

        double angle;

        List<Vector3D> points = new List<Vector3D>();
        List<int[]> surfaces;
        List<Brush> brushes;

        public MainWindow()
        {
            InitializeComponent();

            visual = new DrawingVisual();

            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 40);

            points.Add(new Vector3D(-0.4, -0.4, -0.4 ));
            points.Add(new Vector3D( 0.4, -0.4, -0.4 ));
            points.Add(new Vector3D( 0.4, 0.4, -0.4 ));
            points.Add(new Vector3D( -0.4, 0.4, -0.4 ));
            points.Add(new Vector3D( -0.4, -0.4, 0.4 ));
            points.Add(new Vector3D( 0.4, -0.4, 0.4 ));
            points.Add(new Vector3D( 0.4, 0.4, 0.4 ));
            points.Add(new Vector3D( -0.4, 0.4, 0.4 ));

            // индексы вершин для 6-ти сторон куба
            //
            //   4         5
            //      0   1
            //      3   2
            //   7         6 

            surfaces = new List<int[]>()
            {
                new int[]{0, 1, 2, 3},
                new int[]{0, 3, 7, 4},
                new int[]{1, 2, 6, 5},
                new int[]{0, 1, 5, 4},
                new int[]{3, 2, 6, 7},
                new int[]{4, 5, 6, 7}
            };

            brushes = new List<Brush>() { Brushes.Green, Brushes.Red, Brushes.Yellow, Brushes.Pink, Brushes.Blue, Brushes.Gray};

            timer.Start();
        }

        private void timerTick(object sender, EventArgs e) => Draw();

        void Draw()
        {
            double[,] rotationZ = new double[,]
            {
                {Cos(angle),    -Sin(angle),    0 },
                {Sin(angle),    Cos(angle),     0 },
                {0,             0,              1 }
            };

            double[,] rotationX = new double[,]
            {
                {1,     0,              0},
                {0,     Cos(angle),     -Sin(angle)},
                {0,     Sin(angle),     Cos(angle)}
            };

            double[,] rotationY = new double[,]
            {
                {Cos(angle),    0,      Sin(angle)},
                {0,             1,      0},
                {-Sin(angle),   0,      Cos(angle)}
            };

            List<double[]> projected = new List<double[]>();

            for (int i = 0; i < points.Count; ++i)
            {
                double[,] point = new double[,] // вектор-столбец
                {
                    {points[i].X},
                    {points[i].Y},
                    {points[i].Z}
                };

                var rotated = Matrix.Mult(rotationY, point);
                rotated = Matrix.Mult(rotationX, rotated);
                rotated = Matrix.Mult(rotationZ, rotated);

                // Перспектива
                double distance = 2;
                double z = 1 / (distance - rotated[2, 0]); // rotated[2, 0] - это Z
                
                double[,] projection = new double[,]
                {
                    {z, 0, 0},
                    {0, z, 0}
                };

                var temp = Matrix.Mult(projection, rotated);
                var projected2D = new double[] { temp[0,0] * 200, temp[1,0] * 200};
                projected.Add(projected2D);
            }


            g.RemoveVisual(visual);

            using (dc = visual.RenderOpen())
            {
                for (int i = 0; i < surfaces.Count; ++i)
                {
                    PathGeometry geo = new PathGeometry();
                    geo.FillRule = FillRule.Nonzero;
                    PathFigure fig = new PathFigure();
                    fig.IsFilled = true;
                    
                    fig.StartPoint = new Point(projected[surfaces[i][0]][0] + g.Width * 0.5, projected[surfaces[i][0]][1] + Height * 0.5);

                    for (int j = 1; j < surfaces[i].Length; ++j)
                    {
                        var v = projected[surfaces[i][j]];

                        LineSegment lseg = new LineSegment(new Point(v[0] + g.Width * 0.5, v[1] + Height * 0.5), true);
                        fig.Segments.Add(lseg);
                    }
                    geo.Figures.Add(fig);

                    var brush = Brushes.LimeGreen;
                    var pen = new Pen(Brushes.White, 0.5);

                    dc.DrawGeometry(brushes[i], null, geo);
                }

                dc.Close();
                g.AddVisual(visual);
            }

            angle += 0.03;
        }

        double Sin(double a) => Math.Sin(a);
        double Cos(double a) => Math.Cos(a);
    }
}
