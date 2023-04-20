using WinFix2.Core.Helpers;

namespace WinFix2.Core.Features.Performance;

public class RunExplorerSeparate : FeatureBase
{
  public override string Name => "Run Explorer processes separately";
  public override string Description =>
    "Improves stability, but requires additional system resources.";

  public override bool Default => false;
  public override bool? Recommended => true;
  public override bool? Optimized => true;

  protected override List<RegValueHelper> RegValues { get; } = new()
  {
    new(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer", "DesktopProcess", 1, 0),
    new(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer", "BrowseNewProcess", 1, 0),
    new(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "SeparateProcess", 1, 0),
  };
}
