using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TeslaX.Properties;

namespace TeslaX
{
    static class Texture
    {
        private static string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Growtopia\";

        private static List<(string Name, byte[] Custom, byte[] Original)> files = new List<(string, byte[], byte[])>()
        {
            ("pickup_box.rttex", Resources.pickup_box, Resources.pickup_box_old),
            ("particles.rttex", Resources.particles, Resources.particles_old),
            ("seed.rttex", Resources.seed, Resources.seed_old)
        };

        public static bool Delete()
        {
            bool res = !files.All(x => !File.Exists(path + @"game\" + x.Name));
            foreach (var x in files)
                if (File.Exists(path + @"game\" + x.Name))
                    File.Delete(path + @"game\" + x.Name);
            return res;
        }

        public static void Replace()
        {
            foreach (var x in files)
                File.WriteAllBytes(path + @"cache\game\" + x.Name, x.Custom);
        }

        public static void Restore()
        {
            foreach (var x in files)
                File.WriteAllBytes(path + @"cache\game\" + x.Name, x.Original);
        }
    }
}
