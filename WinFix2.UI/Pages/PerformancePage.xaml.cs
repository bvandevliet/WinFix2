using System.Windows.Controls;
using WinFix2.Core.Helpers;
using WinFix2.UI.Helpers;

namespace WinFix2.UI.Pages;
/// <summary>
/// Interaction logic for PerformancePage.xaml
/// </summary>
public partial class PerformancePage : Page
{
  private readonly List<FeatureBase> features = new()
  {
    new Core.Features.Performance.DisableBackgroundApps(),
    new Core.Features.Performance.DisableHibernate(),
    new Core.Features.Performance.EnhanceNTFS(),
    new Core.Features.Performance.RunExplorerSeparate(),
  };

  private static readonly Lazy<PerformancePage> lazy = new(() => new PerformancePage());

  public static PerformancePage Instance { get => lazy.Value; }

  public PerformancePage()
  {
    InitializeComponent();

    PageHelper.SetupButtons(
      this.features,
      this.stackPanel,
      this.currentButton,
      this.defaultButton,
      this.recommendedButton,
      this.optimizedButton,
      this.applyButton);
  }
}
