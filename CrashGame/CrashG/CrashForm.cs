using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrashG
{
    public partial class CrashForm : System.Windows.Forms.Form
    {
        public string pth = null;

        #region dlls
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("User32.Dll", EntryPoint = "PostMessageA")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("User32.dll")]
        public static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey); // Keys enumeration

        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        #endregion


        public CrashForm()
        {
            InitializeComponent();

            try
            {
                IEnumerable<string> TEPP = File.ReadLines(@Environment.GetEnvironmentVariable("userprofile") + @"\Documents\A.C.CRASH\settings.txt").Skip(0).Take(1);

                foreach (string str in TEPP)
                {
                    pth = str;
                    guna2Button1.Text = pth;
                }
            }
            catch { MessageBox.Show(@"ПУТЬ -> \Documents\A.C.CRASH\settings.txt не найден.", "ВАЙ БЛЯТЬ! ТЫ КУДА ПАПКУ СПРЯТАЛ?"); this.Close(); }
        }


        private void labelfr_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ТЕБЕ ДЕЛАТЬ НЕХУЙ? ", "НЕ НАЖИМАЙ СЮДА");
        }

        private void label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("КЛИКАЙ ПО ДЕЛУ.", "СЛЫЫШЬ?");
        }

        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            while (true)
            {
                if (cbToggle.Text != "<Key>")
                {

                    try
                    {
                        Keys key = (Keys)keyConverter.ConvertFromString(guna2Button1.Text);

                        if (GetAsyncKeyState(key) < 0)
                        {
                            try
                            {
                                Process[] Taskmgr = Process.GetProcessesByName(txt.Text);
                                if (Taskmgr.Length > 0)
                                {
                                    string cm = @"taskkill /IM " + txt.Text + ".exe /F";
                                    var startInfo = new ProcessStartInfo();
                                    startInfo.FileName = "cmd";
                                    startInfo.Arguments = "/c " + cm;
                                    startInfo.Verb = "runas"; // run elevated
                                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                    var proc = Process.Start(startInfo);

                                    if (guna2ToggleSwitch1.Checked)
                                    {
                                        this.Close();
                                    }
                                }
                                else
                                {

                                }
                            }
                            catch { }

                            break;

                            if (guna2ToggleSwitch1.Checked)
                            {
                                this.Close();
                            }
                        }
                    }
                    catch {

                        MessageBox.Show("КНОПКА -> " + guna2Button1.Text + "  ГОВНО, ВЫБЕРИ ДРУГУЮ! ", "ЭЙ БЛЯХА!");
                        break;
                    }
                    
                }
            }
        }
        private void RenameKey(string text, string replaceText)
        {
            if (guna2Button1.Text == text)
            {
                guna2Button1.Text = replaceText;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            guna2Button1.Text = "<Key>";
        }
        
        public static KeysConverter keyConverter = new KeysConverter();

        private void guna2Button1_KeyUp(object sender, KeyEventArgs e)
        {
            if (guna2Button1.Text == "<Key>")
            {
                if (GetAsyncKeyState(Keys.Escape) < 0)
                {
                    guna2Button1.Text = "ClickBind";
                }
                else
                {
                    guna2Button1.Text = e.KeyCode.ToString();
                }

                try
                {
                    RenameKey("D0", "0");
                    RenameKey("D1", "1");
                    RenameKey("D2", "2");
                    RenameKey("D3", "3");
                    RenameKey("D4", "4");
                    RenameKey("D5", "5");
                    RenameKey("D6", "6");
                    RenameKey("D7", "7");
                    RenameKey("D8", "8");
                    RenameKey("D9", "9");
                    RenameKey("ShiftKey", "Shift");
                    RenameKey("Menu", "Alt");
                    RenameKey("Back", "Delete");
                    RenameKey("Oem5", @"\");
                    RenameKey("Return", "Enter");
                    RenameKey("Capital", "CapsLock");
                    RenameKey("ControlKey", "Ctrl");
                    RenameKey("OemQuestion", "/");
                    RenameKey("Oemplus", "+");
                    RenameKey("OemMinus", "-");
                    RenameKey("OemOpenBrackets", "[");
                    RenameKey("Oem6", "]");
                    RenameKey("Oem1", "*no bind key *");
                    RenameKey("Oem7", "'");
                    RenameKey("Oemcomma", ",");
                    RenameKey("OemPeriod", ".");
                }
                catch { }

                string filePath = (@Environment.GetEnvironmentVariable("userprofile") + @"\Documents\A.C.CRASH\settings.txt"); // имя файла
                string st = guna2Button1.Text;

                using (FileStream fileStream = File.Open(filePath, FileMode.Create))
                {
                    using (StreamWriter output = new StreamWriter(fileStream))
                    {
                        output.Write(st);
                    }
                }
            }
        }


        private void guna2ToggleSwitch2_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch2.Checked)
            {
                TopMost = true;
            }
            else if (!guna2ToggleSwitch2.Checked)
            {
                TopMost = false;
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void guna2Button2_Click_2(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        //  НЕИСП. ХРЕНЬ

        private void label5_Click(object sender, EventArgs e){} private void CrashForm_Load(object sender, EventArgs e){}
        private void label3_Click(object sender, EventArgs e){} private void cbToggle_KeyUp(object sender, KeyEventArgs e){}
        private void BindTick_Tick(object sender, EventArgs e){} private void guna2ToggleSwitch1_CheckedChanged(object sender, EventArgs e){}
        private void label4_Click(object sender, EventArgs e) {}
    }
}
