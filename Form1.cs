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


//ToDO
//Tray Icon Minimization not working correctly


namespace EagleEye_Fontend
{
    public partial class frmMain : Form
    {
        public string sDirectory;
        public string FileToWatch;
        public string CommandtoEx;
        public string sLogFilePath;
        public string sCommandArgs;
        public string ToastCommand;
        public bool CanCommand = true;
        public bool Refresh = false;
        public bool TriggerChain;
        public int TriggerInterval;

       private string startUpFolderPath =
                  Environment.GetFolderPath(Environment.SpecialFolder.Startup);

        RegistryKey registryKey = Registry.CurrentUser.OpenSubKey
                    ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

        public frmMain()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            dlgBrowseFolder.ShowDialog();
            edtDir.Text = dlgBrowseFolder.SelectedPath;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            btnTestCommand.Height = edtCommand.Height;

           

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
                        Toast(args[2],args[3],false);
                        ToastCommand = "checkstatus";
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
                if (System.IO.File.Exists(startUpFolderPath + "\\" +
                   Application.ProductName + ".lnk") == false)
                {
                    Toast("EagleEye is not set to run on startup. Click here to add it to startup.", "error", false);
                    ToastCommand = "startup";
                }
            }
            else
            {
                Toast("EagleEye is not installed on this system. Click here to install EagleEye now.", "warning", false);
                ToastCommand = "install";
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
                    NewProc.Arguments = "toast \"EagleEye Successfully Installed on the System\" success";
                    Process.Start(NewProc);
                    Application.Exit();
                }
                else
                {
                    Log("EagleEye could not be installed. Unknown Error");
                    ProcessStartInfo NewProc = new ProcessStartInfo(Application.ExecutablePath);
                    NewProc.Arguments = "toast \"EagleEye could not be installed. Unknown Error\" error";
                    Process.Start(NewProc);
                    Application.Exit();
                }
            }
            catch (Exception e)
            {
                Log("EagleEye could not be installed. Error Follows:");
                Log(e.ToString());
                ProcessStartInfo NewProc = new ProcessStartInfo(Application.ExecutablePath);
                NewProc.Arguments = "toast \"EagleEye could not be installed. Error in Log File\" error";
                Process.Start(NewProc);
                Application.Exit();
            }   
        }

        private void UninstallEagleEye()
        {
            EagleEye.EnableRaisingEvents = false;
            string ProgramFilesPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            string InstallPath = Path.Combine(ProgramFilesPath, "EagleEye");
            string ExePath = Path.Combine(InstallPath, "EagleEye.exe");
        }

        private void Toast(string Message, string MessageType,bool AutoTimeout)
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
                LoadSetup();
                //this.WindowState = FormWindowState.Minimized;
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
                pnlError.Visible = true;
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
                    this.WindowState = FormWindowState.Minimized;
                }
                
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Refresh = true;
            LoadSetup();
        }

        private void RegisterInStartup(bool Register)
        {
            
            if (Register)
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
                    Toast("EagleEye has been added to startup.", "success",true);
                    ToastCommand = "";
                }
                else
                {
                    Toast("EagleEye could not be added to startup. Unknown Error. Click to retry.", "error",false);
                    ToastCommand = "startup";
                }

                   
            }
            else
            {
                System.IO.File.Delete(startUpFolderPath + "\\" +
                    Application.ProductName + ".lnk");
                Toast("EagleEye has been removed from startup.", "error",false);
            }
        }


        private void btnSetupNow_Click(object sender, EventArgs e)
        {
            pnlError.Visible = false;
        }

        private void btnTestCommand_Click(object sender, EventArgs e)
        {
            if ((edtCommand.Text == "") || (edtCommand.Text == " "))
            {
                MessageBox.Show("Please Enter a Command to Test","No Command to Test", MessageBoxButtons.OK, MessageBoxIcon.Warning,
    MessageBoxDefaultButton.Button1);
            }
            else
            {
                ProcessStartInfo NewCmd = new ProcessStartInfo(@edtCommand.Text);
                NewCmd.Arguments = edtArgs.Text;
                Process.Start(NewCmd);
            }
            
        }

        private void trayIcon_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            trayIcon.Visible = false;
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
        }

        private void EagleEye_Changed(object sender, FileSystemEventArgs e)
        {

            if (CanCommand)
            {
                Log("File changed. Executing Command : " + CommandtoEx + ' ' + sCommandArgs);

                ProcessStartInfo NewCmd = new ProcessStartInfo(@CommandtoEx);
                NewCmd.Arguments = sCommandArgs;
                Process.Start(NewCmd);

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
            switch (ToastCommand)
            {
                case "startup":
                    RegisterInStartup(true);
                    ToastCommand = "";
                    break;
                case "install":
                    try
                    {
                        Process NewProc = new Process();
                        NewProc.StartInfo.FileName = Application.ExecutablePath;
                        NewProc.StartInfo.Arguments = "install";
                        NewProc.StartInfo.UseShellExecute = true;
                        NewProc.StartInfo.Verb = "runas";
                        NewProc.Start();
                        Application.Exit();
                    }
                    catch (Exception AuthE)
                    {
                        MessageBox.Show("An error occurred while trying to install EagleEye:" + Environment.NewLine + AuthE.ToString(),"Unable to Install",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
                    }               
                    break;
                case "checkstatus":
                    CheckStatus();
                    break;
                default:
                    pnlToast.Visible = false;
                    break;
            }
        }

        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            dlgBrowseFile.InitialDirectory = edtDir.Text;
            dlgBrowseFile.ShowDialog();
            edtFilename.Text = dlgBrowseFile.FileName.Substring(dlgBrowseFile.FileName.LastIndexOf("\\")+1);
        }

        private void tmrToastTimeout_Tick(object sender, EventArgs e)
        {
            lblToast.Text = "";
            pnlToast.Visible = false;
            tmrToastTimeout.Enabled = false;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
                if (MessageBox.Show("Are you sure you want to close EagleEye? EagleEye will no longer monitor your file for changes!", "Close EagleEye", MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
