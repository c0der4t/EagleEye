﻿namespace EagleEye_Fontend
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.pnlSettings = new System.Windows.Forms.Panel();
            this.pnlNotify = new System.Windows.Forms.Panel();
            this.imgNotifyIcon = new System.Windows.Forms.PictureBox();
            this.btnNotifyButton2 = new System.Windows.Forms.Button();
            this.btnNotifyButton1 = new System.Windows.Forms.Button();
            this.lblToastContent = new System.Windows.Forms.Label();
            this.pnlError = new System.Windows.Forms.Panel();
            this.btnSetupNow = new System.Windows.Forms.Button();
            this.redtErrorMSg = new System.Windows.Forms.RichTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnBrowseFile = new System.Windows.Forms.Button();
            this.pnlToast = new System.Windows.Forms.Panel();
            this.lblToast = new System.Windows.Forms.LinkLabel();
            this.lblms = new System.Windows.Forms.Label();
            this.chckbxPreventTriggerChain = new System.Windows.Forms.CheckBox();
            this.edtTriggerInterval = new System.Windows.Forms.TextBox();
            this.lblArgs = new System.Windows.Forms.Label();
            this.edtArgs = new System.Windows.Forms.TextBox();
            this.btnTestCommand = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblCommand = new System.Windows.Forms.Label();
            this.lblFileName = new System.Windows.Forms.Label();
            this.lblDir = new System.Windows.Forms.Label();
            this.edtCommand = new System.Windows.Forms.TextBox();
            this.edtFilename = new System.Windows.Forms.TextBox();
            this.edtDir = new System.Windows.Forms.TextBox();
            this.dlgBrowseFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuTrayIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuoptionInstall = new System.Windows.Forms.ToolStripMenuItem();
            this.menuoptionUninstall = new System.Windows.Forms.ToolStripMenuItem();
            this.menuoptionAddStartup = new System.Windows.Forms.ToolStripMenuItem();
            this.menuoptionRemoveStartup = new System.Windows.Forms.ToolStripMenuItem();
            this.menuoptionViewLogs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOptionClose = new System.Windows.Forms.ToolStripMenuItem();
            this.EagleEye = new System.IO.FileSystemWatcher();
            this.tmrTriggerChain = new System.Windows.Forms.Timer(this.components);
            this.dlgBrowseFile = new System.Windows.Forms.OpenFileDialog();
            this.tmrToastTimeout = new System.Windows.Forms.Timer(this.components);
            this.pnlSettings.SuspendLayout();
            this.pnlNotify.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgNotifyIcon)).BeginInit();
            this.pnlError.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlToast.SuspendLayout();
            this.menuTrayIcon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EagleEye)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSettings
            // 
            this.pnlSettings.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pnlSettings.Controls.Add(this.pnlNotify);
            this.pnlSettings.Controls.Add(this.pnlError);
            this.pnlSettings.Controls.Add(this.btnBrowseFile);
            this.pnlSettings.Controls.Add(this.pnlToast);
            this.pnlSettings.Controls.Add(this.lblms);
            this.pnlSettings.Controls.Add(this.chckbxPreventTriggerChain);
            this.pnlSettings.Controls.Add(this.edtTriggerInterval);
            this.pnlSettings.Controls.Add(this.lblArgs);
            this.pnlSettings.Controls.Add(this.edtArgs);
            this.pnlSettings.Controls.Add(this.btnTestCommand);
            this.pnlSettings.Controls.Add(this.btnRefresh);
            this.pnlSettings.Controls.Add(this.btnSave);
            this.pnlSettings.Controls.Add(this.btnBrowse);
            this.pnlSettings.Controls.Add(this.lblCommand);
            this.pnlSettings.Controls.Add(this.lblFileName);
            this.pnlSettings.Controls.Add(this.lblDir);
            this.pnlSettings.Controls.Add(this.edtCommand);
            this.pnlSettings.Controls.Add(this.edtFilename);
            this.pnlSettings.Controls.Add(this.edtDir);
            this.pnlSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSettings.Location = new System.Drawing.Point(0, 0);
            this.pnlSettings.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.pnlSettings.Name = "pnlSettings";
            this.pnlSettings.Size = new System.Drawing.Size(646, 327);
            this.pnlSettings.TabIndex = 0;
            // 
            // pnlNotify
            // 
            this.pnlNotify.BackColor = System.Drawing.Color.Gray;
            this.pnlNotify.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlNotify.Controls.Add(this.imgNotifyIcon);
            this.pnlNotify.Controls.Add(this.btnNotifyButton2);
            this.pnlNotify.Controls.Add(this.btnNotifyButton1);
            this.pnlNotify.Controls.Add(this.lblToastContent);
            this.pnlNotify.Location = new System.Drawing.Point(52, 53);
            this.pnlNotify.Name = "pnlNotify";
            this.pnlNotify.Size = new System.Drawing.Size(531, 234);
            this.pnlNotify.TabIndex = 20;
            this.pnlNotify.Visible = false;
            // 
            // imgNotifyIcon
            // 
            this.imgNotifyIcon.Image = global::EagleEye.Properties.Resources.white_notification;
            this.imgNotifyIcon.Location = new System.Drawing.Point(23, 30);
            this.imgNotifyIcon.Name = "imgNotifyIcon";
            this.imgNotifyIcon.Size = new System.Drawing.Size(133, 126);
            this.imgNotifyIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgNotifyIcon.TabIndex = 23;
            this.imgNotifyIcon.TabStop = false;
            // 
            // btnNotifyButton2
            // 
            this.btnNotifyButton2.BackColor = System.Drawing.Color.Transparent;
            this.btnNotifyButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNotifyButton2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnNotifyButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNotifyButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNotifyButton2.ForeColor = System.Drawing.Color.White;
            this.btnNotifyButton2.Location = new System.Drawing.Point(290, 175);
            this.btnNotifyButton2.Name = "btnNotifyButton2";
            this.btnNotifyButton2.Size = new System.Drawing.Size(197, 37);
            this.btnNotifyButton2.TabIndex = 22;
            this.btnNotifyButton2.Text = "Run EagleEye";
            this.btnNotifyButton2.UseVisualStyleBackColor = false;
            this.btnNotifyButton2.Click += new System.EventHandler(this.btnNotifyButton2_Click);
            // 
            // btnNotifyButton1
            // 
            this.btnNotifyButton1.BackColor = System.Drawing.Color.Transparent;
            this.btnNotifyButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNotifyButton1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnNotifyButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNotifyButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNotifyButton1.ForeColor = System.Drawing.Color.White;
            this.btnNotifyButton1.Location = new System.Drawing.Point(38, 175);
            this.btnNotifyButton1.Name = "btnNotifyButton1";
            this.btnNotifyButton1.Size = new System.Drawing.Size(197, 37);
            this.btnNotifyButton1.TabIndex = 21;
            this.btnNotifyButton1.Text = "Install EagleEye";
            this.btnNotifyButton1.UseVisualStyleBackColor = false;
            // 
            // lblToastContent
            // 
            this.lblToastContent.BackColor = System.Drawing.Color.Transparent;
            this.lblToastContent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblToastContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToastContent.ForeColor = System.Drawing.Color.White;
            this.lblToastContent.Location = new System.Drawing.Point(166, 30);
            this.lblToastContent.Name = "lblToastContent";
            this.lblToastContent.Size = new System.Drawing.Size(346, 126);
            this.lblToastContent.TabIndex = 0;
            this.lblToastContent.Text = "!EagleEye is not installed on the system.\r\nDo you want to install EagleEye perman" +
    "ently or run it just this once?";
            this.lblToastContent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlError
            // 
            this.pnlError.Controls.Add(this.btnSetupNow);
            this.pnlError.Controls.Add(this.redtErrorMSg);
            this.pnlError.Controls.Add(this.pictureBox1);
            this.pnlError.Location = new System.Drawing.Point(298, 305);
            this.pnlError.Name = "pnlError";
            this.pnlError.Size = new System.Drawing.Size(10, 10);
            this.pnlError.TabIndex = 11;
            // 
            // btnSetupNow
            // 
            this.btnSetupNow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetupNow.Location = new System.Drawing.Point(145, 230);
            this.btnSetupNow.Name = "btnSetupNow";
            this.btnSetupNow.Size = new System.Drawing.Size(379, 42);
            this.btnSetupNow.TabIndex = 2;
            this.btnSetupNow.Text = "Setup";
            this.btnSetupNow.UseVisualStyleBackColor = true;
            this.btnSetupNow.Click += new System.EventHandler(this.btnSetupNow_Click);
            // 
            // redtErrorMSg
            // 
            this.redtErrorMSg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.redtErrorMSg.Location = new System.Drawing.Point(185, 70);
            this.redtErrorMSg.Name = "redtErrorMSg";
            this.redtErrorMSg.Size = new System.Drawing.Size(379, 132);
            this.redtErrorMSg.TabIndex = 1;
            this.redtErrorMSg.Text = "EagleEye could not find any setup file.\nWithout the setup file EagleEye will not " +
    "function.\nPlease setup EagleEye now to ensure it functions correctly.\n\nPress [Se" +
    "tup] below so setup EagleEye now.";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::EagleEye.Properties.Resources.error;
            this.pictureBox1.Location = new System.Drawing.Point(35, 86);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(102, 98);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnBrowseFile
            // 
            this.btnBrowseFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseFile.Location = new System.Drawing.Point(598, 131);
            this.btnBrowseFile.Name = "btnBrowseFile";
            this.btnBrowseFile.Size = new System.Drawing.Size(31, 23);
            this.btnBrowseFile.TabIndex = 19;
            this.btnBrowseFile.Text = "...";
            this.btnBrowseFile.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnBrowseFile.UseVisualStyleBackColor = true;
            this.btnBrowseFile.Click += new System.EventHandler(this.btnBrowseFile_Click);
            // 
            // pnlToast
            // 
            this.pnlToast.BackColor = System.Drawing.Color.White;
            this.pnlToast.Controls.Add(this.lblToast);
            this.pnlToast.Location = new System.Drawing.Point(0, 0);
            this.pnlToast.Name = "pnlToast";
            this.pnlToast.Size = new System.Drawing.Size(648, 26);
            this.pnlToast.TabIndex = 18;
            // 
            // lblToast
            // 
            this.lblToast.ActiveLinkColor = System.Drawing.Color.Black;
            this.lblToast.BackColor = System.Drawing.Color.Transparent;
            this.lblToast.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToast.ForeColor = System.Drawing.Color.Black;
            this.lblToast.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lblToast.LinkColor = System.Drawing.Color.Black;
            this.lblToast.Location = new System.Drawing.Point(0, 0);
            this.lblToast.Name = "lblToast";
            this.lblToast.Size = new System.Drawing.Size(648, 26);
            this.lblToast.TabIndex = 0;
            this.lblToast.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblToast.TextChanged += new System.EventHandler(this.lblToast_TextChanged);
            this.lblToast.Click += new System.EventHandler(this.lblToast_Click);
            // 
            // lblms
            // 
            this.lblms.AutoSize = true;
            this.lblms.Location = new System.Drawing.Point(575, 241);
            this.lblms.Name = "lblms";
            this.lblms.Size = new System.Drawing.Size(32, 20);
            this.lblms.TabIndex = 17;
            this.lblms.Text = "ms";
            // 
            // chckbxPreventTriggerChain
            // 
            this.chckbxPreventTriggerChain.AutoSize = true;
            this.chckbxPreventTriggerChain.Location = new System.Drawing.Point(11, 237);
            this.chckbxPreventTriggerChain.Name = "chckbxPreventTriggerChain";
            this.chckbxPreventTriggerChain.Size = new System.Drawing.Size(429, 24);
            this.chckbxPreventTriggerChain.TabIndex = 16;
            this.chckbxPreventTriggerChain.Text = "Prevent trigger chains. Execute commands at interval";
            this.chckbxPreventTriggerChain.UseVisualStyleBackColor = true;
            this.chckbxPreventTriggerChain.CheckedChanged += new System.EventHandler(this.chckbxPreventTriggerChain_CheckedChanged);
            // 
            // edtTriggerInterval
            // 
            this.edtTriggerInterval.Enabled = false;
            this.edtTriggerInterval.Location = new System.Drawing.Point(445, 237);
            this.edtTriggerInterval.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.edtTriggerInterval.Name = "edtTriggerInterval";
            this.edtTriggerInterval.Size = new System.Drawing.Size(125, 26);
            this.edtTriggerInterval.TabIndex = 15;
            this.edtTriggerInterval.TextChanged += new System.EventHandler(this.edtTriggerInterval_TextChanged);
            // 
            // lblArgs
            // 
            this.lblArgs.AutoSize = true;
            this.lblArgs.Location = new System.Drawing.Point(424, 182);
            this.lblArgs.Name = "lblArgs";
            this.lblArgs.Size = new System.Drawing.Size(90, 20);
            this.lblArgs.TabIndex = 14;
            this.lblArgs.Text = "Arguments";
            // 
            // edtArgs
            // 
            this.edtArgs.Location = new System.Drawing.Point(428, 206);
            this.edtArgs.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.edtArgs.Name = "edtArgs";
            this.edtArgs.Size = new System.Drawing.Size(131, 26);
            this.edtArgs.TabIndex = 13;
            // 
            // btnTestCommand
            // 
            this.btnTestCommand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestCommand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestCommand.Location = new System.Drawing.Point(564, 206);
            this.btnTestCommand.Name = "btnTestCommand";
            this.btnTestCommand.Size = new System.Drawing.Size(65, 26);
            this.btnTestCommand.TabIndex = 12;
            this.btnTestCommand.Text = "Test";
            this.btnTestCommand.UseVisualStyleBackColor = true;
            this.btnTestCommand.Click += new System.EventHandler(this.btnTestCommand_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Location = new System.Drawing.Point(11, 278);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(197, 37);
            this.btnRefresh.TabIndex = 8;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(432, 278);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(197, 37);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.Location = new System.Drawing.Point(598, 53);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(31, 23);
            this.btnBrowse.TabIndex = 6;
            this.btnBrowse.Text = "...";
            this.btnBrowse.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lblCommand
            // 
            this.lblCommand.AutoSize = true;
            this.lblCommand.Location = new System.Drawing.Point(7, 182);
            this.lblCommand.Name = "lblCommand";
            this.lblCommand.Size = new System.Drawing.Size(285, 20);
            this.lblCommand.TabIndex = 5;
            this.lblCommand.Text = "Command to Be Executed on Trigger";
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(7, 107);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(359, 20);
            this.lblFileName.TabIndex = 4;
            this.lblFileName.Text = "Filename of File to Watch (including extension)";
            // 
            // lblDir
            // 
            this.lblDir.AutoSize = true;
            this.lblDir.Location = new System.Drawing.Point(7, 29);
            this.lblDir.Name = "lblDir";
            this.lblDir.Size = new System.Drawing.Size(117, 20);
            this.lblDir.TabIndex = 3;
            this.lblDir.Text = "Directory Path";
            // 
            // edtCommand
            // 
            this.edtCommand.Location = new System.Drawing.Point(11, 206);
            this.edtCommand.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.edtCommand.Name = "edtCommand";
            this.edtCommand.Size = new System.Drawing.Size(415, 26);
            this.edtCommand.TabIndex = 2;
            // 
            // edtFilename
            // 
            this.edtFilename.Location = new System.Drawing.Point(11, 131);
            this.edtFilename.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.edtFilename.Name = "edtFilename";
            this.edtFilename.Size = new System.Drawing.Size(582, 26);
            this.edtFilename.TabIndex = 1;
            // 
            // edtDir
            // 
            this.edtDir.Location = new System.Drawing.Point(11, 53);
            this.edtDir.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.edtDir.Name = "edtDir";
            this.edtDir.Size = new System.Drawing.Size(582, 26);
            this.edtDir.TabIndex = 0;
            // 
            // trayIcon
            // 
            this.trayIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.trayIcon.BalloonTipText = "Eagle Eye is still watching from down here!";
            this.trayIcon.BalloonTipTitle = "Eagle Eye Filewatcher";
            this.trayIcon.ContextMenuStrip = this.menuTrayIcon;
            this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
            this.trayIcon.Text = "EagleEye Filewatcher";
            this.trayIcon.Visible = true;
            this.trayIcon.Click += new System.EventHandler(this.trayIcon_Click);
            this.trayIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.trayIcon_MouseClick);
            // 
            // menuTrayIcon
            // 
            this.menuTrayIcon.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuTrayIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuoptionInstall,
            this.menuoptionUninstall,
            this.menuoptionAddStartup,
            this.menuoptionRemoveStartup,
            this.menuoptionViewLogs,
            this.menuOptionClose});
            this.menuTrayIcon.Name = "menuoptionClose";
            this.menuTrayIcon.Size = new System.Drawing.Size(312, 148);
            this.menuTrayIcon.Text = "Exit EagleEye";
            // 
            // menuoptionInstall
            // 
            this.menuoptionInstall.Name = "menuoptionInstall";
            this.menuoptionInstall.Size = new System.Drawing.Size(311, 24);
            this.menuoptionInstall.Text = "📥 Install EagleEye";
            // 
            // menuoptionUninstall
            // 
            this.menuoptionUninstall.Name = "menuoptionUninstall";
            this.menuoptionUninstall.Size = new System.Drawing.Size(311, 24);
            this.menuoptionUninstall.Text = "📤 Uninstall EagleEye";
            this.menuoptionUninstall.Visible = false;
            this.menuoptionUninstall.Click += new System.EventHandler(this.menuoptionUninstall_Click);
            // 
            // menuoptionAddStartup
            // 
            this.menuoptionAddStartup.Name = "menuoptionAddStartup";
            this.menuoptionAddStartup.Size = new System.Drawing.Size(311, 24);
            this.menuoptionAddStartup.Text = "➕ Add EagleEye to Startup";
            this.menuoptionAddStartup.Click += new System.EventHandler(this.AddtoStartup);
            // 
            // menuoptionRemoveStartup
            // 
            this.menuoptionRemoveStartup.Name = "menuoptionRemoveStartup";
            this.menuoptionRemoveStartup.Size = new System.Drawing.Size(311, 24);
            this.menuoptionRemoveStartup.Text = "⛔ Remove EagleEye From Startup";
            this.menuoptionRemoveStartup.Click += new System.EventHandler(this.menuoptionRemoveStartup_Click);
            // 
            // menuoptionViewLogs
            // 
            this.menuoptionViewLogs.Name = "menuoptionViewLogs";
            this.menuoptionViewLogs.Size = new System.Drawing.Size(311, 24);
            this.menuoptionViewLogs.Text = "📁 Open Logfile";
            this.menuoptionViewLogs.Click += new System.EventHandler(this.menuoptionViewLogs_Click);
            // 
            // menuOptionClose
            // 
            this.menuOptionClose.Name = "menuOptionClose";
            this.menuOptionClose.Size = new System.Drawing.Size(311, 24);
            this.menuOptionClose.Text = "❌ Exit EagleEye";
            this.menuOptionClose.Click += new System.EventHandler(this.menuOptionClose_Click);
            // 
            // EagleEye
            // 
            this.EagleEye.EnableRaisingEvents = true;
            this.EagleEye.SynchronizingObject = this;
            this.EagleEye.Changed += new System.IO.FileSystemEventHandler(this.EagleEye_Changed);
            // 
            // tmrTriggerChain
            // 
            this.tmrTriggerChain.Tick += new System.EventHandler(this.tmrTriggerChain_Tick);
            // 
            // dlgBrowseFile
            // 
            this.dlgBrowseFile.Title = "Select the file to watch";
            // 
            // tmrToastTimeout
            // 
            this.tmrToastTimeout.Interval = 2500;
            this.tmrToastTimeout.Tick += new System.EventHandler(this.tmrToastTimeout_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(646, 327);
            this.ContextMenuStrip = this.menuTrayIcon;
            this.Controls.Add(this.pnlSettings);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.MaximumSize = new System.Drawing.Size(664, 374);
            this.MinimumSize = new System.Drawing.Size(664, 374);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EagleEye FileWatcher FE";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.pnlSettings.ResumeLayout(false);
            this.pnlSettings.PerformLayout();
            this.pnlNotify.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgNotifyIcon)).EndInit();
            this.pnlError.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlToast.ResumeLayout(false);
            this.menuTrayIcon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.EagleEye)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSettings;
        private System.Windows.Forms.Label lblCommand;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.Label lblDir;
        private System.Windows.Forms.TextBox edtCommand;
        private System.Windows.Forms.TextBox edtFilename;
        private System.Windows.Forms.TextBox edtDir;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.FolderBrowserDialog dlgBrowseFolder;
        private System.Windows.Forms.Button btnTestCommand;
        private System.Windows.Forms.TextBox edtArgs;
        private System.Windows.Forms.Label lblArgs;
        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.IO.FileSystemWatcher EagleEye;
        private System.Windows.Forms.CheckBox chckbxPreventTriggerChain;
        private System.Windows.Forms.TextBox edtTriggerInterval;
        private System.Windows.Forms.Label lblms;
        private System.Windows.Forms.Timer tmrTriggerChain;
        private System.Windows.Forms.Panel pnlToast;
        private System.Windows.Forms.LinkLabel lblToast;
        private System.Windows.Forms.Button btnBrowseFile;
        private System.Windows.Forms.OpenFileDialog dlgBrowseFile;
        private System.Windows.Forms.Timer tmrToastTimeout;
        private System.Windows.Forms.Panel pnlNotify;
        private System.Windows.Forms.Label lblToastContent;
        private System.Windows.Forms.Button btnNotifyButton1;
        private System.Windows.Forms.Button btnNotifyButton2;
        private System.Windows.Forms.PictureBox imgNotifyIcon;
        private System.Windows.Forms.ContextMenuStrip menuTrayIcon;
        private System.Windows.Forms.ToolStripMenuItem menuOptionClose;
        private System.Windows.Forms.ToolStripMenuItem menuoptionRemoveStartup;
        private System.Windows.Forms.ToolStripMenuItem menuoptionInstall;
        private System.Windows.Forms.ToolStripMenuItem menuoptionUninstall;
        private System.Windows.Forms.ToolStripMenuItem menuoptionAddStartup;
        private System.Windows.Forms.ToolStripMenuItem menuoptionViewLogs;
        private System.Windows.Forms.Panel pnlError;
        private System.Windows.Forms.Button btnSetupNow;
        private System.Windows.Forms.RichTextBox redtErrorMSg;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

