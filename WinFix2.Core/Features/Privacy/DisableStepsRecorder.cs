using WinFix2.Core.Helpers;

namespace WinFix2.Core.Features.Privacy;

public class DisableStepsRecorder : FeatureBase
{
  public override string Name => "Disable Steps Recorder";
  public override string Description =>
    ""; // !!

  public override bool Default => false;
  public override bool? Recommended => true;
  public override bool? Optimized => true;

  protected override List<RegValueHelper> RegValues { get; } = new()
  {
    new(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\AppCompat", "NoAutoRun", 1, 0),
    new(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", "NoInstrumentation", 1, 0),
  };
}
