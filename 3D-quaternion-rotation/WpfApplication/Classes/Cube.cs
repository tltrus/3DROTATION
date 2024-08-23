using System.Collections.Generic;



namespace WpfApplication
{
    class Cube
    {
        public List<Vector3D> points = new List<Vector3D>();

        public Cube()
        {
            points.Add(new Vector3D(-0.4, -0.4, -0.4));
            points.Add(new Vector3D(0.4, -0.4, -0.4));
            points.Add(new Vector3D(0.4, 0.4, -0.4));
            points.Add(new Vector3D(-0.4, 0.4, -0.4));
            points.Add(new Vector3D(-0.4, -0.4, 0.4));
            points.Add(new Vector3D(0.4, -0.4, 0.4));
            points.Add(new Vector3D(0.4, 0.4, 0.4));
            points.Add(new Vector3D(-0.4, 0.4, 0.4));
        }

        public void Move(Vector3D v) => points.ForEach(p => p.Add(v));

        public void Rotate(Quaternion q)
        {
            // rotate
            for (int i = 0; i < points.Count; ++i)
            {
                q.Rotate(points[i]);
            }
        }
    }
}
