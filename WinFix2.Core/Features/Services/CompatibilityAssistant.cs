using System.ServiceProcess;
using WinFix2.Core.Helpers;

namespace WinFix2.Core.Features.Services;

public class CompatibilityAssistant : FeatureBase
{
  public override string Name => "Program Compatibility Assistant";
  public override string Description =>
    "Monitors programs installed and run by the user and detects known compatibility problems." +
    "\n\nSince use cases are very rare, it is safe to disable this service to free up resources.";

  public override bool Default => true;
  public override bool? Recommended => false;
  public override bool? Optimized => false;

  protected override List<SvcStateHelper> SvcStates { get; } = new()
  {
    new("PcaSvc", ServiceStartMode.Disabled, ServiceStartMode.Automatic),
  };

  protected override List<RegValueHelper> RegValues { get; } = new()
  {
    new(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\AppCompat", "DisableEngine", 1, 0),
    new(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "NoCompatCheck", 1, 0),
  };

  public override bool? IsWanted() => !base.IsWanted();

  public override void Enable(bool Enable) => base.Enable(!Enable);
}
