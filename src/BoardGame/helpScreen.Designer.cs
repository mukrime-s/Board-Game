
namespace BoardGame
{
    partial class helpScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(helpScreen));
            this.lblTitle1 = new System.Windows.Forms.Label();
            this.lblTitle2 = new System.Windows.Forms.Label();
            this.lblUsage = new System.Windows.Forms.Label();
            this.lblGameDef = new System.Windows.Forms.Label();
            this.btnAbout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle1
            // 
            this.lblTitle1.AutoSize = true;
            this.lblTitle1.Location = new System.Drawing.Point(113, 93);
            this.lblTitle1.Name = "lblTitle1";
            this.lblTitle1.Size = new System.Drawing.Size(96, 20);
            this.lblTitle1.TabIndex = 0;
            this.lblTitle1.Text = "Game Usage:";
            // 
            // lblTitle2
            // 
            this.lblTitle2.AutoSize = true;
            this.lblTitle2.Location = new System.Drawing.Point(113, 164);
            this.lblTitle2.Name = "lblTitle2";
            this.lblTitle2.Size = new System.Drawing.Size(121, 20);
            this.lblTitle2.TabIndex = 1;
            this.lblTitle2.Text = "Game Definition:";
            // 
            // lblUsage
            // 
            this.lblUsage.AutoSize = true;
            this.lblUsage.Location = new System.Drawing.Point(263, 93);
            this.lblUsage.Name = "lblUsage";
            this.lblUsage.Size = new System.Drawing.Size(378, 60);
            this.lblUsage.TabIndex = 2;
            this.lblUsage.Text = "Oyunun amacı, en fazla 2 boşluk dolmadığı sürece\r\naynı şekil ve renkteki kutuları" +
    " yan yana getirip patlatarak\r\nen yüksek puanı kazanmaktır.\r\n";
            // 
            // lblGameDef
            // 
            this.lblGameDef.AutoSize = true;
            this.lblGameDef.Location = new System.Drawing.Point(263, 164);
            this.lblGameDef.Name = "lblGameDef";
            this.lblGameDef.Size = new System.Drawing.Size(411, 80);
            this.lblGameDef.TabIndex = 3;
            this.lblGameDef.Text = resources.GetString("lblGameDef.Text");
            // 
            // btnAbout
            // 
            this.btnAbout.Location = new System.Drawing.Point(126, 271);
            this.btnAbout.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(97, 52);
            this.btnAbout.TabIndex = 4;
            this.btnAbout.Text = "See About";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // helpScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 600);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.lblGameDef);
            this.Controls.Add(this.lblUsage);
            this.Controls.Add(this.lblTitle2);
            this.Controls.Add(this.lblTitle1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "helpScreen";
            this.Text = "helpScreen";
            this.Load += new System.EventHandler(this.helpScreen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle1;
        private System.Windows.Forms.Label lblTitle2;
        private System.Windows.Forms.Label lblUsage;
        private System.Windows.Forms.Label lblGameDef;
        private System.Windows.Forms.Button btnAbout;
    }
}