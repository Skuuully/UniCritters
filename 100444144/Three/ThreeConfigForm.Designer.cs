namespace _100444144
{
    partial class ThreeConfigForm
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxSpeed = new System.Windows.Forms.TextBox();
            this.labelSpeed = new System.Windows.Forms.Label();
            this.labelChaseSpeed = new System.Windows.Forms.Label();
            this.textBoxChaseSpeed = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(171, 209);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click_1);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(50, 209);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click_1);
            // 
            // textBoxSpeed
            // 
            this.textBoxSpeed.Location = new System.Drawing.Point(100, 37);
            this.textBoxSpeed.Name = "textBoxSpeed";
            this.textBoxSpeed.Size = new System.Drawing.Size(100, 20);
            this.textBoxSpeed.TabIndex = 2;
            // 
            // labelSpeed
            // 
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.Location = new System.Drawing.Point(13, 40);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(38, 13);
            this.labelSpeed.TabIndex = 3;
            this.labelSpeed.Text = "Speed";
            // 
            // labelChaseSpeed
            // 
            this.labelChaseSpeed.AutoSize = true;
            this.labelChaseSpeed.Location = new System.Drawing.Point(12, 70);
            this.labelChaseSpeed.Name = "labelChaseSpeed";
            this.labelChaseSpeed.Size = new System.Drawing.Size(71, 13);
            this.labelChaseSpeed.TabIndex = 4;
            this.labelChaseSpeed.Text = "Chase Speed";
            // 
            // textBoxChaseSpeed
            // 
            this.textBoxChaseSpeed.Location = new System.Drawing.Point(100, 70);
            this.textBoxChaseSpeed.Name = "textBoxChaseSpeed";
            this.textBoxChaseSpeed.Size = new System.Drawing.Size(100, 20);
            this.textBoxChaseSpeed.TabIndex = 5;
            this.textBoxChaseSpeed.TextChanged += new System.EventHandler(this.textBoxChaseSpeed_TextChanged);
            // 
            // ThreeConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.textBoxChaseSpeed);
            this.Controls.Add(this.labelChaseSpeed);
            this.Controls.Add(this.labelSpeed);
            this.Controls.Add(this.textBoxSpeed);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Name = "ThreeConfigForm";
            this.Text = "ThreeConfigForm";
            this.Load += new System.EventHandler(this.ThreeConfigForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxSpeed;
        private System.Windows.Forms.Label labelSpeed;
        private System.Windows.Forms.Label labelChaseSpeed;
        private System.Windows.Forms.TextBox textBoxChaseSpeed;
    }
}