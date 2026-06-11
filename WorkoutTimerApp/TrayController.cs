using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WorkoutTimerApp
{
    public class TrayController : IDisposable
    {
        private NotifyIcon _notifyIcon = null!;
        private ContextMenuStrip _contextMenu = null!;
        private WorkoutTimerService _timerService;
        private Form? _activeForm;

        private ToolStripMenuItem _statusItem = null!;
        private ToolStripMenuItem _pauseResumeItem = null!;
        private ToolStripMenuItem _resetItem = null!;
        private ToolStripMenuItem _showHideItem = null!;
        private ToolStripMenuItem[] _presetItems = null!;

        public TrayController(WorkoutTimerService timerService)
        {
            _timerService = timerService;
            _timerService.Tick += (s, e) => UpdateStatusDisplay();
            _timerService.TimerFinished += (s, e) => OnTimerFinished();
            _timerService.StatusChanged += (s, e) => UpdateMenuState();

            BuildContextMenu();
            InitializeNotifyIcon();
        }

        private void InitializeNotifyIcon()
        {
            _notifyIcon = new NotifyIcon
            {
                Icon = new Icon(typeof(Program).Assembly.GetManifestResourceStream("WorkoutTimerApp.profile.ico")!),
                Visible = false,
                Text = "Workout Timer"
            };
            _notifyIcon.MouseUp += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                    ShowActiveForm();
                else if (e.Button == MouseButtons.Right)
                    _contextMenu.Show(Cursor.Position);
            };
        }

        private void BuildContextMenu()
        {
            _contextMenu = new ContextMenuStrip();

            _statusItem = new ToolStripMenuItem("Timer: Idle") { Enabled = false };
            _contextMenu.Items.Add(_statusItem);
            _contextMenu.Items.Add(new ToolStripSeparator());

            _presetItems = new ToolStripMenuItem[]
            {
                CreatePresetItem("1 Minute", 60),
                CreatePresetItem("1 Minute 30 Seconds", 90),
                CreatePresetItem("2 Minutes", 120),
                CreatePresetItem("3 Minutes", 180),
            };
            _contextMenu.Items.AddRange(_presetItems);
            _contextMenu.Items.Add(new ToolStripSeparator());

            _pauseResumeItem = new ToolStripMenuItem("Pause Timer");
            _pauseResumeItem.Click += (s, e) => TogglePauseResume();
            _contextMenu.Items.Add(_pauseResumeItem);

            _resetItem = new ToolStripMenuItem("Reset Timer");
            _resetItem.Click += (s, e) =>
            {
                _timerService.Reset();
                NotificationHelper.ShowToast("Timer Reset!", 1000);
            };
            _contextMenu.Items.Add(_resetItem);
            _contextMenu.Items.Add(new ToolStripSeparator());

            _showHideItem = new ToolStripMenuItem("Show Window");
            _showHideItem.Click += (s, e) => ToggleShowHide();

            var exitItem = new ToolStripMenuItem("Exit");
            exitItem.Click += (s, e) => ExitApplication();
            _contextMenu.Items.Add(_showHideItem);
            _contextMenu.Items.Add(exitItem);

            UpdateMenuState();
        }

        private ToolStripMenuItem CreatePresetItem(string text, int seconds)
        {
            var item = new ToolStripMenuItem(text);
            item.Click += (s, e) =>
            {
                _timerService.Start(seconds);
                NotificationHelper.ShowToast($"Timer: {text}", 1000);
            };
            return item;
        }

        private void UpdateStatusDisplay()
        {
            int total = _timerService.CurrentSeconds;
            string timeStr = total >= 3600
                ? $"{(total / 3600):D2}:{(total % 3600) / 60:D2}:{total % 60:D2}"
                : $"{(total / 60):D2}:{total % 60:D2}";
            _statusItem.Text = $"⏱ {timeStr} remaining";
            _notifyIcon.Text = $"Workout Timer - {timeStr}";
        }

        private void UpdateMenuState()
        {
            bool isIdle = _timerService.Status == TimerStatus.Idle;
            bool isRunning = _timerService.Status == TimerStatus.Running;
            bool isPaused = _timerService.Status == TimerStatus.Paused;

            foreach (var item in _presetItems)
                item.Enabled = isIdle;

            _pauseResumeItem.Text = isRunning ? "Pause Timer"
                : isPaused ? "Resume Timer"
                : "Pause Timer";
            _pauseResumeItem.Enabled = !isIdle;

            _resetItem.Enabled = !isIdle || _timerService.TotalSeconds > 0;

            if (isIdle && _timerService.CurrentSeconds <= 0)
                _statusItem.Text = "Timer: Idle";
        }

        private void TogglePauseResume()
        {
            if (_timerService.Status == TimerStatus.Running)
                _timerService.Pause();
            else if (_timerService.Status == TimerStatus.Paused)
                _timerService.Resume();
        }

        private void ToggleShowHide()
        {
            if (_activeForm != null && _activeForm.Visible)
            {
                _activeForm.Hide();
                _showHideItem.Text = "Show Window";
            }
            else
            {
                ShowActiveForm();
            }
        }

        private void ShowActiveForm()
        {
            if (_activeForm == null || _activeForm.IsDisposed)
            {
                _activeForm = Application.OpenForms.OfType<Form>().FirstOrDefault(f => !f.IsDisposed);
            }

            if (_activeForm != null)
            {
                _activeForm.Show();
                _activeForm.Activate();
                if (_activeForm.WindowState == FormWindowState.Minimized)
                    _activeForm.WindowState = FormWindowState.Normal;
                _showHideItem.Text = "Hide Window";
            }
        }

        private void OnTimerFinished()
        {
            if (_activeForm == null || !_activeForm.Visible)
            {
                _notifyIcon.ShowBalloonTip(3000, "Workout Complete",
                    "Your workout timer has finished!", ToolTipIcon.None);
            }
        }

        public void SetActiveForm(Form form)
        {
            _activeForm = form;
        }

        public void SetTrayVisible(bool visible)
        {
            _notifyIcon.Visible = visible;
        }

        public void ExitApplication()
        {
            _timerService.Dispose();
            _notifyIcon.Visible = false;
            _notifyIcon.Dispose();
            Application.Exit();
        }

        public void Dispose()
        {
            _notifyIcon?.Dispose();
            _contextMenu?.Dispose();
        }
    }
}
