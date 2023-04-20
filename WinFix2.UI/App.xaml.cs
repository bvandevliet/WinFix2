using System.Windows;
using WinFix2.UI.Helpers;

namespace WinFix2.UI;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
  internal static EventWriter ConsoleWriter { get; } = new();

  public App()
  {
    Startup += App_Startup;
  }

  private void App_Startup(object sender, StartupEventArgs e)
  {
    Console.SetOut(ConsoleWriter);
  }
}
