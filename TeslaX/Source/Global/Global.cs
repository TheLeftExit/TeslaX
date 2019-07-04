using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TeslaX
{
    public static partial class Global
    {
        public static readonly Point InvalidPoint = new Point(-1, -1);

        public static List<int> EligibleBetween(int a, int b, int off)
        {
            List<int> result = new List<int>();
            int start = (a / 32) * 32 + off + (a % 32 < off ? 0 : 32);
            for (int i = start; i <= b; i += 32)
                result.Add(i);
            return result;
        }
    }
}
