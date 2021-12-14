using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Infrastructure.Core
{
    public static class PINNumberGenerator
    {
        public static string Generate()
        {
            Random rnd = new Random();
            return rnd.Next(1000, 10001).ToString();
        }
    }
}
