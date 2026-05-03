namespace WorkoutTimerApp
{
    public class TimerState
    {
        public int CurrentSeconds { get; set; }
        public int TotalSeconds { get; set; }
        public bool IsRunning { get; set; }
        public int SelectedPresetSeconds { get; set; }
    }
}
