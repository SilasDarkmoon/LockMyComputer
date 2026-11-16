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
            // Skip the synthetic key while the user is actively providing real input
            // (keyboard, mouse move/click, touch, pen). It is only needed once the
            // session has been idle for the full timer interval.
            LASTINPUTINFO lii = default;
            lii.cbSize = (uint)Marshal.SizeOf<LASTINPUTINFO>();
            if (GetLastInputInfo(ref lii))
            {
                uint idleMilliseconds = GetTickCount() - lii.dwTime;
                if (idleMilliseconds < (uint)timer_nolock.Interval)
                {
                    return;
                }
            }

            keybd_event(VK_F13, 0, 0, UIntPtr.Zero);
            keybd_event(VK_F13, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool LockWorkStation();

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        [DllImport("kernel32.dll")]
        private static extern uint GetTickCount();

        [StructLayout(LayoutKind.Sequential)]
        private struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }

        [DllImport("wtsapi32.dll")]
        private static extern bool WTSRegisterSessionNotification(IntPtr hWnd, int dwFlags);

        [DllImport("wtsapi32.dll")]
        private static extern bool WTSUnRegisterSessionNotification(IntPtr hWnd);

        private const byte VK_F13 = 0x7C;
        private const uint KEYEVENTF_KEYUP = 0x0002;

        private const int NOTIFY_FOR_THIS_SESSION = 0;
        private const int WM_WTSSESSION_CHANGE = 0x02B1;
        private const int WTS_SESSION_LOCK = 0x7;
        private const int WTS_SESSION_UNLOCK = 0x8;

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            WTSRegisterSessionNotification(Handle, NOTIFY_FOR_THIS_SESSION);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            WTSUnRegisterSessionNotification(Handle);
            base.OnHandleDestroyed(e);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_WTSSESSION_CHANGE)
            {
                switch ((int)m.WParam)
                {
                    case WTS_SESSION_LOCK:
                        timer_nolock.Enabled = false;
                        break;
                    case WTS_SESSION_UNLOCK:
                        timer_nolock.Enabled = true;
                        break;
                }
            }
            base.WndProc(ref m);
        }
    }
}
