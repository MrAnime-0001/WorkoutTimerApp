namespace WorkoutTimerApp
{
    public enum TimerStatus
    {
        Idle,
        Running,
        Paused
    }

    public class TimerState
    {
        public int CurrentSeconds { get; set; }
        public int TotalSeconds { get; set; }
        public TimerStatus Status { get; set; }
        public int SelectedPresetSeconds { get; set; }
    }
}
