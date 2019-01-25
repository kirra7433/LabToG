using System;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using IterationMethod;

namespace Experiment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double[,] _table;
        private double _min = double.MaxValue;
        private double[] _p;
        private double[,] _experiments;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtFile_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                TbFile.Text = ofd.FileName;
                try
                {
                    var sr = new StreamReader(ofd.OpenFile());
                    var tmp = sr.ReadLine().Split(new[] { " ", "\t" }, StringSplitOptions.None);
                    int m = int.Parse(tmp[0]);
                    int n = int.Parse(tmp[1]);
                    _table = new double[m, n];
                    for (int i = 0; i < m; i++)
                    {
                        var temp = sr.ReadLine().Split(new[] { " ", "\t" }, StringSplitOptions.None);
                        for (int j = 0; j < n; j++)
                        {
                            _table[i, j] = double.Parse(temp[j]);
                            //if (_table[i, j] < _min)
                            //{
                            //    _min = _table[i, j];
                            //}
                        }
                    }
                    _p = sr.ReadLine().Split(new[] { " ", "\t" }, StringSplitOptions.None).Select(double.Parse).ToArray();
                    int x = int.Parse(sr.ReadLine());
                    _experiments = new double[x, n];
                    for (int i = 0; i < x; i++)
                    {
                        var temp = sr.ReadLine().Split(new[] { " ", "\t" }, StringSplitOptions.None);
                        for (int j = 0; j < n; j++)
                        {
                            _experiments[i, j] = double.Parse(temp[j]);
                        }
                    }
                    
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                    return;
                }
                BtStart.IsEnabled = true;
                TbResult.Text = "";
            }
        }

        private void BtStart_OnClick(object sender, RoutedEventArgs e)
        {
            double lambda;
            if (!string.IsNullOrEmpty(TbLambda.Text))
            {
                lambda = double.Parse(TbLambda.Text);
            }
            else
            {
                MessageBox.Show("Введите λ");
                return;
            }
            TbResult.Text += $"Критерий Байеса: {Criterions.Bayes(_table, _p).index} Выигрыш {Criterions.Bayes(_table, _p).win}\r\n";
            TbResult.Text += $"Критерий Вальда: {Criterions.Wald(_table).index} Выигрыш {Criterions.Wald(_table).win} \r\n";
            TbResult.Text += $"Критерий Сэвиджа: {Criterions.Savage(AT(_table)).index} Выигрыш {Criterions.Savage(AT(_table)).win} \r\n";
            TbResult.Text += $"Критерий Гурвица: {Criterions.Hurwitz(_table,lambda).index} Выигрыш {Criterions.Hurwitz(_table, lambda).win} \r\n";
            var sm = new SimplexMethod(_table);
            TbResult.Text += $"Цена игры: {sm.GamePriceA}\r\n";
            TbResult.Text += $"Стратегия игрока A: {string.Join("\t", sm.ResultsA.Select(x => (x * sm.GamePriceA).ToString("0.#####")))}\r\n";
            BtStartEx.IsEnabled = true;
        }

        private void BtStartEx_OnClick(object sender, RoutedEventArgs e)
        {
            TbResult.Text += "\r\n";
            Exp exp = new Exp(_table, _p, _experiments);
            var r = exp.GetR(_table.GetLength(0), _experiments.GetLength(1));
            for (int i = 0; i < r.GetLength(1); i++)
            {
                TbResult.Text += i + "\t";
            }
            TbResult.Text += "\r\n";
            for (int i = 0; i < r.GetLength(0); i++)
            {
                for (int j = 0; j < r.GetLength(1); j++)
                {
                    TbResult.Text += r[i, j] + "\t";
                }
                TbResult.Text += "\r\n";
            }
            TbResult.Text += "\r\n";
            for (int i = 0; i < r.GetLength(0); i++)
            {
                for (int j = 0; j < r.GetLength(1); j++)
                {
                    TbResult.Text += r[i, j] + "\t";
                }
                TbResult.Text += "\r\n";
            }
            var b = exp.GetB(r);
            var bMin = double.MaxValue;
            int bIndex = 0;
            for (int i = 0; i < b.Length; i++)
            {
                if (b[i]<bMin)
                {
                    bMin = b[i];
                    bIndex = i;
                }
            }
            for (int j = 0; j < 256; j++)
            {
                TbResult.Text += $"d{j}: {{";
                for (int i = 0; i < exp.d.GetLength(0); i++)
                {
                    TbResult.Text += exp.d[i, j] + " ";
                }
                TbResult.Text += "}\t\t";
            }
            TbResult.Text += "\r\n";
            TbResult.Text += $"Min: {bMin}, d{bIndex}: {{";
            for (int i = 0; i < exp.d.GetLength(0); i++)
            {
                TbResult.Text += exp.d[i, bIndex]+" ";
            }
            TbResult.Text += "}\r\n";
            var _min = double.MaxValue;
            for (int i = 0; i < r.GetLength(0); i++)
            {
                for (int j = 0; j < r.GetLength(1); j++)
                {
                    if (r[i,j]<_min)
                    {
                        _min = r[i, j];
                    }
                }
            }
            var Method = _min < 0 ? new IterationMethod.IterationMethod(r, -_min) : new IterationMethod.IterationMethod(r);
            Method.K = 100;
            Method.Eps = 10e-6;
            var result = Method.GetResult();
            //TbResult.Text += "B:\t" + "\r\n" + string.Join("\t", result[0].Select(x => x.ToString("0.##"))) + "\r\n";
            TbResult.Text += "B:\t"+"\r\n" + string.Join("\t", result[1].Select(x => x.ToString("0.##"))) + "\r\n";
            TbResult.Text += result[2][2]+_min;
        }

        private double[,] AT(double[,] table)
        {
            double[,] res = new double[table.GetLength(0), table.GetLength(1)];
            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    res[j, i] = table[i, j];
                }
            }
            return res;
        }
    }
}
