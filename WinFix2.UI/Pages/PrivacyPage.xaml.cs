using System.Windows.Controls;
using WinFix2.Core.Helpers;
using WinFix2.UI.Helpers;

namespace WinFix2.UI.Pages;
/// <summary>
/// Interaction logic for PrivacyPage.xaml
/// </summary>
public partial class PrivacyPage : Page
{
  private readonly List<FeatureBase> features = new()
  {
    new Core.Features.Privacy.DisableRemoteAssistance(),
    new Core.Features.Privacy.DisableHistory(),
    new Core.Features.Privacy.DisableAdvertisement(),
    new Core.Features.Privacy.DisableCortana(),
    new Core.Features.Privacy.DisableExperiments(),
    new Core.Features.Privacy.DisableLocation(),
    new Core.Features.Privacy.DisableHiddenShares(),
    new Core.Features.Privacy.DisableInventory(),
    new Core.Features.Privacy.DisableSensors(),
    //new Core.Features.Privacy.DisableShareAcrossDevices(),
    new Core.Features.Privacy.DisableStepsRecorder(),
    new Core.Features.Privacy.DisableTelemetry(),
    new Core.Features.Privacy.DisableTypingInkingDictionary(),
    new Core.Features.Privacy.DisableWifiSense(),
  };

  private static readonly Lazy<PrivacyPage> lazy = new(() => new PrivacyPage());

  public static PrivacyPage Instance { get => lazy.Value; }

  public PrivacyPage()
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
