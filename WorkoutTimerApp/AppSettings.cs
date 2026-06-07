using System;
using System.IO;
using System.Text.Json;

namespace WorkoutTimerApp
{
    public class AppSettings
    {
        public string SelectedSound { get; set; } = "default";
        public float Volume { get; set; } = 1.0f;
        public bool NotificationMode { get; set; } = false;
        public bool TopMost { get; set; } = false;
        public string? CustomSoundPath { get; set; } = null;

        private static string GetSettingsPath()
        {
            string folder = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Data");
            Directory.CreateDirectory(folder);
            return Path.Combine(folder, "settings.json");
        }

        public static AppSettings Load()
        {
            try
            {
                string path = GetSettingsPath();
                if (File.Exists(path))
                {
                    string json = File.ReadAllText(path);
                    return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
                }
            }
            catch { }
            return new AppSettings();
        }

        public void Save()
        {
            try
            {
                string path = GetSettingsPath();
                string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(path, json);
            }
            catch { }
        }
    }
}
