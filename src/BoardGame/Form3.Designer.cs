
namespace BoardGame
{
    partial class Form3
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
            this.easy = new System.Windows.Forms.RadioButton();
            this.normal = new System.Windows.Forms.RadioButton();
            this.hard = new System.Windows.Forms.RadioButton();
            this.custom = new System.Windows.Forms.RadioButton();
            this.shapeListBox1 = new System.Windows.Forms.CheckedListBox();
            this.save = new System.Windows.Forms.Button();
            this.dLevel = new System.Windows.Forms.GroupBox();
            this.lblCustomError = new System.Windows.Forms.Label();
            this.customCol = new System.Windows.Forms.TextBox();
            this.customRow = new System.Windows.Forms.TextBox();
            this.lblCustomCheck = new System.Windows.Forms.Label();
            this.lblMessage1 = new System.Windows.Forms.Label();
            this.lblMessage2 = new System.Windows.Forms.Label();
            this.colorbox = new System.Windows.Forms.CheckedListBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.dLevel.SuspendLayout();
            this.SuspendLayout();
            // 
            // easy
            // 
            this.easy.AutoSize = true;
            this.easy.Location = new System.Drawing.Point(18, 36);
            this.easy.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.easy.Name = "easy";
            this.easy.Size = new System.Drawing.Size(48, 19);
            this.easy.TabIndex = 0;
            this.easy.TabStop = true;
            this.easy.Text = "easy";
            this.easy.UseVisualStyleBackColor = true;
            this.easy.CheckedChanged += new System.EventHandler(this.easy_CheckedChanged);
            // 
            // normal
            // 
            this.normal.AutoSize = true;
            this.normal.Location = new System.Drawing.Point(18, 70);
            this.normal.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.normal.Name = "normal";
            this.normal.Size = new System.Drawing.Size(63, 19);
            this.normal.TabIndex = 1;
            this.normal.TabStop = true;
            this.normal.Text = "normal";
            this.normal.UseVisualStyleBackColor = true;
            this.normal.CheckedChanged += new System.EventHandler(this.normal_CheckedChanged);
            // 
            // hard
            // 
            this.hard.AutoSize = true;
            this.hard.Location = new System.Drawing.Point(18, 106);
            this.hard.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.hard.Name = "hard";
            this.hard.Size = new System.Drawing.Size(49, 19);
            this.hard.TabIndex = 2;
            this.hard.TabStop = true;
            this.hard.Text = "hard";
            this.hard.UseVisualStyleBackColor = true;
            this.hard.CheckedChanged += new System.EventHandler(this.hard_CheckedChanged);
            // 
            // custom
            // 
            this.custom.AutoSize = true;
            this.custom.Location = new System.Drawing.Point(18, 141);
            this.custom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.custom.Name = "custom";
            this.custom.Size = new System.Drawing.Size(65, 19);
            this.custom.TabIndex = 3;
            this.custom.TabStop = true;
            this.custom.Text = "custom";
            this.custom.UseVisualStyleBackColor = true;
            this.custom.CheckedChanged += new System.EventHandler(this.custom_CheckedChanged);
            // 
            // shapeListBox1
            // 
            this.shapeListBox1.CheckOnClick = true;
            this.shapeListBox1.FormattingEnabled = true;
            this.shapeListBox1.Items.AddRange(new object[] {
            "Square",
            "Triangle",
            "Round"});
            this.shapeListBox1.Location = new System.Drawing.Point(383, 36);
            this.shapeListBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.shapeListBox1.Name = "shapeListBox1";
            this.shapeListBox1.Size = new System.Drawing.Size(133, 58);
            this.shapeListBox1.TabIndex = 5;
            this.shapeListBox1.SelectedIndexChanged += new System.EventHandler(this.shapeListBox1_SelectedIndexChanged);
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(434, 246);
            this.save.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(81, 34);
            this.save.TabIndex = 6;
            this.save.Text = "save";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // dLevel
            // 
            this.dLevel.Controls.Add(this.lblCustomError);
            this.dLevel.Controls.Add(this.customCol);
            this.dLevel.Controls.Add(this.customRow);
            this.dLevel.Controls.Add(this.normal);
            this.dLevel.Controls.Add(this.easy);
            this.dLevel.Controls.Add(this.hard);
            this.dLevel.Controls.Add(this.custom);
            this.dLevel.Location = new System.Drawing.Point(38, 24);
            this.dLevel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dLevel.Name = "dLevel";
            this.dLevel.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dLevel.Size = new System.Drawing.Size(220, 206);
            this.dLevel.TabIndex = 7;
            this.dLevel.TabStop = false;
            this.dLevel.Text = "Difficulty Level";
            // 
            // lblCustomError
            // 
            this.lblCustomError.AutoSize = true;
            this.lblCustomError.Location = new System.Drawing.Point(18, 174);
            this.lblCustomError.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCustomError.Name = "lblCustomError";
            this.lblCustomError.Size = new System.Drawing.Size(0, 15);
            this.lblCustomError.TabIndex = 6;
            // 
            // customCol
            // 
            this.customCol.Location = new System.Drawing.Point(141, 141);
            this.customCol.Margin = new System.Windows.Forms.Padding(4);
            this.customCol.Name = "customCol";
            this.customCol.Size = new System.Drawing.Size(28, 23);
            this.customCol.TabIndex = 5;
            this.customCol.Visible = false;
            this.customCol.TextChanged += new System.EventHandler(this.customCol_TextChanged);
            this.customCol.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.customCol_KeyPress);
            // 
            // customRow
            // 
            this.customRow.Location = new System.Drawing.Point(106, 141);
            this.customRow.Margin = new System.Windows.Forms.Padding(4);
            this.customRow.Name = "customRow";
            this.customRow.Size = new System.Drawing.Size(28, 23);
            this.customRow.TabIndex = 4;
            this.customRow.Visible = false;
            this.customRow.TextChanged += new System.EventHandler(this.customRow_TextChanged);
            this.customRow.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.customRow_KeyPress);
            // 
            // lblCustomCheck
            // 
            this.lblCustomCheck.AutoSize = true;
            this.lblCustomCheck.Location = new System.Drawing.Point(264, 165);
            this.lblCustomCheck.Name = "lblCustomCheck";
            this.lblCustomCheck.Size = new System.Drawing.Size(0, 15);
            this.lblCustomCheck.TabIndex = 7;
            // 
            // lblMessage1
            // 
            this.lblMessage1.AutoSize = true;
            this.lblMessage1.Location = new System.Drawing.Point(434, 215);
            this.lblMessage1.Name = "lblMessage1";
            this.lblMessage1.Size = new System.Drawing.Size(0, 15);
            this.lblMessage1.TabIndex = 8;
            // 
            // lblMessage2
            // 
            this.lblMessage2.AutoSize = true;
            this.lblMessage2.Location = new System.Drawing.Point(434, 173);
            this.lblMessage2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMessage2.Name = "lblMessage2";
            this.lblMessage2.Size = new System.Drawing.Size(0, 15);
            this.lblMessage2.TabIndex = 9;
            this.lblMessage2.Click += new System.EventHandler(this.label1_Click);
            // 
            // colorbox
            // 
            this.colorbox.CheckOnClick = true;
            this.colorbox.FormattingEnabled = true;
            this.colorbox.Items.AddRange(new object[] {
            "red",
            "blue",
            "pink"});
            this.colorbox.Location = new System.Drawing.Point(545, 36);
            this.colorbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.colorbox.Name = "colorbox";
            this.colorbox.Size = new System.Drawing.Size(132, 58);
            this.colorbox.TabIndex = 10;
            this.colorbox.SelectedIndexChanged += new System.EventHandler(this.colorbox_SelectedIndexChanged);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(558, 246);
            this.btnBack.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(82, 34);
            this.btnBack.TabIndex = 11;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 337);
            this.Controls.Add(this.lblCustomCheck);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.colorbox);
            this.Controls.Add(this.lblMessage2);
            this.Controls.Add(this.lblMessage1);
            this.Controls.Add(this.dLevel);
            this.Controls.Add(this.save);
            this.Controls.Add(this.shapeListBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form3";
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.dLevel.ResumeLayout(false);
            this.dLevel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton easy;
        private System.Windows.Forms.RadioButton normal;
        private System.Windows.Forms.RadioButton hard;
        private System.Windows.Forms.RadioButton custom;
        private System.Windows.Forms.CheckedListBox shapeListBox1;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.GroupBox dLevel;
        private System.Windows.Forms.Label lblMessage1;
        private System.Windows.Forms.Label lblMessage2;
        private System.Windows.Forms.TextBox customCol;
        private System.Windows.Forms.TextBox customRow;
        private System.Windows.Forms.Label lblCustomError;
        private System.Windows.Forms.CheckedListBox colorbox;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblCustomCheck;
    }
}