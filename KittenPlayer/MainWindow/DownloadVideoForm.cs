using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KittenPlayer
{
    public partial class DownloadVideoForm : Form
    {
        public DownloadVideoForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            DownloadCall();
        }

        async void DownloadCall()
        {
            string URL = textBox1.Text;
            bool exists = await CheckIfExists(URL);
            if (!exists)
            {
                return;
            }
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "*.webm | *.mp4 | All files (*.*)|*.*",
                RestoreDirectory = true,
                FilterIndex = 2
            };

            var result = dialog.ShowDialog();
            if (result != DialogResult.OK) return;
            
            var filename = dialog.FileName;
            await DownloadFile(URL, filename, ".mp4");

        }

        private Task DownloadFile(string url, string filename, string mp4)
        {
            throw new NotImplementedException();
        }

        private Task<bool> CheckIfExists(string url)
        {
            throw new NotImplementedException();
        }


    }
}
