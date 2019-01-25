using System;
using System.Collections.Generic;
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

namespace Recursion
{
    public struct Round
    {
        public int woman;
        public int cat;
        public int man;
        public int mouse;
        public double X1;
        public double X2;
        public double Y1;
        public double Y2;
        public double V11;
        public double V12;
        public double V21;
        public double V22;
        public double V;
        public int i;

    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static double X1 { get; set; }
        public static double X2 { get; set; }
        public static double Y1 { get; set; }
        public static double Y2 { get; set; }

        static List<Round> iterations = new List<Round>();
        public MainWindow()
        {
            InitializeComponent();
            //tbO.Text += "Рекурсивная игра:\r\n";
            //for (int i = 0; i <= 8; i++)
            //{
            //    tbO.Text += $"{i}, {8 - i} - v: {RecGame.Rec(i, 8 - i)}; X1: {RecGame.X1} X2: {RecGame.X2} Y1: {RecGame.Y1} Y2: {RecGame.Y2}\r\n";
            //}
            //tbO.Text += "Стохастическая игра: 10e-6\r\n";
            //for (int i = 1; i < 8; i++)
            //{
            //    tbO.Text += $"{i}, {8 - i} - v: {StoGame.Sto(i, 8 - i)}; X1: {StoGame.X1} X2: {StoGame.X2} Y1: {StoGame.Y1} Y2: {StoGame.Y2}\r\n";
            //}
            //WCMM w = new WCMM();
            ////w.fill();
            ////tbO.Text += Print(w.getValue(w.matrix));
            //Group wc = new Group { first = 2, second = 2 };
            //Group mm = new Group { first = 2, second = 2 };
            //while (!WCMM.F)
            //{
            //    tbO.Text += $" v: {WCMM.Rec(2,2,2,2)}; X1: {WCMM.X1} X2: {WCMM.X2} Y1: {WCMM.Y1} Y2: {WCMM.Y2}\r\n"; 
            //}
            double v = Rec(2, 2, 2, 2, 0);
            //var i1 = iterations.Where(s => s.i == 1).ToList();
            //var i2 = iterations.Where(s => s.i == 2).ToList();
            //var i3 = iterations.Where(s => s.i == 3).ToList();
            //var i4 = iterations.Where(s => s.i == 4).ToList();
            //foreach (var item in i1)
            //{
            //    tbO.Text += $" Iteration: {item.i}\r\n";
            //    tbO.Text += $" v: {item.V}; X1: {item.X1} X2: {item.X2} Y1: {item.Y1} Y2: {item.Y2}\r\n";
            //    tbO.Text += $"{item.V11} \t {item.V12} \n {item.V21} \t {item.V22}\r\n";
            //}
            int i = 0;
            while (iterations.Count>0)
            {
                var iter = iterations.Where(s => s.i == i).ToList();
                foreach (var item in iter)
                {
                    tbO.Text += $" Iteration: {item.i}\r\n";
                    tbO.Text += $"Woman: {item.woman} \t Cats: {item.cat} \t Men: {item.man} \t Mice: {item.mouse}\r\n";
                    tbO.Text += $" v: {item.V}; X1: {item.X1} X2: {item.X2} Y1: {item.Y1} Y2: {item.Y2}\r\n";
                    tbO.Text += $"{item.V11} \t {item.V12} \n {item.V21} \t {item.V22}\r\n";
                }
                foreach (var item in iter)
                {
                    iterations.Remove(item);
                }
                i++;
            }
        }
        public double Rec(int woman, int cat, int man, int mouse, int i)
        {
            if (woman < 0 || cat < 0)
            {
                return  -1;
            }
            if (man < 0 || mouse < 0)
            {
                return 1;
            }
            i++;
            var v11 = Rec(woman, cat, man - 1, mouse, i);
            var v12 = Rec(woman - 1, cat, man, mouse, i);
            var v21 = Rec(woman, cat - 1, man, mouse, i);
            var v22 = Rec(woman, cat, man, mouse - 1, i);
            X1 = (v22 - v21) / (v11 + v22 - v12 - v21);
            X2 = 1.0 - X1;
            var v = (v11 * v22 - v12 * v21) / (v11 + v22 - v12 - v21);
            Y1 = (v - v12) / (v11 - v12);
            Y2 = 1.0 - Y1;
            //tbO.Text += $" Iteration: {i}\r\n";
            //tbO.Text += $" v: {v}; X1: {X1} X2: {X2} Y1: {Y1} Y2: {Y2}\r\n";
            //tbO.Text += $"{v11} \t {v12} \n {v21} \t {v22}\r\n";
            Round R = new Round { woman = woman, cat = cat, man = man, mouse = mouse, X1 = X1, X2 = X2, Y1 = Y1, Y2 = Y2, V11 = v11, V12 = v12, V21 = v21, V22 = v22, V = v, i = i };
            if (!iterations.Contains(R))
            {
                iterations.Add(R);
            }
            return v;
        }

    }
}
