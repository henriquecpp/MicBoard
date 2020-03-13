namespace MicBoard
{
    partial class InputBox
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
            this.InputField = new System.Windows.Forms.TextBox();
            this.Clear = new System.Windows.Forms.Button();
            this.Confirm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // InputField
            // 
            this.InputField.Location = new System.Drawing.Point(43, 34);
            this.InputField.Name = "InputField";
            this.InputField.ReadOnly = true;
            this.InputField.Size = new System.Drawing.Size(400, 20);
            this.InputField.TabIndex = 0;
            this.InputField.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyCapture);
            this.InputField.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyRelease);
            // 
            // Clear
            // 
            this.Clear.Location = new System.Drawing.Point(43, 71);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(87, 23);
            this.Clear.TabIndex = 1;
            this.Clear.Text = "Clear";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.ClearAll);
            // 
            // Confirm
            // 
            this.Confirm.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Confirm.Location = new System.Drawing.Point(356, 71);
            this.Confirm.Name = "Confirm";
            this.Confirm.Size = new System.Drawing.Size(87, 23);
            this.Confirm.TabIndex = 2;
            this.Confirm.Text = "Ok";
            this.Confirm.UseVisualStyleBackColor = true;
            // 
            // InputBox2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 111);
            this.Controls.Add(this.Confirm);
            this.Controls.Add(this.Clear);
            this.Controls.Add(this.InputField);
            this.Name = "InputBox2";
            this.Text = "Insira a(s) tecla(s) de atalho:";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox InputField;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.Button Confirm;
    }
}