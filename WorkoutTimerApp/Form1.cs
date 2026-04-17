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
    public partial class MainForm : Form
    {
        private WorkoutTimerManager _timerManager;
        private NotifyIcon _notifyIcon;
        private bool _useMessageBox = false;

        // P/Invoke for dragging
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        public MainForm()
        {
            InitializeComponent();
            _timerManager = new WorkoutTimerManager();
            _timerManager.Tick += (s, e) => UpdateUI();
            _timerManager.TimerFinished += OnTimerFinished;
            _timerManager.ShortcutTriggered += OnShortcutTriggered;
            _timerManager.ResetTriggered += (s, e) => btnReset_Click(null, null);

            InitializePresets();
            InitializeNotificationIcon();

            // Default preset
            TimerPreset defaultPreset = GetPresetBySeconds(30);
            if (defaultPreset != null) cbPresets.SelectedItem = defaultPreset;

            UpdateToggleButtonText();
            this.Resize += MainForm_Resize;
            this.Icon = new Icon("profile.ico");

            // Setup dragging for the header
            pnlHeader.MouseDown += DragForm;
            lblPresetName.MouseDown += DragForm;
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
            cbPresets.Items.Clear();
            foreach (var preset in TimerPreset.GetDefaultPresets())
            {
                cbPresets.Items.Add(preset);
            }
        }

        private void InitializeNotificationIcon()
        {
            try
            {
                _notifyIcon = new NotifyIcon
                {
                    Icon = new Icon("profile.ico"),
                    Visible = false,
                    Text = "Workout Timer"
                };
                _notifyIcon.Click += NotifyIcon_Click;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load tray icon: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnShortcutTriggered(object sender, int seconds)
        {
            TimerPreset preset = GetPresetBySeconds(seconds);
            if (preset != null)
            {
                cbPresets.SelectedItem = preset;
                _timerManager.Start(seconds);
                NotificationHelper.ShowToast($"Timer: {preset.Name}", 1000);
                UpdateUI();
            }
        }

        private void OnTimerFinished(object sender, EventArgs e)
        {
            if (_useMessageBox)
                MessageBox.Show("Workout Complete!", "Timer Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                NotificationHelper.ShowToast("Workout Complete!", 1000);

            UpdateUI();
        }

        private void UpdateUI()
        {
            UpdateTimeLabel();
            UpdateProgressBar();
        }

        private void UpdateTimeLabel()
        {
            if (cbPresets.SelectedItem != null)
            {
                int total = _timerManager.CurrentSeconds;
                int hours = total / 3600;
                int minutes = (total % 3600) / 60;
                int seconds = total % 60;
                lblTime.Text = $"Timer: {hours:D2}:{minutes:D2}:{seconds:D2}";
            }
            else
            {
                lblTime.Text = "Timer: Not Set";
            }
        }

        private void UpdateProgressBar()
        {
            if (_timerManager.TotalSeconds > 0)
            {
                int progress = (int)(((double)_timerManager.CurrentSeconds / _timerManager.TotalSeconds) * progressBar.Maximum);
                progressBar.Value = Math.Max(0, Math.Min(progressBar.Maximum, progress));
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!_timerManager.IsRunning && cbPresets.SelectedItem is TimerPreset preset)
            {
                _timerManager.Start(preset.Seconds);
                UpdateUI();
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            _timerManager.Pause();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            _timerManager.Reset();
            UpdateUI();
            if (_timerManager.TotalSeconds == 0) lblTime.Text = "Timer: 00:00:00";
        }

        private void btnTopMost_Click(object sender, EventArgs e)
        {
            this.TopMost = !this.TopMost;
            btnTopMost.BackColor = this.TopMost ? Color.FromArgb(0, 150, 255) : Color.FromArgb(45, 45, 45);
            btnTopMost.Text = this.TopMost ? "Unpin Window" : "Pin Window";
            NotificationHelper.ShowToast(this.TopMost ? "Always on Top: ON" : "Always on Top: OFF", 1000);
            this.ActiveControl = null;
        }

        private void btnToggleNotification_Click(object sender, EventArgs e)
        {
            _useMessageBox = !_useMessageBox;
            UpdateToggleButtonText();
            NotificationHelper.ShowToast(_useMessageBox ? "Message Box mode enabled." : "Notification mode enabled.", 550, false);
        }

        private void UpdateToggleButtonText()
        {
            btnToggleNotification.Text = _useMessageBox ? "Mode: Message Box" : "Mode: Notification";
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

        private void btnSelectAudio_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "Audio Files|*.mp3;*.wav|All Files|*.*" };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _timerManager.InitializeSoundPlayer(openFileDialog.FileName);
            }
        }

        private void trackBarVolume_Scroll(object sender, EventArgs e)
        {
            _timerManager.Volume = (float)trackBarVolume.Value / trackBarVolume.Maximum;
            lblVolumeValue.Text = trackBarVolume.Value.ToString();
        }

        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            this.ShowInTaskbar = true;
            this.Show();
            if (this.WindowState == FormWindowState.Minimized) this.WindowState = FormWindowState.Normal;
            this.Activate();
            _notifyIcon.Visible = false;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                _notifyIcon.Visible = true;
                this.Hide();
                NotificationHelper.ShowToast("Workout Timer is running in the background.", 550, false);
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.ShowInTaskbar = false;
            _notifyIcon.Visible = true;
            NotificationHelper.ShowToast("Workout Timer is running in the background.", 550, false);
        }

        private TimerPreset GetPresetBySeconds(int seconds)
        {
            foreach (TimerPreset preset in cbPresets.Items)
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
                if (preset != null) cbPresets.SelectedItem = preset;
            }
            UpdateUI();
        }

        public TimerState GetCurrentTimerState()
        {
            int selectedSeconds = cbPresets.SelectedItem is TimerPreset p ? p.Seconds : 0;
            return _timerManager.GetState(selectedSeconds);
        }

        private void btnGoToForm2_Click(object sender, EventArgs e)
        {
            TimerState currentState = GetCurrentTimerState();
            _timerManager.Dispose();

            Form2 form2 = new Form2();
            form2.SetTimerState(currentState);
            form2.Show();
            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e) => Close();

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _timerManager?.Dispose();
            _notifyIcon?.Dispose();
            base.OnFormClosing(e);
        }
    }
}
