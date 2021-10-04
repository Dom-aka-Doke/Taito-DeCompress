using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Taito_DeCompress
{
    public partial class Form1 : Form
    {
        byte[] rom;
        string romFilePath;
        List<int> offsets;

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonCompress_Click(object sender, EventArgs e)
        {
            Data.Compress(romFilePath, offsets);
            if (!buttonInsert.Enabled) { buttonInsert.Enabled = true; }
        }

        private void buttonDecompress_Click(object sender, EventArgs e)
        {
            Data.Decompress(ref rom, offsets, romFilePath);
            if (!buttonCompress.Enabled) { buttonCompress.Enabled = true; }
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            Data.Insert(ref rom, offsets, romFilePath);
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

                buttonLoadOffsetFile.Enabled = true;
            }
        }

        private void buttonLoadOffsetFile_Click(object sender, EventArgs e)
        {
            // Select file dialogue
            OpenFileDialog selectFileDialog = new OpenFileDialog();

            selectFileDialog.Filter = "Offset table (*.txt)|*.txt;|" +
                                      "All Files (*.*)|*.*";

            // If successfully selected a file...
            if (selectFileDialog.ShowDialog() == DialogResult.OK)
            {
                offsets = new List<int>();
                string[] lines = File.ReadAllLines(@selectFileDialog.FileName);

                int lineNo = 1;
                int errorCtr = 0;

                foreach(string line in lines)
                {
                    try
                    {
                        offsets.Add(Convert.ToInt32(line.ToLower().Split(';')[0].Split('x')[1].Trim(), 16));
                    }

                    catch
                    {
                        File.AppendAllText(Path.GetDirectoryName(@selectFileDialog.FileName) + @"\errorlog.log",$"[{DateTime.Now}] Failed to parse line {lineNo} into hexadecimal offset. Please use following notation: 0x012DEF. You are allowed to comment by using ;" + Environment.NewLine);
                        errorCtr++;
                    }

                    lineNo++;
                }

                if (errorCtr > 0)
                {
                    MessageBox.Show("Some errors occured while reading offset file.\nFor details go to " + Path.GetFullPath(@selectFileDialog.FileName) + " and check 'errorlog.log'.");
                }

                buttonDecompress.Enabled = true;

                if(Directory.Exists(Path.GetDirectoryName(@selectFileDialog.FileName) + @"\_decompressed"))
                {
                    buttonCompress.Enabled = true;
                    buttonInsert.Enabled = true;
                }
            }
        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            FormAbout formAbout = new FormAbout();
            formAbout.Show();
        }
    }
}