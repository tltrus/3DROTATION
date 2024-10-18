using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace WpfApplication46
{
    public partial class MainWindow : Window
    {
        DrawingVisual visual;
        DrawingContext dc;
        Model model;

        float yaw = 0;
        float pitch = 0;
        float roll = 0;
        float scale = 10;
        Vector3 position;
        PointCollection points;
        List<Vector3> vertexes;
        bool isFilled;
        Brush brush;
        Pen pen;

        public MainWindow()
        {
            InitializeComponent();

            visual = new DrawingVisual();

            Init();
            
            Draw();
        }

        void Init()
        {
            points = new PointCollection();

            model = new Model();
            model.LoadFromObj(new StreamReader("obj.obj"));

            position = new Vector3((float)g.Width/2, (float)g.Height/2, 200);

            scale = (float)slSize.Value;
            pitch = DegToRad(slPitch.Value);
            roll = DegToRad(slRoll.Value);
            yaw = DegToRad(slYaw.Value);
        }

        void Calculation()
        {
            //матрица масштабирования
            var scaleM = Matrix4x4.CreateScale(scale / 2);
            //матрица вращения
            var rotateM = Matrix4x4.CreateFromYawPitchRoll(yaw, pitch, roll);
            //матрица переноса
            var translateM = Matrix4x4.CreateTranslation(position);
            //матрица проекции
            var paneXY = new Matrix4x4() { M11 = 1f, M22 = 1f, M44 = 1f };
            //результирующая матрица
            var m = scaleM * rotateM * translateM * paneXY;

            //умножаем вектора на матрицу
            vertexes = model.Vertexes.Select(v => Vector3.Transform(v, m)).ToList();
        }

        void Draw()
        {
            Calculation();

            g.RemoveVisual(visual);

            using (dc = visual.RenderOpen())
            {
                for (int j = 0; j < model.Figures.Count; ++j)
                {
                    PathGeometry geo = new PathGeometry();
                    PathFigure fig = new PathFigure();
                    fig.StartPoint = new Point(vertexes[model.Figures[j][0]].X, vertexes[model.Figures[j][0]].Y);

                    for (int i = 1; i < model.Figures[j].Count; ++i)
                    {
                        var v = vertexes[model.Figures[j][i]];
                        
                        LineSegment lseg = new LineSegment(new Point(v.X, v.Y), true);
                        fig.Segments.Add(lseg);
                    }
                    geo.Figures.Add(fig);

                    if (isFilled)
                    {
                        brush = Brushes.LimeGreen;
                        pen = null;
                    }
                    else
                    {
                        brush = null;
                        pen = new Pen(Brushes.White, 0.5);
                    }
                    dc.DrawGeometry(brush, pen, geo);
                }

                dc.Close();
                g.AddVisual(visual);
            }
        }

        float DegToRad(double deg) => (float)Math.Round(deg * Math.PI / 180, 1);
        float RadToDeg(double rad) => (float)Math.Round(rad / Math.PI * 180, 0);

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (slPitch == null || slRoll == null || slYaw == null || model == null) return;

            lbSize.Content = scale = (float)Math.Round(slSize.Value, 0);
            pitch = DegToRad(slPitch.Value);
            roll = DegToRad(slRoll.Value);
            yaw = DegToRad(slYaw.Value);

            lbPitch.Content = RadToDeg(pitch);
            lbRoll.Content = RadToDeg(roll);
            lbYaw.Content = RadToDeg(yaw);

            Draw();
        }

        private void rb_Click(object sender, RoutedEventArgs e)
        {
            var name = ((RadioButton)sender).Name;

            if (name == "rbFill") isFilled = true;
            if (name == "rbLine") isFilled = false;

            Draw();
        }
    }
}
