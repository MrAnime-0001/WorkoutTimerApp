using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkoutTimerApp
{
    public partial class Form2 : Form
    {
        private WorkoutTimerService _timerService;
        private TrayController _trayController;
        private ToolTip _toolTip;
        private bool _useMessageBox = false;

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        public Form2(WorkoutTimerService timerService, TrayController trayController)
        {
            InitializeComponent();

            _timerService = timerService;
            _trayController = trayController;

            _timerService.Tick += OnServiceTick;
            _timerService.TimerFinished += OnTimerFinished;
            _timerService.ShortcutTriggered += OnShortcutTriggered;
            _timerService.ResetTriggered += OnResetTriggered;

            InitializePresets();

            TimerPreset? defaultPreset = GetPresetBySeconds(60);
            if (defaultPreset != null) cbPresets2.SelectedItem = defaultPreset;

            UpdateUI();
            this.Icon = new Icon("profile.ico");

            pnlHeader2.MouseDown += DragForm;
            lblTitle2.MouseDown += DragForm;

            _toolTip = new ToolTip { AutoPopDelay = 8000, InitialDelay = 400 };
            _toolTip.SetToolTip(btnStart2, "Start timer\nHotkeys: Alt+Num2=1m | Alt+Num3=90s | Alt+Num4=2m | Alt+Num5=3m");
            _toolTip.SetToolTip(btnPause2, "Pause or Resume the running timer (toggle)");
            _toolTip.SetToolTip(btnReset2, "Reset timer to full duration\nHotkey: Alt+Num0");
            _toolTip.SetToolTip(cbPresets2, "Pick a preset or type custom time (MM:SS or seconds)");

            cbPresets2.DropDownStyle = ComboBoxStyle.DropDown;
            cbPresets2.KeyPress += cbPresets_KeyPress;
            cbPresets2.Leave += cbPresets_Leave;
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
            cbPresets2.Items.Clear();
            foreach (var preset in TimerPreset.GetDefaultPresets())
                cbPresets2.Items.Add(preset);
        }

        private void OnShortcutTriggered(object? sender, int seconds)
        {
            TimerPreset? preset = GetPresetBySeconds(seconds);
            if (preset != null)
            {
                cbPresets2.SelectedItem = preset;
                _timerService.Start(seconds);
                NotificationHelper.ShowToast($"Timer: {preset.Name}", 2000);
                UpdateUI();
            }
        }

        private void OnTimerFinished(object? sender, EventArgs e)
        {
            if (_useMessageBox)
                MessageBox.Show("Workout Complete!", "Timer Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                NotificationHelper.ShowToast("Workout Complete!", 2000);
            UpdateUI();
        }

        private void UpdateUI()
        {
            int total = _timerService.CurrentSeconds;
            int hours = total / 3600;
            int minutes = (total % 3600) / 60;
            int seconds = total % 60;
            lblTime2.Text = $"{hours:D2}:{minutes:D2}:{seconds:D2}";
            UpdateButtonStates();
        }

        private void UpdateButtonStates()
        {
            switch (_timerService.Status)
            {
                case TimerStatus.Idle:
                    btnStart2.Enabled = true;
                    btnPause2.Enabled = false;
                    btnPause2.Text = "⏸";
                    btnReset2.Enabled = _timerService.TotalSeconds > 0 && _timerService.CurrentSeconds < _timerService.TotalSeconds;
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
            if (cbPresets2.SelectedItem is TimerPreset preset)
                return preset.Seconds;

            return ParseCustomTime(cbPresets2.Text);
        }

        private static int? ParseCustomTime(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;
            input = input.Trim();

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

            if (int.TryParse(input, out int total) && total > 0)
                return total;

            return null;
        }

        private void cbPresets_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnStart_Click(sender, e);
            }
        }

        private void cbPresets_Leave(object? sender, EventArgs e)
        {
            if (cbPresets2.SelectedItem is not TimerPreset && !string.IsNullOrWhiteSpace(cbPresets2.Text))
            {
                int? seconds = ParseCustomTime(cbPresets2.Text);
                if (seconds == null && cbPresets2.Items.Count > 0)
                    cbPresets2.SelectedIndex = 0;
            }
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

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x84)
            {
                if ((int)m.Result == 0x1)
                    m.Result = (IntPtr)0x2;
            }
        }

        private TimerPreset? GetPresetBySeconds(int seconds)
        {
            foreach (TimerPreset preset in cbPresets2.Items.OfType<TimerPreset>())
            {
                if (preset.Seconds == seconds) return preset;
            }
            return null;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
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

            // Restore hidden MainForm when Lite view closes
            var mainForm = Application.OpenForms.OfType<MainForm>().FirstOrDefault();
            if (mainForm != null && !mainForm.Visible)
            {
                _trayController.SetActiveForm(mainForm);
                mainForm.Show();
            }

            base.OnFormClosing(e);
        }
    }
}
