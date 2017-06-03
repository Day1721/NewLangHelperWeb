namespace NewLangGeneration
{
    partial class Form1
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
            this.lbx = new System.Windows.Forms.ListBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.lblFileName = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.button4 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbx
            // 
            this.lbx.FormattingEnabled = true;
            this.lbx.Location = new System.Drawing.Point(0, 2);
            this.lbx.Name = "lbx";
            this.lbx.Size = new System.Drawing.Size(397, 251);
            this.lbx.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(748, 259);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(93, 259);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(174, 259);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(75, 23);
            this.btnRead.TabIndex = 4;
            this.btnRead.Text = "Read file";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(255, 264);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(0, 13);
            this.lblFileName.TabIndex = 5;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(258, 259);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Init";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.wordsInit);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(369, 259);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 10;
            this.button3.Text = "Save";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(12, 259);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 11;
            this.button4.Text = "generate top";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            "Polish",
            "Russian"});
            this.listBox1.Location = new System.Drawing.Point(458, 38);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(66, 147);
            this.listBox1.TabIndex = 12;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(580, 58);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "translate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonTranslate_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(603, 137);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 14;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 285);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lbx);
            this.Name = "Form1";
            this.Text = "Environment Information";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbx;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button5;
    }
}

