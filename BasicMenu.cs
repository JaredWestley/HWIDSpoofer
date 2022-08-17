using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyAuth
{
    public partial class BasicMenu : Form
    {
        const String WIN_10_PATH = @"SYSTEM\CurrentControlSet\Control\IDConfigDB\Hardware Profiles\0001";
        string backupHWID = "";
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
         (
               int nLeftRect,
               int nTopRect,
               int nRightRect,
               int nBottomRect,
               int nWidthEllipse,
               int nHeightEllipse

         );

        public BasicMenu()
        {
            InitializeComponent();
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }

        private void BasicMenu_Load(object sender, EventArgs e)
        {
            getHWID();
        }

        public DateTime UnixTimeToDateTime(long unixtime)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Local);
            dtDateTime = dtDateTime.AddSeconds(unixtime).ToLocalTime();
            return dtDateTime;
        }

        private void siticoneControlBox4_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void getHWID()
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(WIN_10_PATH))
                {
                    if (key != null)
                    {
                        backupHWID = key.GetValue("HwProfileGuid").ToString();
                        HWIDprint.Text = backupHWID;
                        HWIDprint.Left = (this.Width - HWIDprint.Width) / 2;

                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error accessing the Registry... Maybe run as admin?\n\n" + ex.ToString(), "HWID_CHNGER", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Change_Click(object sender, EventArgs e)
        {
            if (!Change.Text.Equals("Set"))
            {
                Change.Text = "Set";
                takeInputGUI();
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure you would like to continue?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {

                    if (HWIDprint.Text.Equals(backupHWID))
                    {
                        HWIDprint.Text = siticoneRoundedTextBox1.Text;
                        HWIDprint.Left = (this.Width - HWIDprint.Width) / 2;
                    }
                    setHWIDKey();
                    defualtGUI();
                    Change.Text = "Change";
                    ConfirmMenu f1 = new ConfirmMenu();
                    f1.Show();
                }
            }

        }

        private void Advanced_Click(object sender, EventArgs e)
        {
            AdvancedMenu f1 = new AdvancedMenu();
            f1.Show();
            this.Hide();
        }

        private void takeInputGUI()
        {
            siticoneRoundedTextBox1.Visible = true;
            siticoneRoundedTextBox1.Enabled = true;
            Randomise.Visible = false;
            Randomise.Enabled = false;
            Cancel.Visible = true;
            Cancel.Enabled = true;
        }

        private void defualtGUI()
        {
            siticoneRoundedTextBox1.Visible = false;
            siticoneRoundedTextBox1.Enabled = false;
            Randomise.Visible = true;
            Randomise.Enabled = true;
            Cancel.Visible = false;
            Cancel.Enabled = false;
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(HWIDprint.Text);
        }

        private void randoMize()
        {
            HWIDprint.Text = "{" + generateIDs(8) + "-" + generateIDs(4) + "-" + generateIDs(4) + "-" + generateIDs(4) + "-" + generateIDs(12) + "}";
        }

        private static readonly Random getrandom = new Random();

        public static int GetRandomNumber(int min, int max)
        {
            lock (getrandom) // synchronize
            {
                return getrandom.Next(min, max);
            }
        }

        private string generateIDs(int length)
        {
            String ret = "";
            for (int x = 0; x < length; x++)
            {
                if (GetRandomNumber(0, 100) < 60)
                {
                    ret += "" + GetRandomNumber(0, 9);
                }
                else
                {
                    ret += ("" + (char)GetRandomNumber(97, 122)).ToLower();
                }
                //int val = 86;
                //do
                //{
                //    val = GetRandomNumber(48,90);
                //} while (val >= 58 && val <=64);
                //ret += ("" + (char)val).ToLower();
            }
            return ret;
        }

        private void randomizeGUI()
        {
            siticoneRoundedTextBox1.Visible = false;
            siticoneRoundedTextBox1.Enabled = false;
            Change.Text = "Set";
            Cancel.Visible = true;
            Cancel.Enabled = true;
        }

        private void siticoneRoundedButton3_Click(object sender, EventArgs e)
        {
            randomizeGUI();
            randoMize();
            HWIDprint.Left = (this.Width - HWIDprint.Width) / 2;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            HWIDprint.Text = backupHWID;
            defualtGUI();
            Change.Text = "Change";
            HWIDprint.Left = (this.Width - HWIDprint.Width) / 2;
        }

        private void setHWIDKey()
        {
            RegistryKey myKey = Registry.LocalMachine.OpenSubKey(WIN_10_PATH, true);
            if (myKey != null)
            {
                myKey.SetValue("HwProfileGuid", HWIDprint.Text, RegistryValueKind.String);
                myKey.Close();
            }
            backupHWID = HWIDprint.Text;
        }

        private void siticonePictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/JaredWestley");
        }

        private void siticoneLabel1_Click(object sender, EventArgs e)
        {

        }

        private void siticoneControlBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
