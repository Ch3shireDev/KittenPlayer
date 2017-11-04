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
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savePlaylistsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.abortOperationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playlistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addPlaylistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addYoutubePlaylistToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.trackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadAndPlayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadAgainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadOnlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.volumeBar = new System.Windows.Forms.TrackBar();
            this.LayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.ResultsPage = new KittenPlayer.ResultsPage();
            this.SearchPanel = new System.Windows.Forms.Panel();
            this.searchBarPage = new KittenPlayer.SearchPage();
            this.MainTab = new KittenPlayer.MainTabs();
            this.AddPlaylistStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addPlaylistToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.playControl1 = new KittenPlayer.PlayControl();
            this.ContextTab.SuspendLayout();
            this.MenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.volumeBar)).BeginInit();
            this.LayoutPanel.SuspendLayout();
            this.SearchPanel.SuspendLayout();
            this.AddPlaylistStrip.SuspendLayout();
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
            // MenuStrip
            // 
            this.MenuStrip.BackColor = System.Drawing.SystemColors.Control;
            this.MenuStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.playlistToolStripMenuItem,
            this.trackToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.MenuStrip.Size = new System.Drawing.Size(974, 27);
            this.MenuStrip.TabIndex = 1;
            this.MenuStrip.Text = "Menu";
            this.MenuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.addDirectoryToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.savePlaylistsToolStripMenuItem});
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(41, 23);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(206, 24);
            this.addToolStripMenuItem.Text = "Add files...";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // addDirectoryToolStripMenuItem
            // 
            this.addDirectoryToolStripMenuItem.Name = "addDirectoryToolStripMenuItem";
            this.addDirectoryToolStripMenuItem.Size = new System.Drawing.Size(206, 24);
            this.addDirectoryToolStripMenuItem.Text = "Add directory...";
            this.addDirectoryToolStripMenuItem.Click += new System.EventHandler(this.addDirectoryToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(206, 24);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // savePlaylistsToolStripMenuItem
            // 
            this.savePlaylistsToolStripMenuItem.Name = "savePlaylistsToolStripMenuItem";
            this.savePlaylistsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.savePlaylistsToolStripMenuItem.Size = new System.Drawing.Size(206, 24);
            this.savePlaylistsToolStripMenuItem.Text = "Save Playlists";
            this.savePlaylistsToolStripMenuItem.Click += new System.EventHandler(this.savePlaylistsToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.selectAllToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.renameToolStripMenuItem1,
            this.abortOperationToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 23);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Z)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // renameToolStripMenuItem1
            // 
            this.renameToolStripMenuItem1.Name = "renameToolStripMenuItem1";
            this.renameToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.renameToolStripMenuItem1.Size = new System.Drawing.Size(212, 22);
            this.renameToolStripMenuItem1.Text = "Rename...";
            this.renameToolStripMenuItem1.Click += new System.EventHandler(this.renameToolStripMenuItem1_Click);
            // 
            // abortOperationToolStripMenuItem
            // 
            this.abortOperationToolStripMenuItem.Name = "abortOperationToolStripMenuItem";
            this.abortOperationToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.abortOperationToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.abortOperationToolStripMenuItem.Text = "Abort Operation...";
            this.abortOperationToolStripMenuItem.Click += new System.EventHandler(this.abortOperationToolStripMenuItem_Click);
            // 
            // playlistToolStripMenuItem
            // 
            this.playlistToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addPlaylistToolStripMenuItem,
            this.addYoutubePlaylistToolStripMenuItem1});
            this.playlistToolStripMenuItem.Name = "playlistToolStripMenuItem";
            this.playlistToolStripMenuItem.Size = new System.Drawing.Size(56, 23);
            this.playlistToolStripMenuItem.Text = "Playlist";
            // 
            // addPlaylistToolStripMenuItem
            // 
            this.addPlaylistToolStripMenuItem.Name = "addPlaylistToolStripMenuItem";
            this.addPlaylistToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.addPlaylistToolStripMenuItem.Text = "Add Playlist";
            // 
            // addYoutubePlaylistToolStripMenuItem1
            // 
            this.addYoutubePlaylistToolStripMenuItem1.Name = "addYoutubePlaylistToolStripMenuItem1";
            this.addYoutubePlaylistToolStripMenuItem1.Size = new System.Drawing.Size(183, 22);
            this.addYoutubePlaylistToolStripMenuItem1.Text = "Add Youtube Playlist";
            this.addYoutubePlaylistToolStripMenuItem1.Click += new System.EventHandler(this.addYoutubePlaylistToolStripMenuItem_Click);
            // 
            // trackToolStripMenuItem
            // 
            this.trackToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadAndPlayToolStripMenuItem,
            this.downloadAgainToolStripMenuItem,
            this.downloadOnlyToolStripMenuItem});
            this.trackToolStripMenuItem.Name = "trackToolStripMenuItem";
            this.trackToolStripMenuItem.Size = new System.Drawing.Size(47, 23);
            this.trackToolStripMenuItem.Text = "Track";
            // 
            // downloadAndPlayToolStripMenuItem
            // 
            this.downloadAndPlayToolStripMenuItem.Name = "downloadAndPlayToolStripMenuItem";
            this.downloadAndPlayToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.downloadAndPlayToolStripMenuItem.Text = "Download and Play";
            this.downloadAndPlayToolStripMenuItem.Click += new System.EventHandler(this.downloadAndPlayToolStripMenuItem_Click);
            // 
            // downloadAgainToolStripMenuItem
            // 
            this.downloadAgainToolStripMenuItem.Name = "downloadAgainToolStripMenuItem";
            this.downloadAgainToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.downloadAgainToolStripMenuItem.Text = "Download again";
            this.downloadAgainToolStripMenuItem.Click += new System.EventHandler(this.downloadAgainToolStripMenuItem_Click);
            // 
            // downloadOnlyToolStripMenuItem
            // 
            this.downloadOnlyToolStripMenuItem.Name = "downloadOnlyToolStripMenuItem";
            this.downloadOnlyToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.downloadOnlyToolStripMenuItem.Text = "Download only";
            this.downloadOnlyToolStripMenuItem.Click += new System.EventHandler(this.downloadOnlyToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 23);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // trackBar
            // 
            this.trackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar.LargeChange = 0;
            this.trackBar.Location = new System.Drawing.Point(9, 30);
            this.trackBar.Maximum = 100;
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(627, 45);
            this.trackBar.TabIndex = 3;
            this.trackBar.TickFrequency = 0;
            this.trackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar.Scroll += new System.EventHandler(this.trackBar_Scroll);
            this.trackBar.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            this.trackBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trackBar_MouseDown);
            this.trackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBar_MouseUp);
            // 
            // volumeBar
            // 
            this.volumeBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.volumeBar.LargeChange = 10;
            this.volumeBar.Location = new System.Drawing.Point(642, 30);
            this.volumeBar.Maximum = 100;
            this.volumeBar.Name = "volumeBar";
            this.volumeBar.Size = new System.Drawing.Size(104, 45);
            this.volumeBar.SmallChange = 5;
            this.volumeBar.TabIndex = 4;
            this.volumeBar.TickFrequency = 10;
            this.volumeBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.volumeBar.Value = 100;
            this.volumeBar.ValueChanged += new System.EventHandler(this.volumeBar_ValueChanged);
            // 
            // LayoutPanel
            // 
            this.LayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.LayoutPanel.ColumnCount = 1;
            this.LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LayoutPanel.Controls.Add(this.ResultsPage, 0, 2);
            this.LayoutPanel.Controls.Add(this.SearchPanel, 0, 1);
            this.LayoutPanel.Controls.Add(this.MainTab, 0, 0);
            this.LayoutPanel.Location = new System.Drawing.Point(3, 81);
            this.LayoutPanel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.LayoutPanel.Name = "LayoutPanel";
            this.LayoutPanel.RowCount = 3;
            this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.LayoutPanel.Size = new System.Drawing.Size(971, 448);
            this.LayoutPanel.TabIndex = 7;
            this.LayoutPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.LayoutPanel_Paint);
            this.LayoutPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LayoutPanel_MouseClick);
            // 
            // ResultsPage
            // 
            this.ResultsPage.AutoSize = true;
            this.ResultsPage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ResultsPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResultsPage.Location = new System.Drawing.Point(3, 251);
            this.ResultsPage.Name = "ResultsPage";
            this.ResultsPage.Size = new System.Drawing.Size(965, 194);
            this.ResultsPage.TabIndex = 1;
            this.ResultsPage.Load += new System.EventHandler(this.ResultsPage_Load);
            // 
            // SearchPanel
            // 
            this.SearchPanel.AutoSize = true;
            this.SearchPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SearchPanel.Controls.Add(this.searchBarPage);
            this.SearchPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SearchPanel.Location = new System.Drawing.Point(3, 216);
            this.SearchPanel.Name = "SearchPanel";
            this.SearchPanel.Size = new System.Drawing.Size(965, 29);
            this.SearchPanel.TabIndex = 5;
            this.SearchPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.SearchPanel_Paint);
            // 
            // searchBarPage
            // 
            this.searchBarPage.AutoSize = true;
            this.searchBarPage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.searchBarPage.Location = new System.Drawing.Point(3, 0);
            this.searchBarPage.Name = "searchBarPage";
            this.searchBarPage.Size = new System.Drawing.Size(963, 26);
            this.searchBarPage.TabIndex = 0;
            this.searchBarPage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchBarPage_KeyDown);
            this.searchBarPage.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.searchBarPage_PreviewKeyDown);
            // 
            // MainTab
            // 
            this.MainTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTab.Location = new System.Drawing.Point(3, 3);
            this.MainTab.Name = "MainTab";
            this.MainTab.Size = new System.Drawing.Size(965, 207);
            this.MainTab.TabIndex = 6;
            // 
            // AddPlaylistStrip
            // 
            this.AddPlaylistStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addPlaylistToolStripMenuItem1});
            this.AddPlaylistStrip.Name = "AddPlaylistStrip";
            this.AddPlaylistStrip.Size = new System.Drawing.Size(138, 26);
            // 
            // addPlaylistToolStripMenuItem1
            // 
            this.addPlaylistToolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addPlaylistToolStripMenuItem1.Name = "addPlaylistToolStripMenuItem1";
            this.addPlaylistToolStripMenuItem1.Size = new System.Drawing.Size(137, 22);
            this.addPlaylistToolStripMenuItem1.Text = "Add Playlist";
            this.addPlaylistToolStripMenuItem1.Click += new System.EventHandler(this.addNewPlaylistToolStripMenuItem_Click);
            // 
            // playControl1
            // 
            this.playControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.playControl1.AutoSize = true;
            this.playControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.playControl1.Location = new System.Drawing.Point(752, 30);
            this.playControl1.Name = "playControl1";
            this.playControl1.Size = new System.Drawing.Size(210, 38);
            this.playControl1.TabIndex = 8;
            this.playControl1.Load += new System.EventHandler(this.playControl1_Load);
            // 
            // MainWindow
            // 
            this.AccessibleName = "MainWindow";
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(974, 538);
            this.Controls.Add(this.playControl1);
            this.Controls.Add(this.volumeBar);
            this.Controls.Add(this.trackBar);
            this.Controls.Add(this.MenuStrip);
            this.Controls.Add(this.LayoutPanel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MainWindow";
            this.Text = "Kitten Player";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.Click += new System.EventHandler(this.MainWindow_Click);
            this.DoubleClick += new System.EventHandler(this.MainWindow_DoubleClick);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.MainWindow_PreviewKeyDown);
            this.ContextTab.ResumeLayout(false);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.volumeBar)).EndInit();
            this.LayoutPanel.ResumeLayout(false);
            this.LayoutPanel.PerformLayout();
            this.SearchPanel.ResumeLayout(false);
            this.SearchPanel.PerformLayout();
            this.AddPlaylistStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.ContextMenuStrip ContextTab;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deletePlaylistToolStripMenuItem;
        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewPlaylistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.TrackBar volumeBar;
        private System.Windows.Forms.ToolStripMenuItem addDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem savePlaylistsToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel LayoutPanel;
        public ResultsPage ResultsPage;
        private System.Windows.Forms.ToolStripMenuItem playlistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addPlaylistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addYoutubePlaylistToolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip AddPlaylistStrip;
        private System.Windows.Forms.ToolStripMenuItem addPlaylistToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem trackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadAndPlayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadAgainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadOnlyToolStripMenuItem;
        private System.Windows.Forms.Panel SearchPanel;
        private PlayControl playControl1;
        public SearchPage searchBarPage;
        private MainTabs MainTab;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem abortOperationToolStripMenuItem;
    }
}

