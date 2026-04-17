using System;
using System.Drawing;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkoutTimerApp
{
    public static class NotificationHelper
    {
        public static void ShowToast(string message, int duration = 2000, bool playSound = true)
        {
            Form toast = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.Manual,
                ShowInTaskbar = false,
                TopMost = true,
                BackColor = Color.Black,
                Size = new Size(250, 60),
                Opacity = 0.9
            };

            // Hides from alt-tab by setting the form as a tool window
            toast.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            toast.ShowInTaskbar = false;

            toast.Controls.Add(new Label
            {
                Text = message,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent
            });

            var screen = Screen.PrimaryScreen.WorkingArea;
            toast.Location = new Point(screen.Right - toast.Width - 10, screen.Bottom - toast.Height - 10);

            toast.Shown += async (s, e) =>
            {
                if (playSound)
                {
                    SystemSounds.Exclamation.Play();
                }

                await Task.Delay(duration);

                // Fade out
                for (double i = toast.Opacity; i >= 0; i -= 0.05)
                {
                    toast.Invoke((Action)(() => toast.Opacity = i));
                    await Task.Delay(20);
                }

                toast.Invoke((Action)(() => toast.Close()));
            };

            toast.Show();
        }
    }
}
