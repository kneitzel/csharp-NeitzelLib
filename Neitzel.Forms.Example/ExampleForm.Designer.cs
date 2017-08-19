using System.Diagnostics.CodeAnalysis;

namespace Neitzel.Forms.Example
{
    partial class ExampleForm
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
        [SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Windows.Forms.Control.set_Text(System.String)")]
        private void InitializeComponent()
        {
            this.OpenFormButton = new System.Windows.Forms.Button();
            this.ServerButton = new System.Windows.Forms.Button();
            this.ClientButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OpenFormButton
            // 
            this.OpenFormButton.Location = new System.Drawing.Point(12, 12);
            this.OpenFormButton.Name = "OpenFormButton";
            this.OpenFormButton.Size = new System.Drawing.Size(157, 42);
            this.OpenFormButton.TabIndex = 0;
            this.OpenFormButton.Text = "Open another Window";
            this.OpenFormButton.UseVisualStyleBackColor = true;
            this.OpenFormButton.Click += new System.EventHandler(this.OnOpenFormClick);
            // 
            // ServerButton
            // 
            this.ServerButton.Location = new System.Drawing.Point(13, 61);
            this.ServerButton.Name = "ServerButton";
            this.ServerButton.Size = new System.Drawing.Size(75, 23);
            this.ServerButton.TabIndex = 1;
            this.ServerButton.Text = "Server";
            this.ServerButton.UseVisualStyleBackColor = true;
            this.ServerButton.Click += new System.EventHandler(this.ServerButton_Click);
            // 
            // ClientButton
            // 
            this.ClientButton.Location = new System.Drawing.Point(94, 61);
            this.ClientButton.Name = "ClientButton";
            this.ClientButton.Size = new System.Drawing.Size(75, 23);
            this.ClientButton.TabIndex = 2;
            this.ClientButton.Text = "Client";
            this.ClientButton.UseVisualStyleBackColor = true;
            this.ClientButton.Click += new System.EventHandler(this.OnClientButtonClick);
            // 
            // ExampleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(181, 96);
            this.Controls.Add(this.ClientButton);
            this.Controls.Add(this.ServerButton);
            this.Controls.Add(this.OpenFormButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExampleForm";
            this.Text = "Example Form";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OpenFormButton;
        private System.Windows.Forms.Button ServerButton;
        private System.Windows.Forms.Button ClientButton;
    }
}

