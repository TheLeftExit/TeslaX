using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;

namespace TheLeftExit.TeslaX.Static
{
    [Serializable()]
    public class UserSettings
    {
        public static UserSettings Current;

        private static string cfgpath =
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\teslax.cfg";
        public static void Load()
        {
            try { Current = JsonConvert.DeserializeObject<UserSettings>(File.ReadAllText(cfgpath)); }
            catch { Current = new UserSettings(); }
        }

        public static void Save()
        {
            File.WriteAllText(cfgpath, JsonConvert.SerializeObject(Current));
        }

        public static void Erase()
        {
            if (File.Exists(cfgpath))
                File.Delete(cfgpath);
            Current = new UserSettings();
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Minimum time to stay still whenever \"stay\" is issued (milliseconds).")]
        [Category("Movement")]
        [DisplayName("Minimum pause time")]
        public int MinStop
        {
            get => minStop;
            set => minStop = value.Limit(0, 999);
        }
        private int minStop = 250;

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Maximum time to move at once. Afterward, \"stay\" is issued (milliseconds).")]
        [Category("Movement")]
        [DisplayName("Maximum move time")]
        public int MaxMove
        {
            get => maxMove;
            set => maxMove = value.Limit(0, 999);
        }
        private int maxMove = 150;

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Move forward until this distance to a block is reached when moving left (pixels).")]
        [Category("Movement")]
        [DisplayName("Target distance: left")]
        public int DistanceLeft
        {
            get => distanceLeft;
            set => distanceLeft = value.Limit(1, 58);
        }
        private int distanceLeft = 58;

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Move forward until this distance to a block is reached when moving right. (pixels)")]
        [Category("Movement")]
        [DisplayName("Target distance: right")]
        public int DistanceRight
        {
            get => distanceRight;
            set => distanceRight = value.Limit(1, 38);
        }
        private int distanceRight = 38;

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Whether punch button should be pressed automatically.")]
        [Category("General")]
        [DisplayName("Automatic punch")]
        public bool Punch { get; set; } = true;

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Tile ID of the broken block.")]
        [Category("General")]
        [DisplayName("Block ID")]
        public short BlockID { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Tile ID of the door (when multiple row support is enabled; set to 0 for any tile).")]
        [Category("Multiple row support")]
        [DisplayName("Door ID")]
        public short DoorID { get; set; }

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Range, in pixels from player, to detect blocks in (set to 0 for unlimited).")]
        [Category("General")]
        [DisplayName("Range")]
        public int Range {
            get => range;
            set => range = value.Limit(0, 32000);
        }
        private int range;

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Disable input and don't stop detecting until manually requested.")]
        [Category("Debug")]
        [DisplayName("Enable debug mode")]
        public bool DebugMode { get; set; } = false;

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Display your status on Discord.")]
        [Category("Discord Rich Presence")]
        [DisplayName("Enable")]
        public bool RichPresence { get; set; } = false;

        [Browsable(false)] // Not implemented
        [ReadOnly(false)]
        [Description("Stop whenever there are items dropped behind you.")]
        [Category("Other")]
        [DisplayName("Stop on full inventory")]
        public bool StopOnFull { get; set; } = false;

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("At the end of the row, execute custom script (see Tools), then immediately attempt to start breaking.")]
        [Category("Multiple row support")]
        [DisplayName("Enable")]
        public bool Continue { get; set; } = false;

        [Browsable(false)]
        // Stript executed after successful workflows if Continue is checked.
        public StringCollection ContinueScript { get; set; }
    }
}
