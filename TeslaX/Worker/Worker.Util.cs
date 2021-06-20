using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLeftExit.TeslaX
{
    public partial class Worker
    {
        public class Block
        {
            public int X, Y;
            public short FG, BG;
        }

        // TODO: restructure how those are stored.
        private long playerXAddress, playerYAddress, playerDirAddress, worldBaseAddress;

        private int playerX()
        {
            memoryHandle.ReadFloat(playerXAddress, out float res);
            return Convert.ToInt32(res);
        }

        private int playerY()
        {
            memoryHandle.ReadFloat(playerYAddress, out float res);
            return Convert.ToInt32(res);
        }

        private bool playerDir()
        {
            memoryHandle.ReadByte(playerDirAddress, out byte res);
            return res == 0;
        }

        private Block blockAt(int x, int y)
        {
            int offset = 0x04 + x * 0x90 + y * 0x3840;
            byte[] res = new byte[4];
            memoryHandle.ReadBytes(worldBaseAddress + offset, 4,  res);
            return new Block
            {
                X = x,
                Y = y,
                FG = BitConverter.ToInt16(res, 0),
                BG = BitConverter.ToInt16(res, 2)
             };
        }

        private Block blockAhead()
        {
            // This is only so simple because that's how the game decides where to punch as well.
            int tPlayerX = playerX() / 32;
            int tPlayerY = playerY() / 32;
            int inc = playerDir() ? 1 : -1;
            for(int i = tPlayerX + inc; i >= 0 && i <= 99; i += inc)
            {
                Block tBlock = blockAt(i, tPlayerY);
                if ((tBlock.FG | tBlock.BG) != 0)
                    return tBlock;
            }
            return null;
        }

        // Distance to the furthest point ahead where punching will still hit the block.
        // [0;63] allows the block to be punched.
        private int punchingDistance(Block target)
        {
            int tPlayerX = playerX(); 
            bool tPlayerDir = playerDir();
            int tBlockX = target.X;
            return tPlayerDir ? (tBlockX - 1) * 32 - (tPlayerX + 1) : tPlayerX - (tBlockX + 1) * 32;
            // bX: 0   0  0 ..  2  2  2  2
            // pX: 31 32 33 .. 30 31 32 33
            // rt: -1  0  1 ..  1  0 -1 -2
            // you figure this shit out
        }

        // Distance between player's and block' hitboxes.
        // The whole concept of two different distance values is too complicated to explain to end-user, so I'll hide it for now.
        private int physicalDistance(Block target)
        {
            int tPlayerX = playerX();
            bool tPlayerDir = playerDir();
            int tBlockX = target.X;
            return tPlayerDir ? tBlockX * 32 - (tPlayerX + 20) : tPlayerX - (tBlockX + 1) * 32;
            // 20 is player's width, for collision purposes, in pixels.
        }
    }
}
