using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace CalcBigValue
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }


        public static List<int> Addion( List<int> digit_a, List<int> digit_b)
        {
            int n = Math.Max(digit_a.Count, digit_b.Count);
            var digit_ans = new List<int>(n);

            for( var i = 0; i < n; i++ )
            {
                digit_ans[i] = (i < digit_a.Count ? digit_a[i] : 0) + (i < digit_b.Count ? digit_b[i] : 0);
            }

            return carry_and_fix(digit_ans);
        }

        public static List<int> carry_and_fix(List<int> digit_ans)
        {
            return null;
        }

    }
}
