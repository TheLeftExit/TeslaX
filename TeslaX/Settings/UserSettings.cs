using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;

namespace TheLeftExit.TeslaX
{
    [Serializable()]
    public class UserSettings
    {
        public short BlockID { get; set; } = -1;
        public short DoorID { get; set; } = -1;
        public bool StopOnFull { get; set; } = false;
        public bool Continue { get; set; } = false;
        public StringCollection ContinueScript { get; set; }
    }
}
