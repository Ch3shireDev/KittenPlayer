using System;
//using System.Object;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace KittehPlayer
{
    public partial class MainWindow : Form
    {

        MusicPlayer musicPlayer = MusicPlayer.NewMusicPlayer();
        LocalData localData = LocalData.NewLocalData();
        Playlists playlists = Playlists.NewPlaylists();


        public MainWindow()
        {
            InitializeComponent();
            this.KeyPress += MainTabs_KeyPress;
            this.KeyPreview = true;


            playlists.List.Add(new Playlist(MusicList));
        }

        private void MainTab_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void MainTab_Click(object sender, EventArgs e)
        {
            
        }
        

        private void MusicList_Click(object sender, EventArgs e)
        {
            
        }
        

        ///<summary>
        ///<para>Method for adding files to list.</para>
        ///</summary>

        private void MusicList_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string s in FileList)
            {
                ListBox List = (ListBox)(sender);
                List.Items.Add(s);
            }
        }

        private void MusicList_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;

            }
            else
                e.Effect = DragDropEffects.None;
        }

        private void MusicList_DoubleClick(object sender, EventArgs e)
        {

            MouseEventArgs me = (MouseEventArgs)e;

            int index = this.MusicList.IndexFromPoint(me.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                musicPlayer.Play(MusicList.Items[index].ToString());
            }

        }


        private void AddNewTab(String Name)
        {

            TabPage TP = new TabPage();
            MainTabs.Controls.Add(TP);
            TP.Text = Name;
            TP.UseVisualStyleBackColor = true;

            ListBox LB = new ListBox();
            CopyList(ref MusicList, ref LB);

            TP.Controls.Add(LB);
            
        }

        private void CopyList(ref ListBox In, ref ListBox Out)
        {
            Out.AllowDrop = In.AllowDrop;
            Out.BorderStyle = In.BorderStyle;
            Out.CausesValidation = In.CausesValidation;
            Out.FormattingEnabled = In.FormattingEnabled;
            Out.Location = In.Location;
            Out.Name = In.Name;
            Out.Size = In.Size;
            Out.TabIndex = In.TabIndex;
            Out.Click += new System.EventHandler(this.MusicList_Click);
            Out.TabIndexChanged += new System.EventHandler(this.MusicList_TabIndexChanged);
            Out.DragDrop += new System.Windows.Forms.DragEventHandler(this.MusicList_DragDrop);
            Out.DragEnter += new System.Windows.Forms.DragEventHandler(this.MusicList_DragEnter);
            Out.DoubleClick += new System.EventHandler(this.MusicList_DoubleClick);
        }


        private void MainTabs_DoubleClick(object sender, EventArgs e)
        {
            if(sender is TabControl)
            {
                TabControl Tab = (TabControl)sender;
                Debug.WriteLine(Tab.Name);
            }
            
        }


        private void MainTabs_Selected(object sender, TabControlEventArgs e)
        {
            Debug.WriteLine(e.TabPage.Text);
        }

        private void MusicList_TabIndexChanged(object sender, EventArgs e)
        {

        }
        
        private void MainTabs_Click(object sender, EventArgs Event)
        {
            if (Event is MouseEventArgs)
            {
                

                MouseEventArgs mouseEvent = (MouseEventArgs)Event;
                if (mouseEvent.Button == MouseButtons.Right)
                {
                    if(sender is TabControl)
                    {
                        TabControl tabControl = (TabControl)sender;
                    }
                    ContextTab.Show(MainTabs, mouseEvent.Location);
                }
            }
        }

        private void MainWindow_Click(object sender, EventArgs e)
        {

        }

        private void MainWindow_DoubleClick(object sender, EventArgs e)
        {
            Debug.WriteLine("MainWindow double click");
        }

        private void ContextTab_Opening(object sender, CancelEventArgs e)
        {

        }

        TextBox RenameBox = null;

        private void RenameTab()
        {
            int TabNum = MainTabs.SelectedIndex;
            
            Rectangle rect = MainTabs.GetTabRect(TabNum);
            Point point = MainTabs.Location;

            rect = MainTabs.RectangleToScreen(rect);
            rect = MainTabs.Parent.RectangleToClient(rect);

            RenameBox = new TextBox();
            MainTabs.GetControl(TabNum).Controls.Add(RenameBox);
            MainTabs.Parent.Controls.Add(RenameBox);
            RenameBox.TextAlign = HorizontalAlignment.Center;
            RenameBox.Font = MainTabs.GetControl(TabNum).Font;
            RenameBox.SetBounds(rect.X, rect.Y, rect.Width, rect.Height);
            RenameBox.Text = MainTabs.Controls[TabNum].Text;
            RenameBox.BringToFront();
            RenameBox.KeyPress += textBox1_KeyPress;
            RenameBox.Focus();
            RenameBox.Show();
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RenameTab();
        }

        private void TabHover(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Auxiliary method for killing a textbox.
        /// </summary>

        private void KillTextBox()
        {
            RenameBox.Hide();
            RenameBox.Parent.Controls.Remove(RenameBox);
            RenameBox = null; //if it doesn't kill it i don't know what does
        }

        /// <summary>
        /// Method for handling the textbox. For now Enter means to update the name and Esc to back to state before.
        /// </summary>

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (RenameBox == null) return;

            if(e.KeyChar == (char)Keys.Enter)
            {
                MainTabs.SelectedTab.Text = RenameBox.Text;
                KillTextBox();
            }

            else if(e.KeyChar == (char)Keys.Escape){
                KillTextBox();
            }
        }

        private void MainTabs_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void MainTabs_KeyPress(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                RenameTab();
            }

        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Multiselect = true;
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "mp3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach(String s in openFileDialog.FileNames)
                {
                    Debug.WriteLine(s);

                    //here i need to add the adding files handling, i wait for playlist to be fully implemented


                }
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
