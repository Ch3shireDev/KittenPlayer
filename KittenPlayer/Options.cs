using System;
using System.IO;
using System.Windows.Forms;

namespace KittenPlayer
{
    public partial class Options : Form
    {
        public string DefaultDirectory;

        public Options(string SelectedDirectory = "")
        {
            InitializeComponent();
            if (File.Exists(SelectedDirectory) && MusicTab.IsDirectory(SelectedDirectory))
            {
                DefaultDirectory = SelectedDirectory;
            }
            else
            {
                var path = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
                DefaultDirectory = path;
            }
            UpdateDir();
        }

        public void UpdateDir() => textBox1.Text = DefaultDirectory;

        private void Button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer;
            var result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                DefaultDirectory = folderBrowserDialog1.SelectedPath;
                UpdateDir();
            }
        }
    }
}