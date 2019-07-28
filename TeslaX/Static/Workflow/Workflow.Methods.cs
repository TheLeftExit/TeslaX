﻿using System;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using TheLeftExit.TeslaX.Entities;
using TheLeftExit.TeslaX.Helpers;
using TheLeftExit.TeslaX.Interface;
using TheLeftExit.TeslaX.Properties;
using System.Linq;

namespace TheLeftExit.TeslaX.Static
{
    internal static partial class Workflow
    {
        private class NextBlockInfo
        {
            public int Distance;
            public short Foreground;
            public short Background;
        }

        private static NextBlockInfo GetNextBlockInfo(this ProcessHandle handle)
        {
            Point rawPlayer = handle.GetPlayer();
            bool rawDirection = handle.GetDirection();

            Point player = new Point(rawPlayer.X / 32, (rawPlayer.Y - 2 + 31) / 32);
            bool onTwoBlocks = rawPlayer.X % 32 > 12;

            int firstSearchedX = player.X + (rawDirection ? 1 : -1);
            int increment = rawDirection ? 1 : -1;

            NextBlockInfo res = null;
            int NextBlockX = -1;

            for(int i = firstSearchedX; i>=0 && i<100; i += increment)
            {
                short fore = handle.GetBlock(i, player.Y);
                short back = handle.GetBackground(i, player.Y);
                if (fore != 0 || back != 0)
                {
                    res = new NextBlockInfo()
                    {
                        Background = back,
                        Foreground = fore
                    };
                    NextBlockX = i;
                    break;
                }
            }

            if (NextBlockX == -1)
                return null;
            if (res.Foreground != UserSettings.Current.Foreground)
                return null;
            if (res.Background != UserSettings.Current.Background)
                return null;

            res.Distance = ((rawPlayer.X - 6) - (NextBlockX * 32)) * (rawDirection ? -1 : 1) - 32;
            return res;
        }
    }
}