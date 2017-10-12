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
            this.PlaylistIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TrackName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DropDownMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ContextPlay = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextPause = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextStop = new System.Windows.Forms.ToolStripMenuItem();
            this.ContextRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.Album = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.DropDownMenu.SuspendLayout();
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
            this.PlaylistView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlaylistView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PlaylistView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PlaylistIndex,
            this.TrackName,
            this.Album});
            this.PlaylistView.FullRowSelect = true;
            this.PlaylistView.HideSelection = false;
            this.PlaylistView.Location = new System.Drawing.Point(0, 0);
            this.PlaylistView.Name = "PlaylistView";
            this.PlaylistView.Size = new System.Drawing.Size(449, 281);
            this.PlaylistView.TabIndex = 0;
            this.PlaylistView.UseCompatibleStateImageBehavior = false;
            this.PlaylistView.View = System.Windows.Forms.View.Details;
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
            // 
            // PlaylistIndex
            // 
            this.PlaylistIndex.Text = "Index";
            this.PlaylistIndex.Width = 49;
            // 
            // TrackName
            // 
            this.TrackName.DisplayIndex = 1;
            this.TrackName.Text = "Name";
            this.TrackName.Width = 269;
            // 
            // DropDownMenu
            // 
            this.DropDownMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ContextPlay,
            this.ContextPause,
            this.ContextStop,
            this.ContextRemove});
            this.DropDownMenu.Name = "contextMenuStrip1";
            this.DropDownMenu.Size = new System.Drawing.Size(142, 92);
            // 
            // ContextPlay
            // 
            this.ContextPlay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.ContextPlay.Name = "ContextPlay";
            this.ContextPlay.Size = new System.Drawing.Size(141, 22);
            this.ContextPlay.Text = "Play";
            this.ContextPlay.Click += new System.EventHandler(this.Play_Click);
            // 
            // ContextPause
            // 
            this.ContextPause.Name = "ContextPause";
            this.ContextPause.Size = new System.Drawing.Size(141, 22);
            this.ContextPause.Text = "Pause";
            this.ContextPause.Click += new System.EventHandler(this.pauseToolStripMenuItem_Click);
            // 
            // ContextStop
            // 
            this.ContextStop.Name = "ContextStop";
            this.ContextStop.Size = new System.Drawing.Size(141, 22);
            this.ContextStop.Text = "Stop";
            this.ContextStop.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // ContextRemove
            // 
            this.ContextRemove.Name = "ContextRemove";
            this.ContextRemove.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.ContextRemove.Size = new System.Drawing.Size(141, 22);
            this.ContextRemove.Text = "Remove";
            this.ContextRemove.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // Album
            // 
            this.Album.DisplayIndex = 1;
            this.Album.Text = "Album";
            this.Album.Width = 99;
            // 
            // MusicTab
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.CausesValidation = false;
            this.Controls.Add(this.PlaylistView);
            this.Name = "MusicTab";
            this.Size = new System.Drawing.Size(449, 281);
            this.Click += new System.EventHandler(this.MusicTab_Click);
            this.DoubleClick += new System.EventHandler(this.MusicTab_DoubleClick);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.DropDownMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ColumnHeader TrackName;
        private System.Windows.Forms.ColumnHeader PlaylistIndex;
        private System.Windows.Forms.ContextMenuStrip DropDownMenu;
        private System.Windows.Forms.ToolStripMenuItem ContextPlay;
        private System.Windows.Forms.ToolStripMenuItem ContextPause;
        private System.Windows.Forms.ToolStripMenuItem ContextStop;
        private System.Windows.Forms.ToolStripMenuItem ContextRemove;
        public System.Windows.Forms.ListView PlaylistView;
        private System.Windows.Forms.ColumnHeader Album;
    }
}
