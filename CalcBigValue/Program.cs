using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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

            return;
        }


        public static List<int> Addition( List<int> digit_a, List<int> digit_b)
        {
            int n = Math.Max(digit_a.Count, digit_b.Count);
            var digit_ans = new List<int>(n);

            for( var i = 0; i < n; i++ )
            {
                digit_ans[i] = (i < digit_a.Count ? digit_a[i] : 0) + (i < digit_b.Count ? digit_b[i] : 0);
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
            for( var i = 0; i < s.Length; i++ )
            {
                digit.Add( int.Parse( s.Substring(i, 1)));
            }
            return digit;
        }


        public static string BigintToString(List<int> digit)
        {
            var str = new StringBuilder();
            foreach( var num in digit)
            {
                str.Append(num);
            }
            return str.ToString();
        }

    }
}
