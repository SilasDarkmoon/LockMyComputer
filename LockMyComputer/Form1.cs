using System.Runtime.InteropServices;

namespace LockMyComputer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string exePath = @"C:\Windows\System32\shell32.dll"; // 替换为你的目标系统exe路径
            this.Icon = System.Drawing.Icon.ExtractIcon(exePath, 47);
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            timer_lock.Enabled = true;
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            timer_lock.Enabled = false;
        }

        private void timer_lock_Tick(object sender, EventArgs e)
        {
            LockWorkStation();
            this.WindowState = FormWindowState.Minimized;
            timer_lock.Enabled = false;
        }

        private void timer_nolock_Tick(object sender, EventArgs e)
        {
            keybd_event(VK_F13, 0, 0, UIntPtr.Zero);
            keybd_event(VK_F13, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool LockWorkStation();

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        private const byte VK_F13 = 0x7C;
        private const uint KEYEVENTF_KEYUP = 0x0002;
    }
}
