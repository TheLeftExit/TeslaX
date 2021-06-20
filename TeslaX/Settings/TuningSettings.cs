using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheLeftExit.TeslaX
{
    [Serializable()]
    public class TuningSettings
    {
        private static int limit(int value, int min, int max)
        {
            return Math.Max(min, Math.Min(max, value));
        }

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Minimum time to stay still whenever \"stay\" is issued (milliseconds).")]
        [Category("Movement")]
        [DisplayName("Minimum pause time")]
        public int MinStop
        {
            get => minStop;
            set => minStop = limit(value, 0, 999);
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
            set => maxMove = limit(value, 0, 999);
        }
        private int maxMove = 150;

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Move forward until this punching distance to a block is reached.")]
        [Category("Movement")]
        [DisplayName("Target distance")]
        public int TargetDistance
        {
            get => targetDistance;
            set => targetDistance = limit(value, 0, 63);
        }
        private int targetDistance = 20;

        [Browsable(true)]
        [ReadOnly(false)]
        [Description("Range, in pixels from player, to detect blocks in (set to 0 for unlimited).")]
        [Category("General")]
        [DisplayName("Range")]
        public int Range
        {
            get => range;
            set => range = limit(value, 0, 32000);
        }
        private int range;
    }
}
