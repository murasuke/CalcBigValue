using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CalcBigValue
{
    class Program
    {
        static void Main(string[] args)
        {
            var bigint = StringToBigint("123456789012345");
            Console.WriteLine(BigintToString(bigint));

            Func<string,List<int>> stb = StringToBigint;
            Func<List<int>, string> bts = BigintToString;

            var add1 = bts( Addition(stb("1234"), stb("6420")));
            Console.WriteLine(add1);

            var add2 = bts(Addition(stb("99999999999999999"), stb("1")));
            Console.WriteLine(add2);

            add2 = bts(Addition(stb("99999999999999999"), stb("99999999999999999")));
            Console.WriteLine(add2);

            var sub1 = bts(Subtraction(stb("6420"), stb("1234")));
            Console.WriteLine(sub1);

            var sub2 = bts(Subtraction(stb("1000000000000000000"), stb("1")));
            Console.WriteLine(sub2);

            sub2 = bts(Subtraction(stb("1000000000000000000"), stb("1000000000000000000")));
            Console.WriteLine(sub2);
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
