using System.Windows.Forms;

namespace WorkoutTimerApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var timerService = new WorkoutTimerService();
            var trayController = new TrayController(timerService);

            var mainForm = new MainForm(timerService, trayController);
            trayController.SetActiveForm(mainForm);
            mainForm.Show();

            Application.Run();
        }
    }
}
