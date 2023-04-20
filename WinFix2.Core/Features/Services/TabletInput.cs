using System.ServiceProcess;
using WinFix2.Core.Helpers;

namespace WinFix2.Core.Features.Services;

public class TabletInput : FeatureBase
{
  public override string Name => "Tablet Input";
  public override string Description =>
    "Touch Keyboard and Handwriting Panel Service.";

  public override bool Default => true;
  public override bool? Recommended => false;
  public override bool? Optimized => false;

  protected override List<SvcStateHelper> SvcStates { get; } = new()
  {
    new("TabletInputService", ServiceStartMode.Disabled, ServiceStartMode.Automatic),
  };

  public override bool? IsWanted() => !base.IsWanted();

  public override void Enable(bool Enable) => base.Enable(!Enable);
}
