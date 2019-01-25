using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recursion
{
    public static class RecGame
    {
        public static double X1 { get; set; }
        public static double X2 { get; set; }
        public static double Y1 { get; set; }
        public static double Y2 { get; set; }
        static public Dictionary<int,Dictionary<int,double>> Cash { get; set; }

        public static double Rec(int w, int m)
        {
            Cash = new Dictionary<int, Dictionary<int, double>>();
            if (Cash.ContainsKey(w))
            {
                if (Cash[w].ContainsKey(m))
                {
                    return Cash[w][m];
                }
            }
            if (m <= 0 || w <= 0)
            {
                return 0.0;
            }
            var v11 = Rec(w + 1, m - 2);
            var v12 = Rec(w - 2, m + 1);
            var v21 = Rec(w - 3, m + 2);
            var v22 = Rec(w + 2, m - 3);
            X1 = (v22 - v21) / (v11 + v22 - v12 - v21);
            X2 = 1.0 - X1;
            var v = (v11 * v22 - v12 * v21) / (v11 + v22 - v12 - v21);
            Y1 = (v - v12) / (v11 - v12);
            Y2 = 1.0 - Y1;
            if (!Cash.ContainsKey(w))
            {
                Cash[w] = new Dictionary<int, double> { [m] = v };
            }
            return v;
        }
    }
}
