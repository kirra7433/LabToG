using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experiment
{
    class Exp
    {
        private double[] _p;
        private double[,] _experiments;
        private double[,] _table;
        public int[,] d;

        public Exp(double[,] table, double[] p, double[,] experiments)
        {
            _p =new double[p.Length];
            Array.Copy(p,_p,p.Length);
            _experiments = new double[experiments.GetLength(0),experiments.GetLength(1)];
            Array.Copy(experiments,_experiments,experiments.Length);
            _table = new double[experiments.GetLength(0), experiments.GetLength(1)];
            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    _table[i, j] = -table[i, j];
                }
            }
        }

        public double[,] GetR(int m, int x)
        {
            d = new int[x, (int) Math.Pow(m, x)];
            for (int j = 0; j < d.GetLength(1); j++)
            {
                for (int i = 0; i < x; i++)
                {
                    d[i, j] = (j/(int) Math.Pow(m, x - i - 1))%m;
                }
            }
            var r = new double[_table.GetLength(1),d.GetLength(1)];
            for (int i = 0; i < r.GetLength(0); i++)
            {
                for (int j = 0; j < r.GetLength(1); j++)
                {
                    double sum = 0.0;
                    for (int k = 0; k < x; k++)
                    {
                        sum += _table[d[k, j], i]*_experiments[k,i];
                    }
                    r[i, j] = sum;
                }
            }
            return r;
        }

        public double[] GetB(double[,] r)
        {
            double[] b = new double[r.GetLength(1)];
            for (int j = 0; j < r.GetLength(1); j++)
            {
                for (int i = 0; i < r.GetLength(0); i++)
                {
                    b[j] += _p[i]*r[i, j];
                }
            }
            return b;
        }
    }
}
