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
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                if (args[1] == "e")
                {
                    pnlError.Visible = true;
                }
            }
            btnTestCommand.Height = edtCommand.Height;
            if (registryKey.GetValue("EagleEye") == null)
            {
                Toast("EagleEye is not set to run on startup. Click here to add it to startup.", "error");
                ToastCommand = "startup";
            }
            LoadSetup();
        }

        private void Toast(string Message, string MessageType)
        {
            if (MessageType == "error")
            {
                pnlToast.BackColor = Color.Firebrick;
            }

            if (MessageType == "success")
            {
                pnlToast.BackColor = Color.MediumSeaGreen;
            }

            lblToast.Text = Message;
            lblToast.Location = new Point((pnlToast.Width - lblToast.Width) / 2, lblToast.Location.Y);
            pnlToast.Visible = true;
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
                this.WindowState = FormWindowState.Minimized;
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

            if (Directory.Exists(@"C:\Program Files\EagleEye") == false)
            {
                StreamWriter ErrorTxT = new StreamWriter(@"C:\Users\Public\Documents\EagleEyeFatalError.txt");
                ErrorTxT.WriteLine("Eagle Eye Fatal Error !!:");
                ErrorTxT.WriteLine("The Eagle Eye Installation is Missing.");
                ErrorTxT.WriteLine("Please Re-install Eagle Eye.");
                ErrorTxT.Close();
                Process.Start("CMD.exe", @"C:\Users\Public\Documents\EagleEyeFatalError.txt");
                return;
            }

            if (File.Exists(@"C:\Users\Public\Documents\EagleEye\setup.txt") == false)
            {
                pnlError.Visible = true;
                return;
            }
            else
            {


                if (File.Exists(@"C:\Users\Public\Documents\EagleEye\setup.txt"))
                {
                    using (StreamReader sr = File.OpenText(@"C:\Users\Public\Documents\EagleEye\setup.txt"))
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
                registryKey.SetValue("EagleEye", Application.ExecutablePath);
                Toast("EagleEye has been added to startup.","success");
            }
            else
            {
                registryKey.DeleteValue("EagleEye");
                Toast("EagleEye has been removed from startup.", "error");
            }
        }


        private void btnSetupNow_Click(object sender, EventArgs e)
        {
            pnlError.Visible = false;
        }

        private void btnTestCommand_Click(object sender, EventArgs e)
        {
            ProcessStartInfo NewCmd = new ProcessStartInfo(@edtCommand.Text);
            NewCmd.Arguments = edtArgs.Text;
            Process.Start(NewCmd);
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
    }
}
