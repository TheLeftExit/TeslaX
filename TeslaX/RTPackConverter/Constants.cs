using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTPackConverter
{
    public static class Constants
    {
        public enum eCompressionType
        {
            C_COMPRESSION_NONE = 0,
            C_COMPRESSION_ZLIB = 1
        };


        public enum GL_FORMATS
        {
            OGL_PVRTC2 = 0x8C00,
            OGL_PVRTC2_2 = 0x8C01,
            OGL_PVRTC4 = 0x8C02,
            OGL_PVRTC4_2 = 0x8C03,
            OGL_RGBA_4444 = 0x8033,

            OGL_RGBA_8888 = 0x1401,
            OGL_RGB_565 = 0x8363
        }
    }
}
