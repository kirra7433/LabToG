using System;
using System.Collections.Generic;

namespace IterationMethod
{
    public class IterationMethod
    {
        public double[,] Table { get; set; }

        public int M { get; set; }
        public int N { get; set; }
        public int K { get; set; }

        public double Eps { get; set; }

        private readonly List<int> _a;
        private readonly List<int> _b;
        private readonly List<double[]> _aSum;
        private readonly List<double[]> _bSum;
        private readonly List<double[]> _v; 

        public IterationMethod()
        {
            _a = new List<int>();
            _b = new List<int>();
            _v = new List<double[]>();
            _aSum = new List<double[]>();
            _bSum = new List<double[]>();
        }
        
        public IterationMethod(double[,] table):this()
        {
            Table = new double[table.GetLength(0),table.GetLength(1)];
            for (int i = 0; i < Table.GetLength(0); i++)
            {
                for (int j = 0; j < Table.GetLength(1); j++)
                {
                    Table[i, j] = table[i, j];
                }
            }
            M = Table.GetLength(0);
            N = Table.GetLength(1);
        }

        public IterationMethod(double[,] table, double min):this()
        {
            Table = new double[table.GetLength(0), table.GetLength(1)];
            for (int i = 0; i < Table.GetLength(0); i++)
            {
                for (int j = 0; j < Table.GetLength(1); j++)
                {
                    Table[i, j] = table[i, j]+min;
                }
            }
            M = Table.GetLength(0);
            N = Table.GetLength(1);
        }

        public List<double[]> GetResult()
        {
            Random random = new Random();
            _a.Add(random.Next(0,M-1));
            var tempB = new double[N];
            var min = double.MaxValue;
            var minIndex = 0;
            for (int j = 0; j < N; j++)
            {
                tempB[j] = Table[_a[_a.Count - 1], j];
                if (tempB[j]<min)
                {
                    min = tempB[j];
                    minIndex = j;
                }
            }
            _bSum.Add(tempB);
            _b.Add(minIndex);
            var tempA = new double[M];
            var max = double.MinValue;
            var maxIndex = 0;
            for (int i = 0; i < M; i++)
            {
                tempA[i] = Table[i, _b[_b.Count - 1]];
                if (tempA[i]>max)
                {
                    max = tempA[i];
                    maxIndex = i;
                }
            }
            _aSum.Add(tempA);
            _a.Add(maxIndex);
            var tempV = new double[3];
            tempV[0] = min;
            tempV[1] = max;
            tempV[2] = (tempV[0] + tempV[1])/2.0;
            _v.Add(tempV);
            for (int k = 1; k < K; k++)
            {
                tempB = new double[N];
                min = double.MaxValue;
                minIndex = 0;
                for (int j = 0; j < N; j++)
                {
                    tempB[j] = _bSum[_bSum.Count - 1][j] + Table[_a[_a.Count - 1], j];
                    if (tempB[j] < min)
                    {
                        min = tempB[j];
                        minIndex = j;
                    }
                }
                _bSum.Add(tempB);
                _b.Add(minIndex);
                tempA = new double[M];
                max = double.MinValue;
                maxIndex = 0;
                for (int i = 0; i < M; i++)
                {
                    tempA[i] = _aSum[_aSum.Count - 1][i] + Table[i, _b[_b.Count - 1]];
                    if (tempA[i] > max)
                    {
                        max = tempA[i];
                        maxIndex = i;
                    }
                }
                _aSum.Add(tempA);
                _a.Add(maxIndex);
                tempV = new double[3];
                tempV[0] = min/(k+1);
                tempV[1] = max/(k+1);
                tempV[2] = (tempV[0] + tempV[1]) / 2.0;
                _v.Add(tempV);
                if (Math.Abs(_v[_v.Count-1][2]-_v[_v.Count-2][2])>Eps)
                {
                    K++;
                }
            }
            _a.RemoveAt(_a.Count-1);
            var aCount = new double[M];
            var bCount = new double[N];
            for (int k = 0; k < K; k++)
            {
                aCount[_a[k]]++;
                bCount[_b[k]]++;
            }
            for (int i = 0; i < M; i++)
            {
                aCount[i] /= K;
            }
            for (int j = 0; j < N; j++)
            {
                bCount[j] /= K;
            }
            return new List<double[]> {aCount,bCount,_v[_v.Count-1]};
        }
    }
}