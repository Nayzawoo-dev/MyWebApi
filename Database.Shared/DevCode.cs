using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Database.Shared
{
    public static class DevCode
    {
        public static bool isNull(this string? str)
        {
            var res = str == null && string.IsNullOrEmpty(str.Trim()) && string.IsNullOrWhiteSpace(str.Trim());
            return res;
        }

        public static bool isInt(this int? num)
        {
            bool res = num == null || int.TryParse(num.ToString(), out int number);
            return res;
           
        }

        public static string toJson(object obj)
        {
            var res = JsonConvert.SerializeObject(obj);
            return res;
        }
    }
}
