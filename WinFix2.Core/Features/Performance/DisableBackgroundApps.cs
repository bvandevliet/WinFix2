using WinFix2.Core.Helpers;

namespace WinFix2.Core.Features.Performance;

public class DisableBackgroundApps : FeatureBase
{
  public override string Name => "Disallow apps to run in background";
  public override string Description =>
    "By default, apps can receive updates and send notifications, even if they are not being used." +
    "\n\nDisallow background apps to save battery and slightly improve overall performance.";

  public override bool Default => false;
  public override bool? Recommended => true;
  public override bool? Optimized => true;

  protected override List<RegValueHelper> RegValues { get; } = new()
  {
    new(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\BackgroundAccessApplications", "GlobalUserDisabled", 1, 0),
    new(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Search", "BackgroundAppGlobalToggle", 0, 1),
  };
}
