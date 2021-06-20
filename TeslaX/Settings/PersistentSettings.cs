using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace TheLeftExit.TeslaX
{
    [Serializable()]
    class PersistentSettings
    {
        // Stuff that's passed to Worker with each call to Break. Accessed directly through PropertyGrid. Most people won't need this.
        public TuningSettings Tuning { get; set; } = new TuningSettings();

        // Stuff that's configured through UI.
        public UserSettings User { get; set; } = new UserSettings();

        // Previously found addresses, bound to a specific game version (tracked by file creation date).
        public Dictionary<string, long> AddressCache { get; set; } = new Dictionary<string, long> { { "gamever", 0 } };

        public PersistentSettings(string fname = "")
        {
            if (!File.Exists(fname))
                return;

            PersistentSettings parsed;

            try { parsed = JsonSerializer.Deserialize<PersistentSettings>(fname); }
            catch { return; }

            Tuning = parsed.Tuning;
            User = parsed.User;
            AddressCache = parsed.AddressCache;
        }

        public void Save(string fname) =>
            File.WriteAllText(fname, JsonSerializer.Serialize(this));
    }
}