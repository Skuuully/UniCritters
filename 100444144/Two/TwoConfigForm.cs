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
    public partial class TwoConfigForm : Form
    {
        public TwoConfigForm()
        {
            InitializeComponent();
        }

        Two brain;

        public TwoConfigForm(Two brain)
        {
            this.brain = brain;
            InitializeComponent();
            //Fills textBox with current speed
            textBoxSpeed.Text = brain.Speed.ToString();
        }

        private void TwoConfigForm_Load(object sender, EventArgs e)
        {

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBoxSpeed.Text, out int speed))
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
            Close();
        }

        private void DemoCritterBrain1ConfigForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
