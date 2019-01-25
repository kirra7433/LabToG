using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace IterationMethod
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private double[,] _table;
        private double _min = double.MaxValue;

        public IterationMethod Method { get; set; }

        private void BtFile_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog()==true)
            {
                TbFile.Text = ofd.FileName;
                try
                {
                    var sr = new StreamReader(ofd.OpenFile());
                    var str = sr.ReadToEnd();
                    var lines = str.Split(new[] {"\r\n"}, StringSplitOptions.None);
                    _table = new double[lines.Length,lines[0].Split(new[] {" ","\t"}, StringSplitOptions.None).Length];
                    for (int i = 0; i < lines.Length; i++)
                    {
                        var temp = lines[i].Split(new[] {" ", "\t"},StringSplitOptions.None);
                        for (int j = 0; j < temp.Length; j++)
                        {
                            _table[i, j] = double.Parse(temp[j]);
                            if (_table[i,j]<_min)
                            {
                                _min = _table[i, j];
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                    return;
                }
                BtStart.IsEnabled = true;
            }
        }

        private void BtStart_OnClick(object sender, RoutedEventArgs e)
        {
            int k=0;
            if (!string.IsNullOrEmpty(TbK.Text))
            {
                k = int.Parse(TbK.Text);
            }
            else
            {
                MessageBox.Show("Введите минимальное количество итераций");
                return;
            }
            double eps = 0;
            if (!string.IsNullOrEmpty(TbE.Text))
            {
                eps = double.Parse(TbE.Text);
            }
            else
            {
                MessageBox.Show("Введите точность");
            }
            Method = _min < 0 ? new IterationMethod(_table, -_min) : new IterationMethod(_table);
            Method.K = k;
            Method.Eps = eps;
            var result = Method.GetResult();
            var sm = new SimplexMethod(_table);
            TbResult.Text = "";
            TbResult.Text += "K: " + Method.K + "\r\n";
            TbResult.Text += "A:\t" + string.Join("\t", result[0].Select(x=>x.ToString("0.#####"))) + "\r\n";
            TbResult.Text += "B:\t" + string.Join("\t", result[1].Select(x => x.ToString("0.#####"))) + "\r\n";
            TbResult.Text += "\tMin\tMax\tAvg\r\n";
            TbResult.Text += "V:\t" + string.Join("\t", result[2].Select(x => x.ToString("0.#####"))) + "\r\n";
            TbResult.Text += $"Цена игры: {sm.GamePriceA}\r\n";
            TbResult.Text += $"Стратегия игрока A: {string.Join("\t", sm.ResultsA.Select(x=>(x*sm.GamePriceA).ToString("0.#####")))}\r\n";
            TbResult.Text += $"Цена игры: {sm.GamePriceB}\r\n";
            TbResult.Text += $"Стратегия игрока B: {string.Join("\t", sm.ResultsB.Select(x => (x * sm.GamePriceB).ToString("0.#####")))}\r\n";
        }
    }
}
