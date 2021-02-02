using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Taito_Compress
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
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
                    string saveFilePath = Path.ChangeExtension(@selectFileDialog.FileName, ".cmp");

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
    }
}