using System.Windows.Controls;
using WinFix2.Core.Helpers;
using WinFix2.UI.Helpers;

namespace WinFix2.UI.Pages;
/// <summary>
/// Interaction logic for ServicesPage.xaml
/// </summary>
public partial class ServicesPage : Page
{
  private readonly List<FeatureBase> features = new()
  {
    new Core.Features.Services.FontCache(),
    new Core.Features.Services.DeliveryOptimization(),
    new Core.Features.Services.WindowsSearch(),
    new Core.Features.Services.ErrorReporting(),
    new Core.Features.Services.BackupAndShadowCopy(),
    new Core.Features.Services.CompatibilityAssistant(),
    new Core.Features.Services.DistributedLinkTracking(),
    new Core.Features.Services.SuperfetchAndPrefetch(),
    new Core.Features.Services.TabletInput(),
  };

  private static readonly Lazy<ServicesPage> lazy = new(() => new ServicesPage());

  public static ServicesPage Instance { get => lazy.Value; }

  public ServicesPage()
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
