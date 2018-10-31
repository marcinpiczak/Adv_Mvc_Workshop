using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMessagePortal.Helpers
{
    public static class ColorHelper
    {
        public static string GetRandomColor()
        {
            var random = new Random();
            var color = String.Format("{0:X6}", random.Next(0x1000000));
            return color;
        }
    }
}
