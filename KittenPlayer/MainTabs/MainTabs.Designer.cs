using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace KittenPlayer
{

    partial class MainTabs : UserControl
    {


        private void InitializeComponent()
        {
            this.MainTab = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // MainTab
            // 
            this.MainTab.AllowDrop = true;
            this.MainTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainTab.CausesValidation = false;
            this.MainTab.Location = new System.Drawing.Point(0, 0);
            this.MainTab.Margin = new System.Windows.Forms.Padding(0);
            this.MainTab.Name = "MainTab";
            this.MainTab.SelectedIndex = 0;
            this.MainTab.Size = new System.Drawing.Size(516, 370);
            this.MainTab.TabIndex = 0;
            this.MainTab.SelectedIndexChanged += new System.EventHandler(this.MainTabs_SelectedIndexChanged);
            this.MainTab.Selected += new System.Windows.Forms.TabControlEventHandler(this.MainTabs_Selected);
            this.MainTab.Click += new System.EventHandler(this.MainTabs_Click);
            this.MainTab.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainTabs_DragDrop);
            this.MainTab.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainTabs_DragEnter);
            this.MainTab.DragOver += new System.Windows.Forms.DragEventHandler(this.MainTabs_DragOver);
            this.MainTab.DoubleClick += new System.EventHandler(this.MainTabs_DoubleClick);
            this.MainTab.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainTabs_KeyPress);
            this.MainTab.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainTabs_MouseDown);
            this.MainTab.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainTabs_MouseMove);
            this.MainTab.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainTabs_MouseUp);
            this.MainTab.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.MainTabs_KeyPress);
            // 
            // MainTabs
            // 
            this.Controls.Add(this.MainTab);
            this.Name = "MainTabs";
            this.Size = new System.Drawing.Size(516, 370);
            this.Load += new System.EventHandler(this.MainTabs_Load);
            this.ResumeLayout(false);

        }

        public TabControl MainTab;
    }
}
