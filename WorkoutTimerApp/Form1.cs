using System;
using System.Windows.Forms;
using NAudio.Wave;
using System.IO;
using Gma.System.MouseKeyHook;

namespace WorkoutTimerApp
{
    public partial class MainForm : Form
    {
        private int currentSeconds;
        private int totalSeconds;
        private bool isTimerRunning;
        private WaveOutEvent waveOut;
        private AudioFileReader mp3FileReader;
        private System.Windows.Forms.Timer timer1;
        private NotifyIcon notifyIcon;
        private IKeyboardMouseEvents globalHook;

        private bool useMessageBox = false; // Ensure notifications are the default


        public MainForm()
        {
            InitializeComponent();
            InitializePresets();
            InitializeNotificationIcon();

            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 1000; // Interval in milliseconds (1 second)
            timer1.Tick += Timer1_Tick;

            waveOut = new WaveOutEvent();
            InitializeSoundPlayer();

            // Find and set the TimerPreset with 30 seconds as the default selection
            TimerPreset defaultPreset = GetPresetBySeconds(30);
            if (defaultPreset != null)
            {
                cbPresets.SelectedItem = defaultPreset;
            }

            // Ensure the toggle button reflects the default notification mode
            UpdateToggleButtonText();

            globalHook = Hook.GlobalEvents();
            globalHook.KeyDown += GlobalHook_KeyDown;

            this.Load += MainForm_Load;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            globalHook = Hook.GlobalEvents();
            globalHook.KeyDown += GlobalHook_KeyDown;
        }

        private void StartTimerWithDuration(int seconds)
        {
            TimerPreset preset = GetPresetBySeconds(seconds);
            if (preset != null)
            {
                cbPresets.SelectedItem = preset;
                totalSeconds = preset.Seconds;
                currentSeconds = totalSeconds;
                UpdateProgressBar();
                isTimerRunning = true;
                timer1.Start();
                UpdateTimeLabel();
            }
        }

        private TimerPreset GetPresetBySeconds(int seconds)
        {
            foreach (TimerPreset preset in cbPresets.Items)
            {
                if (preset.Seconds == seconds)
                {
                    return preset;
                }
            }
            return null;
        }

        private void InitializePresets()
        {
            // Add your preset workout and rest periods
            cbPresets.Items.Add(new TimerPreset("5 Seconds", 5));
            cbPresets.Items.Add(new TimerPreset("10 Seconds", 10));
            cbPresets.Items.Add(new TimerPreset("20 Seconds", 20));
            cbPresets.Items.Add(new TimerPreset("30 Seconds", 30));
            cbPresets.Items.Add(new TimerPreset("1 Minute", 60));
            cbPresets.Items.Add(new TimerPreset("1 Minute, 30 Seconds", 90));
            cbPresets.Items.Add(new TimerPreset("2 Minutes", 120));
            cbPresets.Items.Add(new TimerPreset("3 Minutes", 180));
            cbPresets.Items.Add(new TimerPreset("5 Minutes", 300));
            cbPresets.Items.Add(new TimerPreset("10 Minutes", 600));
            cbPresets.Items.Add(new TimerPreset("20 Minutes", 1200));
            cbPresets.Items.Add(new TimerPreset("30 Minutes", 1800));
            cbPresets.Items.Add(new TimerPreset("1 Hour", 3600));
            cbPresets.Items.Add(new TimerPreset("2 Hours", 7200));
            cbPresets.Items.Add(new TimerPreset("3 Hours", 10800));
            cbPresets.Items.Add(new TimerPreset("5 Hours", 18000));
            cbPresets.Items.Add(new TimerPreset("10 Hours", 36000));
            cbPresets.Items.Add(new TimerPreset("12 Hours", 43200));
            cbPresets.Items.Add(new TimerPreset("24 Hours", 86400));
        }

