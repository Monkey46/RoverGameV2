using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;
namespace RoverGameV2
{
    public static class ColorExtension
    {
        public static Color SetA(this Color color, byte a)
        {
            List<byte> colorBits = BitConverter.GetBytes((uint)color.ToArgb()).ToList();
            colorBits[0] = a;

            color = new Color(toARGB(colorBits));
            return color;
        }
        private static int toARGB(List<byte> bytes)
        {
            // @Task fix the alpha on this 
            int color = 0;
            color |= bytes[0] << 24;
            color |= bytes[1] << 16;
            color |= bytes[2] << 8;
            color |= bytes[3];
            return color;
        }
    }
}
