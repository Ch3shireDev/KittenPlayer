namespace KittehPlayer
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.MainTab = new System.Windows.Forms.TabPage();
            this.MusicList = new System.Windows.Forms.ListBox();
            this.MainTabs = new System.Windows.Forms.TabControl();
            this.ContextTab = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deletePlaylistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainTab.SuspendLayout();
            this.MainTabs.SuspendLayout();
            this.ContextTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainTab
            // 
            this.MainTab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.MainTab.Controls.Add(this.MusicList);
            this.MainTab.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.MainTab.Location = new System.Drawing.Point(4, 22);
            this.MainTab.Name = "MainTab";
            this.MainTab.Padding = new System.Windows.Forms.Padding(3);
            this.MainTab.Size = new System.Drawing.Size(752, 511);
            this.MainTab.TabIndex = 0;
            this.MainTab.Text = "MainTab";
            this.MainTab.UseVisualStyleBackColor = true;
            this.MainTab.Click += new System.EventHandler(this.MainTab_Click);
            this.MainTab.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainTab_DragDrop);
            // 
            // MusicList
            // 
            this.MusicList.AllowDrop = true;
            this.MusicList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MusicList.CausesValidation = false;
            this.MusicList.FormattingEnabled = true;
            this.MusicList.Location = new System.Drawing.Point(6, 6);
            this.MusicList.Name = "MusicList";
            this.MusicList.Size = new System.Drawing.Size(740, 494);
            this.MusicList.TabIndex = 0;
            this.MusicList.Click += new System.EventHandler(this.MusicList_Click);
            this.MusicList.TabIndexChanged += new System.EventHandler(this.MusicList_TabIndexChanged);
            this.MusicList.DragDrop += new System.Windows.Forms.DragEventHandler(this.MusicList_DragDrop);
            this.MusicList.DragEnter += new System.Windows.Forms.DragEventHandler(this.MusicList_DragEnter);
            this.MusicList.DoubleClick += new System.EventHandler(this.MusicList_DoubleClick);
            // 
            // MainTabs
            // 
            this.MainTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainTabs.Controls.Add(this.MainTab);
            this.MainTabs.Location = new System.Drawing.Point(12, 12);
            this.MainTabs.Name = "MainTabs";
            this.MainTabs.SelectedIndex = 0;
            this.MainTabs.Size = new System.Drawing.Size(760, 537);
            this.MainTabs.TabIndex = 0;
            this.MainTabs.Selected += new System.Windows.Forms.TabControlEventHandler(this.MainTabs_Selected);
            this.MainTabs.Click += new System.EventHandler(this.MainTabs_Click);
            this.MainTabs.DoubleClick += new System.EventHandler(this.MainTabs_DoubleClick);
            this.MainTabs.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainTabs_KeyPress);
            this.MainTabs.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.MainTabs_KeyPress);
            // 
            // ContextTab
            // 
            this.ContextTab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renameToolStripMenuItem,
            this.deletePlaylistToolStripMenuItem});
            this.ContextTab.Name = "ContextTab";
            this.ContextTab.Size = new System.Drawing.Size(162, 48);
            this.ContextTab.Opening += new System.ComponentModel.CancelEventHandler(this.ContextTab_Opening);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.renameToolStripMenuItem.Text = "Rename playlist";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // deletePlaylistToolStripMenuItem
            // 
            this.deletePlaylistToolStripMenuItem.Name = "deletePlaylistToolStripMenuItem";
            this.deletePlaylistToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.deletePlaylistToolStripMenuItem.Text = "Delete playlist";
            // 
            // MainWindow
            // 
            this.AccessibleName = "MainWindow";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.MainTabs);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MainWindow";
            this.Text = "KittehPlayer";
            this.DoubleClick += new System.EventHandler(this.MainWindow_DoubleClick);
            this.MainTab.ResumeLayout(false);
            this.MainTabs.ResumeLayout(false);
            this.ContextTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage MainTab;
        private System.Windows.Forms.ListBox MusicList;
        private System.Windows.Forms.TabControl MainTabs;
        private System.Windows.Forms.ContextMenuStrip ContextTab;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deletePlaylistToolStripMenuItem;
    }
}

