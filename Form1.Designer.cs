namespace youtube_dl_ez_2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.audioRadioButton = new System.Windows.Forms.RadioButton();
            this.videoRadioButton = new System.Windows.Forms.RadioButton();
            this.urlTextBox = new System.Windows.Forms.TextBox();
            this.startDownloadButton = new System.Windows.Forms.Button();
            this.consoleOutputTextbox = new System.Windows.Forms.TextBox();
            this.qualitySelectListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.thumbnailPreviewBox = new System.Windows.Forms.PictureBox();
            this.thumbnailPreviewLabel = new System.Windows.Forms.Label();
            this.playVideoButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDownloadsFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeDownloadsFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreDefaultDownloadsFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetDefaultConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForYtdlpUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoBestQualityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subtitlesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noThumbnailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.writeLogsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showProgressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.thumbnailPreviewBox)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // audioRadioButton
            // 
            this.audioRadioButton.AutoSize = true;
            this.audioRadioButton.ForeColor = System.Drawing.Color.White;
            this.audioRadioButton.Location = new System.Drawing.Point(14, 37);
            this.audioRadioButton.Name = "audioRadioButton";
            this.audioRadioButton.Size = new System.Drawing.Size(57, 19);
            this.audioRadioButton.TabIndex = 1;
            this.audioRadioButton.Text = "Audio";
            this.audioRadioButton.UseVisualStyleBackColor = true;
            this.audioRadioButton.CheckedChanged += new System.EventHandler(this.audioRadioButton_CheckedChanged);
            // 
            // videoRadioButton
            // 
            this.videoRadioButton.AutoSize = true;
            this.videoRadioButton.Checked = true;
            this.videoRadioButton.ForeColor = System.Drawing.Color.White;
            this.videoRadioButton.Location = new System.Drawing.Point(14, 12);
            this.videoRadioButton.Name = "videoRadioButton";
            this.videoRadioButton.Size = new System.Drawing.Size(55, 19);
            this.videoRadioButton.TabIndex = 0;
            this.videoRadioButton.TabStop = true;
            this.videoRadioButton.Text = "Video";
            this.videoRadioButton.UseVisualStyleBackColor = true;
            this.videoRadioButton.CheckedChanged += new System.EventHandler(this.videoRadioButton_CheckedChanged);
            // 
            // urlTextBox
            // 
            this.urlTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.urlTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.urlTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.urlTextBox.ForeColor = System.Drawing.Color.White;
            this.urlTextBox.Location = new System.Drawing.Point(12, 212);
            this.urlTextBox.MaxLength = 1000;
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.Size = new System.Drawing.Size(686, 22);
            this.urlTextBox.TabIndex = 2;
            this.urlTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.urlTextBox_KeyPress);
            // 
            // startDownloadButton
            // 
            this.startDownloadButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.startDownloadButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.startDownloadButton.ForeColor = System.Drawing.Color.White;
            this.startDownloadButton.Location = new System.Drawing.Point(704, 212);
            this.startDownloadButton.Name = "startDownloadButton";
            this.startDownloadButton.Size = new System.Drawing.Size(83, 22);
            this.startDownloadButton.TabIndex = 2;
            this.startDownloadButton.Text = "Download";
            this.startDownloadButton.UseVisualStyleBackColor = false;
            this.startDownloadButton.Click += new System.EventHandler(this.startDownloadButton_Click);
            // 
            // consoleOutputTextbox
            // 
            this.consoleOutputTextbox.AcceptsReturn = true;
            this.consoleOutputTextbox.AcceptsTab = true;
            this.consoleOutputTextbox.AllowDrop = true;
            this.consoleOutputTextbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.consoleOutputTextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.consoleOutputTextbox.ForeColor = System.Drawing.Color.White;
            this.consoleOutputTextbox.Location = new System.Drawing.Point(12, 239);
            this.consoleOutputTextbox.Multiline = true;
            this.consoleOutputTextbox.Name = "consoleOutputTextbox";
            this.consoleOutputTextbox.ReadOnly = true;
            this.consoleOutputTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.consoleOutputTextbox.Size = new System.Drawing.Size(776, 199);
            this.consoleOutputTextbox.TabIndex = 0;
            // 
            // qualitySelectListBox
            // 
            this.qualitySelectListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.qualitySelectListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.qualitySelectListBox.ForeColor = System.Drawing.Color.White;
            this.qualitySelectListBox.FormattingEnabled = true;
            this.qualitySelectListBox.ItemHeight = 15;
            this.qualitySelectListBox.Location = new System.Drawing.Point(12, 56);
            this.qualitySelectListBox.Name = "qualitySelectListBox";
            this.qualitySelectListBox.Size = new System.Drawing.Size(120, 150);
            this.qualitySelectListBox.TabIndex = 3;
            this.qualitySelectListBox.SelectedIndexChanged += new System.EventHandler(this.qualitySelectListBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Quality Select:";
            // 
            // thumbnailPreviewBox
            // 
            this.thumbnailPreviewBox.Location = new System.Drawing.Point(177, 29);
            this.thumbnailPreviewBox.Name = "thumbnailPreviewBox";
            this.thumbnailPreviewBox.Size = new System.Drawing.Size(433, 177);
            this.thumbnailPreviewBox.TabIndex = 5;
            this.thumbnailPreviewBox.TabStop = false;
            // 
            // thumbnailPreviewLabel
            // 
            this.thumbnailPreviewLabel.AutoSize = true;
            this.thumbnailPreviewLabel.Location = new System.Drawing.Point(177, 9);
            this.thumbnailPreviewLabel.Name = "thumbnailPreviewLabel";
            this.thumbnailPreviewLabel.Size = new System.Drawing.Size(0, 15);
            this.thumbnailPreviewLabel.TabIndex = 6;
            // 
            // playVideoButton
            // 
            this.playVideoButton.Enabled = false;
            this.playVideoButton.Image = ((System.Drawing.Image)(resources.GetObject("playVideoButton.Image")));
            this.playVideoButton.Location = new System.Drawing.Point(638, 218);
            this.playVideoButton.Name = "playVideoButton";
            this.playVideoButton.Size = new System.Drawing.Size(23, 10);
            this.playVideoButton.TabIndex = 11;
            this.playVideoButton.UseVisualStyleBackColor = true;
            this.playVideoButton.Visible = false;
            this.playVideoButton.Click += new System.EventHandler(this.playVideoButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(796, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openDownloadsFolderToolStripMenuItem,
            this.changeDownloadsFolderToolStripMenuItem,
            this.restoreDefaultDownloadsFolderToolStripMenuItem,
            this.resetDefaultConfigToolStripMenuItem,
            this.checkForYtdlpUpdateToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openDownloadsFolderToolStripMenuItem
            // 
            this.openDownloadsFolderToolStripMenuItem.Name = "openDownloadsFolderToolStripMenuItem";
            this.openDownloadsFolderToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.openDownloadsFolderToolStripMenuItem.Text = "Open Downloads Folder";
            this.openDownloadsFolderToolStripMenuItem.Click += new System.EventHandler(this.openDownloadDirButton_Click);
            // 
            // changeDownloadsFolderToolStripMenuItem
            // 
            this.changeDownloadsFolderToolStripMenuItem.Name = "changeDownloadsFolderToolStripMenuItem";
            this.changeDownloadsFolderToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.changeDownloadsFolderToolStripMenuItem.Text = "Change Downloads Folder";
            this.changeDownloadsFolderToolStripMenuItem.Click += new System.EventHandler(this.chooseDownloadDirButton_Click);
            // 
            // restoreDefaultDownloadsFolderToolStripMenuItem
            // 
            this.restoreDefaultDownloadsFolderToolStripMenuItem.Name = "restoreDefaultDownloadsFolderToolStripMenuItem";
            this.restoreDefaultDownloadsFolderToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.restoreDefaultDownloadsFolderToolStripMenuItem.Text = "Restore Default Downloads Folder";
            this.restoreDefaultDownloadsFolderToolStripMenuItem.Click += new System.EventHandler(this.restoreDefaultDownloadsFolderToolStripMenuItem_Click);
            // 
            // resetDefaultConfigToolStripMenuItem
            // 
            this.resetDefaultConfigToolStripMenuItem.Name = "resetDefaultConfigToolStripMenuItem";
            this.resetDefaultConfigToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.resetDefaultConfigToolStripMenuItem.Text = "Restore Default Settings";
            this.resetDefaultConfigToolStripMenuItem.Click += new System.EventHandler(this.resetDefaultConfigToolStripMenuItem_Click);
            // 
            // checkForYtdlpUpdateToolStripMenuItem
            // 
            this.checkForYtdlpUpdateToolStripMenuItem.Name = "checkForYtdlpUpdateToolStripMenuItem";
            this.checkForYtdlpUpdateToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.checkForYtdlpUpdateToolStripMenuItem.Text = "Check for yt-dlp Update";
            this.checkForYtdlpUpdateToolStripMenuItem.Click += new System.EventHandler(this.checkForYtdlpUpdateToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoBestQualityToolStripMenuItem,
            this.subtitlesToolStripMenuItem,
            this.noThumbnailToolStripMenuItem,
            this.writeLogsToolStripMenuItem,
            this.showProgressToolStripMenuItem});
            this.optionsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // autoBestQualityToolStripMenuItem
            // 
            this.autoBestQualityToolStripMenuItem.CheckOnClick = true;
            this.autoBestQualityToolStripMenuItem.Name = "autoBestQualityToolStripMenuItem";
            this.autoBestQualityToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.autoBestQualityToolStripMenuItem.Text = "Auto Best Quality";
            this.autoBestQualityToolStripMenuItem.Click += new System.EventHandler(this.autoBestQualityToolStripMenuItem_Click);
            // 
            // subtitlesToolStripMenuItem
            // 
            this.subtitlesToolStripMenuItem.Checked = true;
            this.subtitlesToolStripMenuItem.CheckOnClick = true;
            this.subtitlesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.subtitlesToolStripMenuItem.Name = "subtitlesToolStripMenuItem";
            this.subtitlesToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.subtitlesToolStripMenuItem.Text = "Subtitles";
            this.subtitlesToolStripMenuItem.Click += new System.EventHandler(this.downloadSubtitlesCheckBox_CheckedChanged);
            // 
            // noThumbnailToolStripMenuItem
            // 
            this.noThumbnailToolStripMenuItem.CheckOnClick = true;
            this.noThumbnailToolStripMenuItem.Name = "noThumbnailToolStripMenuItem";
            this.noThumbnailToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.noThumbnailToolStripMenuItem.Text = "No Thumbnail";
            this.noThumbnailToolStripMenuItem.Click += new System.EventHandler(this.noThumbnailToolStripMenuItem_Click);
            // 
            // writeLogsToolStripMenuItem
            // 
            this.writeLogsToolStripMenuItem.Checked = true;
            this.writeLogsToolStripMenuItem.CheckOnClick = true;
            this.writeLogsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.writeLogsToolStripMenuItem.Name = "writeLogsToolStripMenuItem";
            this.writeLogsToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.writeLogsToolStripMenuItem.Text = "Save Download History";
            this.writeLogsToolStripMenuItem.Click += new System.EventHandler(this.writeLogTextCheckBox_CheckedChanged);
            // 
            // showProgressToolStripMenuItem
            // 
            this.showProgressToolStripMenuItem.CheckOnClick = true;
            this.showProgressToolStripMenuItem.Name = "showProgressToolStripMenuItem";
            this.showProgressToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.showProgressToolStripMenuItem.Text = "Show Progress";
            this.showProgressToolStripMenuItem.Click += new System.EventHandler(this.showProgressCheckBox_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.panel1.Controls.Add(this.videoRadioButton);
            this.panel1.Controls.Add(this.audioRadioButton);
            this.panel1.Location = new System.Drawing.Point(704, 139);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(83, 67);
            this.panel1.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.ClientSize = new System.Drawing.Size(796, 446);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.thumbnailPreviewLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.qualitySelectListBox);
            this.Controls.Add(this.startDownloadButton);
            this.Controls.Add(this.urlTextBox);
            this.Controls.Add(this.consoleOutputTextbox);
            this.Controls.Add(this.thumbnailPreviewBox);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.playVideoButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "youtube-dl-ez-2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.thumbnailPreviewBox)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private RadioButton audioRadioButton;
        private RadioButton videoRadioButton;
        private TextBox urlTextBox;
        private Button startDownloadButton;
        private TextBox consoleOutputTextbox;
        private ListBox qualitySelectListBox;
        private Label label1;
        private PictureBox thumbnailPreviewBox;
        private Label thumbnailPreviewLabel;
        private Button playVideoButton;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openDownloadsFolderToolStripMenuItem;
        private ToolStripMenuItem changeDownloadsFolderToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private Panel panel1;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem subtitlesToolStripMenuItem;
        private ToolStripMenuItem writeLogsToolStripMenuItem;
        private ToolStripMenuItem showProgressToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem autoBestQualityToolStripMenuItem;
        private ToolStripMenuItem resetDefaultConfigToolStripMenuItem;
        private ToolStripMenuItem noThumbnailToolStripMenuItem;
        private ToolStripMenuItem checkForYtdlpUpdateToolStripMenuItem;
        private ToolStripMenuItem restoreDefaultDownloadsFolderToolStripMenuItem;
    }
}