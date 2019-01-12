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
    public partial class ThreeConfigForm : Form
    {
        Three brain;

        public ThreeConfigForm(Three brain)
        {
            this.brain = brain;
            InitializeComponent();
            textBoxSpeed.Text = brain.Speed.ToString();
            textBoxChaseSpeed.Text = brain.ChaseSpeed.ToString();
        }
        public ThreeConfigForm()
        {
            InitializeComponent();
        }

        private void ThreeConfigForm_Load(object sender, EventArgs e)
        {

        }

        private void DemoCritterBrain1ConfigForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
        }

        private void buttonOK_Click_1(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxSpeed.Text, out int speed) || !int.TryParse(textBoxChaseSpeed.Text, out int chaseSpeed))
            {
                MessageBox.Show("Speed must be a whole number.", "Edit Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    brain.Speed = speed;
                    brain.ChaseSpeed = chaseSpeed;
                    brain.SaveConfiguration();
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to set values: " + ex.Message, "Edit Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonCancel_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void textBoxChaseSpeed_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
