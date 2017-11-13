using System;
using System.Windows.Forms;

namespace KittenPlayer
{
    public partial class Options : Form
    {
        public String DefaultDirectory;

        public Options(String SelectedDirectory = "")
        {
            InitializeComponent();
            if (System.IO.File.Exists(SelectedDirectory) && MusicTab.IsDirectory(SelectedDirectory))
            {
                this.DefaultDirectory = SelectedDirectory;
            }
            else
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
                this.DefaultDirectory = path;
            }
            UpdateDir();
        }

        public void UpdateDir()
        {
            textBox1.Text = this.DefaultDirectory;
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer;
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                DefaultDirectory = folderBrowserDialog1.SelectedPath;
                UpdateDir();
            }
        }
    }
}