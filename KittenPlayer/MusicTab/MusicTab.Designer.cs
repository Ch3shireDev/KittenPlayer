namespace KittenPlayer
{
    partial class MusicTab
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.PlaylistView = new System.Windows.Forms.ListView();
            this.TrackName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Artist = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Album = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Track = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.filePath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.YoutubeID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DropDownMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextPlay = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextPause = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextStop = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.changePropertyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChangeArtistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChangeAlbumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChangeTrackNumberToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChangeTitleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PlaylistProperties = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.xxxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.DropDownMenu.SuspendLayout();
            this.PlaylistProperties.SuspendLayout();
            this.SuspendLayout();
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // PlaylistView
            // 
            this.PlaylistView.AllowColumnReorder = true;
            this.PlaylistView.AllowDrop = true;
            this.PlaylistView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PlaylistView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TrackName,
            this.Artist,
            this.Album,
            this.Track,
            this.Status,
            this.filePath,
            this.fileName,
            this.YoutubeID});
            this.PlaylistView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PlaylistView.FullRowSelect = true;
            this.PlaylistView.HideSelection = false;
            this.PlaylistView.LabelEdit = true;
            this.PlaylistView.Location = new System.Drawing.Point(0, 0);
            this.PlaylistView.Name = "PlaylistView";
            this.PlaylistView.Size = new System.Drawing.Size(449, 281);
            this.PlaylistView.TabIndex = 0;
            this.PlaylistView.UseCompatibleStateImageBehavior = false;
            this.PlaylistView.View = System.Windows.Forms.View.Details;
            this.PlaylistView.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.PlaylistView_AfterLabelEdit);
            this.PlaylistView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.PlaylistView_ItemDrag);
            this.PlaylistView.SelectedIndexChanged += new System.EventHandler(this.PlaylistView_SelectedIndexChanged);
            this.PlaylistView.Click += new System.EventHandler(this.PlaylistView_Click);
            this.PlaylistView.DragDrop += new System.Windows.Forms.DragEventHandler(this.PlaylistView_DragDrop);
            this.PlaylistView.DragEnter += new System.Windows.Forms.DragEventHandler(this.PlaylistView_DragEnter);
            this.PlaylistView.DragOver += new System.Windows.Forms.DragEventHandler(this.PlaylistView_DragOver);
            this.PlaylistView.DragLeave += new System.EventHandler(this.PlaylistView_DragLeave);
            this.PlaylistView.DoubleClick += new System.EventHandler(this.PlaylistView_DoubleClick);
            this.PlaylistView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PlaylistView_KeyPress);
            this.PlaylistView.Leave += new System.EventHandler(this.PlaylistView_Leave);
            this.PlaylistView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PlaylistView_MouseDown);
            this.PlaylistView.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.PlaylistView_PreviewKeyDown);
            // 
            // TrackName
            // 
            this.TrackName.DisplayIndex = 3;
            this.TrackName.Text = "Track Name";
            this.TrackName.Width = 269;
            // 
            // Artist
            // 
            this.Artist.DisplayIndex = 0;
            this.Artist.Text = "Artist";
            this.Artist.Width = 92;
            // 
            // Album
            // 
            this.Album.DisplayIndex = 1;
            this.Album.Text = "Album";
            this.Album.Width = 99;
            // 
            // Track
            // 
            this.Track.DisplayIndex = 2;
            this.Track.Text = "Index";
            this.Track.Width = 49;
            // 
            // Status
            // 
            this.Status.Text = "Status";
            // 
            // filePath
            // 
            this.filePath.Text = "Path";
            this.filePath.Width = 200;
            // 
            // fileName
            // 
            this.fileName.Text = "Name";
            this.fileName.Width = 400;
            // 
            // YoutubeID
            // 
            this.YoutubeID.Text = "ID";
            this.YoutubeID.Width = 100;
            // 
            // DropDownMenu
            // 
            this.DropDownMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextPlay,
            this.ContextPause,
            this.ContextStop,
            this.ContextRemove,
            this.changePropertyToolStripMenuItem});
            this.DropDownMenu.Name = "contextMenuStrip1";
            this.DropDownMenu.Size = new System.Drawing.Size(164, 114);
            this.DropDownMenu.Opening += new System.ComponentModel.CancelEventHandler(this.DropDownMenu_Opening);
            // 
            // ContextPlay
            // 
            this.ContextPlay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.ContextPlay.Name = "ContextPlay";
            this.ContextPlay.Size = new System.Drawing.Size(163, 22);
            this.ContextPlay.Text = "Play";
            this.ContextPlay.Click += new System.EventHandler(this.Play_Click);
            // 
            // ContextPause
            // 
            this.ContextPause.Name = "ContextPause";
            this.ContextPause.Size = new System.Drawing.Size(163, 22);
            this.ContextPause.Text = "Pause";
            this.ContextPause.Click += new System.EventHandler(this.pauseToolStripMenuItem_Click);
            // 
            // ContextStop
            // 
            this.ContextStop.Name = "ContextStop";
            this.ContextStop.Size = new System.Drawing.Size(163, 22);
            this.ContextStop.Text = "Stop";
            this.ContextStop.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // ContextRemove
            // 
            this.ContextRemove.Name = "ContextRemove";
            this.ContextRemove.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.ContextRemove.Size = new System.Drawing.Size(163, 22);
            this.ContextRemove.Text = "Remove";
            this.ContextRemove.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // changePropertyToolStripMenuItem
            // 
            this.changePropertyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ChangeArtistToolStripMenuItem,
            this.ChangeAlbumToolStripMenuItem,
            this.ChangeTrackNumberToolStripMenuItem,
            this.ChangeTitleToolStripMenuItem});
            this.changePropertyToolStripMenuItem.Name = "changePropertyToolStripMenuItem";
            this.changePropertyToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.changePropertyToolStripMenuItem.Text = "Change Property";
            this.changePropertyToolStripMenuItem.Click += new System.EventHandler(this.changePropertyToolStripMenuItem_Click);
            // 
            // ChangeArtistToolStripMenuItem
            // 
            this.ChangeArtistToolStripMenuItem.Name = "ChangeArtistToolStripMenuItem";
            this.ChangeArtistToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.ChangeArtistToolStripMenuItem.Text = "Artist";
            this.ChangeArtistToolStripMenuItem.Click += new System.EventHandler(this.ChangeArtistToolStripMenuItem_Click);
            // 
            // ChangeAlbumToolStripMenuItem
            // 
            this.ChangeAlbumToolStripMenuItem.Name = "ChangeAlbumToolStripMenuItem";
            this.ChangeAlbumToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.ChangeAlbumToolStripMenuItem.Text = "Album";
            this.ChangeAlbumToolStripMenuItem.Click += new System.EventHandler(this.ChangeAlbumToolStripMenuItem_Click);
            // 
            // ChangeTrackNumberToolStripMenuItem
            // 
            this.ChangeTrackNumberToolStripMenuItem.Name = "ChangeTrackNumberToolStripMenuItem";
            this.ChangeTrackNumberToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.ChangeTrackNumberToolStripMenuItem.Text = "Track Number";
            this.ChangeTrackNumberToolStripMenuItem.Click += new System.EventHandler(this.ChangeTrackNumberToolStripMenuItem_Click);
            // 
            // ChangeTitleToolStripMenuItem
            // 
            this.ChangeTitleToolStripMenuItem.Name = "ChangeTitleToolStripMenuItem";
            this.ChangeTitleToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.ChangeTitleToolStripMenuItem.Text = "Title";
            this.ChangeTitleToolStripMenuItem.Click += new System.EventHandler(this.ChangeTitleToolStripMenuItem_Click);
            // 
            // PlaylistProperties
            // 
            this.PlaylistProperties.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xxxToolStripMenuItem});
            this.PlaylistProperties.Name = "PlaylistProperties";
            this.PlaylistProperties.Size = new System.Drawing.Size(90, 26);
            this.PlaylistProperties.Opened += new System.EventHandler(this.PlaylistProperties_Opened);
            this.PlaylistProperties.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.PlaylistProperties_PreviewKeyDown);
            // 
            // xxxToolStripMenuItem
            // 
            this.xxxToolStripMenuItem.Name = "xxxToolStripMenuItem";
            this.xxxToolStripMenuItem.Size = new System.Drawing.Size(89, 22);
            this.xxxToolStripMenuItem.Text = "xxx";
            // 
            // MusicTab
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.CausesValidation = false;
            this.ContextMenuStrip = this.DropDownMenu;
            this.Controls.Add(this.PlaylistView);
            this.Name = "MusicTab";
            this.Size = new System.Drawing.Size(449, 281);
            this.Click += new System.EventHandler(this.MusicTab_Click);
            this.DoubleClick += new System.EventHandler(this.MusicTab_DoubleClick);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.DropDownMenu.ResumeLayout(false);
            this.PlaylistProperties.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ColumnHeader TrackName;
        private System.Windows.Forms.ColumnHeader Track;
        private System.Windows.Forms.ContextMenuStrip DropDownMenu;
        private System.Windows.Forms.ToolStripMenuItem ContextPlay;
        private System.Windows.Forms.ToolStripMenuItem ContextPause;
        private System.Windows.Forms.ToolStripMenuItem ContextStop;
        private System.Windows.Forms.ToolStripMenuItem ContextRemove;
        public System.Windows.Forms.ListView PlaylistView;
        private System.Windows.Forms.ColumnHeader Album;
        private System.Windows.Forms.ContextMenuStrip PlaylistProperties;
        private System.Windows.Forms.ToolStripMenuItem xxxToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader Artist;
        private System.Windows.Forms.ToolStripMenuItem changePropertyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ChangeArtistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ChangeAlbumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ChangeTrackNumberToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ChangeTitleToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader Status;
        private System.Windows.Forms.ColumnHeader filePath;
        private System.Windows.Forms.ColumnHeader fileName;
        private System.Windows.Forms.ColumnHeader YoutubeID;
    }
}
