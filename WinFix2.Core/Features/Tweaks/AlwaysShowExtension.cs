using WinFix2.Core.Helpers;

namespace WinFix2.Core.Features.Tweaks;

public class AlwaysShowExtension : FeatureBase
{
  public override string Name => "Always show file extension";
  public override string Description =>
    "Always show the file extension in Windows Explorer, also for well known file types.";

  public override bool Default => false;
  public override bool? Recommended => true;
  public override bool? Optimized => true;

  protected override List<RegValueHelper> RegValues { get; } = new()
  {
    new(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "HideFileExt", 0, 1),
  };
}