        private void InitializeSoundPlayer(string filePath = null)
        {
            try
            {
                if (filePath == null)
                {
                    filePath = Path.Combine(Application.StartupPath, "timer_end.mp3");
                }

                mp3FileReader = new AudioFileReader(filePath);
                waveOut.Init(mp3FileReader);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing sound player: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Add a ProgressBar for visual countdown
        private void UpdateProgressBar()
        {
            if (totalSeconds > 0)
            {
                int progress = (int)(((double)currentSeconds / totalSeconds) * progressBar.Maximum);
                progressBar.Value = progress;
            }
        }

        private void InitializeNotificationIcon()
        {
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = SystemIcons.Information;
            notifyIcon.Visible = false;
            notifyIcon.Click += NotifyIcon_Click;
        }

        private void ShowSilentToast(string message)
        {
            Form toast = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.Manual,
                ShowInTaskbar = false,
                TopMost = true,
                BackColor = Color.LightYellow,
                Size = new Size(200, 50),
            };

            // Position it at the bottom right of the screen
            var workingArea = Screen.PrimaryScreen.WorkingArea;
            toast.Location = new Point(workingArea.Right - toast.Width - 10, workingArea.Bottom - toast.Height - 10);

            Label lbl = new Label
            {
                Text = message,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10),
            };
            toast.Controls.Add(lbl);

            toast.Shown += async (s, e) =>
            {
                await Task.Delay(550); // 0.55 second
                toast.Close();
            };

            toast.Show();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (this.Focused || this.ContainsFocus)
            {
                if (!isTimerRunning)
                {
                    switch (keyData)
                    {
                        case Keys.Control | Keys.D4:
                        case Keys.Control | Keys.NumPad4:
                            StartTimerWithDuration(30);
                            return true;
                        case Keys.Control | Keys.D7:
                        case Keys.Control | Keys.NumPad7:
                            StartTimerWithDuration(60);
                            return true;
                        case Keys.Control | Keys.D8:
                        case Keys.Control | Keys.NumPad8:
                            StartTimerWithDuration(90);
                            return true;
                        case Keys.Control | Keys.D9:
                        case Keys.Control | Keys.NumPad9:
                            StartTimerWithDuration(120);
                            return true;
                    }
                }

                // Allow reset regardless of whether the timer is running
                switch (keyData)
                {
                    case Keys.Control | Keys.D6:
                    case Keys.Control | Keys.NumPad6:
                        btnReset.PerformClick(); // Simulate reset button click
                        return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void GlobalHook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                if (!isTimerRunning)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.NumPad4:
                            StartTimerWithDuration(30);
                            ShowSilentToast("Timer: 30 sec");
                            break;
                        case Keys.NumPad7:
                            StartTimerWithDuration(60);
                            ShowSilentToast("Timer: 1 min");
                            break;
                        case Keys.NumPad8:
                            StartTimerWithDuration(90);
                            ShowSilentToast("Timer: 1:30");
                            break;
                        case Keys.NumPad9:
                            StartTimerWithDuration(120);
                            ShowSilentToast("Timer: 2 min");
                            break;
                    }
                }

                if (e.KeyCode == Keys.NumPad6)
                {
                    btnReset.PerformClick();
                    ShowSilentToast("Timer Reset");
                }
            }
        }

        private void ShowCustomToast(string message, bool playSound = false)
        {
            Form toast = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.Manual,
                ShowInTaskbar = false,
                TopMost = true,
                BackColor = Color.LightYellow,
                Size = new Size(200, 50),
            };

            // Position at bottom right of screen
            var workingArea = Screen.PrimaryScreen.WorkingArea;
            toast.Location = new Point(workingArea.Right - toast.Width - 10, workingArea.Bottom - toast.Height - 10);

