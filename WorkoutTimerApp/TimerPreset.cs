using System.Collections.Generic;

namespace WorkoutTimerApp
{
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

        public static List<TimerPreset> GetDefaultPresets()
        {
            return new List<TimerPreset>
            {
                new TimerPreset("5 Seconds", 5),
                new TimerPreset("30 Seconds", 30),
                new TimerPreset("1 Minute", 60),
                new TimerPreset("1 Minute, 30 Seconds", 90),
                new TimerPreset("2 Minutes", 120),
                new TimerPreset("3 Minutes", 180),
                new TimerPreset("5 Minutes", 300),
                new TimerPreset("10 Minutes", 600),
                new TimerPreset("20 Minutes", 1200),
                new TimerPreset("30 Minutes", 1800),
                new TimerPreset("1 Hour", 3600),
                new TimerPreset("2 Hours", 7200),
                new TimerPreset("3 Hours", 10800),
                new TimerPreset("5 Hours", 18000)
            };
        }
    }
}
