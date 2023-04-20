using System.Windows.Controls;
using WinFix2.Core.Helpers;
using WinFix2.UI.Helpers;

namespace WinFix2.UI.Pages;
/// <summary>
/// Interaction logic for TweaksPage.xaml
/// </summary>
public partial class TweaksPage : Page
{
  private readonly List<FeatureBase> features = new()
  {
    new Core.Features.Tweaks.DisableLogonBackground(),
    new Core.Features.Tweaks.AlwaysShowExtension(),
    new Core.Features.Tweaks.EnableNumlockOnBoot(),
  };

  private static readonly Lazy<TweaksPage> lazy = new(() => new TweaksPage());

  public static TweaksPage Instance { get => lazy.Value; }

  public TweaksPage()
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
