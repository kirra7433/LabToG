using System;

namespace Recursion
{
    public class StoGame
    {
        static double Norm(double[] v, double[] v1)
        {
            var max = double.MinValue;
            for (int i = 0; i < v.Length; i++)
            {
                if (Math.Abs(v[i]-v1[i])>max)
                {
                    max = Math.Abs(v[i] - v1[i]);
                }
            }
            return max;
        }

        public static double X1 { get; set; }
        public static double Y1 { get; set; }
        public static double X2 { get; set; }
        public static double Y2 { get; set; }

        public static double Sto(int n, int m)
        {
            double[] v1 = new double[n+m];
            double[] v = new double[n+m];
            do
            {
                Array.Copy(v, v1, v.Length);
                var tmp = n + m;
                for (int i = 0; i < n+m-1; i++)
                {
                    var v11 = 2.0 + v1[i + 1 > i + tmp-i - 1 ? i + tmp-i - 1 : i + 1];
                    var v12 = -1.0 + v1[i - 2 < 0 ? 0 : i - 2];
                    var v21 = -2.0 + v1[i - 3 < 0 ? 0 : i - 3];
                    var v22 = 3.0 + v1[i + 2 > i + tmp-i - 1 ? i + tmp-i - 1 : i +2];
                    v[i]= (v11 * v22 - v12 * v21) / (v11 + v22 - v12 - v21);
                    X1 = (v22 - v21) / (v11 + v22 - v12 - v21);
                    X2 = 1.0 - X1;
                    Y1 = (v[i] - v12) / (v11 - v12);
                    Y2 = 1.0 - Y1;
                }
            } while (Norm(v,v1)>10e-6);
            return v[n - 1 < 0 ? 0 : n -1];
        }
    }
}