using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkoutTimerApp
{
    public partial class MainForm : Form
    {
        private WorkoutTimerService _timerService;
        private TrayController _trayController;
        private ToolTip _toolTip;
        private bool _useMessageBox = false;
        private AppSettings _settings = null!;

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        public MainForm(WorkoutTimerService timerService, TrayController trayController)
        {
            InitializeComponent();

            _timerService = timerService;
            _trayController = trayController;

            _timerService.Tick += OnServiceTick;
            _timerService.TimerFinished += OnTimerFinished;
            _timerService.ShortcutTriggered += OnShortcutTriggered;
            _timerService.ResetTriggered += OnResetTriggered;

            InitializePresets();

            // Load settings
            _settings = AppSettings.Load();
            ApplySettings();

            TimerPreset? defaultPreset = GetPresetBySeconds(60);
            if (defaultPreset != null) cbPresets.SelectedItem = defaultPreset;

            UpdateToggleButtonText();
            using var iconStream = typeof(Program).Assembly.GetManifestResourceStream("WorkoutTimerApp.profile.ico")!;
            this.Icon = new Icon(iconStream);

            pnlHeader.MouseDown += DragForm;
            lblPresetName.MouseDown += DragForm;

            _toolTip = new ToolTip { AutoPopDelay = 8000, InitialDelay = 400 };
            _toolTip.SetToolTip(btnStart, "Start timer\nHotkeys: Alt+Num2=1m | Alt+Num3=90s | Alt+Num4=2m | Alt+Num5=3m");
            _toolTip.SetToolTip(btnPause, "Pause or Resume the running timer (toggle)");
            _toolTip.SetToolTip(btnReset, "Reset timer to full duration\nHotkey: Alt+Num0");
            _toolTip.SetToolTip(cbPresets, "Pick a preset or type custom time (MM:SS or seconds)");

        }

        private void DragForm(object? sender, MouseEventArgs e)
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
                cbPresets.Items.Add(preset);
        }

        private void OnShortcutTriggered(object? sender, int seconds)
        {
            TimerPreset? preset = GetPresetBySeconds(seconds);
            if (preset != null)
            {
                cbPresets.SelectedItem = preset;
                _timerService.Start(seconds);
                NotificationHelper.ShowToast($"Timer: {preset.Name}", 1000);
                UpdateUI();
            }
        }

        private void OnTimerFinished(object? sender, EventArgs e)
        {
            if (_useMessageBox)
                MessageBox.Show("Workout Complete!", "Timer Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                NotificationHelper.ShowToast("Workout Complete!", 1000, false);
            UpdateUI();
        }

        private void UpdateUI()
        {
            UpdateTimeLabel();
            UpdateProgressBar();
            UpdateButtonStates();
        }

        private void UpdateButtonStates()
        {
            switch (_timerService.Status)
            {
                case TimerStatus.Idle:
                    btnStart.Enabled = true;
                    btnPause.Enabled = false;
                    btnPause.Text = "Pause";
                    btnReset.Enabled = _timerService.TotalSeconds > 0 && _timerService.CurrentSeconds < _timerService.TotalSeconds;
                    break;
                case TimerStatus.Running:
                    btnStart.Enabled = false;
                    btnPause.Enabled = true;
                    btnPause.Text = "Pause";
                    btnReset.Enabled = true;
                    break;
                case TimerStatus.Paused:
                    btnStart.Enabled = false;
                    btnPause.Enabled = true;
                    btnPause.Text = "Resume";
                    btnReset.Enabled = true;
                    break;
            }
        }

        private void UpdateTimeLabel()
        {
            int total = _timerService.CurrentSeconds;
            int hours = total / 3600;
            int minutes = (total % 3600) / 60;
            int seconds = total % 60;
            lblTime.Text = (hours > 0 || _timerService.TotalSeconds > 0)
                ? $"Timer: {hours:D2}:{minutes:D2}:{seconds:D2}"
                : "Timer: 00:00:00";
        }

        private void UpdateProgressBar()
        {
            if (_timerService.TotalSeconds > 0)
            {
                int progress = (int)(((double)_timerService.CurrentSeconds / _timerService.TotalSeconds) * progressBar.Maximum);
                progressBar.Value = Math.Max(0, Math.Min(progressBar.Maximum, progress));
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (_timerService.Status != TimerStatus.Idle) return;

            int? seconds = GetCurrentDuration();
            if (seconds.HasValue)
            {
                _timerService.Start(seconds.Value);
                UpdateUI();
            }
        }

        private int? GetCurrentDuration()
        {
            if (cbPresets.SelectedItem is TimerPreset preset)
                return preset.Seconds;

            return ParseCustomTime(cbPresets.Text);
        }

        private static int? ParseCustomTime(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;
            input = input.Trim();

            // MM:SS or H:MM:SS
            if (input.Contains(':'))
            {
                string[] parts = input.Split(':');
                if (parts.Length == 2
                    && int.TryParse(parts[0], out int minutes)
                    && int.TryParse(parts[1], out int secs)
                    && secs >= 0 && secs < 60 && minutes >= 0)
                    return minutes * 60 + secs;

                if (parts.Length == 3
                    && int.TryParse(parts[0], out int hours)
                    && int.TryParse(parts[1], out minutes)
                    && int.TryParse(parts[2], out secs)
                    && secs >= 0 && secs < 60 && minutes >= 0 && minutes < 60 && hours >= 0)
                    return hours * 3600 + minutes * 60 + secs;

                return null;
            }

            // Bare seconds
            if (int.TryParse(input, out int total) && total > 0)
                return total;

            return null;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (_timerService.Status == TimerStatus.Running)
                _timerService.Pause();
            else if (_timerService.Status == TimerStatus.Paused)
                _timerService.Resume();
            UpdateUI();
        }

        private async void btnReset_Click(object? sender, EventArgs e)
        {
            _timerService.Reset();
            if (sender == null)
                NotificationHelper.ShowToast("Timer Reset!", 1000);
            lblTime.ForeColor = Color.Red;
            lblTime.Text = "RESET!";
            UpdateButtonStates();
            UpdateProgressBar();
            await Task.Delay(500);
            lblTime.ForeColor = Color.FromArgb(0, 122, 204);
            UpdateUI();
            if (_timerService.TotalSeconds == 0) lblTime.Text = "Timer: 00:00:00";
        }

        private void btnTopMost_Click(object sender, EventArgs e)
        {
            this.TopMost = !this.TopMost;
            btnTopMost.BackColor = this.TopMost ? Color.FromArgb(0, 150, 255) : Color.FromArgb(45, 45, 45);
            btnTopMost.Text = this.TopMost ? "Unpin Window" : "Pin Window";
            _settings.TopMost = this.TopMost;
            _settings.Save();
            NotificationHelper.ShowToast(this.TopMost ? "Always on Top: ON" : "Always on Top: OFF", 1000);
            this.ActiveControl = null;
        }

        private void btnToggleNotification_Click(object sender, EventArgs e)
        {
            _useMessageBox = !_useMessageBox;
            UpdateToggleButtonText();
            _settings.NotificationMode = _useMessageBox;
            _settings.Save();
            NotificationHelper.ShowToast(_useMessageBox ? "Message Box mode enabled." : "Notification mode enabled.", 550, false);
        }

        private void UpdateToggleButtonText()
        {
            btnToggleNotification.Text = _useMessageBox ? "Mode: Message Box" : "Mode: Notification";
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00040000; // WS_EX_APPWINDOW — force taskbar button
                return cp;
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x84)
            {
                if ((int)m.Result == 0x1)
                    m.Result = (IntPtr)0x2;
            }
        }

        private void ApplySettings()
        {
            // Sound selection — fall back to index 0 if saved name no longer valid
            if (!string.IsNullOrEmpty(_settings.CustomSoundPath) && File.Exists(_settings.CustomSoundPath))
            {
                _timerService.SwitchToCustomSound(_settings.CustomSoundPath);
                cbSoundSelect.SelectedItem = "Browse custom sound...";
            }
            else
            {
                _settings.CustomSoundPath = null;
                int idx = Array.IndexOf(WorkoutTimerService.BuiltInSoundNames, _settings.SelectedSound);
                if (idx < 0 || idx >= cbSoundSelect.Items.Count - 1)
                {
                    idx = 0;
                    _settings.SelectedSound = WorkoutTimerService.BuiltInSoundNames[0];
                }
                cbSoundSelect.SelectedIndex = idx;
                _timerService.SwitchToBuiltInSound(_settings.SelectedSound);
            }

            // Volume
            int vol = (int)(_settings.Volume * 100);
            trackBarVolume.Value = Math.Clamp(vol, trackBarVolume.Minimum, trackBarVolume.Maximum);
            lblVolumeValue.Text = trackBarVolume.Value.ToString();
            _timerService.Volume = _settings.Volume;

            // Notification mode
            _useMessageBox = _settings.NotificationMode;

            // TopMost
            TopMost = _settings.TopMost;
            btnTopMost.BackColor = TopMost ? Color.FromArgb(0, 150, 255) : Color.FromArgb(45, 45, 45);
            btnTopMost.Text = TopMost ? "Unpin Window" : "Pin Window";
        }

        private void cbSoundSelect_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cbSoundSelect.SelectedItem is not string selected) return;

            if (selected == "Browse custom sound...")
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Audio Files|*.mp3;*.wav|All Files|*.*",
                    Title = "Select Custom Timer Sound"
                };
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _timerService.SwitchToCustomSound(openFileDialog.FileName);
                    _settings.SelectedSound = "custom";
                    _settings.CustomSoundPath = openFileDialog.FileName;
                    _settings.Save();
                    cbSoundSelect.SelectedItem = "Browse custom sound...";
                }
                else
                {
                    // Revert to previously selected sound
                    int prevIdx = Array.IndexOf(WorkoutTimerService.BuiltInSoundNames, _settings.SelectedSound);
                    cbSoundSelect.SelectedIndex = prevIdx >= 0 ? prevIdx : 0;
                }
                return;
            }

            string soundName = selected.ToLower();
            _timerService.SwitchToBuiltInSound(soundName);
            _settings.SelectedSound = soundName;
            _settings.CustomSoundPath = null;
            _settings.Save();
        }

        private void trackBarVolume_Scroll(object sender, EventArgs e)
        {
            _timerService.Volume = (float)trackBarVolume.Value / trackBarVolume.Maximum;
            lblVolumeValue.Text = trackBarVolume.Value.ToString();
            _settings.Volume = _timerService.Volume;
            _settings.Save();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.Hide();
            _trayController.SetTrayVisible(true);
            NotificationHelper.ShowToast("Workout Timer is running in the background.", 550, false);
        }

        private TimerPreset? GetPresetBySeconds(int seconds)
        {
            foreach (TimerPreset preset in cbPresets.Items.OfType<TimerPreset>())
            {
                if (preset.Seconds == seconds) return preset;
            }
            return null;
        }

        private void btnGoToForm2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form2 = new Form2(_timerService, _trayController);
            _trayController.SetActiveForm(form2);
            form2.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            _trayController.ExitApplication();
        }

        private void OnServiceTick(object? sender, EventArgs e) => UpdateUI();

        private void OnResetTriggered(object? sender, EventArgs e) => btnReset_Click(null, EventArgs.Empty);

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _timerService.Tick -= OnServiceTick;
            _timerService.TimerFinished -= OnTimerFinished;
            _timerService.ShortcutTriggered -= OnShortcutTriggered;
            _timerService.ResetTriggered -= OnResetTriggered;
            base.OnFormClosing(e);
        }
    }
}
