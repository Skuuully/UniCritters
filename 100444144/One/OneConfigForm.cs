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
    public partial class OneConfigForm : Form
    {
        One brain;
        int notifyFoodChecked;
        int notifyPooChecked;

        public OneConfigForm(One brain)
        {
            this.brain = brain;
            InitializeComponent();
            textBoxSpeed.Text = brain.Speed.ToString();
            if (brain.NotifyFood == 1)
                checkBoxNotifyFood.Checked = true;
            else
                checkBoxNotifyFood.Checked = false;
            if (brain.NotifyPoo == 1)
                checkBoxNotifyPoo.Checked = true;
            else
                checkBoxNotifyPoo.Checked = false;
        }

        private void OneConfigForm_Load(object sender, EventArgs e)
        {

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (checkBoxNotifyFood.Checked)
            {
                notifyFoodChecked = 1;
            }
            else
            {
                notifyFoodChecked = 0;
            }
            if (checkBoxNotifyPoo.Checked)
            {
                notifyPooChecked = 1;
            }
            else
            {
                notifyPooChecked = 0;
            }
            if (!int.TryParse(textBoxSpeed.Text, out int speed))
            {
                MessageBox.Show("Speed must be a whole number.", "Edit Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    brain.Speed = speed;
                    brain.NotifyFood = notifyFoodChecked;
                    brain.NotifyPoo = notifyPooChecked;
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
    }
}
