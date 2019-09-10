namespace PPD.WindowsFormsApp
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
            this.resultsListBox = new System.Windows.Forms.ListBox();
            this.fillOnceButton = new System.Windows.Forms.Button();
            this.fillMultipleButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // resultsListBox
            // 
            this.resultsListBox.Font = new System.Drawing.Font(
                "Segoe UI",
                12F,
                System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point,
                ((byte) (0))
            );
            this.resultsListBox.FormattingEnabled = true;
            this.resultsListBox.ItemHeight = 21;
            this.resultsListBox.Location = new System.Drawing.Point(12, 12);
            this.resultsListBox.Name = "resultsListBox";
            this.resultsListBox.Size = new System.Drawing.Size(412, 298);
            this.resultsListBox.TabIndex = 0;
            // 
            // fillOnceButton
            // 
            this.fillOnceButton.Font = new System.Drawing.Font(
                "Segoe UI",
                12F,
                System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point,
                ((byte) (0))
            );
            this.fillOnceButton.Location = new System.Drawing.Point(430, 12);
            this.fillOnceButton.Name = "fillOnceButton";
            this.fillOnceButton.Size = new System.Drawing.Size(134, 38);
            this.fillOnceButton.TabIndex = 1;
            this.fillOnceButton.Text = "Fill once";
            this.fillOnceButton.UseVisualStyleBackColor = true;
            this.fillOnceButton.Click += new System.EventHandler(this.fillOnceButton_Click);
            // 
            // fillMultipleButton
            // 
            this.fillMultipleButton.Font = new System.Drawing.Font(
                "Segoe UI",
                12F,
                System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point,
                ((byte) (0))
            );
            this.fillMultipleButton.Location = new System.Drawing.Point(430, 57);
            this.fillMultipleButton.Name = "fillMultipleButton";
            this.fillMultipleButton.Size = new System.Drawing.Size(134, 38);
            this.fillMultipleButton.TabIndex = 1;
            this.fillMultipleButton.Text = "Fill multiple";
            this.fillMultipleButton.UseVisualStyleBackColor = true;
            this.fillMultipleButton.Click += new System.EventHandler(this.fillMultipleButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 327);
            this.Controls.Add(this.fillMultipleButton);
            this.Controls.Add(this.fillOnceButton);
            this.Controls.Add(this.resultsListBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ListBox resultsListBox;
        private System.Windows.Forms.Button fillMultipleButton;
        private System.Windows.Forms.Button fillOnceButton;
    }
}