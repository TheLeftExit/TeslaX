using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;
using System.Collections.Specialized;
using TheLeftExit.TeslaX.Properties;
using TheLeftExit.TeslaX;

namespace TheLeftExit.TeslaX.Interface
{
    public partial class TuningForm : Form
    {
        private TuningSettings NewSettings;

        public TuningForm(TuningSettings settings)
        {
            InitializeComponent();
            NewSettings = settings;
            propertyGrid.SelectedObject = NewSettings;
        }
    }
}
