using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _100444144
{
    public partial class FourConfigForm : Form
    {
        Four brain;
        public FourConfigForm(Four brain)
        {
            this.brain = brain;
            InitializeComponent();
            textBoxSpeed.Text = brain.Speed.ToString();
        }

        private void FourConfigForm_Load(object sender, EventArgs e)
        {

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxSpeed.Text, out int speed) || !int.TryParse(textBoxSpeed.Text, out int chaseSpeed))
            {
                MessageBox.Show("Speed must be a whole number.", "Edit Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    brain.Speed = speed;
                    brain.SaveConfiguration();
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to set values: " + ex.Message, "Edit Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