            Label lbl = new Label
            {
                Text = message,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10),
            };
            toast.Controls.Add(lbl);

            toast.Shown += async (s, e) =>
            {
                if (playSound)
                {
                    try
                    {
                        // Play your audio here (e.g., your mp3 or wav sound)
                        mp3FileReader.Position = 0;
                        waveOut.Play();
                    }
                    catch { /* optionally handle errors */ }
                }

                await Task.Delay(1000); // Show for 1 second
                toast.Close();
            };

            toast.Show();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!isTimerRunning)
            {
                UpdateProgressBar();
                isTimerRunning = true;
                totalSeconds = ((TimerPreset)cbPresets.SelectedItem).Seconds;
                currentSeconds = totalSeconds;
                timer1.Start();
                UpdateTimeLabel();
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            isTimerRunning = false;
            timer1.Stop();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            isTimerRunning = false;
            timer1.Stop();
            currentSeconds = totalSeconds;
            UpdateTimeLabel();
            lblTime.Text = "Timer: 00:00:00";
            UpdateProgressBar();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (currentSeconds > 0)
            {
                currentSeconds--;
                UpdateTimeLabel();
                UpdateProgressBar();
            }
            else
            {
                isTimerRunning = false;
                timer1.Stop();

                mp3FileReader.Position = 0;
                waveOut.Play();

                if (useMessageBox)
                {
                    MessageBox.Show("Workout Complete!", "Timer Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ShowCustomToast("Workout Complete!", playSound: true);
                }

                ResetTimer();
            }
        }

        private void ResetTimer()
        {
            isTimerRunning = false;
            currentSeconds = totalSeconds;
            UpdateTimeLabel();
        }

        private void UpdateTimeLabel()
        {
            if (cbPresets.SelectedItem != null)
            {
                int hours = currentSeconds / 3600;
                int minutes = (currentSeconds % 3600) / 60;
                int seconds = currentSeconds % 60;

                lblTime.Text = $"Timer: {hours:D2}:{minutes:D2}:{seconds:D2}";
            }
            else
            {
                lblTime.Text = "Timer: Not Set";
            }
        }

        private void btnTopMost_Click(object sender, EventArgs e)
        {
            bool isTopMost = this.TopMost;

            // Toggle the TopMost property
            this.TopMost = !isTopMost;

            // Show a message box notifying the user of the change
            string message = isTopMost ? "TopMost is now turned off." : "TopMost is now turned on.";
            MessageBox.Show(message, "TopMost Toggled");

            // Deselect the button
            this.ActiveControl = null;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Add a TrackBar for volume control
        private void trackBarVolume_Scroll(object sender, EventArgs e)
        {
            waveOut.Volume = (float)trackBarVolume.Value / trackBarVolume.Maximum;

            lblVolumeValue.Text = trackBarVolume.Value.ToString();
        }

        // Add a button for custom audio selection
        private void btnSelectAudio_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Audio Files|*.mp3;*.wav|All Files|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;
                InitializeSoundPlayer(selectedFilePath);
            }
        }

        private void UpdateToggleButtonText()
        {
            btnToggleNotification.Text = useMessageBox ? "Switch to Notification" : "Switch to Message Box";
        }

        private void ShowNotification(string title, string text)
        {
            notifyIcon.BalloonTipTitle = title;
            notifyIcon.BalloonTipText = text;
            notifyIcon.Visible = true;
            notifyIcon.ShowBalloonTip(5000); // Display for 5 seconds
        }

        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            // Handle the click on the notification icon
            // Show the form and bring it to the front

            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }

            // Bring the form to the front
            Activate();

            // Show the form
            Show();

            // Bring the form to the front again to ensure it is on top
            Activate();
        }

        private void btnToggleNotification_Click(object sender, EventArgs e)
        {
            useMessageBox = !useMessageBox;
            UpdateToggleButtonText();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }

    public class TimerPreset
    {
        public string Name { get; set; }
        public int Seconds { get; set; }

        public TimerPreset(string name, int seconds)
        {
            Name = name;
            Seconds = seconds;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}