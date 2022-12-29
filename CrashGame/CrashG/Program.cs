using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrashG
{
    static class Program
    {

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Directory.CreateDirectory(@Environment.GetEnvironmentVariable("userprofile") + @"\Documents\A.C.CRASH");
            }
            catch { }
            
            try
            {
                if (!File.Exists(@Environment.GetEnvironmentVariable("userprofile") + @"\Documents\A.C.CRASH\settings.txt"))
                {
                    string filePath = (@Environment.GetEnvironmentVariable("userprofile") + @"\Documents\A.C.CRASH\settings.txt"); // имя файла
                    string st = "1";

                    using (FileStream fileStream = File.Open(filePath, FileMode.Create))
                    {
                        using (StreamWriter output = new StreamWriter(fileStream))
                        {
                            output.Write(st);
                        }
                    }
                }

            }
            catch { }
            

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.ApplicationExit += (o, e) => SayBye();
                AppDomain.CurrentDomain.UnhandledException += (o, e) => { if (e.IsTerminating) SayBye(); };
                Application.ThreadException += (o, e) => { MessageBox.Show(e.Exception.ToString()); SayBye(); Application.Exit(); };
                Application.Run(new CrashForm());
            }
            catch { }
            
        }



        public static KeysConverter keyConverter = new KeysConverter();
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
        private static void SayBye()
        {}
    }
}
