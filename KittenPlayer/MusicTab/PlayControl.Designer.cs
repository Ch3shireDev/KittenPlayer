namespace KittenPlayer
{
    partial class PlayControl
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
            this.RepeatButton = new System.Windows.Forms.Button();
            this.NextButton = new System.Windows.Forms.Button();
            this.PreviousButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.PauseButton = new System.Windows.Forms.Button();
            this.PlayButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RepeatButton
            // 
            this.RepeatButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.RepeatButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.RepeatButton.Location = new System.Drawing.Point(178, 3);
            this.RepeatButton.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.RepeatButton.Name = "RepeatButton";
            this.RepeatButton.Size = new System.Drawing.Size(32, 32);
            this.RepeatButton.TabIndex = 6;
            this.RepeatButton.UseVisualStyleBackColor = true;
            this.RepeatButton.Click += new System.EventHandler(this.RepeatButton_Click);
            this.RepeatButton.ChangeUICues += new System.Windows.Forms.UICuesEventHandler(this.RepeatButton_ChangeUICues);
            // 
            // NextButton
            // 
            this.NextButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.NextButton.BackgroundImage = global::KittenPlayer.Properties.Resources.skip;
            this.NextButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.NextButton.Location = new System.Drawing.Point(143, 3);
            this.NextButton.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(32, 32);
            this.NextButton.TabIndex = 5;
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // PreviousButton
            // 
            this.PreviousButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PreviousButton.BackgroundImage = global::KittenPlayer.Properties.Resources.previous;
            this.PreviousButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.PreviousButton.Location = new System.Drawing.Point(108, 3);
            this.PreviousButton.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.PreviousButton.Name = "PreviousButton";
            this.PreviousButton.Size = new System.Drawing.Size(32, 32);
            this.PreviousButton.TabIndex = 4;
            this.PreviousButton.UseVisualStyleBackColor = true;
            this.PreviousButton.Click += new System.EventHandler(this.PreviousButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.StopButton.BackgroundImage = global::KittenPlayer.Properties.Resources.stop;
            this.StopButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.StopButton.Location = new System.Drawing.Point(3, 3);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(32, 32);
            this.StopButton.TabIndex = 1;
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // PauseButton
            // 
            this.PauseButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PauseButton.BackgroundImage = global::KittenPlayer.Properties.Resources.pause;
            this.PauseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.PauseButton.Location = new System.Drawing.Point(73, 3);
            this.PauseButton.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.PauseButton.Name = "PauseButton";
            this.PauseButton.Size = new System.Drawing.Size(32, 32);
            this.PauseButton.TabIndex = 3;
            this.PauseButton.UseVisualStyleBackColor = true;
            this.PauseButton.Click += new System.EventHandler(this.PauseButton_Click);
            // 
            // PlayButton
            // 
            this.PlayButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PlayButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PlayButton.BackgroundImage = global::KittenPlayer.Properties.Resources.play;
            this.PlayButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.PlayButton.Location = new System.Drawing.Point(38, 3);
            this.PlayButton.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(32, 32);
            this.PlayButton.TabIndex = 2;
            this.PlayButton.UseVisualStyleBackColor = true;
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // PlayControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.RepeatButton);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.PreviousButton);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.PauseButton);
            this.Controls.Add(this.PlayButton);
            this.Name = "PlayControl";
            this.Size = new System.Drawing.Size(210, 38);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button PauseButton;
        private System.Windows.Forms.Button PlayButton;
        private System.Windows.Forms.Button PreviousButton;
        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.Button RepeatButton;
    }
}
