using WinFix2.Core.Helpers;

namespace WinFix2.Core.Features.Privacy;

public class DisableInventory : FeatureBase
{
  public override string Name => "Disable Inventory Collector";
  public override string Description =>
    "Collects information on installed applications, devices and system information from all computers in a network.";

  public override bool Default => false;
  public override bool? Recommended => true;
  public override bool? Optimized => true;

  protected override List<RegValueHelper> RegValues { get; } = new()
  {
    new(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\AppCompat", "DisableInventory", 1, 0),
  };
}
