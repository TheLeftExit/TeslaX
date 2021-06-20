using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Drawing;

namespace TheLeftExit.TeslaX
{
    [ReadOnly(true)]
    public class DecodedItem
    {
        public int ItemID { get; set; }
        public byte EditableType { get; set; }
        public byte ItemCategory { get; set; }
        public byte ActionType { get; set; }
        public byte HitSoundType { get; set; }
        public string Name { get; set; }
        public string Texture { get; set; }
        public int TextureHash { get; set; }
        public byte ItemKind { get; set; }
        public int val1 { get; set; }
        public byte TextureX { get; set; }
        public byte TextureY { get; set; }
        public byte SpreadType { get; set; }
        public byte IsStripeyWallpaper { get; set; }
        public byte CollisionType { get; set; }
        public byte BreakHits { get; set; }
        public int DropChance { get; set; }
        public byte ClothingType { get; set; }
        public short Rarity { get; set; }
        public byte MaxAmount { get; set; }
        public string ExtraFile { get; set; }
        public int ExtraFileHash { get; set; }
        public int AudioVolume { get; set; }
        public string PetName { get; set; }
        public string PetPrefix { get; set; }
        public string PetSuffix { get; set; }
        public string PetAbility { get; set; }
        public byte SeedBase { get; set; }
        public byte SeedOverlay { get; set; }
        public byte TreeBase { get; set; }
        public byte TreeLeaves { get; set; }
        public int SeedColor { get; set; }
        public int SeedOverlayColor { get; set; }
        public int GrowTime { get; set; }
        public short val2 { get; set; }
        public short IsRayman { get; set; }
        public string ExtraOptions { get; set; }
        public string Texture2 { get; set; }
        public string ExtraOptions2 { get; set; }
        public string PunchOptions { get; set; }
        public int TextureRealX { get
            {
                switch (SpreadType)
                {
                    case 3:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                        return TextureX + 3;
                    case 2:
                    case 4:
                    case 5:
                        return TextureX + 4;
                    default:
                        return TextureX;
                }
            }
        }
        public int TextureRealY { get
            {
                switch (SpreadType)
                {
                    case 2:
                    case 5:
                        return TextureY + 1;
                    default:
                        return TextureY;
                }
            } }

    }
    public class ItemDecoder
    {
        private static readonly string key = "PBG892FXX982ABC*";
        private Stream stream;
        public ItemDecoder(Stream str)
        {
            stream = str;
        }
        private byte[] ReadBytes(int count)
        {
            byte[] res = new byte[count];
            stream.Read(res, 0, count);
            return res;
        }
        private string ReadString()
        {
            short length = BitConverter.ToInt16(ReadBytes(2), 0);
            StringBuilder res = new StringBuilder(length);
            for (int i = 0; i < length; i++)
                res.Append(char.ConvertFromUtf32(stream.ReadByte()));
            return res.ToString();
        }

        private byte ReadByte() =>
            (byte)stream.ReadByte();

        private short ReadInt16(int count = 2) =>
            BitConverter.ToInt16(ReadBytes(count), 0);

        private int ReadInt32(int count = 4) =>
            BitConverter.ToInt32(ReadBytes(count), 0);

        public DecodedItem DecodeItem()
        {
            DecodedItem res = new DecodedItem();
            short length = 0;
            byte[] buffer;

            res.ItemID = ReadInt32();
            res.EditableType = ReadByte();
            res.ItemCategory = ReadByte();
            res.ActionType = ReadByte();
            res.HitSoundType = ReadByte();

            // Luckily, only item name is encrypted this way
            length = BitConverter.ToInt16(ReadBytes(2), 0);
            buffer = new byte[length];
            for (int i = 0; i < length; i++)
                buffer[i] = (byte)(stream.ReadByte() ^ (key[(i + res.ItemID) % key.Length]));
            res.Name = Encoding.UTF8.GetString(buffer);

            res.Texture = ReadString();
            res.TextureHash = ReadInt32();
            res.ItemKind = ReadByte();
            res.val1 = ReadInt32();
            res.TextureX = ReadByte();
            res.TextureY = ReadByte();
            res.SpreadType = ReadByte();
            res.IsStripeyWallpaper = ReadByte();
            res.CollisionType = ReadByte();
            res.BreakHits = ReadByte();
            res.DropChance = ReadInt32();
            res.ClothingType = ReadByte();
            res.Rarity = ReadInt16();
            res.MaxAmount = ReadByte();
            res.ExtraFile = ReadString();
            res.ExtraFileHash = ReadInt32();
            res.AudioVolume = ReadInt32();
            res.PetName = ReadString();
            res.PetPrefix = ReadString();
            res.PetSuffix = ReadString();
            res.PetAbility = ReadString();
            res.SeedBase = ReadByte();
            res.SeedOverlay = ReadByte();
            res.TreeBase = ReadByte();
            res.TreeLeaves = ReadByte();
            res.SeedColor = ReadInt32();
            res.SeedOverlayColor = ReadInt32();
            stream.Position += 4; // Alledgedly, removed recipe data
            res.GrowTime = ReadInt32();
            res.val2 = ReadInt16();
            res.IsRayman = ReadInt16();
            res.ExtraOptions = ReadString();
            res.Texture2 = ReadString();
            res.ExtraOptions2 = ReadString();
            stream.Position += 80;
            res.PunchOptions = ReadString();
            stream.Position += 13; // ???
            stream.Position += 4; // ???

            return res;
        }

        public (short Version, int ItemCount) ReadHeader() =>
            (ReadInt16(), ReadInt32());
    }
}