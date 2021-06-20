using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.IO;
using System.IO.Compression;

using static RTPackConverter.Constants;

namespace RTPackConverter
{
    public static class TextureDecoder
    {
        /// <summary>
        /// Converts a RTPack File to a Bitmap by file name. Returns false if invalid file.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static Bitmap ConvertRTPACKFile(string filename)
        {
            if (!File.Exists(filename))
                return new Bitmap(32, 32);
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                BinaryReader br = new BinaryReader(fs);

                /*
                 * Main File Header.
                 * - RTTXTR -> Texture File.
                 * - RTFONT -> Font File.
                 * - RTPACK -> One of them, recompressed after output. (RTPack.exe ".rtfont/.rttex")
                 * 
                 * */
                string rtpack_magic = new string(br.ReadChars(6));
                if (String.Equals(rtpack_magic, "RTTXTR"))
                {
                    return new RTTEX(br).texture;
                }
                else if (String.Equals(rtpack_magic, "RTFONT"))
                {
                    throw new FileFormatException("RTFONT file selected.");
                }
                else if (String.Equals(rtpack_magic, "RTPACK"))
                {
                    byte version = br.ReadByte();
                    byte reserved = br.ReadByte();

                    //RTPACK Header (24 bytes)
                    uint compressedSize = br.ReadUInt32();
                    uint decompressedSize = br.ReadUInt32();

                    eCompressionType compressionType = (eCompressionType)br.ReadByte();

                    fs.Seek(15, SeekOrigin.Current);

                    //Zlib Magic header (78 9C), IO.Compression doesn't want it for deflate so just skip it
                    fs.ReadByte();
                    fs.ReadByte();

                    //RTFONT Header
                    using (MemoryStream ms = new MemoryStream())
                    {
                        if (compressionType == eCompressionType.C_COMPRESSION_ZLIB)
                        {
                            using (DeflateStream zs = new DeflateStream(fs, CompressionMode.Decompress))
                            {
                                zs.CopyTo(ms);
                            }
                            ms.Position = 0;
                        }
                        else
                        {
                            fs.CopyTo(ms);
                            ms.Position = 0;
                        }

                        BinaryReader bdr = new BinaryReader(ms);

                        //RTFile Header again (8 bytes)
                        string decomp_magic = new string(bdr.ReadChars(6));
                        if (String.Equals(decomp_magic, "RTTXTR"))
                        {
                            return new RTTEX(bdr).texture;
                        }
                        else if (String.Equals(decomp_magic, "RTFONT"))
                        {
                            throw new FileFormatException("RTFONT file selected.");
                        }
                    }
                }
                else
                {
                    throw new FileFormatException("Not a RTPACK file.");
                }
            }

            return null;
        }
    }
}



