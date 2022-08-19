using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Resources;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;
using Microsoft.Win32;
using IWshRuntimeLibrary;
using System.Runtime.InteropServices;
using System.CodeDom;

namespace EagleEye_Fontend
{
    public partial class frmMain : Form
    {
        public string VersionString = "Twitch" ;
        public string VersionCode =  "2022.08.18" ;

        public string sDirectory;
        public string FileToWatch;
        public string CommandtoEx;
        public string sLogFilePath;
        public string sCommandArgs;
        public string ToastCommand;
        public bool CanCommand = true;
        public bool Refresh = false;
        public bool Terminate;
        public bool ForceTerminate;
        public bool TriggerChain;
        public int TriggerInterval;

        private EventHandler LastNotifyBTN1;
        private EventHandler LastNotifyBTN2;

        private string startUpFolderPath =
                  Environment.GetFolderPath(Environment.SpecialFolder.Startup);

        RegistryKey registryKey = Registry.CurrentUser.OpenSubKey
                    ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

        public frmMain()
        {
            InitializeComponent();
            btnNotifyButton1.Click += new EventHandler(ClearNotification);
            LastNotifyBTN1 = ClearNotification;
            btnNotifyButton2.Click += new EventHandler(ClearNotification);
            LastNotifyBTN2 = ClearNotification;

            
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            dlgBrowseFolder.ShowDialog();
            edtDir.Text = dlgBrowseFolder.SelectedPath;  
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            btnTestCommand.Height = edtCommand.Height;

            lblToast.Text = "EagleEye Version : " + VersionCode + " (" + VersionString + ")";


            //Checking Command Arguments
            string[] args = Environment.GetCommandLineArgs();

            if (args.Length > 1)
            {
                if (args[1] == "install")
                {
                    InstallEagleEye();
                }

                if (args.Length >= 4)
                {
                    if (args[1] == "toast")
                    {
                        Notify(args[2], args[3], 2000, false, false, "Ok", "", ClearNotification, ClearNotification);
                        return;
                    }
                }
            }
            //Checking Command Arguments

            LoadSetup();
            CheckStatus();

        }

        private void CheckStatus()
        {
            //Checking Status of Application on System
            if (IsInstalled() == true)
            {
                menuoptionInstall.Visible = false;
                menuoptionUninstall.Visible = true;

                if (System.IO.File.Exists(startUpFolderPath + "\\" +
                   Application.ProductName + ".lnk") == false)
                {
                    menuoptionAddStartup.Visible = true;
                    menuoptionRemoveStartup.Visible = false;
                    string NotifyMsg = "EagleEye is not set to run on startup. This means EagleEye will not monitor your files automatically." + Environment.NewLine + "Add EagleEye to startup now?";
                    Notify(NotifyMsg, "error", 0, true, true, "Yes", "No", AddtoStartup, ClearNotification);
                }
                else
                {
                    menuoptionAddStartup.Visible = false;
                    menuoptionRemoveStartup.Visible = true;
                }
            }
            else
            {
                menuoptionInstall.Visible = true;
                menuoptionUninstall.Visible = false;
                Notify("EagleEye is not installed on the system." + Environment.NewLine + "Do you want to install EagleEye permanently or run it just this once ? ", "neutral", 0, true, true, "Install", "Run", StageInstall, ClearNotification);
            }
            //Checking Status of Application on System
        }

        private bool IsInstalled()
        {
            string ProgramFilesPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            string InstallPath = Path.Combine(ProgramFilesPath, "EagleEye");
            if (System.IO.File.Exists(Path.Combine(InstallPath, "EagleEye.exe")))
            {
                return true;
            }
            return false;
        }



