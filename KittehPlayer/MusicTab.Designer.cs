namespace KittehPlayer
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
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
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
            this.PlaylistView.Location = new System.Drawing.Point(0, 0);
            this.PlaylistView.Name = "PlaylistView";
            this.PlaylistView.Size = new System.Drawing.Size(449, 281);
            this.PlaylistView.TabIndex = 0;
            this.PlaylistView.UseCompatibleStateImageBehavior = false;
            this.PlaylistView.View = System.Windows.Forms.View.Details;
            this.PlaylistView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.PlaylistView_ItemDrag);
            this.PlaylistView.Click += new System.EventHandler(this.PlaylistView_Click);
            this.PlaylistView.DragDrop += new System.Windows.Forms.DragEventHandler(this.PlaylistView_DragDrop);
            this.PlaylistView.DragEnter += new System.Windows.Forms.DragEventHandler(this.PlaylistView_DragEnter);
            this.PlaylistView.DragOver += new System.Windows.Forms.DragEventHandler(this.PlaylistView_DragOver);
            this.PlaylistView.DragLeave += new System.EventHandler(this.PlaylistView_DragLeave);
            this.PlaylistView.DoubleClick += new System.EventHandler(this.PlaylistView_DoubleClick);
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
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ListView PlaylistView;
        private System.Windows.Forms.ColumnHeader TrackName;
        private System.Windows.Forms.ColumnHeader PlaylistIndex;
    }
}
