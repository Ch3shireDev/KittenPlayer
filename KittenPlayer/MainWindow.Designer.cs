namespace KittenPlayer
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
            this.ContextTab = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewPlaylistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deletePlaylistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.youTubeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadLinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findTrackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainTabs = new System.Windows.Forms.TabControl();
            this.playControl1 = new KittenPlayer.PlayControl();
            this.savePlaylistsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadPlaylistsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextTab.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContextTab
            // 
            this.ContextTab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renameToolStripMenuItem,
            this.addNewPlaylistToolStripMenuItem,
            this.deletePlaylistToolStripMenuItem});
            this.ContextTab.Name = "ContextTab";
            this.ContextTab.Size = new System.Drawing.Size(162, 70);
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
            // addNewPlaylistToolStripMenuItem
            // 
            this.addNewPlaylistToolStripMenuItem.Name = "addNewPlaylistToolStripMenuItem";
            this.addNewPlaylistToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.addNewPlaylistToolStripMenuItem.Text = "Add playlist";
            this.addNewPlaylistToolStripMenuItem.Click += new System.EventHandler(this.addNewPlaylistToolStripMenuItem_Click);
            // 
            // deletePlaylistToolStripMenuItem
            // 
            this.deletePlaylistToolStripMenuItem.Name = "deletePlaylistToolStripMenuItem";
            this.deletePlaylistToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.deletePlaylistToolStripMenuItem.Text = "Delete playlist";
            this.deletePlaylistToolStripMenuItem.Click += new System.EventHandler(this.deletePlaylistToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.youTubeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(784, 27);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.savePlaylistsToolStripMenuItem,
            this.loadPlaylistsToolStripMenuItem});
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(41, 23);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(160, 24);
            this.addToolStripMenuItem.Text = "Add files...";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 23);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Z)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // youTubeToolStripMenuItem
            // 
            this.youTubeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadLinkToolStripMenuItem,
            this.findTrackToolStripMenuItem});
            this.youTubeToolStripMenuItem.Name = "youTubeToolStripMenuItem";
            this.youTubeToolStripMenuItem.Size = new System.Drawing.Size(66, 23);
            this.youTubeToolStripMenuItem.Text = "YouTube";
            // 
            // downloadLinkToolStripMenuItem
            // 
            this.downloadLinkToolStripMenuItem.Name = "downloadLinkToolStripMenuItem";
            this.downloadLinkToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.downloadLinkToolStripMenuItem.Text = "Get from URL";
            this.downloadLinkToolStripMenuItem.Click += new System.EventHandler(this.downloadLinkToolStripMenuItem_Click);
            // 
            // findTrackToolStripMenuItem
            // 
            this.findTrackToolStripMenuItem.Name = "findTrackToolStripMenuItem";
            this.findTrackToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.findTrackToolStripMenuItem.Text = "Find track...";
            this.findTrackToolStripMenuItem.Click += new System.EventHandler(this.findTrackToolStripMenuItem_Click);
            // 
            // MainTabs
            // 
            this.MainTabs.AllowDrop = true;
            this.MainTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainTabs.CausesValidation = false;
            this.MainTabs.Location = new System.Drawing.Point(12, 49);
            this.MainTabs.Name = "MainTabs";
            this.MainTabs.SelectedIndex = 0;
            this.MainTabs.Size = new System.Drawing.Size(760, 500);
            this.MainTabs.TabIndex = 0;
            this.MainTabs.Selected += new System.Windows.Forms.TabControlEventHandler(this.MainTabs_Selected);
            this.MainTabs.Click += new System.EventHandler(this.MainTabs_Click);
            this.MainTabs.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainTabs_DragDrop);
            this.MainTabs.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainTabs_DragEnter);
            this.MainTabs.DragOver += new System.Windows.Forms.DragEventHandler(this.MainTabs_DragOver);
            this.MainTabs.DoubleClick += new System.EventHandler(this.MainTabs_DoubleClick);
            this.MainTabs.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainTabs_KeyPress);
            this.MainTabs.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainTabs_MouseDown);
            this.MainTabs.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainTabs_MouseMove);
            this.MainTabs.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainTabs_MouseUp);
            this.MainTabs.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.MainTabs_KeyPress);
            // 
            // playControl1
            // 
            this.playControl1.Location = new System.Drawing.Point(594, 12);
            this.playControl1.Name = "playControl1";
            this.playControl1.Size = new System.Drawing.Size(178, 38);
            this.playControl1.TabIndex = 2;
            // 
            // savePlaylistsToolStripMenuItem
            // 
            this.savePlaylistsToolStripMenuItem.Name = "savePlaylistsToolStripMenuItem";
            this.savePlaylistsToolStripMenuItem.Size = new System.Drawing.Size(160, 24);
            this.savePlaylistsToolStripMenuItem.Text = "Save Playlists";
            this.savePlaylistsToolStripMenuItem.Click += new System.EventHandler(this.savePlaylistsToolStripMenuItem_Click);
            // 
            // loadPlaylistsToolStripMenuItem
            // 
            this.loadPlaylistsToolStripMenuItem.Name = "loadPlaylistsToolStripMenuItem";
            this.loadPlaylistsToolStripMenuItem.Size = new System.Drawing.Size(160, 24);
            this.loadPlaylistsToolStripMenuItem.Text = "Load Playlists";
            this.loadPlaylistsToolStripMenuItem.Click += new System.EventHandler(this.loadPlaylistsToolStripMenuItem_Click);
            // 
            // MainWindow
            // 
            this.AccessibleName = "MainWindow";
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.playControl1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.MainTabs);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MainWindow";
            this.Text = "KittenPlayer";
            this.Click += new System.EventHandler(this.MainWindow_Click);
            this.DoubleClick += new System.EventHandler(this.MainWindow_DoubleClick);
            this.ContextTab.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip ContextTab;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deletePlaylistToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewPlaylistToolStripMenuItem;
        private PlayControl playControl1;
        private System.Windows.Forms.ToolStripMenuItem youTubeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadLinkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findTrackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        public System.Windows.Forms.TabControl MainTabs;
        private System.Windows.Forms.ToolStripMenuItem savePlaylistsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadPlaylistsToolStripMenuItem;
    }
}

