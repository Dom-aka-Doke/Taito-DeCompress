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
        string offsetFilePath;

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonCompress_Click(object sender, EventArgs e)
        {
            Data.Compress(romFilePath, offsets, checkBoxOverwrite.Checked);
            if (!buttonInsert.Enabled) { buttonInsert.Enabled = true; }
        }

        private void buttonDecompress_Click(object sender, EventArgs e)
        {
            Data.Decompress(ref rom, offsets, romFilePath, checkBoxOverwrite.Checked);
            if (!buttonCompress.Enabled) { buttonCompress.Enabled = true; }
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            if (checkBoxBackup.Checked && !File.Exists(romFilePath + ".bak"))
            {
                File.WriteAllBytes(romFilePath + ".bak", rom);
            }

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

                if (checkBoxBackup.Checked && !File.Exists(romFilePath + ".bak"))
                {
                    File.WriteAllBytes(romFilePath + ".bak", rom);
                }

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
                offsetFilePath = @selectFileDialog.FileName;
                offsets = new List<int>();
                string[] lines = File.ReadAllLines(offsetFilePath);

                int lineNo = 1;
                int errorCtr = 0;

                foreach(string line in lines)
                {
                    if (line.StartsWith(";") || line == string.Empty)
                    {
                        continue;
                    }

                    try
                    {
                        offsets.Add(Convert.ToInt32(line.ToLower().Split(';')[0].Split('x')[1].Trim(), 16));
                    }

                    catch
                    {
                        if (!Directory.Exists(Path.Combine(Path.GetDirectoryName(romFilePath), Path.GetFileNameWithoutExtension(romFilePath))))
                        {
                            Directory.CreateDirectory(Path.Combine(Path.GetDirectoryName(romFilePath), Path.GetFileNameWithoutExtension(romFilePath)));
                        }

                        File.AppendAllText(Path.Combine(Path.GetDirectoryName(romFilePath), Path.GetFileNameWithoutExtension(romFilePath), "errorlog.log"), $"[{DateTime.Now}] Failed to parse line {lineNo} into hexadecimal offset. Please use following notation: 0x012DEF. You are allowed to comment by using ;" + Environment.NewLine);
                        errorCtr++;
                    }

                    lineNo++;
                }

                if (errorCtr > 0)
                {
                    MessageBox.Show("Some errors occured while reading offset file.\nFor details go to " + Path.Combine(Path.GetDirectoryName(romFilePath), Path.GetFileNameWithoutExtension(romFilePath)) + " and check 'errorlog.log'.");
                }

                buttonDecompress.Enabled = true;
                
                if (Directory.Exists(Path.Combine(Path.GetDirectoryName(romFilePath), Path.GetFileNameWithoutExtension(romFilePath), "_decompressed")))
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

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}