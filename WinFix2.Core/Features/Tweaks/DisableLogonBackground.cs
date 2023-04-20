using WinFix2.Core.Helpers;

namespace WinFix2.Core.Features.Tweaks;

public class DisableLogonBackground : FeatureBase
{
  public override string Name => "Disable lock screen background image";
  public override string Description =>
    "Disable the lock screen background image to login right away.";

  public override bool Default => false;
  public override bool? Recommended => null;
  public override bool? Optimized => true;

  protected override List<RegValueHelper> RegValues { get; } = new()
  {
    new(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\System", "DisableLogonBackgroundImage", 1, 0),
    new(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Personalization", "NoLockScreen", 1, 0),
  };
}
