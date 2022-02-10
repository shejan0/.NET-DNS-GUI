
namespace DNSGUI
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
            this.AddressInput = new System.Windows.Forms.TextBox();
            this.IPAddressOutput = new System.Windows.Forms.ListBox();
            this.PingLabel = new System.Windows.Forms.Label();
            this.InformationLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // AddressInput
            // 
            this.AddressInput.Location = new System.Drawing.Point(12, 30);
            this.AddressInput.Name = "AddressInput";
            this.AddressInput.Size = new System.Drawing.Size(470, 22);
            this.AddressInput.TabIndex = 0;
            this.AddressInput.Text = "google.com";
            this.AddressInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.AddressInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AddressInput_KeyPressed);
            // 
            // IPAddressOutput
            // 
            this.IPAddressOutput.FormattingEnabled = true;
            this.IPAddressOutput.ItemHeight = 16;
            this.IPAddressOutput.Location = new System.Drawing.Point(12, 75);
            this.IPAddressOutput.Name = "IPAddressOutput";
            this.IPAddressOutput.Size = new System.Drawing.Size(470, 228);
            this.IPAddressOutput.TabIndex = 1;
            this.IPAddressOutput.SelectedIndexChanged += new System.EventHandler(this.IPAddressOutput_SelectedIndexChanged);
            // 
            // PingLabel
            // 
            this.PingLabel.AutoSize = true;
            this.PingLabel.Location = new System.Drawing.Point(13, 418);
            this.PingLabel.Name = "PingLabel";
            this.PingLabel.Size = new System.Drawing.Size(0, 17);
            this.PingLabel.TabIndex = 2;
            this.PingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // InformationLabel
            // 
            this.InformationLabel.AutoSize = true;
            this.InformationLabel.Location = new System.Drawing.Point(13, 347);
            this.InformationLabel.MaximumSize = new System.Drawing.Size(470, 0);
            this.InformationLabel.Name = "InformationLabel";
            this.InformationLabel.Size = new System.Drawing.Size(0, 17);
            this.InformationLabel.TabIndex = 3;
            this.InformationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(470, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "Enter URL or IP (Hit Enter)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 465);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.InformationLabel);
            this.Controls.Add(this.PingLabel);
            this.Controls.Add(this.IPAddressOutput);
            this.Controls.Add(this.AddressInput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "DNS GUI Program by Shejan Shuza";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        

        #endregion

        private System.Windows.Forms.TextBox AddressInput;
        private System.Windows.Forms.ListBox IPAddressOutput;
        private System.Windows.Forms.Label PingLabel;
        private System.Windows.Forms.Label InformationLabel;
        private System.Windows.Forms.Label label1;
    }
}

