using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KittehPlayer
{
    public partial class YouTubeForm : Form
    {
        public YouTubeForm()
        {
            InitializeComponent();
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Download_Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
