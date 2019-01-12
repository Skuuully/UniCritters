namespace _100444144
{
    partial class OneConfigForm
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
            this.labelSpeed = new System.Windows.Forms.Label();
            this.textBoxSpeed = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkBoxNotifyFood = new System.Windows.Forms.CheckBox();
            this.checkBoxNotifyPoo = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // labelSpeed
            // 
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.Location = new System.Drawing.Point(13, 13);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(38, 13);
            this.labelSpeed.TabIndex = 0;
            this.labelSpeed.Text = "Speed";
            // 
            // textBoxSpeed
            // 
            this.textBoxSpeed.Location = new System.Drawing.Point(58, 13);
            this.textBoxSpeed.Name = "textBoxSpeed";
            this.textBoxSpeed.Size = new System.Drawing.Size(100, 20);
            this.textBoxSpeed.TabIndex = 1;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(177, 211);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(35, 211);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // checkBoxNotifyFood
            // 
            this.checkBoxNotifyFood.AutoSize = true;
            this.checkBoxNotifyFood.Location = new System.Drawing.Point(18, 53);
            this.checkBoxNotifyFood.Name = "checkBoxNotifyFood";
            this.checkBoxNotifyFood.Size = new System.Drawing.Size(125, 17);
            this.checkBoxNotifyFood.TabIndex = 6;
            this.checkBoxNotifyFood.Text = "Should I go for food?";
            this.checkBoxNotifyFood.UseVisualStyleBackColor = true;
            // 
            // checkBoxNotifyPoo
            // 
            this.checkBoxNotifyPoo.AutoSize = true;
            this.checkBoxNotifyPoo.Location = new System.Drawing.Point(18, 76);
            this.checkBoxNotifyPoo.Name = "checkBoxNotifyPoo";
            this.checkBoxNotifyPoo.Size = new System.Drawing.Size(127, 17);
            this.checkBoxNotifyPoo.TabIndex = 7;
            this.checkBoxNotifyPoo.Text = "Should I avoid poop?";
            this.checkBoxNotifyPoo.UseVisualStyleBackColor = true;
            // 
            // OneConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.checkBoxNotifyPoo);
            this.Controls.Add(this.checkBoxNotifyFood);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.textBoxSpeed);
            this.Controls.Add(this.labelSpeed);
            this.Name = "OneConfigForm";
            this.Text = "OneConfigForm";
            this.Load += new System.EventHandler(this.OneConfigForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelSpeed;
        private System.Windows.Forms.TextBox textBoxSpeed;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkBoxNotifyFood;
        private System.Windows.Forms.CheckBox checkBoxNotifyPoo;
    }
}