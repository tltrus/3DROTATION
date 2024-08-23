using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;


namespace WpfApplication
{

    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer timer;
        DrawingVisual visual;
        DrawingContext dc;

        Cube cube;

        bool xRot_pos, yRot_pos, zRot_pos, xRot_neg, yRot_neg, zRot_neg;

        public MainWindow()
        {
            InitializeComponent();

            visual = new DrawingVisual();

            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 20);

            Init();
        }

        void Init()
        {
            cube = new Cube();
            Draw();

            timer.Start();
        }

        private void timerTick(object sender, EventArgs e)
        {
            Control();
            Draw();
        }

        void Control()
        {
            var vector = new Vector3D();
            var dir = 0.0;

            if (xRot_pos)
            {
                vector = new Vector3D(1, 0, 0);
                dir = 0.1;
            }

            if (xRot_neg)
            {
                vector = new Vector3D(1, 0, 0);
                dir = -0.1;
            }

            if (yRot_pos)
            {
                vector = new Vector3D(0, 1, 0);
                dir = 0.1;
            }

            if (yRot_neg)
            {
                vector = new Vector3D(0, 1, 0);
                dir = -0.1;
            }

            if (zRot_pos)
            {
                vector = new Vector3D(0, 0, 1);
                dir = 0.1;
            }

            if (zRot_neg)
            {
                vector = new Vector3D(0, 0, 1);
                dir = -0.1;
            }

            Quaternion q = Quaternion.FromAngle(vector, dir);
            cube.Rotate(q);
        }

        void Draw()
        {
            List<double[]> projected = new List<double[]>();

            for (int i = 0; i < cube.points.Count; ++i)
            {
                double[] point = new double[] // вектор
                {
                    cube.points[i].X,
                    cube.points[i].Y,
                    cube.points[i].Z
                };

                // Перспектива
                double distance = 2;
                double z = 1 / (distance - cube.points[i].Z);

                double[,] projection = new double[,]
                {
                    {z, 0, 0},
                    {0, z, 0},
                    {0, 0, 1}
                };

                var temp = Matrix2D.Mult(point, projection);
                var projected2D = new double[] { temp[0] * 100, temp[1] * 100};
                projected.Add(projected2D);
            }


            g.RemoveVisual(visual);

            using (dc = visual.RenderOpen())
            {
                foreach (var p in projected)
                {
                    Point point = new Point(p[0] + g.Width/2, p[1] + Height/3);
                    dc.DrawEllipse(Brushes.White, null, point, 3, 3);
                }

                // Connecting
                for (int i = 0; i < 4; i++)
                {
                    Connect(i, (i + 1) % 4, projected, dc);
                    Connect(i + 4, ((i + 1) % 4) + 4, projected, dc);
                    Connect(i, i + 4, projected, dc);
                }

                dc.Close();
                g.AddVisual(visual);
            }
        }

        void Connect(int i, int j, List<double[]> points, DrawingContext dc)
        {
            var a = points[i];
            var b = points[j];
            Point p0 = new Point(a[0] + g.Width / 2, a[1] + Height / 3);
            Point p1 = new Point(b[0] + g.Width / 2, b[1] + Height / 3);
            dc.DrawLine(new Pen(Brushes.White, 1), p0, p1);
        }


        // ==========================================================================
        // MOVE CUBE
        // ==========================================================================

        private void btnCubeLeft_Click(object sender, RoutedEventArgs e)
        {
            cube.Move(new Vector3D(-0.1, 0, 0));
            Draw();
        }

        private void btnCubeRight_Click(object sender, RoutedEventArgs e)
        {
            cube.Move(new Vector3D(0.1, 0, 0));
            Draw();
        }

        private void btnCubeUp_Click(object sender, RoutedEventArgs e)
        {
            cube.Move(new Vector3D(0, -0.1, 0));
            Draw();
        }



        private void btnCubeDown_Click(object sender, RoutedEventArgs e)
        {
            cube.Move(new Vector3D(0, 0.1, 0));
            Draw();
        }

        private void btnCubeIn_Click(object sender, RoutedEventArgs e)
        {
            cube.Move(new Vector3D(0, 0, -0.1));
            Draw();
        }

        private void btnCubeOut_Click(object sender, RoutedEventArgs e)
        {
            cube.Move(new Vector3D(0, 0, 0.1));
            Draw();
        }


        // ==========================================================================
        // ROTATE CUBE
        // ==========================================================================

        private void btnCubeYpos_Click(object sender, RoutedEventArgs e)
        {
            Quaternion q = Quaternion.FromAngle(new Vector3D(0, 1, 0), 0.1);
            cube.Rotate(q);
            Draw();
        }

        private void btnCubeYneg_Click(object sender, RoutedEventArgs e)
        {
            Quaternion q = Quaternion.FromAngle(new Vector3D(0, 1, 0), -0.1);
            cube.Rotate(q);
            Draw();
        }

        private void btnCubeZpos_Click(object sender, RoutedEventArgs e)
        {
            Quaternion q = Quaternion.FromAngle(new Vector3D(0, 0, 1), 0.1);
            cube.Rotate(q);
            Draw();
        }

        private void btnCubeZneg_Click(object sender, RoutedEventArgs e)
        {
            Quaternion q = Quaternion.FromAngle(new Vector3D(0, 0, 1), -0.1);
            cube.Rotate(q);
            Draw();
        }

        private void Button_Click(object sender, RoutedEventArgs e) => Init();

        private void btnCubeXpos_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => xRot_pos = true;
        private void btnCubeXpos_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e) => xRot_pos = false;

        private void btnCubeXneg_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => xRot_neg = true;
        private void btnCubeXneg_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e) => xRot_neg = false;


        private void btnCubeYpos_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => yRot_pos = true;
        private void btnCubeYpos_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e) => yRot_pos = false;

        private void btnCubeYneg_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => yRot_neg = true;
        private void btnCubeYneg_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e) => yRot_neg = false;

        private void btnCubeZpos_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => zRot_pos = true;
        private void btnCubeZpos_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e) => zRot_pos = false;

        private void btnCubeZneg_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => zRot_neg = true;
        private void btnCubeZneg_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e) => zRot_neg = false;
    }
}
