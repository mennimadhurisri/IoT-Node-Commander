using iotcommender;
using System.Windows;

namespace IotCommander;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        MainWindow window = new MainWindow();

        window.Show();
    }
}
