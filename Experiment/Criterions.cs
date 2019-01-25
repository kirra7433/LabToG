using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experiment
{
    public struct CritWin
    {
        public int index;
        public double win;
    }
    class Criterions
    {
        public static CritWin Bayes(double[,] table, double[] p)
        {
            double maxSum = Double.MinValue;
            int maxIndex = 0;
            for (int i = 0; i < table.GetLength(0); i++)
            {
                double sum = 0;
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    sum += p[j]*table[i, j];
                }
                if (sum > maxSum)
                {
                    maxIndex = i;
                    maxSum = sum;
                }
            }
            return new CritWin { index = maxIndex+1, win = maxSum};
        }

        public static CritWin Wald(double[,] table)
        {
            double max = double.MinValue;
            int maxindex = 0;
            for (int i = 0; i < table.GetLength(0); i++)
            {
                double min = Double.MaxValue;
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    if (table[i,j]<min)
                    {
                        min = table[i, j];
                    }
                }
                if (min>max)
                {
                    max = min;
                    maxindex = i;
                }
            }
            return new CritWin { index = maxindex+1, win = max };
        }

        public static CritWin Savage(double[,] table)
        {
            double [,] r = new double[table.GetLength(0),table.GetLength(1)];
            for (int j = 0; j < r.GetLength(1); j++)
            {
                double max = Double.MinValue;
                for (int i = 0; i < r.GetLength(0); i++)
                {
                    if (table[i,j]>max)
                    {
                        max = table[i, j];
                    }
                }
                for (int i = 0; i < r.GetLength(0); i++)
                {
                    r[i, j] = max - table[i, j];
                }
            }
            int minIndex = 0;
            double min = Double.MaxValue;
            for (int i = 0; i < r.GetLength(0); i++)
            {
                double max = Double.MinValue;
                for (int j = 0; j < r.GetLength(1); j++)
                {
                    if (r[i,j]>max)
                    {
                        max = r[i, j];
                    }
                }
                if (max<min)
                {
                    min = max;
                    minIndex = i;
                }
            }
            return new CritWin { index = minIndex+1, win = minIndex };
        }

        public static CritWin Hurwitz(double[,] table, double lambda)
        {
            double maxSum=Double.MinValue;
            int maxIndex=0;
            for (int i = 0; i < table.GetLength(0); i++)
            {
                double min = Double.MaxValue;
                double max = double.MinValue;
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    if (table[i, j] < min)
                    {
                        min = table[i, j];
                    }
                    if (table[i,j]>max)
                    {
                        max = table[i, j];
                    }
                }
                if (lambda*min+(1-lambda)*max>maxSum)
                {
                    maxSum = lambda*min + (1 - lambda)*max;
                    maxIndex = i;
                }
            }
            return new CritWin { index = maxIndex+1, win = maxSum };
        }
    }
}
