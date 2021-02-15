
namespace Taito_Compress
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
            this.textBoxDecOffset = new System.Windows.Forms.TextBox();
            this.textBoxInsertOffset = new System.Windows.Forms.TextBox();
            this.buttonInsert = new System.Windows.Forms.Button();
            this.buttonLoadCMP = new System.Windows.Forms.Button();
            this.buttonLoadROM = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonCompress
            // 
            this.buttonCompress.Location = new System.Drawing.Point(12, 41);
            this.buttonCompress.Name = "buttonCompress";
            this.buttonCompress.Size = new System.Drawing.Size(301, 23);
            this.buttonCompress.TabIndex = 0;
            this.buttonCompress.Text = "Compress file";
            this.buttonCompress.UseVisualStyleBackColor = true;
            this.buttonCompress.Click += new System.EventHandler(this.buttonCompress_Click);
            // 
            // buttonDecompress
            // 
            this.buttonDecompress.Location = new System.Drawing.Point(12, 12);
            this.buttonDecompress.Name = "buttonDecompress";
            this.buttonDecompress.Size = new System.Drawing.Size(228, 23);
            this.buttonDecompress.TabIndex = 1;
            this.buttonDecompress.Text = "Decompress file";
            this.buttonDecompress.UseVisualStyleBackColor = true;
            this.buttonDecompress.Click += new System.EventHandler(this.buttonDecompress_Click);
            // 
            // buttonAbout
            // 
            this.buttonAbout.Location = new System.Drawing.Point(12, 98);
            this.buttonAbout.Name = "buttonAbout";
            this.buttonAbout.Size = new System.Drawing.Size(301, 23);
            this.buttonAbout.TabIndex = 2;
            this.buttonAbout.Text = "About";
            this.buttonAbout.UseVisualStyleBackColor = true;
            this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
            // 
            // textBoxDecOffset
            // 
            this.textBoxDecOffset.Location = new System.Drawing.Point(246, 14);
            this.textBoxDecOffset.Name = "textBoxDecOffset";
            this.textBoxDecOffset.Size = new System.Drawing.Size(67, 20);
            this.textBoxDecOffset.TabIndex = 3;
            this.textBoxDecOffset.Text = "0x000000";
            // 
            // textBoxInsertOffset
            // 
            this.textBoxInsertOffset.Location = new System.Drawing.Point(246, 73);
            this.textBoxInsertOffset.Name = "textBoxInsertOffset";
            this.textBoxInsertOffset.ReadOnly = true;
            this.textBoxInsertOffset.Size = new System.Drawing.Size(67, 20);
            this.textBoxInsertOffset.TabIndex = 6;
            this.textBoxInsertOffset.Text = "0x000000";
            // 
            // buttonInsert
            // 
            this.buttonInsert.Location = new System.Drawing.Point(144, 70);
            this.buttonInsert.Name = "buttonInsert";
            this.buttonInsert.Size = new System.Drawing.Size(96, 22);
            this.buttonInsert.TabIndex = 7;
            this.buttonInsert.Text = "Insert";
            this.buttonInsert.UseVisualStyleBackColor = true;
            this.buttonInsert.Click += new System.EventHandler(this.buttonInsert_Click);
            // 
            // buttonLoadCMP
            // 
            this.buttonLoadCMP.Image = global::Taito_Compress.Properties.Resources.open;
            this.buttonLoadCMP.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonLoadCMP.Location = new System.Drawing.Point(78, 70);
            this.buttonLoadCMP.Name = "buttonLoadCMP";
            this.buttonLoadCMP.Size = new System.Drawing.Size(60, 22);
            this.buttonLoadCMP.TabIndex = 5;
            this.buttonLoadCMP.Text = "CMP";
            this.buttonLoadCMP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonLoadCMP.UseVisualStyleBackColor = true;
            this.buttonLoadCMP.Click += new System.EventHandler(this.buttonLoadCMP_Click);
            // 
            // buttonLoadROM
            // 
            this.buttonLoadROM.Image = global::Taito_Compress.Properties.Resources.open;
            this.buttonLoadROM.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonLoadROM.Location = new System.Drawing.Point(12, 70);
            this.buttonLoadROM.Name = "buttonLoadROM";
            this.buttonLoadROM.Size = new System.Drawing.Size(60, 22);
            this.buttonLoadROM.TabIndex = 4;
            this.buttonLoadROM.Text = "ROM";
            this.buttonLoadROM.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonLoadROM.UseVisualStyleBackColor = true;
            this.buttonLoadROM.Click += new System.EventHandler(this.buttonLoadROM_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 131);
            this.Controls.Add(this.buttonInsert);
            this.Controls.Add(this.textBoxInsertOffset);
            this.Controls.Add(this.buttonLoadCMP);
            this.Controls.Add(this.buttonLoadROM);
            this.Controls.Add(this.textBoxDecOffset);
            this.Controls.Add(this.buttonAbout);
            this.Controls.Add(this.buttonDecompress);
            this.Controls.Add(this.buttonCompress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "TAITO Compress v1.0";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCompress;
        private System.Windows.Forms.Button buttonDecompress;
        private System.Windows.Forms.Button buttonAbout;
        private System.Windows.Forms.TextBox textBoxDecOffset;
        private System.Windows.Forms.Button buttonLoadROM;
        private System.Windows.Forms.Button buttonLoadCMP;
        private System.Windows.Forms.TextBox textBoxInsertOffset;
        private System.Windows.Forms.Button buttonInsert;
    }
}

