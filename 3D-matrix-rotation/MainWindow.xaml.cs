using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace WpfApp
{
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer timer;
        WriteableBitmap wb;
        static int imgWidth, imgHeight;

        Shape Shape;
        Matrix matrix;
        Axis axis;
        double X, Y, Z;
        int N = 150; // Глубина экрана

        bool xRot_pos, yRot_pos, zRot_pos, xRot_neg, yRot_neg, zRot_neg;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            imgWidth = (int)image.Width; imgHeight = (int)image.Height;

            wb = new WriteableBitmap(imgWidth, imgHeight, 96, 96, PixelFormats.Bgra32, null); image.Source = wb;

            Init();
            Draw();
        }

        void Init()
        {
            Shape = new Shape(imgWidth, imgHeight, N);
            //Shape.AddVertex(-100, -100, 0);
            //Shape.AddVertex(-100, 100, 0);
            //Shape.AddVertex(100, 100, 0);
            //Shape.AddVertex(100, -100, 0);
            //Shape.AddVertex(0, 0, -141.42);
            //Shape.AddVertex(0, 0, 141.42);

            //Shape.AddEdge(0, 1);
            //Shape.AddEdge(1, 2);
            //Shape.AddEdge(2, 3);
            //Shape.AddEdge(3, 0);
            //Shape.AddEdge(4, 0);
            //Shape.AddEdge(4, 1);
            //Shape.AddEdge(4, 2);
            //Shape.AddEdge(4, 3);
            //Shape.AddEdge(5, 0);
            //Shape.AddEdge(5, 1);
            //Shape.AddEdge(5, 2);
            //Shape.AddEdge(5, 3);

            Shape.AddVertex(-100, -100, 100);
            Shape.AddVertex(-100, 100, 100);
            Shape.AddVertex(100, 100, 100);
            Shape.AddVertex(100, -100, 100);

            Shape.AddVertex(-100, -100, -100);
            Shape.AddVertex(-100, 100, -100);
            Shape.AddVertex(100, 100, -100);
            Shape.AddVertex(100, -100, -100);

            Shape.AddEdge(0, 1);
            Shape.AddEdge(1, 2);
            Shape.AddEdge(2, 3);
            Shape.AddEdge(3, 0);

            Shape.AddEdge(0, 4);
            Shape.AddEdge(1, 5);
            Shape.AddEdge(2, 6);
            Shape.AddEdge(3, 7);

            Shape.AddEdge(4, 5);
            Shape.AddEdge(5, 6);
            Shape.AddEdge(6, 7);
            Shape.AddEdge(7, 4);

            matrix = new Matrix();
            matrix.RotateShape(Shape, 0, 0, 0);

            axis = new Axis(imgWidth, imgHeight, N);

            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 20);

            timer.Start();
        }

        private void timerTick(object sender, EventArgs e)
        {
            Control();
            Draw();
        }

        void Control()
        {
            if (xRot_pos)
            {
                RotationControl(X + 1, Y, Z);
            }

            if (xRot_neg)
            {
                RotationControl(X - 1, Y, Z);
            }

            if (yRot_pos)
            {
                RotationControl(X, Y + 1, Z);
            }

            if (yRot_neg)
            {
                RotationControl(X, Y - 1, Z);
            }

            if (zRot_pos)
            {
                RotationControl(X, Y, Z + 1);
            }

            if (zRot_neg)
            {
                RotationControl(X, Y, Z - 1);
            }
        }

        void Draw()
        {
            // Shape
            Shape.Draw(wb);

            // axis
            axis.Draw(wb);
        }

        private double ToRadians(double deg) => deg * Math.PI / 180;
        private void RotationControl(double inpX, double inpY, double inpZ)
        {
            double Xoffs = inpX - X;
            X = inpX;

            double Yoffs = inpY - Y;
            Y = inpY;

            double Zoffs = inpZ - Z;
            Z = inpZ;

            Xoffs = ToRadians(Xoffs);
            Yoffs = ToRadians(Yoffs);
            Zoffs = ToRadians(Zoffs);

            wb.Clear(Colors.Black);
            matrix.RotateShape(Shape, Xoffs, Yoffs, Zoffs);
            matrix.RotateAxis(axis.verticesX, Xoffs, Yoffs, Zoffs);   // поворачиваем ось Х координат
            matrix.RotateAxis(axis.verticesY, Xoffs, Yoffs, Zoffs);   // поворачиваем ось Y координат
            matrix.RotateAxis(axis.verticesZ, Xoffs, Yoffs, Zoffs);   // поворачиваем ось Z координат
            Draw();

            lbXposDeg.Content = X.ToString();
            lbYposDeg.Content = Y.ToString();
            lbZposDeg.Content = Z.ToString();

            lbXposRad.Content = Math.Round(ToRadians(X), 2).ToString();
            lbYposRad.Content = Math.Round(ToRadians(Y), 2).ToString();
            lbZposRad.Content = Math.Round(ToRadians(Z), 2).ToString();
        }

        private void btnXplus_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => xRot_pos = true;
        private void btnXplus_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e) => xRot_pos = false;

        private void btnXminus_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => xRot_neg = true;
        private void btnXminus_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e) => xRot_neg = false;


        private void btnYplus_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => yRot_pos = true;
        private void btnYplus_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e) => yRot_pos = false;

        private void btnYminus_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => yRot_neg = true;
        private void btnYminus_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e) => yRot_neg = false;

        private void btnZplus_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => zRot_pos = true;
        private void btnZplus_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e) => zRot_pos = false;

        private void btnZminus_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => zRot_neg = true;
        private void btnZminus_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e) => zRot_neg = false;

        private void BtnRotate_Click(object sender, RoutedEventArgs e) => RotationControl(int.Parse(tbX.Text), int.Parse(tbY.Text), int.Parse(tbZ.Text));
    }
}
