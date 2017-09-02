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
            this.PlaylistBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // PlaylistBox
            // 
            this.PlaylistBox.AllowDrop = true;
            this.PlaylistBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PlaylistBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PlaylistBox.FormattingEnabled = true;
            this.PlaylistBox.Location = new System.Drawing.Point(0, 0);
            this.PlaylistBox.Name = "PlaylistBox";
            this.PlaylistBox.Size = new System.Drawing.Size(449, 281);
            this.PlaylistBox.TabIndex = 0;
            this.PlaylistBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.PlaylistBox_DragDrop);
            this.PlaylistBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.PlaylistBox_DragEnter);
            this.PlaylistBox.DoubleClick += new System.EventHandler(this.PlaylistBox_DoubleClick);
            // 
            // MusicTab
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.CausesValidation = false;
            this.Controls.Add(this.PlaylistBox);
            this.Name = "MusicTab";
            this.Size = new System.Drawing.Size(449, 281);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox PlaylistBox;
    }
}
