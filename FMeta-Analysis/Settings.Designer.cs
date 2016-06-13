namespace FMeta_Analysis
{
    partial class Settings
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
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.accept = new System.Windows.Forms.Button();
            this.restoreDef = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.autosaveInterval = new System.Windows.Forms.NumericUpDown();
            this.foreColor = new System.Windows.Forms.PictureBox();
            this.commandColor = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.savePath = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.autosaveInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.foreColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandColor)).BeginInit();
            this.SuspendLayout();
            // 
            // accept
            // 
            this.accept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.accept.Location = new System.Drawing.Point(124, 145);
            this.accept.Name = "accept";
            this.accept.Size = new System.Drawing.Size(99, 23);
            this.accept.TabIndex = 0;
            this.accept.Text = "Сохранить";
            this.accept.UseVisualStyleBackColor = true;
            this.accept.Click += new System.EventHandler(this.accept_Click);
            // 
            // restoreDef
            // 
            this.restoreDef.Location = new System.Drawing.Point(12, 145);
            this.restoreDef.Name = "restoreDef";
            this.restoreDef.Size = new System.Drawing.Size(99, 23);
            this.restoreDef.TabIndex = 1;
            this.restoreDef.Text = "По умолчанию";
            this.restoreDef.UseVisualStyleBackColor = true;
            this.restoreDef.Click += new System.EventHandler(this.restoreDef_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Цвет шрифта:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Цвет шрифта команд:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Период автосохранений(мин.):";
            // 
            // autosaveInterval
            // 
            this.autosaveInterval.Location = new System.Drawing.Point(176, 63);
            this.autosaveInterval.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.autosaveInterval.Name = "autosaveInterval";
            this.autosaveInterval.Size = new System.Drawing.Size(46, 20);
            this.autosaveInterval.TabIndex = 6;
            // 
            // foreColor
            // 
            this.foreColor.Location = new System.Drawing.Point(176, 9);
            this.foreColor.Name = "foreColor";
            this.foreColor.Size = new System.Drawing.Size(29, 21);
            this.foreColor.TabIndex = 7;
            this.foreColor.TabStop = false;
            this.foreColor.Click += new System.EventHandler(this.foreColor_Click);
            // 
            // commandColor
            // 
            this.commandColor.Location = new System.Drawing.Point(176, 36);
            this.commandColor.Name = "commandColor";
            this.commandColor.Size = new System.Drawing.Size(29, 21);
            this.commandColor.TabIndex = 7;
            this.commandColor.TabStop = false;
            this.commandColor.Click += new System.EventHandler(this.commandColor_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Путь к сохранениям:";
            // 
            // savePath
            // 
            this.savePath.Location = new System.Drawing.Point(11, 107);
            this.savePath.Name = "savePath";
            this.savePath.ReadOnly = true;
            this.savePath.Size = new System.Drawing.Size(208, 20);
            this.savePath.TabIndex = 9;
            this.savePath.Click += new System.EventHandler(this.savePath_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 180);
            this.Controls.Add(this.savePath);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.commandColor);
            this.Controls.Add(this.foreColor);
            this.Controls.Add(this.autosaveInterval);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.restoreDef);
            this.Controls.Add(this.accept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Settings";
            this.Text = "Настройки";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.autosaveInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.foreColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandColor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button accept;
        private System.Windows.Forms.Button restoreDef;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown autosaveInterval;
        private System.Windows.Forms.PictureBox foreColor;
        private System.Windows.Forms.PictureBox commandColor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox savePath;
    }
}