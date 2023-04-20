using System.Windows;
using System.Windows.Controls;
using WinFix2.UI.Components;

namespace WinFix2.UI;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
  internal static MainWindow Instance { get; private set; } = null!;

  public MainWindow()
  {
    Instance = this;

    IsEnabled = false;

    InitializeComponent();

    App.ConsoleWriter.OnWrite += (object? sender, string consoleOut) =>
    {
      Dispatcher.InvokeAsync(() =>
      {
        try
        {
          this.consoleBox.Text += consoleOut;
          this.consoleScroll.ScrollToBottom();
        }
        catch (Exception)
        {
        }
      });
    };

    ContentRendered += MainWindow_ContentRendered;
  }

  private void MainWindow_ContentRendered(object? sender, EventArgs e)
  {
    // Defer listening to the selection changed event untill after all rendered.
    this.navMenu.SelectionChanged += NavMenu_SelectionChanged; ;

    // Enable GUI.
    IsEnabled = true;
  }

  private void NavMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
  {
    if (this.navMenu.SelectedItem is not NavItem selected || selected.NavPage is not Page page)
    {
      this.navFrame.Content = null;
    }
    else
    {
      this.navFrame.Content = page; page.Focus();
    }
  }
}
