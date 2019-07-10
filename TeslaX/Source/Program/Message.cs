using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeslaX
{
    public static class Message
    {
        public static void NoWindow()
        {
            MessageBox.Show("Growtopia isn't open.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void NoNewOffset()
        {
            MessageBox.Show("Failed to generate the block grid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void NoNewPlayer()
        {
            MessageBox.Show("Failed to find the player.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void NoCustomSpritesheet()
        {
            MessageBox.Show("Invalid spritesheet file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
