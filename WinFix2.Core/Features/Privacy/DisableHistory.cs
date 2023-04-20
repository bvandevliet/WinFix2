using WinFix2.Core.Helpers;

namespace WinFix2.Core.Features.Privacy;

public class DisableHistory : FeatureBase
{
  public override string Name => "Disable Activity history";
  public override string Description =>
    "Stop storing your activity history on your device.";

  public override bool Default => false;
  public override bool? Recommended => true;
  public override bool? Optimized => true;

  protected override List<RegValueHelper> RegValues { get; } = new()
  {
    new(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ActivityFeed", 0, null),
  };
}