        private void InstallEagleEye()
        {

            if (Directory.Exists(@"C:\Users\Public\Documents\EagleEye") == false)
            {
                Directory.CreateDirectory(@"C:\Users\Public\Documents\EagleEye");
            }
            sLogFilePath = @"C:\Users\Public\Documents\EagleEye\log.txt";

            try
            {
                EagleEye.EnableRaisingEvents = false;
                string ProgramFilesPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                string InstallPath = Path.Combine(ProgramFilesPath, "EagleEye");
                string ExePath = Path.Combine(InstallPath, "EagleEye.exe");

                if (Directory.Exists(InstallPath) == false)
                {
                    Directory.CreateDirectory(InstallPath);
                }

                System.IO.File.Copy(Application.ExecutablePath, Path.Combine(InstallPath, "EagleEye.exe"));


                if (IsInstalled())
                {
                    Log("EagleEye Successfully Installed");
                    ProcessStartInfo NewProc = new ProcessStartInfo(ExePath);
                    // (string Message, string MessageType, int MessageTimeout, bool ShowIcon, bool ShowFrontend, string Button1Text, string Button2Text, EventHandler Button1Click, EventHandler Button2Click)
                    NewProc.Arguments = "toast \"EagleEye Successfully Installed on the System\" success";
                    System.Diagnostics.Process.Start(NewProc);
                }
                else
                {
                    Log("EagleEye could not be installed. Unknown Error");
                    ProcessStartInfo NewProc = new ProcessStartInfo(Application.ExecutablePath);
                    NewProc.Arguments = "toast \"EagleEye could not be installed. Unknown Error\" error";
                    System.Diagnostics.Process.Start(NewProc);
                }
            }
            catch (Exception cError)
            {
                Log("EagleEye could not be installed. Error Follows:");
                Log(cError.ToString());
                ProcessStartInfo NewProc = new ProcessStartInfo(Application.ExecutablePath);
                NewProc.Arguments = "toast \"EagleEye could not be installed. Error in Log File\" error";
                System.Diagnostics.Process.Start(NewProc);
            }
            ForceExit();
        }

        private void UninstallEagleEye()
        {
            EagleEye.EnableRaisingEvents = false;
            string ProgramFilesPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            string InstallPath = Path.Combine(ProgramFilesPath, "EagleEye");
            string ExePath = Path.Combine(InstallPath, "EagleEye.exe");
        }

        /// <summary>
        /// Displays a notification window with the given information.
        /// </summary>
        /// <param name="Message">The message of the notification. Including necessary line breaks.</param>
        /// <param name="MessageType">Determines the notification color. neutral,success,error,warning.</param>
        /// <param name="MessageTimeout">Determines if the message should close after a set period. 0 means message will not disappear.</param>
        /// <param name="ShowIcon">Determines if the notification icon should be shown.</param>
        /// <param name="ShowFrontend">Forces frontend to pop up/maximize.</param>
        /// <param name="Button1Text">Text of button 1 on the notification. If empty button will not be shown.</param>
        /// <param name="Button2Text">Text of button 2 on the notification. If empty button will not be shown.</param>
        /// <param name="Button1Click">Method to be called on button 1 click.</param>
        /// <param name="Button2Click">Method to be called on button 2 click.</param>
        private void Notify(string Message, string MessageType, int MessageTimeout, bool ShowIcon, bool ShowFrontend, string Button1Text, string Button2Text, EventHandler Button1Click, EventHandler Button2Click)
        {

            if (ShowIcon)
            {
                imgNotifyIcon.Visible = ShowIcon;
                lblToastContent.Left = 166;
                lblToastContent.Top = 30;
                lblToastContent.Width = 346;
            }
            else
            {
                imgNotifyIcon.Visible = ShowIcon;
                lblToastContent.Left = 23;
                lblToastContent.Top = 30;
                lblToastContent.Width = 489;
            }

            lblToastContent.Text = Message;

            if (Button1Text.Length > 0)
            {
                btnNotifyButton1.Text = Button1Text;
                btnNotifyButton1.Click -= new EventHandler(LastNotifyBTN1);
                btnNotifyButton1.Click += new EventHandler(Button1Click);
                LastNotifyBTN1 = Button1Click;
                btnNotifyButton1.Left = 38;
                btnNotifyButton1.Visible = true;
            }
            else
            {
                btnNotifyButton1.Visible = false;
            }

            if (Button2Text.Length > 0)
            {
                btnNotifyButton2.Text = Button2Text;
                btnNotifyButton2.Click -= new EventHandler(LastNotifyBTN2);
                btnNotifyButton2.Click += new EventHandler(Button2Click);
                LastNotifyBTN2 = Button2Click;
                btnNotifyButton2.Left = 290;
                btnNotifyButton2.Visible = true;
            }
            else
            {
                btnNotifyButton2.Visible = false;
            }

            if ((Button1Text.Length == 0) || (Button2Text.Length == 0))
            {
                btnNotifyButton1.Left = 167;
                btnNotifyButton2.Left = 167;
            }

            switch (MessageType)
            {
                case "neutral":
                    pnlNotify.BackColor = Color.Gray;
                    break;
                case "error":
                    pnlNotify.BackColor = Color.Firebrick;
                    break;
                case "success":
                    pnlNotify.BackColor = Color.MediumSeaGreen;
                    break;
                case "warning":
                    pnlNotify.BackColor = Color.FromArgb(255, 180, 0);
                    break;
                default:
                    pnlNotify.BackColor = Color.Gray;
                    break;
            }

            if (MessageTimeout > 0)
            {
                tmrToastTimeout.Interval = MessageTimeout;
                tmrToastTimeout.Enabled = true;
            }

            if (ShowFrontend)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
                trayIcon.Visible = false;
            }

