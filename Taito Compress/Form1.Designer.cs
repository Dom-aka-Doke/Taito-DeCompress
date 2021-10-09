
namespace Taito_DeCompress
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.buttonCompress = new System.Windows.Forms.Button();
            this.buttonDecompress = new System.Windows.Forms.Button();
            this.buttonAbout = new System.Windows.Forms.Button();
            this.buttonInsert = new System.Windows.Forms.Button();
            this.buttonLoadROM = new System.Windows.Forms.Button();
            this.buttonLoadOffsetFile = new System.Windows.Forms.Button();
            this.checkBoxBackup = new System.Windows.Forms.CheckBox();
            this.checkBoxOverwrite = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonCompress
            // 
            this.buttonCompress.Enabled = false;
            this.buttonCompress.Location = new System.Drawing.Point(12, 94);
            this.buttonCompress.Name = "buttonCompress";
            this.buttonCompress.Size = new System.Drawing.Size(273, 23);
            this.buttonCompress.TabIndex = 0;
            this.buttonCompress.Text = "Compress";
            this.buttonCompress.UseVisualStyleBackColor = true;
            this.buttonCompress.Click += new System.EventHandler(this.buttonCompress_Click);
            // 
            // buttonDecompress
            // 
            this.buttonDecompress.Enabled = false;
            this.buttonDecompress.Location = new System.Drawing.Point(12, 65);
            this.buttonDecompress.Name = "buttonDecompress";
            this.buttonDecompress.Size = new System.Drawing.Size(273, 23);
            this.buttonDecompress.TabIndex = 1;
            this.buttonDecompress.Text = "Decompress";
            this.buttonDecompress.UseVisualStyleBackColor = true;
            this.buttonDecompress.Click += new System.EventHandler(this.buttonDecompress_Click);
            // 
            // buttonAbout
            // 
            this.buttonAbout.Location = new System.Drawing.Point(12, 151);
            this.buttonAbout.Name = "buttonAbout";
            this.buttonAbout.Size = new System.Drawing.Size(273, 23);
            this.buttonAbout.TabIndex = 2;
            this.buttonAbout.Text = "About";
            this.buttonAbout.UseVisualStyleBackColor = true;
            this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
            // 
            // buttonInsert
            // 
            this.buttonInsert.Enabled = false;
            this.buttonInsert.Location = new System.Drawing.Point(12, 123);
            this.buttonInsert.Name = "buttonInsert";
            this.buttonInsert.Size = new System.Drawing.Size(273, 22);
            this.buttonInsert.TabIndex = 7;
            this.buttonInsert.Text = "Insert";
            this.buttonInsert.UseVisualStyleBackColor = true;
            this.buttonInsert.Click += new System.EventHandler(this.buttonInsert_Click);
            // 
            // buttonLoadROM
            // 
            this.buttonLoadROM.Image = ((System.Drawing.Image)(resources.GetObject("buttonLoadROM.Image")));
            this.buttonLoadROM.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonLoadROM.Location = new System.Drawing.Point(12, 12);
            this.buttonLoadROM.Name = "buttonLoadROM";
            this.buttonLoadROM.Size = new System.Drawing.Size(130, 24);
            this.buttonLoadROM.TabIndex = 4;
            this.buttonLoadROM.Text = "Load ROM";
            this.buttonLoadROM.UseVisualStyleBackColor = true;
            this.buttonLoadROM.Click += new System.EventHandler(this.buttonLoadROM_Click);
            // 
            // buttonLoadOffsetFile
            // 
            this.buttonLoadOffsetFile.Enabled = false;
            this.buttonLoadOffsetFile.Image = global::Taito_Compress.Properties.Resources.table;
            this.buttonLoadOffsetFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonLoadOffsetFile.Location = new System.Drawing.Point(155, 12);
            this.buttonLoadOffsetFile.Name = "buttonLoadOffsetFile";
            this.buttonLoadOffsetFile.Size = new System.Drawing.Size(130, 24);
            this.buttonLoadOffsetFile.TabIndex = 8;
            this.buttonLoadOffsetFile.Text = "Load offset table";
            this.buttonLoadOffsetFile.UseVisualStyleBackColor = true;
            this.buttonLoadOffsetFile.Click += new System.EventHandler(this.buttonLoadOffsetFile_Click);
            // 
            // checkBoxBackup
            // 
            this.checkBoxBackup.AutoSize = true;
            this.checkBoxBackup.Location = new System.Drawing.Point(12, 42);
            this.checkBoxBackup.Name = "checkBoxBackup";
            this.checkBoxBackup.Size = new System.Drawing.Size(91, 17);
            this.checkBoxBackup.TabIndex = 9;
            this.checkBoxBackup.Text = "Backup ROM";
            this.checkBoxBackup.UseVisualStyleBackColor = true;
            // 
            // checkBoxOverwrite
            // 
            this.checkBoxOverwrite.Location = new System.Drawing.Point(119, 38);
            this.checkBoxOverwrite.Name = "checkBoxOverwrite";
            this.checkBoxOverwrite.Size = new System.Drawing.Size(166, 24);
            this.checkBoxOverwrite.TabIndex = 10;
            this.checkBoxOverwrite.Text = "Overwrite Files without asking";
            this.checkBoxOverwrite.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 182);
            this.Controls.Add(this.checkBoxOverwrite);
            this.Controls.Add(this.checkBoxBackup);
            this.Controls.Add(this.buttonLoadOffsetFile);
            this.Controls.Add(this.buttonInsert);
            this.Controls.Add(this.buttonLoadROM);
            this.Controls.Add(this.buttonAbout);
            this.Controls.Add(this.buttonDecompress);
            this.Controls.Add(this.buttonCompress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "TAITO DeCompress v1.0";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCompress;
        private System.Windows.Forms.Button buttonDecompress;
        private System.Windows.Forms.Button buttonAbout;
        private System.Windows.Forms.Button buttonLoadROM;
        private System.Windows.Forms.Button buttonInsert;
        private System.Windows.Forms.Button buttonLoadOffsetFile;
        private System.Windows.Forms.CheckBox checkBoxBackup;
        private System.Windows.Forms.CheckBox checkBoxOverwrite;
    }
}

