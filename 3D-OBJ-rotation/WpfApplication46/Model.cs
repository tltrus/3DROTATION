using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Numerics;


namespace WpfApplication46
{
    internal class Model
    {
        public List<Vector3> Vertexes = new List<Vector3>();
        public List<List<int>> Figures = new List<List<int>>();

        public void LoadFromObj(TextReader tr)
        {
            string line;
            Vertexes.Clear();
            Vertexes.Add(new Vector3());

            while ((line = tr.ReadLine()) != null)
            {
                var line_ = line.Trim();
                var parts = line_.Split(' ');
                if (parts.Length == 0) continue;
                switch (parts[0])
                {
                    case "v":
                        Vertexes.Add(new Vector3(float.Parse(parts[1], CultureInfo.InvariantCulture),
                                      float.Parse(parts[2], CultureInfo.InvariantCulture),
                                      float.Parse(parts[3], CultureInfo.InvariantCulture)));
                        break;
                    case "f":
                        List<int> f = new List<int>();

                        for (int i = 1; i < parts.Length; i++)
                            f.Add(int.Parse(parts[i].Split('/')[0]));

                        Figures.Add(f);
                        break;
                }
            }
        }
    }
}
