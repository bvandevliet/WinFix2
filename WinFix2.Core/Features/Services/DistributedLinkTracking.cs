using System.ServiceProcess;
using WinFix2.Core.Helpers;

namespace WinFix2.Core.Features.Services;

public class DistributedLinkTracking : FeatureBase
{
  public override string Name => "Distributed Link Tracking Client";
  public override string Description =>
    "Maintains links between NTFS files within a computer or across computers in a network." +
    "\n\nIf this service is disabled, shortcuts do not automatically update but get broken after moving the targeted file or folder." +
    "\nIf this is not important for you, it's safe to disable this service to free up resources.";

  public override bool Default => true;
  public override bool? Recommended => false;
  public override bool? Optimized => false;

  protected override List<SvcStateHelper> SvcStates { get; } = new()
  {
    new("TrkWks", ServiceStartMode.Disabled, ServiceStartMode.Automatic),
  };

  public override bool? IsWanted() => !base.IsWanted();

  public override void Enable(bool Enable) => base.Enable(!Enable);
}
