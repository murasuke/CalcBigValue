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

            var sub1 = bts(Subtraction(stb("6420"), stb("1234")));

            Console.WriteLine(sub1);
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

        public static List<int> CarryAndFix(List<int> digit_ans)
        {
            return digit_ans;
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
