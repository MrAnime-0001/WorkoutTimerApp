using Gma.System.MouseKeyHook;
using NAudio.Wave;
using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkoutTimerApp
{
    public partial class Form2 : Form
    {
        private WorkoutTimerManager _timerManager;
        private NotifyIcon _notifyIcon;
        private bool _useMessageBox = false;

        // P/Invoke for dragging
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        public Form2()
        {
            InitializeComponent();
            _timerManager = new WorkoutTimerManager();
            _timerManager.Tick += (s, e) => UpdateUI();
            _timerManager.TimerFinished += OnTimerFinished;
            _timerManager.ShortcutTriggered += OnShortcutTriggered;
            _timerManager.ResetTriggered += (s, e) => btnReset_Click(null, null);

            InitializePresets();
            InitializeNotificationIcon();

            TimerPreset defaultPreset = GetPresetBySeconds(30);
            if (defaultPreset != null) cbPresets2.SelectedItem = defaultPreset;

            UpdateUI();
            this.Icon = new Icon("profile.ico");

            // Setup dragging for the header
            pnlHeader2.MouseDown += DragForm;
            lblTitle2.MouseDown += DragForm;
        }

        private void DragForm(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        private void InitializePresets()
        {
            cbPresets2.Items.Clear();
            foreach (var preset in TimerPreset.GetDefaultPresets())
            {
                cbPresets2.Items.Add(preset);
            }
        }

        private void InitializeNotificationIcon()
        {
            _notifyIcon = new NotifyIcon
            {
                Icon = SystemIcons.Information,
                Visible = false,
                Text = "Workout Timer Lite"
            };
            _notifyIcon.Click += NotifyIcon_Click;
        }

        private void OnShortcutTriggered(object sender, int seconds)
        {
            TimerPreset preset = GetPresetBySeconds(seconds);
            if (preset != null)
            {
                cbPresets2.SelectedItem = preset;
                _timerManager.Start(seconds);
                NotificationHelper.ShowToast($"Timer: {preset.Name}", 2000);
                UpdateUI();
            }
        }

        private void OnTimerFinished(object sender, EventArgs e)
        {
            if (_useMessageBox)
                MessageBox.Show("Workout Complete!", "Timer Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                NotificationHelper.ShowToast("Workout Complete!", 2000);

            UpdateUI();
        }

        private void UpdateUI()
        {
            int total = _timerManager.CurrentSeconds;
            int hours = total / 3600;
            int minutes = (total % 3600) / 60;
            int seconds = total % 60;
            lblTime2.Text = $"{hours:D2}:{minutes:D2}:{seconds:D2}";
            UpdateButtonStates();
        }

        private void UpdateButtonStates()
        {
            switch (_timerManager.Status)
            {
                case TimerStatus.Idle:
                    btnStart2.Enabled = true;
                    btnPause2.Enabled = false;
                    btnPause2.Text = "⏸";
                    btnReset2.Enabled = _timerManager.TotalSeconds > 0 && _timerManager.CurrentSeconds < _timerManager.TotalSeconds;
                    break;
                case TimerStatus.Running:
                    btnStart2.Enabled = false;
                    btnPause2.Enabled = true;
                    btnPause2.Text = "⏸";
                    btnReset2.Enabled = true;
                    break;
                case TimerStatus.Paused:
                    btnStart2.Enabled = false;
                    btnPause2.Enabled = true;
                    btnPause2.Text = "▶";
                    btnReset2.Enabled = true;
                    break;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (_timerManager.Status == TimerStatus.Idle && cbPresets2.SelectedItem is TimerPreset preset)
            {
                _timerManager.Start(preset.Seconds);
                UpdateUI();
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (_timerManager.Status == TimerStatus.Running)
                _timerManager.Pause();
            else if (_timerManager.Status == TimerStatus.Paused)
                _timerManager.Resume();
            UpdateUI();
        }

        private async void btnReset_Click(object sender, EventArgs e)
        {
            _timerManager.Reset();
            
            if (sender == null)
            {
                NotificationHelper.ShowToast("Timer Reset!", 1000);
            }
            lblTime2.ForeColor = Color.Red;
            lblTime2.Text = "RESET!";
            UpdateButtonStates();
            
            await Task.Delay(500);
            
            lblTime2.ForeColor = Color.FromArgb(0, 122, 204);
            UpdateUI();
        }

        private void btnTopMost_Click(object sender, EventArgs e)
        {
            this.TopMost = !this.TopMost;
            btnTopMost2.BackColor = this.TopMost ? Color.FromArgb(0, 150, 255) : Color.FromArgb(45, 45, 45);
            NotificationHelper.ShowToast(this.TopMost ? "Always on Top: ON" : "Always on Top: OFF", 2000);
            this.ActiveControl = null;
        }

        // Add dragging for borderless form
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x84) // WM_NCHITTEST
            {
                if ((int)m.Result == 0x1) // HTCLIENT
                {
                    m.Result = (IntPtr)0x2; // HTCAPTION
                }
            }
        }

        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized) WindowState = FormWindowState.Normal;
            this.Show();
            this.Activate();
            this.BringToFront();
        }

        private TimerPreset GetPresetBySeconds(int seconds)
        {
            foreach (TimerPreset preset in cbPresets2.Items)
            {
                if (preset.Seconds == seconds) return preset;
            }
            return null;
        }

        public void SetTimerState(TimerState state)
        {
            if (state == null) return;
            _timerManager.SetState(state);
            if (state.SelectedPresetSeconds > 0)
            {
                TimerPreset preset = GetPresetBySeconds(state.SelectedPresetSeconds);
                if (preset != null) cbPresets2.SelectedItem = preset;
            }
            UpdateUI();
        }

        public TimerState GetCurrentTimerState()
        {
            int selectedSeconds = cbPresets2.SelectedItem is TimerPreset p ? p.Seconds : 0;
            return _timerManager.GetState(selectedSeconds);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            TimerState currentState = GetCurrentTimerState();
            _timerManager.Dispose();

            this.Hide();
            MainForm form1 = new MainForm();
            form1.SetTimerState(currentState);
            form1.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            _timerManager?.Dispose();
            _notifyIcon?.Dispose();
            Application.Exit();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _timerManager?.Dispose();
            _notifyIcon?.Dispose();
            base.OnFormClosing(e);
        }
    }
}
