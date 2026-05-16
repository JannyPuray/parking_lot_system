namespace ParkingLotDesktop;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.ThreadException += (_, e) => MessageBox.Show(e.Exception.ToString(), "Application error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        AppDomain.CurrentDomain.UnhandledException += (_, e) => MessageBox.Show(Convert.ToString(e.ExceptionObject), "Fatal application error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        Application.Run(new LoginForm());
    }
}
