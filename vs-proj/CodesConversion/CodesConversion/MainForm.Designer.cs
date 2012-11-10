namespace CodesConversion
{
    partial class MainForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.theInputFileTextBox = new System.Windows.Forms.TextBox();
            this.theInputFileBrowseButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.theOutputFileTextBox = new System.Windows.Forms.TextBox();
            this.theOutputFileBrowseButton = new System.Windows.Forms.Button();
            this.theConvertButton = new System.Windows.Forms.Button();
            this.theVideoFileBrowseButton = new System.Windows.Forms.Button();
            this.theVideoFileTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.theProgressBar = new System.Windows.Forms.ProgressBar();
            this.theProgressTextBox = new System.Windows.Forms.TextBox();
            this.theConversionStatusLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "iCoda File";
            // 
            // theInputFileTextBox
            // 
            this.theInputFileTextBox.Location = new System.Drawing.Point(92, 10);
            this.theInputFileTextBox.Name = "theInputFileTextBox";
            this.theInputFileTextBox.ReadOnly = true;
            this.theInputFileTextBox.Size = new System.Drawing.Size(374, 20);
            this.theInputFileTextBox.TabIndex = 1;
            // 
            // theInputFileBrowseButton
            // 
            this.theInputFileBrowseButton.Location = new System.Drawing.Point(472, 8);
            this.theInputFileBrowseButton.Name = "theInputFileBrowseButton";
            this.theInputFileBrowseButton.Size = new System.Drawing.Size(91, 23);
            this.theInputFileBrowseButton.TabIndex = 2;
            this.theInputFileBrowseButton.Text = "Browse...";
            this.theInputFileBrowseButton.UseVisualStyleBackColor = true;
            this.theInputFileBrowseButton.Click += new System.EventHandler(this.theInputFileBrowseButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Output File";
            // 
            // theOutputFileTextBox
            // 
            this.theOutputFileTextBox.Location = new System.Drawing.Point(92, 62);
            this.theOutputFileTextBox.Name = "theOutputFileTextBox";
            this.theOutputFileTextBox.ReadOnly = true;
            this.theOutputFileTextBox.Size = new System.Drawing.Size(374, 20);
            this.theOutputFileTextBox.TabIndex = 4;
            // 
            // theOutputFileBrowseButton
            // 
            this.theOutputFileBrowseButton.Location = new System.Drawing.Point(472, 60);
            this.theOutputFileBrowseButton.Name = "theOutputFileBrowseButton";
            this.theOutputFileBrowseButton.Size = new System.Drawing.Size(91, 23);
            this.theOutputFileBrowseButton.TabIndex = 5;
            this.theOutputFileBrowseButton.Text = "Browse...";
            this.theOutputFileBrowseButton.UseVisualStyleBackColor = true;
            this.theOutputFileBrowseButton.Click += new System.EventHandler(this.theOutputFileBrowseButton_Click);
            // 
            // theConvertButton
            // 
            this.theConvertButton.Location = new System.Drawing.Point(15, 257);
            this.theConvertButton.Name = "theConvertButton";
            this.theConvertButton.Size = new System.Drawing.Size(551, 45);
            this.theConvertButton.TabIndex = 6;
            this.theConvertButton.Text = "Convert!";
            this.theConvertButton.UseVisualStyleBackColor = true;
            this.theConvertButton.Click += new System.EventHandler(this.theConvertButton_Click);
            // 
            // theVideoFileBrowseButton
            // 
            this.theVideoFileBrowseButton.Location = new System.Drawing.Point(472, 34);
            this.theVideoFileBrowseButton.Name = "theVideoFileBrowseButton";
            this.theVideoFileBrowseButton.Size = new System.Drawing.Size(91, 23);
            this.theVideoFileBrowseButton.TabIndex = 9;
            this.theVideoFileBrowseButton.Text = "Browse...";
            this.theVideoFileBrowseButton.UseVisualStyleBackColor = true;
            this.theVideoFileBrowseButton.Click += new System.EventHandler(this.theVideoFileBrowseButton_Click);
            // 
            // theVideoFileTextBox
            // 
            this.theVideoFileTextBox.Location = new System.Drawing.Point(92, 36);
            this.theVideoFileTextBox.Name = "theVideoFileTextBox";
            this.theVideoFileTextBox.ReadOnly = true;
            this.theVideoFileTextBox.Size = new System.Drawing.Size(374, 20);
            this.theVideoFileTextBox.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Video File";
            // 
            // theProgressBar
            // 
            this.theProgressBar.Location = new System.Drawing.Point(15, 135);
            this.theProgressBar.Name = "theProgressBar";
            this.theProgressBar.Size = new System.Drawing.Size(551, 23);
            this.theProgressBar.TabIndex = 10;
            this.theProgressBar.Visible = false;
            // 
            // theProgressTextBox
            // 
            this.theProgressTextBox.Location = new System.Drawing.Point(15, 164);
            this.theProgressTextBox.Multiline = true;
            this.theProgressTextBox.Name = "theProgressTextBox";
            this.theProgressTextBox.ReadOnly = true;
            this.theProgressTextBox.Size = new System.Drawing.Size(548, 87);
            this.theProgressTextBox.TabIndex = 11;
            this.theProgressTextBox.Visible = false;
            // 
            // theConversionStatusLabel
            // 
            this.theConversionStatusLabel.AutoSize = true;
            this.theConversionStatusLabel.Location = new System.Drawing.Point(15, 116);
            this.theConversionStatusLabel.Name = "theConversionStatusLabel";
            this.theConversionStatusLabel.Size = new System.Drawing.Size(96, 13);
            this.theConversionStatusLabel.TabIndex = 12;
            this.theConversionStatusLabel.Text = "Conversion Status:";
            this.theConversionStatusLabel.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 314);
            this.Controls.Add(this.theConversionStatusLabel);
            this.Controls.Add(this.theProgressTextBox);
            this.Controls.Add(this.theProgressBar);
            this.Controls.Add(this.theVideoFileBrowseButton);
            this.Controls.Add(this.theVideoFileTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.theConvertButton);
            this.Controls.Add(this.theOutputFileBrowseButton);
            this.Controls.Add(this.theOutputFileTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.theInputFileBrowseButton);
            this.Controls.Add(this.theInputFileTextBox);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "iCoda-to-SportsCode Converter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox theInputFileTextBox;
        private System.Windows.Forms.Button theInputFileBrowseButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox theOutputFileTextBox;
        private System.Windows.Forms.Button theOutputFileBrowseButton;
        private System.Windows.Forms.Button theConvertButton;
        private System.Windows.Forms.Button theVideoFileBrowseButton;
        private System.Windows.Forms.TextBox theVideoFileTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar theProgressBar;
        private System.Windows.Forms.TextBox theProgressTextBox;
        private System.Windows.Forms.Label theConversionStatusLabel;
    }
}

