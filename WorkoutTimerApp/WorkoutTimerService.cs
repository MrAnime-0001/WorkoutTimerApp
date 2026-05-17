using System;
using System.IO;
using System.Windows.Forms;
using NAudio.Wave;
using Gma.System.MouseKeyHook;

namespace WorkoutTimerApp
{
    public class WorkoutTimerService : IDisposable
    {
        private System.Windows.Forms.Timer _timer;
        private WaveOutEvent _waveOut;
        private AudioFileReader? _audioReader;
        private IKeyboardMouseEvents _globalHook;
        private string? _soundFilePath;

        public int CurrentSeconds { get; private set; }
        public int TotalSeconds { get; private set; }
        public TimerStatus Status { get; private set; } = TimerStatus.Idle;
        public bool IsRunning => Status == TimerStatus.Running;

        public float Volume
        {
            get => _waveOut?.Volume ?? 1.0f;
            set { if (_waveOut != null) _waveOut.Volume = value; }
        }

        public event EventHandler? Tick;
        public event EventHandler? TimerFinished;
        public event EventHandler? StatusChanged;
        public event EventHandler<int>? ShortcutTriggered;
        public event EventHandler? ResetTriggered;

        public WorkoutTimerService()
        {
            _timer = new System.Windows.Forms.Timer { Interval = 1000 };
            _timer.Tick += OnTimerTick;

            _waveOut = new WaveOutEvent();
            InitializeSoundPlayer();

            _globalHook = Hook.GlobalEvents();
            _globalHook.KeyDown += OnGlobalKeyDown;
        }

        public void InitializeSoundPlayer(string? filePath = null)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    string[] possiblePaths = new string[]
                    {
                        Path.Combine(Application.StartupPath, "timer_end.mp3"),
                        Path.Combine(Application.StartupPath, "timer_end v2.mp3"),
                        "timer_end.mp3"
                    };
                    foreach (var path in possiblePaths)
                    {
                        if (File.Exists(path)) { filePath = path; break; }
                    }
                }
                if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath)) return;

                _soundFilePath = filePath;
                float currentVolume = Volume;
                _waveOut?.Stop();
                _waveOut?.Dispose();
                _audioReader?.Dispose();
                _waveOut = new WaveOutEvent();
                _audioReader = new AudioFileReader(filePath);
                _waveOut.Init(_audioReader);
                _waveOut.Volume = currentVolume;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing sound player: {ex.Message}");
            }
        }

        public void Start(int seconds)
        {
            TotalSeconds = seconds;
            CurrentSeconds = seconds;
            SetStatus(TimerStatus.Running);
            _timer.Start();
        }

        public void Pause()
        {
            if (Status == TimerStatus.Running)
            {
                SetStatus(TimerStatus.Paused);
                _timer.Stop();
            }
        }

        public void Resume()
        {
            if (Status == TimerStatus.Paused && CurrentSeconds > 0)
            {
                SetStatus(TimerStatus.Running);
                _timer.Start();
            }
        }

        public void Reset()
        {
            SetStatus(TimerStatus.Idle);
            _timer.Stop();
            CurrentSeconds = TotalSeconds;
        }

        public void SetCurrentSeconds(int seconds) => CurrentSeconds = seconds;
        public void SetTotalSeconds(int seconds) => TotalSeconds = seconds;

        private void SetStatus(TimerStatus newStatus)
        {
            if (Status != newStatus)
            {
                Status = newStatus;
                StatusChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void OnTimerTick(object? sender, EventArgs e)
        {
            if (CurrentSeconds > 0)
            {
                CurrentSeconds--;
                Tick?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                StopAndNotify();
            }
        }

        private void StopAndNotify()
        {
            SetStatus(TimerStatus.Idle);
            _timer.Stop();
            PlaySound();
            TimerFinished?.Invoke(this, EventArgs.Empty);
        }

        public void PlaySound()
        {
            try
            {
                if (_audioReader != null && _waveOut != null)
                {
                    _audioReader.Position = 0;
                    _waveOut.Play();
                }
                else
                {
                    System.Media.SystemSounds.Beep.Play();
                }
            }
            catch
            {
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void OnGlobalKeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Alt)
            {
                int duration = 0;
                switch (e.KeyCode)
                {
                    case Keys.NumPad1: duration = 30; break;
                    case Keys.NumPad2: duration = 60; break;
                    case Keys.NumPad3: duration = 90; break;
                    case Keys.NumPad4: duration = 120; break;
                    case Keys.NumPad5: duration = 180; break;
                    case Keys.NumPad6: duration = 300; break;
                }
                if (duration > 0 && !IsRunning)
                    ShortcutTriggered?.Invoke(this, duration);
                if (e.KeyCode == Keys.NumPad0)
                    ResetTriggered?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Dispose()
        {
            _timer?.Stop();
            _timer?.Dispose();
            _waveOut?.Stop();
            _waveOut?.Dispose();
            _audioReader?.Dispose();
            _globalHook?.Dispose();
        }
    }
}
