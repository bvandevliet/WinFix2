using WinFix2.Core.Helpers;

namespace WinFix2.Core.Features.Privacy;

public class DisableExperiments : FeatureBase
{
  public override string Name => "Disable conducting experiments";
  public override string Description =>
    "Disallow Microsoft to conduct experiments with the settings on your PC.";

  public override bool Default => false;
  public override bool? Recommended => true;
  public override bool? Optimized => true;

  protected override List<RegValueHelper> RegValues { get; } = new()
  {
    new(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\DataCollection", "AllowExperimentation", 0, 1),
    new(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\PolicyManager\current\device\System", "AllowExperimentation", 0, 1),
  };
}
