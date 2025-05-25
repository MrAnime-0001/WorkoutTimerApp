using Gma.System.MouseKeyHook;
using NAudio.Wave;
using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace WorkoutTimerApp
{
    public partial class Form2 : Form
    {
        private int currentSeconds;
        private int totalSeconds;
        private bool isTimerRunning;
        private WaveOutEvent waveOut;
        private AudioFileReader mp3FileReader;
        private System.Windows.Forms.Timer timer2;
        private NotifyIcon notifyIcon2;
        private IKeyboardMouseEvents globalHook2;
        private bool useMessageBox2 = false;

        // Add properties to store timer state when switching forms
        private bool wasTimerRunningBeforeSwitch = false;

        public Form2()
        {
            InitializeComponent();
            InitializePresets();
            InitializeNotificationIcon();

            timer2 = new System.Windows.Forms.Timer { Interval = 1000 };
            timer2.Tick += Timer2_Tick;

            waveOut = new WaveOutEvent();
            InitializeSoundPlayer();

            TimerPreset2 defaultPreset = GetPresetBySeconds(30);
            if (defaultPreset != null)
            {
                cbPresets2.SelectedItem = defaultPreset;
            }

            globalHook2 = Hook.GlobalEvents();
            globalHook2.KeyDown += GlobalHook_KeyDown;

            // Set initial timer display
            UpdateTimeLabel();
        }

        // Method to pause timer when switching forms
        public void PauseTimerForFormSwitch()
        {
            if (isTimerRunning)
            {
                wasTimerRunningBeforeSwitch = true;
                isTimerRunning = false;
                timer2.Stop();
            }
            else
            {
                wasTimerRunningBeforeSwitch = false;
            }
        }

        // Method to resume timer when returning to this form
        public void ResumeTimerFromFormSwitch()
        {
            if (wasTimerRunningBeforeSwitch)
            {
                isTimerRunning = true;
                timer2.Start();
                wasTimerRunningBeforeSwitch = false;
            }
        }

        // Method to get current timer state for passing to other forms
        public TimerState GetCurrentTimerState()
        {
            return new TimerState
            {
                CurrentSeconds = currentSeconds,
                TotalSeconds = totalSeconds,
                IsRunning = isTimerRunning || wasTimerRunningBeforeSwitch,
                SelectedPresetSeconds = cbPresets2.SelectedItem != null ? ((TimerPreset2)cbPresets2.SelectedItem).Seconds : 0
            };
        }

        // Method to set timer state from other forms
        public void SetTimerState(TimerState state)
        {
            if (state != null)
            {
                currentSeconds = state.CurrentSeconds;
                totalSeconds = state.TotalSeconds;

                // Set the preset selection
                if (state.SelectedPresetSeconds > 0)
                {
                    TimerPreset2 preset = GetPresetBySeconds(state.SelectedPresetSeconds);
                    if (preset != null)
                    {
                        cbPresets2.SelectedItem = preset;
                    }
                }

                UpdateTimeLabel();

                if (state.IsRunning)
                {
                    wasTimerRunningBeforeSwitch = true;
                }
            }
        }

        private void InitializePresets()
        {
            // Add your preset workout and rest periods
            cbPresets2.Items.Add(new TimerPreset2("5 Seconds", 5));
            cbPresets2.Items.Add(new TimerPreset2("10 Seconds", 10));
            cbPresets2.Items.Add(new TimerPreset2("20 Seconds", 20));
            cbPresets2.Items.Add(new TimerPreset2("30 Seconds", 30));
            cbPresets2.Items.Add(new TimerPreset2("1 Minute", 60));
            cbPresets2.Items.Add(new TimerPreset2("1 Minute, 30 Seconds", 90));
            cbPresets2.Items.Add(new TimerPreset2("2 Minutes", 120));
            cbPresets2.Items.Add(new TimerPreset2("3 Minutes", 180));
            cbPresets2.Items.Add(new TimerPreset2("5 Minutes", 300));
            cbPresets2.Items.Add(new TimerPreset2("10 Minutes", 600));
            cbPresets2.Items.Add(new TimerPreset2("20 Minutes", 1200));
            cbPresets2.Items.Add(new TimerPreset2("30 Minutes", 1800));
            cbPresets2.Items.Add(new TimerPreset2("1 Hour", 3600));
            cbPresets2.Items.Add(new TimerPreset2("2 Hours", 7200));
            cbPresets2.Items.Add(new TimerPreset2("3 Hours", 10800));
            cbPresets2.Items.Add(new TimerPreset2("5 Hours", 18000));
            cbPresets2.Items.Add(new TimerPreset2("10 Hours", 36000));
            cbPresets2.Items.Add(new TimerPreset2("12 Hours", 43200));
            cbPresets2.Items.Add(new TimerPreset2("24 Hours", 86400));
        }

        private void InitializeSoundPlayer(string filePath = null)
        {
            try
            {
                if (filePath == null)
                    filePath = Path.Combine(Application.StartupPath, "timer_end.mp3");

                if (File.Exists(filePath))
                {
                    mp3FileReader = new AudioFileReader(filePath);
                    waveOut.Init(mp3FileReader);
                }
                else
                {
                    // If file doesn't exist, we'll use system beep instead
                    mp3FileReader = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing sound player: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mp3FileReader = null;
            }
        }

        private void InitializeNotificationIcon()
        {
            notifyIcon2 = new NotifyIcon
            {
                Icon = SystemIcons.Information,
                Visible = false,
                Text = "Workout Timer"
            };
            notifyIcon2.Click += NotifyIcon_Click;
        }

        private TimerPreset2 GetPresetBySeconds(int seconds)
        {
            foreach (TimerPreset2 preset in cbPresets2.Items)
            {
                if (preset.Seconds == seconds) return preset;
            }
            return null;
        }

        private void StartTimerWithDuration(int seconds)
        {
            TimerPreset2 preset = GetPresetBySeconds(seconds);
            if (preset != null)
            {
                cbPresets2.SelectedItem = preset;
                totalSeconds = preset.Seconds;
                currentSeconds = totalSeconds;
                isTimerRunning = true;
                timer2.Start();
                UpdateTimeLabel();
            }
            else
            {
                // Create a temporary preset if it doesn't exist
                totalSeconds = seconds;
                currentSeconds = totalSeconds;
                isTimerRunning = true;
                timer2.Start();
                UpdateTimeLabel();
            }
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            if (currentSeconds > 0)
            {
                currentSeconds--;
                UpdateTimeLabel();
            }
            else
            {
                isTimerRunning = false;
                timer2.Stop();

                // Play sound
                PlayTimerSound();

                if (useMessageBox2)
                    MessageBox.Show("Workout Complete!", "Timer Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    ShowCustomToast("Workout Complete!", playSound: false); // Don't play sound again

                ResetTimer();
            }
        }

        private void PlayTimerSound()
        {
            try
            {
                if (mp3FileReader != null)
                {
                    mp3FileReader.Position = 0;
                    waveOut.Play();
                }
                else
                {
                    // Fallback to system beep if no MP3 file
                    SystemSounds.Beep.Play();
                }
            }
            catch
            {
                SystemSounds.Beep.Play();
            }
        }

        private void UpdateTimeLabel()
        {
            int hours = currentSeconds / 3600;
            int minutes = (currentSeconds % 3600) / 60;
            int seconds = currentSeconds % 60;

            lblTime2.Text = $"{hours:D2}:{minutes:D2}:{seconds:D2}";
        }

        private void ResetTimer()
        {
            isTimerRunning = false;
            timer2.Stop();
            if (cbPresets2.SelectedItem != null)
            {
                totalSeconds = ((TimerPreset2)cbPresets2.SelectedItem).Seconds;
                currentSeconds = totalSeconds;
            }
            else
            {
                currentSeconds = 0;
                totalSeconds = 0;
            }
            UpdateTimeLabel();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!isTimerRunning && cbPresets2.SelectedItem != null)
            {
                isTimerRunning = true;
                totalSeconds = ((TimerPreset2)cbPresets2.SelectedItem).Seconds;
                currentSeconds = totalSeconds;
                timer2.Start();
                UpdateTimeLabel();
            }
            else if (isTimerRunning)
            {
                // Resume if paused
                isTimerRunning = true;
                timer2.Start();
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (isTimerRunning)
            {
                isTimerRunning = false;
                timer2.Stop();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            isTimerRunning = false;
            timer2.Stop();
            if (cbPresets2.SelectedItem != null)
            {
                totalSeconds = ((TimerPreset2)cbPresets2.SelectedItem).Seconds;
                currentSeconds = totalSeconds;
            }
            UpdateTimeLabel();
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
            if (WindowState == FormWindowState.Minimized)
                WindowState = FormWindowState.Normal;

            this.Show();
            this.Activate();
            this.BringToFront();
        }

        private void ShowCustomToast(string message, bool playSound = false)
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

            Label messageLabel = new Label
            {
                Text = message,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White
            };

            toast.Controls.Add(messageLabel);

            toast.Location = new Point(
                Screen.PrimaryScreen.WorkingArea.Right - toast.Width - 10,
                Screen.PrimaryScreen.WorkingArea.Bottom - toast.Height - 10);

            toast.Shown += async (s, e) =>
            {
                if (playSound)
                {
                    PlayTimerSound();
                }
                await Task.Delay(2000);
                try
                {
                    toast.Close();
                }
                catch { }
            };

            toast.Show();
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
                            ShowCustomToast("Timer: 30 seconds");
                            break;
                        case Keys.NumPad7:
                            StartTimerWithDuration(60);
                            ShowCustomToast("Timer: 1 minute");
                            break;
                        case Keys.NumPad8:
                            StartTimerWithDuration(90);
                            ShowCustomToast("Timer: 1:30 minutes");
                            break;
                        case Keys.NumPad9:
                            StartTimerWithDuration(120);
                            ShowCustomToast("Timer: 2 minutes");
                            break;
                    }
                }

                if (e.KeyCode == Keys.NumPad6)
                {
                    btnReset_Click(null, null);
                    ShowCustomToast("Timer Reset");
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Pause timer before switching back
            PauseTimerForFormSwitch();

            // Get current timer state
            TimerState currentState = GetCurrentTimerState();

            this.Hide();
            MainForm form1 = new MainForm();
            // Pass current timer state back to Form1
            form1.SetTimerState(currentState);
            form1.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // Clean up resources
            timer2?.Stop();
            globalHook2?.Dispose();
            waveOut?.Dispose();
            mp3FileReader?.Dispose();
            notifyIcon2?.Dispose();

            Application.Exit();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Clean up resources when form is closing
            timer2?.Stop();
            globalHook2?.Dispose();
            waveOut?.Dispose();
            mp3FileReader?.Dispose();
            notifyIcon2?.Dispose();

            base.OnFormClosing(e);
        }
    }

    public class TimerPreset2
    {
        public string Name { get; set; }
        public int Seconds { get; set; }

        public TimerPreset2(string name, int seconds)
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