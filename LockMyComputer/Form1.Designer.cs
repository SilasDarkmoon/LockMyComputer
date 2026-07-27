namespace LockMyComputer
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
            components = new System.ComponentModel.Container();
            timer_lock = new System.Windows.Forms.Timer(components);
            timer_nolock = new System.Windows.Forms.Timer(components);
            label1 = new Label();
            SuspendLayout();
            // 
            // timer_lock
            // 
            timer_lock.Tick += timer_lock_Tick;
            // 
            // timer_nolock
            // 
            timer_nolock.Enabled = true;
            timer_nolock.Interval = 10000;
            timer_nolock.Tick += timer_nolock_Tick;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(354, 58);
            label1.TabIndex = 0;
            label1.Text = "点击我以锁定电脑";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(22F, 46F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(356, 67);
            ControlBox = false;
            Controls.Add(label1);
            Name = "Form1";
            Opacity = 0D;
            WindowState = FormWindowState.Minimized;
            Activated += Form1_Activated;
            Deactivate += Form1_Deactivate;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer timer_lock;
        private System.Windows.Forms.Timer timer_nolock;
        private Label label1;
    }
}