            pnlNotify.Visible = true;

        }


        private void Toast(string Message, string MessageType, bool AutoTimeout)
        {
            switch (MessageType)
            {
                case "error":
                    pnlToast.BackColor = Color.Firebrick;
                    break;
                case "success":
                    pnlToast.BackColor = Color.MediumSeaGreen;
                    break;
                case "warning":
                    pnlToast.BackColor = Color.FromArgb(255, 180, 0);
                    break;
                default:
                    break;
            }

            lblToast.Text = Message;
            lblToast.Location = new Point((pnlToast.Width - lblToast.Width) / 2, lblToast.Location.Y);
            pnlToast.Visible = true;

            if (AutoTimeout == true)
            {
                tmrToastTimeout.Enabled = true;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.Show();
                this.ShowInTaskbar = true;
                trayIcon.Visible = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string InputInterval = "0";

            if (chckbxPreventTriggerChain.Checked == true)
            {
                if ((edtTriggerInterval.Text == ""))
                {
                    edtTriggerInterval.Text = "0";
                }

                if (Convert.ToInt32(edtTriggerInterval.Text) == 0)
                {
                    MessageBox.Show("Please enter an interval higher than 0.", "Invalid Interval.", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    edtTriggerInterval.Focus();
                    return;
                }
                else
                {
                    InputInterval = edtTriggerInterval.Text;
                }
            }

            if (MessageBox.Show("Save setup and minimize to tray?", "Save and minimize?",
    MessageBoxButtons.YesNo, MessageBoxIcon.Question,
    MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                StreamWriter SetupFile = new StreamWriter(@"C:\Users\Public\Documents\EagleEye\setup.txt");
                SetupFile.WriteLine(edtDir.Text);
                SetupFile.WriteLine(edtFilename.Text);
                SetupFile.WriteLine(edtCommand.Text);
                SetupFile.WriteLine(edtArgs.Text);
                SetupFile.WriteLine(InputInterval);
                SetupFile.Close();
                Refresh = false;
                LoadSetup();
            }

        }

        private void LoadSetup()
        {

            EagleEye.EnableRaisingEvents = false;
            if (Directory.Exists(@"C:\Users\Public\Documents\EagleEye") == false)
            {
                Directory.CreateDirectory(@"C:\Users\Public\Documents\EagleEye");
            }
            sLogFilePath = @"C:\Users\Public\Documents\EagleEye\log.txt";
            Log("EagleEye Started");

            if (System.IO.File.Exists(@"C:\Users\Public\Documents\EagleEye\setup.txt") == false)
            {
                //string NotifyMsg = "EagleEye could not find any setup file." + Environment.NewLine + "Without the setup file EagleEye will not function." + Environment.NewLine + "Please setup EagleEye now to ensure it functions correctly.";
                // Notify(NotifyMsg,"error",0,true,true,"Setup Now","Exit",ClearNotification,ForceExit);
                return;
            }
            else
            {


                if (System.IO.File.Exists(@"C:\Users\Public\Documents\EagleEye\setup.txt"))
                {
                    using (StreamReader sr = System.IO.File.OpenText(@"C:\Users\Public\Documents\EagleEye\setup.txt"))
                    {
                        string s = String.Empty;
                        int LineCount = 1;
                        while ((s = sr.ReadLine()) != null)
                        {
                            switch (LineCount)
                            {
                                case 1:
                                    sDirectory = s;
                                    break;
                                case 2:
                                    FileToWatch = s;
                                    break;
                                case 3:
                                    CommandtoEx = s;
                                    break;
                                case 4:
                                    sCommandArgs = s;
                                    break;
                                case 5:
                                    TriggerInterval = Convert.ToInt32(s);
                                    if (TriggerInterval > 0)
                                    {
                                        TriggerChain = true;
                                    }
                                    else
                                    {
                                        TriggerChain = false;
                                    }
                                    break;
                                default:
                                    break;
                            }
                            LineCount += 1;
                        }
                        sr.Close();
                    }

                }
                edtDir.Text = sDirectory;
                edtFilename.Text = FileToWatch;
                edtCommand.Text = CommandtoEx;
                edtArgs.Text = sCommandArgs;
                edtTriggerInterval.Text = Convert.ToString(TriggerInterval);
                chckbxPreventTriggerChain.Checked = TriggerChain;


                EagleEye.Path = sDirectory;
                EagleEye.Filter = FileToWatch;
                EagleEye.EnableRaisingEvents = true;

                if (Refresh == false)
                {
                    MinimizeToTray();
                }

            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Refresh = true;
            LoadSetup();
        }


        private void btnSetupNow_Click(object sender, EventArgs e)
        {
            pnlError.Visible = false;
        }

        private void btnTestCommand_Click(object sender, EventArgs e)
        {
            if ((edtCommand.Text == "") || (edtCommand.Text == " "))
            {
                MessageBox.Show("Please Enter a Command to Test", "No Command to Test", MessageBoxButtons.OK, MessageBoxIcon.Warning,
    MessageBoxDefaultButton.Button1);
            }
            else
            {
                ProcessStartInfo NewCmd = new ProcessStartInfo(@edtCommand.Text);
                NewCmd.Arguments = edtArgs.Text;
                System.Diagnostics.Process.Start(NewCmd);
            }

        }

        private void trayIcon_Click(object sender, EventArgs e)
        {

        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                MinimizeToTray();
            }
        }

        public void MinimizeToTray()
        {
            trayIcon.Visible = true;
            trayIcon.ShowBalloonTip(3000);
            this.ShowInTaskbar = false;
            this.Hide();
        }

        private void EagleEye_Changed(object sender, FileSystemEventArgs e)
        {

            if (CanCommand)
            {
                Log("File changed. Executing Command : " + CommandtoEx + ' ' + sCommandArgs);

                ProcessStartInfo NewCmd = new ProcessStartInfo(@CommandtoEx);
                NewCmd.Arguments = sCommandArgs;
                System.Diagnostics.Process.Start(NewCmd);

                Log("Executed command successfully");
                if (TriggerChain)
                {
                    CanCommand = false;
                    tmrTriggerChain.Interval = TriggerInterval;
                    tmrTriggerChain.Start();
                }
            }
            else
            {
                Log("File Changed. Cannot Execute Command: Trigger Chain Blocked.");
            }

        }

        private void Log(string sLogMsg)
        {
            StreamWriter LogFile = new StreamWriter(sLogFilePath, append: true);
            LogFile.WriteLine(Convert.ToString(DateTime.Now) + " - " + sLogMsg);
            LogFile.Close();
        }

        private void edtTriggerInterval_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(edtTriggerInterval.Text, "[^0-9]"))
            {
                edtTriggerInterval.Text = edtTriggerInterval.Text.Remove(edtTriggerInterval.Text.Length - 1);
            }
        }

        private void chckbxPreventTriggerChain_CheckedChanged(object sender, EventArgs e)
        {
            if (chckbxPreventTriggerChain.Checked)
            {
                edtTriggerInterval.Enabled = true;
            }
            else
            {
                edtTriggerInterval.Enabled = false;
            }
        }

        private void tmrTriggerChain_Tick(object sender, EventArgs e)
        {
            CanCommand = true;
        }

        private void lblToast_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblToast_Click(object sender, EventArgs e)
        {
        }

        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            dlgBrowseFile.InitialDirectory = edtDir.Text;
            dlgBrowseFile.ShowDialog();
            edtFilename.Text = dlgBrowseFile.FileName.Substring(dlgBrowseFile.FileName.LastIndexOf("\\") + 1);
        }

