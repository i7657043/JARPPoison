using System.Drawing;

namespace JArpPoison
{
    partial class MainPage
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
            this.ipTxtBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sendJarpBtn = new System.Windows.Forms.Button();
            this.outputBox = new System.Windows.Forms.RichTextBox();
            this.progressLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.macTxtBox = new System.Windows.Forms.TextBox();
            this.scanBtn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.stopJarpBtn = new System.Windows.Forms.Button();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ipTxtBox
            // 
            this.ipTxtBox.BackColor = System.Drawing.Color.AliceBlue;
            this.ipTxtBox.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ipTxtBox.ForeColor = System.Drawing.Color.Green;
            this.ipTxtBox.Location = new System.Drawing.Point(6, 50);
            this.ipTxtBox.Name = "ipTxtBox";
            this.ipTxtBox.Size = new System.Drawing.Size(113, 20);
            this.ipTxtBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Target IP Address";
            // 
            // sendJarpBtn
            // 
            this.sendJarpBtn.BackColor = System.Drawing.Color.AliceBlue;
            this.sendJarpBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sendJarpBtn.Font = new System.Drawing.Font("Stencil", 14.25F);
            this.sendJarpBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sendJarpBtn.Location = new System.Drawing.Point(142, 28);
            this.sendJarpBtn.Name = "sendJarpBtn";
            this.sendJarpBtn.Size = new System.Drawing.Size(125, 42);
            this.sendJarpBtn.TabIndex = 3;
            this.sendJarpBtn.Text = "Send JArp";
            this.sendJarpBtn.UseVisualStyleBackColor = false;
            this.sendJarpBtn.Click += new System.EventHandler(this.sendJarpBtn_Click);
            // 
            // outputBox
            // 
            this.outputBox.BackColor = System.Drawing.Color.AliceBlue;
            this.outputBox.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputBox.ForeColor = System.Drawing.Color.Green;
            this.outputBox.Location = new System.Drawing.Point(15, 281);
            this.outputBox.Name = "outputBox";
            this.outputBox.ReadOnly = true;
            this.outputBox.Size = new System.Drawing.Size(627, 287);
            this.outputBox.TabIndex = 4;
            this.outputBox.Text = "";
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.BackColor = System.Drawing.Color.Transparent;
            this.progressLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.progressLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.progressLabel.Location = new System.Drawing.Point(12, 263);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(65, 15);
            this.progressLabel.TabIndex = 5;
            this.progressLabel.Text = "Progress...";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(6, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Target MAC Address";
            // 
            // macTxtBox
            // 
            this.macTxtBox.BackColor = System.Drawing.Color.AliceBlue;
            this.macTxtBox.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.macTxtBox.ForeColor = System.Drawing.Color.Green;
            this.macTxtBox.Location = new System.Drawing.Point(6, 100);
            this.macTxtBox.Name = "macTxtBox";
            this.macTxtBox.Size = new System.Drawing.Size(113, 20);
            this.macTxtBox.TabIndex = 6;
            // 
            // scanBtn
            // 
            this.scanBtn.BackColor = System.Drawing.Color.AliceBlue;
            this.scanBtn.Font = new System.Drawing.Font("Stencil", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scanBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.scanBtn.Location = new System.Drawing.Point(23, 22);
            this.scanBtn.Name = "scanBtn";
            this.scanBtn.Size = new System.Drawing.Size(120, 28);
            this.scanBtn.TabIndex = 8;
            this.scanBtn.Text = "Scan";
            this.scanBtn.UseVisualStyleBackColor = false;
            this.scanBtn.Click += new System.EventHandler(this.scanBtn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.stopJarpBtn);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.sendJarpBtn);
            this.groupBox2.Controls.Add(this.macTxtBox);
            this.groupBox2.Controls.Add(this.ipTxtBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox2.Location = new System.Drawing.Point(318, 120);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(273, 129);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Poison Target and Gateway";
            // 
            // stopJarpBtn
            // 
            this.stopJarpBtn.BackColor = System.Drawing.Color.AliceBlue;
            this.stopJarpBtn.Font = new System.Drawing.Font("Stencil", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stopJarpBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.stopJarpBtn.Location = new System.Drawing.Point(147, 92);
            this.stopJarpBtn.Name = "stopJarpBtn";
            this.stopJarpBtn.Size = new System.Drawing.Size(120, 28);
            this.stopJarpBtn.TabIndex = 9;
            this.stopJarpBtn.Text = "Stop";
            this.stopJarpBtn.UseVisualStyleBackColor = false;
            this.stopJarpBtn.Visible = false;
            this.stopJarpBtn.Click += new System.EventHandler(this.stopJarpBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.scanBtn);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Location = new System.Drawing.Point(55, 120);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(152, 60);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ARP Scan";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::JArpPoison.Properties.Resources.cool_logo;
            this.pictureBox1.Location = new System.Drawing.Point(88, -5);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(486, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Green;
            this.BackgroundImage = global::JArpPoison.Properties.Resources.camo_back;
            this.ClientSize = new System.Drawing.Size(654, 577);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.progressLabel);
            this.Controls.Add(this.outputBox);
            this.Name = "MainPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "JARP-POISON";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainPage_FormClosed);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox ipTxtBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button sendJarpBtn;
        private System.Windows.Forms.RichTextBox outputBox;
        private System.Windows.Forms.Label progressLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox macTxtBox;
        private System.Windows.Forms.Button scanBtn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button stopJarpBtn;
    }
}

