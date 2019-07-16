using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TeslaX.Properties;

namespace TeslaX
{
    public static class Texture
    {
        private static string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Growtopia\";

        public static bool Delete()
        {
            bool res = File.Exists(path + @"game\pickup_box.rttex") || File.Exists(path + @"game\particles.rttex");
            if (File.Exists(path + @"game\pickup_box.rttex"))
                File.Delete(path + @"game\pickup_box.rttex");
            if (File.Exists(path + @"game\particles.rttex"))
                File.Delete(path + @"game\particles.rttex");
            return res;
        }

        public static void Replace()
        {
            File.WriteAllBytes(path + @"cache\game\particles.rttex", Resources.particles);
            File.WriteAllBytes(path + @"cache\game\pickup_box.rttex", Resources.pickup_box);
        }

        public static void Restore()
        {
            File.WriteAllBytes(path + @"cache\game\particles.rttex", Resources.particles_old);
            File.WriteAllBytes(path + @"cache\game\pickup_box.rttex", Resources.pickup_box_old);
        }
    }
}
