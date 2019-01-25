using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recursion
{
    //public struct Round {
    //    public double X1;
    //    public double X2;
    //    public double Y1;
    //    public double Y2;
    //    public double V;
    //    public string print;

    //}
    public class WCMM
    {
        //public static double X1 { get; set; }
        //public static double X2 { get; set; }
        //public static double Y1 { get; set; }
        //public static double Y2 { get; set; }

        //public static bool F = false;
        
        //public static Round Rec(int woman, int cat, int man, int mouse)
        //{
        //    if (woman<0 || cat < 0)
        //    {
        //        return new Round { V = -1 };
        //    }
        //    if (man < 0 || mouse < 0)
        //    {
        //        return new Round { V = 1 };
        //    }
        //    //if (woman < 0 && cat < 0 && man < 0 && mouse < 0)
        //    //{
        //    //    F = true;
        //    //}
        //    //double v11=0, v12=0, v21=0, v22=0;
        //    //Cash = new Dictionary<Group, Dictionary<Group, double>>();
        //    //if (Cash.ContainsKey(wc))
        //    //{
        //    //    if (Cash[wc].ContainsKey(mm))
        //    //    {
        //    //        return Cash[wc][mm];
        //    //    }
        //    //}
        //    //if (wc.first <= 0 || wc.second <= 0 || mm.first <= 0 || mm.second <= 0)
        //    //{
        //    //    return 0.0;
        //    //}
        //    //var na1 = mm;
        //    //if (na1.first>0)
        //    //{
        //    //    na1.first--;
        //    //    v11 = Rec(wc, na1);
        //    //}
        //    //var na2 = wc;
        //    //if (na2.first>0)
        //    //{
        //    //    na2.first--;
        //    //    v12 = Rec(na2, mm);
        //    //}
        //    //var na3 = wc;
        //    //if (na3.second>0)
        //    //{
        //    //    na3.second--;
        //    //    v21 = Rec(na3, mm);
        //    //}
        //    //var na4 = mm;
        //    //if (na4.second>0)
        //    //{
        //    //    na4.second--;
        //    //    v22 = Rec(wc, na4);
        //    //}
        //    //X1 = (v22 - v21) / (v11 + v22 - v12 - v21);
        //    //X2 = 1.0 - X1;
        //    //var v = (v11 * v22 - v12 * v21) / (v11 + v22 - v12 - v21);
        //    //Y1 = (v - v12) / (v11 - v12);
        //    //Y2 = 1.0 - Y1;
        //    //if (!Cash.ContainsKey(wc))
        //    //{
        //    //    Cash[wc] = new Dictionary<Group, double> { [mm] = v };
        //    //}
        //    //return v;
        //    var v11 = Rec(woman, cat, man - 1, mouse).V;
        //    var v12 = Rec(woman-1, cat, man, mouse).V;
        //    var v21 = Rec(woman, cat-1, man, mouse).V;
        //    var v22 = Rec(woman, cat, man, mouse-1).V;
        //    X1 = (v22 - v21) / (v11 + v22 - v12 - v21);
        //    X2 = 1.0 - X1;
        //    var v = (v11 * v22 - v12 * v21) / (v11 + v22 - v12 - v21);
        //    Y1 = (v - v12) / (v11 - v12);
        //    Y2 = 1.0 - Y1;
        //    //if (!Cash.ContainsKey(w))
        //    //{
        //    //    Cash[w] = new Dictionary<int, double> { [m] = v };
        //    //}
        //    return new Round { X1 = X1, X2 = X2};
        //}
    }
}
