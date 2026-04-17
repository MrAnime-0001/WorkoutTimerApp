using Gma.System.MouseKeyHook;
using NAudio.Wave;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkoutTimerApp
{
    public partial class Form2 : Form
    {
        private WorkoutTimerManager _timerManager;
        private NotifyIcon _notifyIcon;
        private bool _useMessageBox = false;

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
                ShowCustomToast($"Timer: {preset.Name}");
                UpdateUI();
            }
        }

        private void OnTimerFinished(object sender, EventArgs e)
        {
            if (_useMessageBox)
                MessageBox.Show("Workout Complete!", "Timer Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                ShowCustomToast("Workout Complete!");

            UpdateUI();
        }

        private void UpdateUI()
        {
            int total = _timerManager.CurrentSeconds;
            int hours = total / 3600;
            int minutes = (total % 3600) / 60;
            int seconds = total % 60;
            lblTime2.Text = $"{hours:D2}:{minutes:D2}:{seconds:D2}";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!_timerManager.IsRunning && cbPresets2.SelectedItem is TimerPreset preset)
            {
                _timerManager.Start(preset.Seconds);
                UpdateUI();
            }
            else if (!_timerManager.IsRunning && _timerManager.CurrentSeconds > 0)
            {
                _timerManager.Resume();
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
        }

        private void btnTopMost_Click(object sender, EventArgs e)
        {
            this.TopMost = !this.TopMost;
            btnTopMost2.BackColor = this.TopMost ? Color.FromArgb(156, 39, 176) : Color.FromArgb(103, 58, 183);
            ShowCustomToast(this.TopMost ? "Always on Top: ON" : "Always on Top: OFF");
            this.ActiveControl = null;
        }

        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized) WindowState = FormWindowState.Normal;
            this.Show();
            this.Activate();
            this.BringToFront();
        }

        private void ShowCustomToast(string message)
        {
            Form toast = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.Manual,
                ShowInTaskbar = false,
                TopMost = true,
                BackColor = Color.FromArgb(45, 45, 48),
                Size = new Size(250, 60)
            };

            Label lbl = new Label
            {
                Text = message,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White
            };
            toast.Controls.Add(lbl);

            toast.Location = new Point(
                Screen.PrimaryScreen.WorkingArea.Right - toast.Width - 10,
                Screen.PrimaryScreen.WorkingArea.Bottom - toast.Height - 10);

            toast.Shown += async (s, e) =>
            {
                await Task.Delay(2000);
                toast.Close();
            };
            toast.Show();
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
