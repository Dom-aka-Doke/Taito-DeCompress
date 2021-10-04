namespace Taito_DeCompress
{
    partial class FormAbout
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
            this.labelAboutCreators = new System.Windows.Forms.Label();
            this.labelAboutDisclaimer = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelAboutCreators
            // 
            this.labelAboutCreators.AutoSize = true;
            this.labelAboutCreators.Location = new System.Drawing.Point(12, 74);
            this.labelAboutCreators.Name = "labelAboutCreators";
            this.labelAboutCreators.Size = new System.Drawing.Size(127, 26);
            this.labelAboutCreators.TabIndex = 5;
            this.labelAboutCreators.Text = "Hacking by Svambo\r\nCoded by Dom aka Doke";
            // 
            // labelAboutDisclaimer
            // 
            this.labelAboutDisclaimer.AutoSize = true;
            this.labelAboutDisclaimer.Location = new System.Drawing.Point(12, 9);
            this.labelAboutDisclaimer.Name = "labelAboutDisclaimer";
            this.labelAboutDisclaimer.Size = new System.Drawing.Size(429, 52);
            this.labelAboutDisclaimer.TabIndex = 4;
            this.labelAboutDisclaimer.Text = resources.GetString("labelAboutDisclaimer.Text");
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 112);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(438, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 147);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelAboutCreators);
            this.Controls.Add(this.labelAboutDisclaimer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAbout";
            this.Text = "About TAITO DeCompress";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelAboutCreators;
        private System.Windows.Forms.Label labelAboutDisclaimer;
        private System.Windows.Forms.Button button1;
    }
}