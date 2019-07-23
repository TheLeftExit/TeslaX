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
        [Category("Input")]
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
        [Category("Input")]
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
        [Category("Input")]
        [DisplayName("Target distance: left")]
        public int DistanceLeft
        {
            get => distanceLeft;
            set => distanceLeft = value.Limit(1, 58);
        }
        private int distanceLeft = 58;

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Can we display a tuple here?.")]
        [Category("Other")]
        [DisplayName("Tuple")]
        public (int, int) Tuple { set; get; } = (2, 3);

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Move forward until this distance to a block is reached when moving right. (pixels)")]
        [Category("Input")]
        [DisplayName("Target distance: right")]
        public int DistanceRight
        {
            get => distanceRight;
            set => distanceRight = value.Limit(1, 38);
        }
        private int distanceRight = 38;

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Player's skin color, in order as it's displayed in game settings (from 0 to 13).")]
        [Category("Player")]
        [DisplayName()]
        public int SkinColor
        {
            get => skinColor;
            set => skinColor = value.Limit(0, 13);
        }
        private int skinColor = 0;

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Whether the game is windowed.")]
        [Category("Game")]
        [DisplayName()]
        public bool Windowed { get; set; } = true;

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Whether punch button should be pressed automatically.")]
        [Category("Input")]
        [DisplayName("Automatic punch")]
        public bool Punch { get; set; } = true;

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Display a debug form.")]
        [Category("Debug")]
        [DisplayName("Display debug form")]
        public bool DebugForm { get; set; } = true;

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

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Stop whenever there are items dropped behind you (custom textures only).")]
        [Category("Other")]
        [DisplayName("Stop on full inventory")]
        public bool StopOnFull { get; set; } = false;

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("At the end of the row, execute custom script (see Tools), then immediately attempt to start breaking.")]
        [Category("Other")]
        [DisplayName("Multiple row support")]
        public bool Continue { get; set; } = false;

        [Browsable(false)]
        [ReadOnly(false)]
        [Description("Enable features that take advantage of custom textures.")]
        [Category("Other")]
        [DisplayName("Support custom textures")]
        // Enabling this makes it mandatory to update it, which might be painful without injection automation.
        // For now we'll default this to false and exclude new crack.rttex from custom textures.
        public bool CustomTextures { get; set; } = false;

        [Browsable(false)]
        // How to treat BlockState.Uncertain.
        public bool UncertainIsBlock { get; set; } = true;

        [Browsable(false)]
        // Blocks behind the player to include in a screenshot.
        public int BlocksBehind { get; set; } = 1;

        [Browsable(false)]
        // Blocks in front of the player to include in a screenshot.
        public int BlocksAhead { get; set; } = 3;

        [Browsable(false)]
        // Stript executed after successful workflows if Continue is checked.
        public StringCollection ContinueScript { get; set; }

        [Browsable(false)]
        public int SelectedBlock { get; set; } = 0;
    }
}
