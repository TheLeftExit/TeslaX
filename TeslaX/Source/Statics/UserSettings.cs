using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeslaX.Properties;

namespace TeslaX
{
    [Serializable()]
    public class UserSettings
    {
        public static UserSettings Current;

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Minimum time to stay still whenever \"stay\" is issued.")]
        [Category("Input")]
        [DisplayName("Minimum pause time")]
        public int MinStop {
            get => minStop;
            set => minStop = value.Limit(0, 999);
        }
        private int minStop = 250;

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Maximum time to move at once. Afterward, \"stay\" is issued.")]
        [Category("Input")]
        [DisplayName("Maximum move time")]
        public int MaxMove {
            get => maxMove;
            set => maxMove = value.Limit(0, 999);
        }
        private int maxMove = 150;

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Move forward until this distance to a block is reached (when moving left).")]
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
        [Description("Move forward until this distance to a block is reached (when moving right).")]
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
        public int SkinColor {
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
        // How to treat BlockState.Uncertain.
        public bool UncertainIsBlock { get; set; } = true;

        [Browsable(false)]
        // Blocks behind the player to include in a screenshot.
        public int BlocksBehind { get; set; } = 1;

        [Browsable(false)]
        // Blocks in front of the player to include in a screenshot.
        public int BlocksAhead { get; set; } = 3;

        [Browsable(false)]
        // Whether TextureForm instructions are displayed.
        public bool TextureInfo { get; set; } = false;

        [Browsable(false)]
        // Stript executed after successful workflows if Continue is checked.
        public StringCollection ContinueScript { get; set; }

        [Browsable(false)]
        public int SelectedBlock { get; set; } = 0;
    }
}
