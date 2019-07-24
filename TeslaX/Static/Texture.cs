using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TheLeftExit.TeslaX.Properties;

namespace TheLeftExit.TeslaX.Static
{
    internal static class Texture
    {
        private static string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Growtopia\";

        private static List<(string Name, byte[] Custom, byte[] Original)> files = new List<(string, byte[], byte[])>()
        {
            ("pickup_box.rttex", Resources.pickup_box, Resources.pickup_box_old),
            ("particles.rttex", Resources.particles, Resources.particles_old),
            ("seed.rttex", Resources.seed, Resources.seed_old)
            //("crack.rttex", Resources.crack, Resources.crack_old)
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

        public static bool Replaced()
        {
            if (File.Exists(path + @"game\seed.rttex") || !File.Exists(path + @"cache\game\seed.rttex"))
                return false;
            if (File.ReadAllBytes(path + @"cache\game\seed.rttex").SequenceEqual(Resources.seed))
                return true;
            return false;
        }
    }
}