        private void tmrToastTimeout_Tick(object sender, EventArgs e)
        {



            lblToastContent.Text = "";
            pnlNotify.Visible = false;

            tmrToastTimeout.Enabled = false;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (!Terminate)
            {
                e.Cancel = true;
                MinimizeToTray();
            }

        }


        //Notify Actions

        private void StageInstall(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process NewProc = new System.Diagnostics.Process();
                NewProc.StartInfo.FileName = Application.ExecutablePath;
                NewProc.StartInfo.Arguments = "install";
                NewProc.StartInfo.UseShellExecute = true;
                NewProc.StartInfo.Verb = "runas";
                NewProc.Start();
                ForceExit();
            }
            catch (Exception AuthE)
            {
                MessageBox.Show("An error occurred while trying to install EagleEye:" + Environment.NewLine + AuthE.ToString(), "Unable to Install", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void StageUninstall(object sender, EventArgs e)
        {
            System.IO.File.Delete(startUpFolderPath + "\\" +
                    Application.ProductName + ".lnk");

            string ProgramFilesPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            MarkForDeleteOnReboot(Path.Combine(ProgramFilesPath, "EagleEye"));

            DialogResult ConfirmRestart = MessageBox.Show("A restart is required to complete the uninstall process." + Environment.NewLine + "Restart Now?", "Restart Now?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ConfirmRestart == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start("shutdown", "/r /t 0");
            }
        }

        //From Stackoverflow Begin
        private void MarkForDeleteOnReboot(string directoryPath)
        {


            foreach (string directory in Directory.GetDirectories(directoryPath, "*", SearchOption.TopDirectoryOnly))
            {
                MarkForDeleteOnReboot(directory);
            }

            foreach (string file in Directory.GetFiles(directoryPath, "*", SearchOption.TopDirectoryOnly))
            {
                NativeMethods.MoveFileEx(file, null, MoveFileFlags.DelayUntilReboot);
            }
            NativeMethods.MoveFileEx(directoryPath, null, MoveFileFlags.DelayUntilReboot);
        }

        public static class NativeMethods
        {
            [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
            internal static extern bool MoveFileEx(string lpExistingFileName, string lpNewFileName, MoveFileFlags dwFlags);
        }

        [Flags]
        public enum MoveFileFlags
        {
            DelayUntilReboot = 0x00000004
        }
        //From Stackoverflow end

        private void AddtoStartup(object sender, EventArgs e)
        {
            //From StackOverflow
            WshShell wshShell = new WshShell();

            IWshRuntimeLibrary.IWshShortcut shortcut;

            shortcut =
              (IWshRuntimeLibrary.IWshShortcut)wshShell.CreateShortcut(
                startUpFolderPath + "\\" +
                Application.ProductName + ".lnk");

            shortcut.TargetPath = Application.ExecutablePath;
            shortcut.WorkingDirectory = Application.StartupPath;
            shortcut.Description = "Launch EagleEye on Startup";
            shortcut.Save();
            //From StackOverflow

            if (System.IO.File.Exists(startUpFolderPath + "\\" +
               Application.ProductName + ".lnk") == true)
            {
                Notify("EagleEye has been added to startup.", "success", 120000, false, false, "Ok", "", ClearNotification, ClearNotification);
                menuoptionAddStartup.Visible = false;
                menuoptionRemoveStartup.Visible = true;
            }
            else
            {
                Notify("EagleEye could not be added to startup. Unknown Error.", "error", 0, true, true, "Retry", "Cancel", AddtoStartup, ClearNotification);
                menuoptionAddStartup.Visible = true;
                menuoptionRemoveStartup.Visible = false;
            }
        }

        private void RemoveFromStartup(object sender, EventArgs e)
        {
            System.IO.File.Delete(startUpFolderPath + "\\" +
                    Application.ProductName + ".lnk");

            if (System.IO.File.Exists(startUpFolderPath + "\\" +
              Application.ProductName + ".lnk") == true)
            {
                Notify("EagleEye could not be removed from startup. Unknown Error.", "error", 0, true, true, "Retry", "Cancel", RemoveFromStartup, ClearNotification);
            }
            else
            {
                Notify("EagleEye has been removed from startup.", "warning", 0, false, false, "Ok", "", ClearNotification, ClearNotification);
            }
        }

        private void ClearNotification(object sender, EventArgs e)
        {
            pnlNotify.Visible = false;

        }

        private void ForceExit()
        {
            Terminate = true;
            Close();
        }

        private void trayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.ShowInTaskbar = true;
                trayIcon.Visible = false;
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.BringToFront();
            }

        }

        private void menuOptionClose_Click(object sender, EventArgs e)
        {
            DialogResult ConfirmExit = MessageBox.Show("Are you sure you want to close EagleEye. It will no longer monitor your files.", "Exit EagleEye ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ConfirmExit == DialogResult.Yes)
            {
                ForceExit();
            }

        }

        private void menuoptionRemoveStartup_Click(object sender, EventArgs e)
        {
            Notify("Removing EagleEye from startup will stop it from monitoring your files automatically." + Environment.NewLine + "Continue?", "warning", 0, true, true, "Yes", "No", RemoveFromStartup, ClearNotification);
        }

        private void menuoptionViewLogs_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(sLogFilePath);
        }

        private void menuoptionUninstall_Click(object sender, EventArgs e)
        {
            string NotifyMsg = "Are you sure you would like to uninstall EagleEye?" + Environment.NewLine + "This will also remove EagleEye from startup." + Environment.NewLine + "A restart will be required.";

            Notify(NotifyMsg, "neutral", 0, true, true, "Uninstall", "Cancel", StageUninstall, ClearNotification);
        }

        private void btnNotifyButton2_Click(object sender, EventArgs e)
        {

        }
    }
}
