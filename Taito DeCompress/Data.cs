using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Taito_DeCompress
{
    class Data
    {
        public static void Decompress(ref byte[] rom, List<int> offsets, string romFilePath, bool overwrite)
        {
            string saveDir = Path.Combine(Path.GetDirectoryName(romFilePath), Path.GetFileNameWithoutExtension(romFilePath), "_decompressed");

            if (!Directory.Exists(saveDir))
            {
                Directory.CreateDirectory(saveDir);
            }

            foreach (int offset in offsets)
            {
                List<byte> decompressedByteCodeList = new List<byte>();

                int bytePosition = offset;
                int startOffset = offset;

                // Get number of 32 byte blocks
                byte[] byteNumberOfBlocks = { rom[bytePosition], rom[bytePosition + 1] };
                int numberOfBlocks = BitConverter.ToInt16(byteNumberOfBlocks, 0);
                bytePosition += 2;

                // Generate decompressed byte code
                for (int i = 0; i < numberOfBlocks; i++)
                {
                    // Get addresses of most common byte
                    byte[] mostCommonByteAddresses = new byte[4];
                    Buffer.BlockCopy(rom, bytePosition, mostCommonByteAddresses, 0, 4);
                    bytePosition += 4;

                    // Reverse mostCommonByteAddresses because BitArray is little endian
                    mostCommonByteAddresses[0] = reverseByte(mostCommonByteAddresses[0]);
                    mostCommonByteAddresses[1] = reverseByte(mostCommonByteAddresses[1]);
                    mostCommonByteAddresses[2] = reverseByte(mostCommonByteAddresses[2]);
                    mostCommonByteAddresses[3] = reverseByte(mostCommonByteAddresses[3]);

                    // Create BitArray for processing generation of decompressed byte code
                    BitArray mostCommonByteAddressesBitArray = new BitArray(mostCommonByteAddresses);

                    // Get most common byte
                    byte[] mostCommonByte = { 0x00 };
                    Buffer.BlockCopy(rom, bytePosition, mostCommonByte, 0, 1);
                    bytePosition++;

                    // Generate decompressed data
                    foreach (bool b in mostCommonByteAddressesBitArray)
                    {
                        if (b)
                        {
                            decompressedByteCodeList.Add(mostCommonByte[0]);
                        }

                        else
                        {
                            decompressedByteCodeList.Add(rom[bytePosition]);
                            bytePosition++;
                        }
                    }
                }

                int cmpSize = bytePosition - startOffset;
                string saveFileName = "0x" + offset.ToString("X6") + "_decompressed_" + Path.GetFileNameWithoutExtension(romFilePath).Replace("_", "-") + "_[" + cmpSize + "].dec";
                string saveFilePath = Path.Combine(saveDir, saveFileName);

                // Check if file already exists
                if (File.Exists(saveFilePath) && !overwrite)
                {
                    DialogResult fileExistsDialogue = MessageBox.Show(saveFilePath + " already exisits!\n\nDo you want to proceed and overwrite this file?", "Attention!", MessageBoxButtons.YesNo);

                    if (fileExistsDialogue == DialogResult.No)
                    {
                        continue;
                    }
                }

                // Write compressed byte code to file
                byte[] decompressedByteCode = decompressedByteCodeList.ToArray();
                File.WriteAllBytes(saveFilePath, decompressedByteCode);
            }

            MessageBox.Show("Successfully decompressed!\nFiles have been saved to: " + saveDir);
        }

        public static void Compress(string romFilePath, List<int> offsets, bool overwrite)
        {
            string decompDir = Path.Combine(Path.GetDirectoryName(romFilePath), Path.GetFileNameWithoutExtension(romFilePath), "_decompressed");

            if (Directory.Exists(decompDir))
            {
                string[] files = Directory.GetFiles(decompDir);
                List<string> decFiles = new List<string>();
                List<string> offsetList = new List<string>();

                foreach(int offset in offsets)
                {
                    offsetList.Add("0x" + offset.ToString("X6"));
                }

                foreach (string file in files)
                {
                    string fileName = Path.GetFileName(file);

                    if (offsetList.Contains(fileName.Substring(0, 8)) && fileName.Contains("_decompressed_") && fileName.Contains("[") && fileName.Contains("]"))
                    {
                        decFiles.Add(file);
                    }
                }

                if (decFiles.Any())
                {
                    string saveDir = Path.Combine(Path.GetDirectoryName(romFilePath), Path.GetFileNameWithoutExtension(romFilePath), "_compressed");

                    if (!Directory.Exists(saveDir))
                    {
                        Directory.CreateDirectory(saveDir);
                    }

                    foreach (string decFilePath in decFiles)
                    {
                        byte[] decFile = File.ReadAllBytes(decFilePath);

                        if (decFile.Length % 32 == 0 && decFile.Length <= 2097152)
                        {
                            int blocks = decFile.Length / 32;
                            List<byte> compressedByteCodeList = new List<byte>();
                            byte[] numberOfBlocks = BitConverter.GetBytes(Convert.ToInt16(blocks));

                            string saveFileName = Path.GetFileNameWithoutExtension(decFilePath).Replace("_decompressed_", "_compressed_");
                            string saveFileExt = ".cmp";
                            string saveFilePath = Path.Combine(saveDir, saveFileName + saveFileExt);

                            if (!BitConverter.IsLittleEndian) { Array.Reverse(numberOfBlocks); }

                            compressedByteCodeList.Add(numberOfBlocks[0]);
                            compressedByteCodeList.Add(numberOfBlocks[1]);

                            for (int i = 0; i < blocks; i++)
                            {
                                byte[] block = new byte[32];
                                List<byte> byteList = new List<byte>();
                                Buffer.BlockCopy(decFile, i * 32, block, 0, 32);
                                byte mostCommonByte = 0x00;
                                BitArray mostCommonByteAddressesBitArray = new BitArray(32);
                                byte[] mostCommonByteAddresses = new byte[4];
                                List<byte> compressedBlock = new List<byte>();
                                int score = 0;

                                // Get most common byte in block
                                foreach (byte b in block)
                                {
                                    if (!byteList.Contains(b))
                                    {
                                        byteList.Add(b);
                                        int byteCtr = 0;

                                        foreach (byte c in block)
                                        {
                                            if (c == b) { byteCtr++; }
                                        }

                                        if (byteCtr > score) { mostCommonByte = b; score = byteCtr; }
                                    }
                                }

                                // Get addresses of most common byte and generate compressed data
                                int bCtr = 0;

                                foreach (byte b in block)
                                {
                                    if (b != mostCommonByte)
                                    {
                                        mostCommonByteAddressesBitArray.Set(bCtr, false);
                                        compressedBlock.Add(b);
                                    }

                                    else
                                    {
                                        mostCommonByteAddressesBitArray.Set(bCtr, true);
                                    }

                                    bCtr++;
                                }

                                // Convert BitArray to byte array
                                int bitCtr = 0;
                                int bitShift = 0;

                                foreach (bool b in mostCommonByteAddressesBitArray)
                                {
                                    if (b)
                                    {
                                        mostCommonByteAddresses[(bitCtr / 8)] |= (byte)(1 << (7 - bitShift));
                                    }

                                    bitCtr++;
                                    bitShift++;

                                    if (bitShift > 7)
                                    {
                                        bitShift = 0;
                                    }
                                }

                                // Add mostCommonByteAddresses, mostCommonByte and compressedBlock to compressedByteCode
                                foreach (byte b in mostCommonByteAddresses) { compressedByteCodeList.Add(b); }
                                compressedByteCodeList.Add(mostCommonByte);
                                foreach (byte b in compressedBlock) { compressedByteCodeList.Add(b); }
                            }

                            // Check if file already exists
                            if (File.Exists(saveFilePath) && !overwrite)
                            {
                                DialogResult fileExistsDialogue = MessageBox.Show(saveFilePath + " already exisits!\n\nDo you want to proceed and overwrite this file?", "Attention!", MessageBoxButtons.YesNo);

                                if (fileExistsDialogue == DialogResult.No)
                                {
                                    continue;
                                }
                            }

                            // Write compressed byte code to file
                            byte[] compressedByteCode = compressedByteCodeList.ToArray();

                            File.WriteAllBytes(saveFilePath, compressedByteCode);
                        }

                        else
                        {
                            MessageBox.Show("Only decompressed files up to 2 MB are supported.");
                        }
                    }

                    MessageBox.Show("Successfully compressed!\n\nFiles have been saved to: " + saveDir);
                }

                else
                {
                    MessageBox.Show("No decompressed files were found.\n\nPlease decompress files first and edit them before compressing again.");
                }
            }
        }

        public static void Insert(ref byte[] rom, List<int> offsets, string romFilePath)
        {
            string cmpDir = Path.Combine(Path.GetDirectoryName(romFilePath), Path.GetFileNameWithoutExtension(romFilePath), "_compressed");

            if (Directory.Exists(cmpDir))
            {
                string[] files = Directory.GetFiles(cmpDir);
                List<string> cmpFiles = new List<string>();
                IDictionary<int, string> offsetKeyValue = new Dictionary<int, string>();
                int errorCtr = 0;

                foreach (int offset in offsets)
                {
                    offsetKeyValue.Add(offset, "0x" + offset.ToString("X6"));
                }

                foreach (string file in files)
                {
                    string fileName = Path.GetFileNameWithoutExtension(file);

                    if (offsetKeyValue.Values.Contains(fileName.Substring(0, 8)) && fileName.Contains("_compressed_") && fileName.Contains("[") && fileName.Contains("]"))
                    {
                        int cmpSize = 0;
                        string[] fileNameSplitUndersore = fileName.Split('_');

                        if (int.TryParse(fileNameSplitUndersore[3].Replace("[", "").Replace("]", ""), out cmpSize))
                        {
                            byte[] cmpFile = File.ReadAllBytes(file);

                            if (cmpFile.Length <= cmpSize)
                            {
                                var offsetKeyValuePair = offsetKeyValue.FirstOrDefault(t => t.Value == fileName.Substring(0, 8));
                                Buffer.BlockCopy(cmpFile, 0, rom, offsetKeyValuePair.Key, cmpFile.Length);

                                cmpFiles.Add(file);
                            }

                            else
                            {
                                if (!Directory.Exists(Path.Combine(Path.GetDirectoryName(romFilePath), Path.GetFileNameWithoutExtension(romFilePath))))
                                {
                                    Directory.CreateDirectory(Path.Combine(Path.GetDirectoryName(romFilePath), Path.GetFileNameWithoutExtension(romFilePath)));
                                }

                                File.AppendAllText(Path.Combine(Path.GetDirectoryName(romFilePath), Path.GetFileNameWithoutExtension(romFilePath), "errorlog.log"), $"[{DateTime.Now}] Failed to insert '{Path.GetFileName(file)}' into ROM. The compressed file is larger than before and has to be inserted manually." + Environment.NewLine);
                                errorCtr++;
                            }
                        }
                    }
                }

                if (cmpFiles.Any())
                {
                    File.WriteAllBytes(romFilePath, rom);
                    
                    if (errorCtr > 0)
                    {
                        MessageBox.Show("Some errors occured while inserting compressed data into ROM.\nFor details go to " + Path.Combine(Path.GetDirectoryName(romFilePath), Path.GetFileNameWithoutExtension(romFilePath)) + " and check 'errorlog.log'.\n\nFile has been saved to: " + romFilePath);
                    }

                    else
                    {
                        MessageBox.Show("Successfully inserted compressed files into ROM!\n\nFile has been saved to: " + romFilePath);
                    }
                }

                else
                {
                    MessageBox.Show("No compressed files were found.\n\nPlease compress files first and try to insert again.");
                }
            }
        }

        private static byte reverseByte(byte b)
        {
            int rByte = 0;

            for (int i = 0; i < 8; i++)
            {
                if ((b & (1 << i)) != 0)
                {
                    rByte |= 1 << (7 - i);
                }
            }

            return (byte)rByte;
        }
    }
}