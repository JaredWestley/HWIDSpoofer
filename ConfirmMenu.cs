using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyAuth
{
    public partial class ConfirmMenu : Form
    {
        public ConfirmMenu()
        {
            InitializeComponent();
        }

        private void siticoneControlBox3_Click(object sender, EventArgs e)
        {

        }

        private void siticoneControlBox4_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void siticoneRoundedButton1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("shutdown.exe", "-r -t 0");
        }

        private void siticoneRoundedButton2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void siticonePictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/JaredWestley");
        }
    }
}
