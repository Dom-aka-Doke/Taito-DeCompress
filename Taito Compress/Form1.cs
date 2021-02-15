using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Taito_Compress
{
    public partial class Form1 : Form
    {
        byte[] rom;
        byte[] cmp;
        string romFilePath;
        string insertFilePath;

        public Form1()
        {
            InitializeComponent();

            buttonInsert.Enabled = false;
            buttonLoadCMP.Enabled = false;
        }

        private void buttonCompress_Click(object sender, EventArgs e)
        {
            // Select file dialogue
            OpenFileDialog selectFileDialog = new OpenFileDialog();

            selectFileDialog.Filter = "Decompressed (*.dec)|*.dec;|" +
                                      "All Files (*.*)|*.*";

            // If successfully selected a file...
            if (selectFileDialog.ShowDialog() == DialogResult.OK)
            {
                string loadFilePath = @selectFileDialog.FileName;
                byte[] decFile = File.ReadAllBytes(loadFilePath);

                if (decFile.Length % 32 == 0 && decFile.Length <= 2097152)
                {
                    int blocks = decFile.Length / 32;
                    List<byte> compressedByteCodeList = new List<byte>();
                    byte[] numberOfBlocks = BitConverter.GetBytes(Convert.ToInt16(blocks));

                    string saveDir = Path.GetDirectoryName(@selectFileDialog.FileName);
                    string saveFileName = Path.GetFileNameWithoutExtension(loadFilePath).Replace("_decompressed_", "_compressed_");
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
                                mostCommonByteAddresses[(bitCtr/8)] |= (byte)(1 << (7 - bitShift));
                            }

                            bitCtr++;
                            bitShift++;

                            if (bitShift > 7)
                            {
                                bitShift = 0;
                            }
                        }

                        // Add mostCommonByteAddresses, mostCommonByte and compressedBlock to compressedByteCode
                        foreach(byte b in mostCommonByteAddresses) { compressedByteCodeList.Add(b); }
                        compressedByteCodeList.Add(mostCommonByte);
                        foreach (byte b in compressedBlock) { compressedByteCodeList.Add(b); }
                    }

                    // Check if file already exists
                    if (File.Exists(saveFilePath))
                    {
                        DialogResult fileExistsDialogue = MessageBox.Show(saveFilePath + " already exisits!\n\nDo you want to proceed an overwrite this file?", "Attention!", MessageBoxButtons.YesNo);

                        if (fileExistsDialogue == DialogResult.No)
                        {
                            return;
                        }
                    }

                    // Write compressed byte code to file
                    byte[] compressedByteCode = compressedByteCodeList.ToArray();

                    File.WriteAllBytes(saveFilePath, compressedByteCode);
                    
                    MessageBox.Show("Successfully compressed!\n\nFile has been saved to: " + saveFilePath);
                }

                else
                {
                    MessageBox.Show("There's something going wrong!\n\nOnly decompressed files up to 2 MB are supported.");
                }
            }
        }

        private void buttonDecompress_Click(object sender, EventArgs e)
        {
            // Select file dialogue
            OpenFileDialog selectFileDialog = new OpenFileDialog();

            selectFileDialog.Filter = "Compressed (*.sfc;*.smc;*.cmp;*.hex)|*.sfc;*.smc;*.cmp;*.hex;|" +
                                      "All Files (*.*)|*.*";

            // If successfully selected a file...
            if (selectFileDialog.ShowDialog() == DialogResult.OK)
            {
                string loadFilePath = @selectFileDialog.FileName;
                byte[] cmpFile = File.ReadAllBytes(loadFilePath);

                List<byte> decompressedByteCodeList = new List<byte>();

                int bytePosition = 0;
                int startOffset = 0;

                if (textBoxDecOffset.Text.StartsWith("0x"))
                {
                    bytePosition = Convert.ToInt32(textBoxDecOffset.Text, 16);
                    startOffset = Convert.ToInt32(textBoxDecOffset.Text, 16);
                }

                // Get number of 32 byte blocks
                byte[] byteNumberOfBlocks = { cmpFile[bytePosition], cmpFile[bytePosition + 1] };
                int numberOfBlocks = BitConverter.ToInt16(byteNumberOfBlocks, 0);
                bytePosition += 2;

                // Generate decompressed byte code
                for (int i = 0; i < numberOfBlocks; i++)
                {
                    // Get addresses of most common byte
                    byte[] mostCommonByteAddresses = new byte[4];
                    Buffer.BlockCopy(cmpFile, bytePosition, mostCommonByteAddresses, 0, 4);
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
                    Buffer.BlockCopy(cmpFile, bytePosition, mostCommonByte, 0, 1);
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
                            decompressedByteCodeList.Add(cmpFile[bytePosition]);
                            bytePosition++;
                        }
                    }
                }

                int cmpSize = bytePosition - startOffset;
                string saveDir = Path.GetDirectoryName(@selectFileDialog.FileName);
                string saveFileName = textBoxDecOffset.Text + "_decompressed_" + Path.GetFileNameWithoutExtension(loadFilePath).Replace("_","-") + "_[" + cmpSize + "]";
                string saveFileExt = ".dec";
                string saveFilePath = Path.Combine(saveDir, saveFileName + saveFileExt);

                // Check if file already exists
                if (File.Exists(saveFilePath))
                {
                    DialogResult fileExistsDialogue = MessageBox.Show(saveFilePath + " already exisits!\n\nDo you want to proceed an overwrite this file?", "Attention!", MessageBoxButtons.YesNo);

                    if (fileExistsDialogue == DialogResult.No)
                    {
                        return;
                    }
                }

                // Write compressed byte code to file
                byte[] decompressedByteCode = decompressedByteCodeList.ToArray();

                File.WriteAllBytes(saveFilePath, decompressedByteCode);

                MessageBox.Show("Successfully decompressed!\n\nFile has been saved to: " + saveFilePath);
            }
        }

        private byte reverseByte(byte b)
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

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            FormAbout formAbout = new FormAbout();
            formAbout.Show();
        }

        private void buttonLoadCMP_Click(object sender, EventArgs e)
        {
            // Select file dialogue
            OpenFileDialog selectFileDialog = new OpenFileDialog();

            selectFileDialog.Filter = "Compressed (*.cmp)|*.cmp;|" +
                                      "All Files (*.*)|*.*";

            // If successfully selected a file...
            if (selectFileDialog.ShowDialog() == DialogResult.OK)
            {
                insertFilePath = @selectFileDialog.FileName;
                cmp = File.ReadAllBytes(insertFilePath);
                int cmpSize = 0;

                string[] splitFilename = Path.GetFileNameWithoutExtension(insertFilePath).Split('_');

                if (splitFilename.Length < 3)
                {
                    MessageBox.Show("Wrong format of file name!\nPlease only load files created by TAITO Compress!");
                    buttonInsert.Enabled = false;
                    return;
                }
                    
                if (splitFilename[0].StartsWith("0x"))
                {
                    textBoxInsertOffset.Text = splitFilename[0];
                }

                else
                {
                    MessageBox.Show("File name contains invalid offset!\nPlease only load files created by TAITO Compress!");
                    buttonInsert.Enabled = false;
                    return;
                }

                if (!int.TryParse(splitFilename[3].Replace("[","").Replace("]",""), out cmpSize))
                {
                    MessageBox.Show("File name contains invalid size for compressed file!\nPlease only load files created by TAITO Compress!");
                    buttonInsert.Enabled = false;
                    return;
                }

                if (cmp.Length > cmpSize)
                {
                    MessageBox.Show("Compressed file is too large for inserting into originally offset!\nPlease insert manually using another offset with enough space!");
                    buttonInsert.Enabled = false;
                    return;
                }

                buttonInsert.Enabled = true;
            }
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            int startOffset = Convert.ToInt32(textBoxInsertOffset.Text, 16);

            Buffer.BlockCopy(cmp, 0, rom, startOffset, cmp.Length);
            File.WriteAllBytes(romFilePath, rom);
            MessageBox.Show("Inserted compressed code successfully into ROM!\n\nFile has been saved to: " + romFilePath);
        }

        private void buttonLoadROM_Click(object sender, EventArgs e)
        {
            // Select file dialogue
            OpenFileDialog selectFileDialog = new OpenFileDialog();

            selectFileDialog.Filter = "ROMs (*.sfc;*.smc)|*.sfc;*.smc;|" +
                                      "All Files (*.*)|*.*";

            // If successfully selected a file...
            if (selectFileDialog.ShowDialog() == DialogResult.OK)
            {
                romFilePath = @selectFileDialog.FileName;
                rom = File.ReadAllBytes(romFilePath);

                buttonLoadCMP.Enabled = true;
            }
        }
    }
}