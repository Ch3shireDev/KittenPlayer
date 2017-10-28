namespace KittenPlayer.MainTabs
{
    partial class Pages
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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ContextTab = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewPlaylistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deletePlaylistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddPlaylistStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addPlaylistToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.MainTabs = new System.Windows.Forms.TabControl();
            this.ContextTab.SuspendLayout();
            this.AddPlaylistStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(561, 365);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(192, 74);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ContextTab
            // 
            this.ContextTab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renameToolStripMenuItem,
            this.addNewPlaylistToolStripMenuItem,
            this.deletePlaylistToolStripMenuItem});
            this.ContextTab.Name = "ContextTab";
            this.ContextTab.Size = new System.Drawing.Size(162, 70);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.renameToolStripMenuItem.Text = "Rename playlist";
            // 
            // addNewPlaylistToolStripMenuItem
            // 
            this.addNewPlaylistToolStripMenuItem.Name = "addNewPlaylistToolStripMenuItem";
            this.addNewPlaylistToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.addNewPlaylistToolStripMenuItem.Text = "Add playlist";
            // 
            // deletePlaylistToolStripMenuItem
            // 
            this.deletePlaylistToolStripMenuItem.Name = "deletePlaylistToolStripMenuItem";
            this.deletePlaylistToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.deletePlaylistToolStripMenuItem.Text = "Delete playlist";
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
            this.addPlaylistToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.addPlaylistToolStripMenuItem1.Text = "Add Playlist";
            // 
            // MainTabs
            // 
            this.MainTabs.AllowDrop = true;
            this.MainTabs.CausesValidation = false;
            this.MainTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTabs.Location = new System.Drawing.Point(0, 0);
            this.MainTabs.Name = "MainTabs";
            this.MainTabs.SelectedIndex = 0;
            this.MainTabs.Size = new System.Drawing.Size(569, 391);
            this.MainTabs.TabIndex = 0;
            this.MainTabs.Click += new System.EventHandler(this.MainTabs_Click);
            // 
            // Pages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MainTabs);
            this.Name = "Pages";
            this.Size = new System.Drawing.Size(569, 391);
            this.ContextTab.ResumeLayout(false);
            this.AddPlaylistStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ContextMenuStrip ContextTab;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewPlaylistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deletePlaylistToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip AddPlaylistStrip;
        private System.Windows.Forms.ToolStripMenuItem addPlaylistToolStripMenuItem1;
        private System.Windows.Forms.TabControl MainTabs;
    }
}
