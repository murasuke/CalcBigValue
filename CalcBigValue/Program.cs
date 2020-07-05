using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CalcBigValue
{
    public class BigintCalculator        
    {
        static void Main(string[] args)
        {
            Func<string, List<int>> stb = BigintCalculator.StringToBigint;
            Func<List<int>, string> bts = BigintCalculator.BigintToString;

            //var val = StringToBigint("1");
            ////2^100
            //for (var i = 0; i< 1000; i++)
            //{
            //    val = Addition(val, val);

            //    Console.WriteLine(((i + 1).ToString("000") + " : " + BigintToString(val)));
            //}
            Console.WriteLine(bts(Division(stb("1234"), stb("1234"))));

            Console.WriteLine(bts(Division(stb("9999"), stb("3"))));

            Console.WriteLine( bts(Division(stb("98765"), stb("1234"))));

            Console.WriteLine(bts(Division(stb("123456789"), stb("54321"))));

            return;
        }

        


        public static List<int> Addition( List<int> digit_a, List<int> digit_b)
        {
            int n = Math.Max(digit_a.Count, digit_b.Count);
            var digit_ans = new int[n].ToList();

            for( var i = 0; i < n; i++ )
            {
                digit_ans[i] = (i < digit_a.Count ? digit_a[i] : 0) + (i < digit_b.Count ? digit_b[i] : 0);
            }

            return CarryAndFix(digit_ans);
        }

        public static List<int> Subtraction(List<int> digit_a, List<int> digit_b)
        {
            int n = Math.Max(digit_a.Count, digit_b.Count);
            var digit_ans = new int[n].ToList();

            for (var i = 0; i < n; i++)
            {
                digit_ans[i] = (i < digit_a.Count ? digit_a[i] : 0) - (i < digit_b.Count ? digit_b[i] : 0);
            }

            return CarryAndFix(digit_ans);
        }

        public static List<int> Multiplication(List<int> digit_a, List<int> digit_b)
        {
            int lena = digit_a.Count;
            int lenb = digit_b.Count;
            int[] res = new int[lena + lenb - 1];

            for( var i = 0; i < lena; i++)
            {
                for( var j = 0; j < lenb; j++)
                {
                    res[i + j] += digit_a[i] * digit_b[j];
                }
            }
            return CarryAndFix( new List<int>(res) );
        }

        public static List<int> Division(List<int> digit_a, List<int> digit_b)
        {
            var ansZero = new List<int>() { 0 };

            int NA = digit_a.Count;
            int NB = digit_b.Count;
            if (NA < NB) return ansZero;

            // ----- ステップ 1. A ÷ B の桁数を求める ----- //
            int D = NA - NB;

            // digit_a_partial : A の上 NB 桁を取り出したもの
            // digit_a_partial(digit_a.begin() + (NA - NB), digit_a.end());
            var digit_a_partial = new List<int>(digit_a.Skip(NA - NB));
       
            if( CompareBigint(digit_a_partial, digit_b) >= 0)
            {
                D = NA - NB + 1;
            }

            // ----- ステップ 2. A ÷ B を筆算で求める ----- //
            if (D == 0) return ansZero;
            //vector<int> remain(digit_a.begin() +(D - 1), digit_a.end());
            var remain = new List<int>(digit_a.Skip(D - 1));

            var digit_ans = new List<int>(new int[D]);

            for( var i = D - 1; i >= 0; i--)
            {
                digit_ans[i] = 9;
                for( var j = 1; j <= 9; j++)
                {
                    var x = Multiplication(digit_b, new List<int>() { j });
                    if( CompareBigint(x, remain) == 1)
                    {
                        digit_ans[i] = j - 1;
                        break;
                    }
                }

                var x_result = Multiplication(digit_b, new List<int>() { digit_ans[i] });
                remain = Subtraction(remain, x_result);
                if( i >= 1)
                {
                    // 新しく 10^(i-1) の位が降りてくる
                    remain.Insert(0, digit_a[i - 1]);
                }
            }

            return digit_ans;
        }

        /// <summary>
        /// 比較
        /// ・同じ=0
        /// ・左が大きい=1
        /// ・右が大きい=-1
        /// </summary>
        /// <param name="digit_a"></param>
        /// <param name="digit_b"></param>
        /// <returns></returns>
        public static int CompareBigint(List<int> digit_a, List<int> digit_b)
        {
            int lena = digit_a.Count;
            int lenb = digit_b.Count;
            if (lena > lenb) return 1;
            if (lena < lenb) return -1;
            for( var i = lena - 1; i >= 0; i--)
            {
                if (digit_a[i] > digit_b[i]) return 1;
                if (digit_a[i] < digit_b[i]) return -1;
            }
            return 0;
        }

        public static List<int> CarryAndFix(List<int> digit)
        {
            int n = digit.Count;
            for (int i = 0; i < n - 1; ++i)
            {
                // 繰り上がり処理 (K は繰り上がりの回数)
                if (digit[i] >= 10)
                {
                    int K = digit[i] / 10;
                    digit[i] -= K * 10;
                    digit[i + 1] += K;
                }
                // 繰り下がり処理 (K は繰り下がりの回数)
                if (digit[i] < 0)
                {
                    int K = (-digit[i] - 1) / 10 + 1;
                    digit[i] += K * 10;
                    digit[i + 1] -= K;
                }
            }

            // 一番上の桁が 10 以上なら、桁数を増やすことを繰り返す
            while (digit.Last() >= 10)
            {
                int K = digit.Last() / 10;
                digit[digit.Count-1] -= K * 10;
                digit.Add(K);
            }

            // 1 桁の「0」以外なら、一番上の桁の 0 (リーディング・ゼロ) を消す
            while (digit.Count() >= 2 && digit.Last() == 0)
            {
                digit.RemoveAt(digit.Count - 1);
            }
            
            return digit;
        }

        public static List<int> StringToBigint( string s)
        {
            var digit = new List<int>();
            for( var i = s.Length-1; i >= 0; i-- )
            {
                digit.Add( int.Parse( s.Substring(i, 1)));
            }
            return digit;
        }


        public static string BigintToString(List<int> digit)
        {
            var str = new StringBuilder();
            for( var i = digit.Count -1; i >= 0; i-- )
            {
                str.Append(digit[i]);
            }
            return str.ToString();
        }

    }
}
