using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Tools
    {
        public static string[] StrToArr(string str)
        {
            int len = 0;
            foreach (var item in str)
            {
                if (item == '-')
                {
                    len++;
                }
            }
            string[] result = new string[len];
            for (int i = 0; i < len; i++)
            {
                result[i] = (str.Split('-')[i]);
            }
            return result;
        }
        public static string ArrToStr(string[] strArr)
        {
            var result = "";
            foreach (var item in strArr)
            {
                result = result + item + '-';
            }
            return result;
        }
    }
}
