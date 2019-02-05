using System;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Drawing;

namespace GUISystemInfo
{
    public partial class Form1 : Form
    {
        GetSystemInfo app = new GetSystemInfo();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            app.StartProgram();
            
            lblPCName.Text = app.SysInfo.Name;
            lblServiceTag.Text = app.SysInfo.ServiceTag;
            lblPCModel.Text = app.SysInfo.Model;
            lblOS.Text = app.SysInfo.OS;
            lblBits.Text = app.SysInfo.Bit;
            lblRam.Text = app.SysInfo.Ram;
            lblProcessor.Text = app.SysInfo.Processor;
            lblPeopleYear.Text = "People (" + app.year + ")";

            txtRoomNum.Focus();
        }

        private void lblPCName_Click(object sender, EventArgs e)
        {
            app.SysInfo.Name = Microsoft.VisualBasic.Interaction.InputBox("Enter New PC name");
            lblPCName.Text = app.SysInfo.Name;
        }

        private void lblRam_Click(object sender, EventArgs e)
        {
            app.SysInfo.Ram = Microsoft.VisualBasic.Interaction.InputBox("Fix Ram Ammount");
            lblRam.Text = app.SysInfo.Ram;
        }

        private void btnWriteToFile_Click(object sender, EventArgs e)
        {
            app.SysInfo.RoomNum = txtRoomNum.Text;
            app.SysInfo.People = txtPeople.Text;
            app.SysInfo.Comments = txtComments.Text;
            app.MoveDataToFile();
            Close();
        }

        private void computerNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void computerNameToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            app.SysInfo.Name = Microsoft.VisualBasic.Interaction.InputBox("Enter New PC name");
            lblPCName.Text = app.SysInfo.Name;
        }

        private void ramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            app.SysInfo.Ram = Microsoft.VisualBasic.Interaction.InputBox("Enter Ram ammount");
            lblRam.Text = app.SysInfo.Ram;
        }
        private void serviceTagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            app.SysInfo.ServiceTag = Microsoft.VisualBasic.Interaction.InputBox("Enter service tag/serial number");
            lblServiceTag.Text = app.SysInfo.ServiceTag;
        }

        private void computerModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            app.SysInfo.Model = Microsoft.VisualBasic.Interaction.InputBox("Enter computer model");
            lblPCModel.Text = app.SysInfo.Model;
        }

        private void oSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            app.SysInfo.OS = Microsoft.VisualBasic.Interaction.InputBox("Enter Operating System");
            lblOS.Text = app.SysInfo.OS;
        }

        private void processorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            app.SysInfo.Processor = Microsoft.VisualBasic.Interaction.InputBox("Enter processor");
            lblProcessor.Text = app.SysInfo.Processor;
        }

        private void aboutProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm formAbout = new AboutForm();
            formAbout.Show();
        }
    }
}
