using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace KittenPlayer
{
    public partial class Thumbnail : UserControl
    {
        public String ID;

        void InitializeControls()
        {
            foreach (Control control in Controls)
            {
                control.Click += Clicked;
                control.MouseDown += Grab;
                control.MouseUp += Release;
                control.MouseMove += Moved;
            }
        }

        public Thumbnail()
        {
            InitializeComponent();
            InitializeControls();
        }

        public Thumbnail(String ID)
        {
            this.ID = ID;
            InitializeComponent();
            InitializeControls();
        }

        bool isGrabbed = false;

        void Grab(object sender, EventArgs e)
        {
            isGrabbed = true;
        }

        void Release(object sender, EventArgs e)
        {
            isGrabbed = false;
        }

        public bool isSelected = false;

        void Clicked(object sender, EventArgs e)
        {
            if (!isSelected)
            {
                BackColor = System.Drawing.SystemColors.Highlight;
                isSelected = true;
            }
            else
            {
                BackColor = System.Drawing.SystemColors.Control;
                isSelected = false;
            }
        }

        void Moved(object sender, EventArgs e)
        {
            if (isGrabbed)
            {
                isGrabbed = false;
                MainWindow.ActiveTab.PlaylistView.DoDragDrop(this, DragDropEffects.Move);
            }
        }


    }
}
