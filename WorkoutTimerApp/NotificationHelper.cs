using System;
using System.Drawing;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkoutTimerApp
{
    public static class NotificationHelper
    {
        public static void ShowToast(
            string message,
            int duration = 2000,
            bool playSound = true
        )
        {
            // CONFIG
            Size size = new Size(250, 60);
            Color backColor = Color.Black;
            Color textColor = Color.White;
            double opacity = 0.9;
            int margin = 10;

            // CREATE FORM
            Form toast = new Form
            {
                FormBorderStyle = FormBorderStyle.FixedToolWindow,
                StartPosition = FormStartPosition.Manual,
                ShowInTaskbar = false,
                TopMost = true,
                ControlBox = false,
                Text = string.Empty,
                BackColor = backColor,
                Size = size,
                Opacity = opacity
            };

            // LABEL
            toast.Controls.Add(new Label
            {
                Text = message,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = textColor,
                BackColor = Color.Transparent
            });

            // POSITION (SCREEN)
            var screen = Screen.PrimaryScreen.WorkingArea;
            toast.Location = new Point(
                screen.Right - toast.Width - margin,
                screen.Bottom - toast.Height - margin
            );

            // BEHAVIOUR
            toast.Shown += async (s, e) =>
            {
                if (playSound)
                    SystemSounds.Exclamation.Play();

                await Task.Delay(duration);

                for (double i = toast.Opacity; i >= 0; i -= 0.05)
                {
                    if (toast.IsDisposed) break;

                    toast.Invoke((Action)(() => toast.Opacity = i));
                    await Task.Delay(20);
                }

                if (!toast.IsDisposed)
                    toast.Invoke((Action)(() => toast.Close()));
            };

            toast.Show();
        }
    }
}
