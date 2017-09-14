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
            this.Play = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.PlaylistView.AllowDrop = true;
            this.PlaylistView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlaylistView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PlaylistView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PlaylistIndex,
            this.TrackName});
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
            this.PlaylistView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PlaylistView_MouseDown);
            this.PlaylistView.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.PlaylistView_PreviewKeyDown);
            // 
            // PlaylistIndex
            // 
            this.PlaylistIndex.Text = "Index";
            this.PlaylistIndex.Width = 49;
            // 
            // TrackName
            // 
            this.TrackName.Text = "Name";
            this.TrackName.Width = 269;
            // 
            // DropDownMenu
            // 
            this.DropDownMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Play,
            this.pauseToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.removeToolStripMenuItem});
            this.DropDownMenu.Name = "contextMenuStrip1";
            this.DropDownMenu.Size = new System.Drawing.Size(142, 92);
            // 
            // Play
            // 
            this.Play.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.Play.Name = "Play";
            this.Play.Size = new System.Drawing.Size(141, 22);
            this.Play.Text = "Play";
            this.Play.Click += new System.EventHandler(this.Play_Click);
            // 
            // pauseToolStripMenuItem
            // 
            this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
            this.pauseToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.pauseToolStripMenuItem.Text = "Pause";
            this.pauseToolStripMenuItem.Click += new System.EventHandler(this.pauseToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
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
        private System.Windows.Forms.ListView PlaylistView;
        private System.Windows.Forms.ColumnHeader TrackName;
        private System.Windows.Forms.ColumnHeader PlaylistIndex;
        private System.Windows.Forms.ContextMenuStrip DropDownMenu;
        private System.Windows.Forms.ToolStripMenuItem Play;
        private System.Windows.Forms.ToolStripMenuItem pauseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
    }
}
