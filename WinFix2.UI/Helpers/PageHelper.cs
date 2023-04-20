using System.Windows.Controls;
using WinFix2.Core.Helpers;

namespace WinFix2.UI.Helpers;

internal static class PageHelper
{
  public static void SetupButtons(
    List<FeatureBase> features,
    Panel container,
    Button currentButton,
    Button defaultButton,
    Button recommendedButton,
    Button optimizedButton,
    Button applyButton)
  {
    foreach (FeatureBase service in features)
    {
      CheckBox checkbox = new()
      {
        Content = service.Name,
        IsChecked = service.IsWanted(),
      };

      currentButton.Click += (object sender, System.Windows.RoutedEventArgs e) =>
      {
        checkbox.IsChecked = service.IsWanted();
      };

      defaultButton.Click += (object sender, System.Windows.RoutedEventArgs e) =>
      {
        checkbox.IsChecked = service.Default;
      };

      recommendedButton.Click += (object sender, System.Windows.RoutedEventArgs e) =>
      {
        if (null != service.Recommended)
        {
          checkbox.IsChecked = service.Recommended;
        }
      };

      optimizedButton.Click += (object sender, System.Windows.RoutedEventArgs e) =>
      {
        checkbox.IsChecked = service.Optimized;
      };

      applyButton.Click += async (object sender, System.Windows.RoutedEventArgs e) =>
      {
        if (checkbox.IsChecked is bool isChecked)
        {
          await Task.Run(() => { service.Enable(isChecked); });

          checkbox.IsChecked = service.IsWanted();
        }
      };

      container.Children.Add(checkbox);
    }
  }
}
