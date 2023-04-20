using System.ServiceProcess;
using WinFix2.Core.Helpers;

namespace WinFix2.Core.Features.Services;

public class FontCache : FeatureBase
{
  public override string Name => "Font Cache Service";
  public override string Description =>
    "Optimizes performance of applications by caching commonly used font data." +
    "\n\nWhile this has almost no drawback on performance, it still is a non-essential extra resource.";

  public override bool Default => true;
  public override bool? Recommended => false;
  public override bool? Optimized => false;

  protected override List<SvcStateHelper> SvcStates { get; } = new()
  {
    new("FontCache",        ServiceStartMode.Disabled, ServiceStartMode.Automatic),
    new("FontCache3.0.0.0", ServiceStartMode.Disabled, ServiceStartMode.Automatic),
  };

  public override bool? IsWanted() => !base.IsWanted();

  public override void Enable(bool Enable) => base.Enable(!Enable);
}
