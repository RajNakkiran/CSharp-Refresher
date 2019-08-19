using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * n such that when LH and RH of n*n is added , results in n
 */

namespace Kaprekar_Numbers
{
    class Program
    {
        static void Main(string[] args)
        {
            int start = Convert.ToInt32(Console.ReadLine());
            int end = Convert.ToInt32(Console.ReadLine());
            for (int n = start; n < end; n++ )
            {
                int sq = n * n;
                string sqStr = string.Format("{0}", sq);
                int sqLen = sqStr.Length;
                int rtLen = sqLen / 2;
                int ltLen = sqLen - rtLen;
                string rtStr = sqStr.Substring(ltLen);
                string ltStr = sqStr.Substring(0, ltLen);
                int lt = ( ltStr != null ) && ( ltStr.Length > 0 )? Convert.ToInt32(ltStr) : 0 ;
                int rt = (rtStr != null ) && ( rtStr.Length > 0 ) ? Convert.ToInt32(rtStr) : 0 ;
                if ( n == lt + rt )
                {
                    Console.WriteLine(n);
                }
            }
        }
    }
}
