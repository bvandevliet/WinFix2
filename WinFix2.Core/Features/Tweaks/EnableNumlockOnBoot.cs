using WinFix2.Core.Helpers;

namespace WinFix2.Core.Features.Tweaks;

public class EnableNumlockOnBoot : FeatureBase
{
  public override string Name => "Enable numlock on boot";
  public override string Description =>
    "Numlock will be turned on by default.";

  public override bool Default => false;
  public override bool? Recommended => null;
  public override bool? Optimized => true;

  protected override List<RegValueHelper> RegValues { get; } = new()
  {
    new(@"HKEY_CURRENT_USER\Control Panel\Keyboard", "InitialKeyboardIndicators", "2", "2147483650"),
    new(@"HKEY_USERS\.DEFAULT\Control Panel\Keyboard", "InitialKeyboardIndicators", "2", "2147483650"),
  };
}
