using WinFix2.Core.Helpers;

namespace WinFix2.Core.Features.Privacy;

public class DisableRemoteAssistance : FeatureBase
{
  public override string Name => "Disable Remote Assistance";
  public override string Description =>
    ""; // !!

  public override bool Default => false;
  public override bool? Recommended => true;
  public override bool? Optimized => true;

  protected override List<RegValueHelper> RegValues { get; } = new()
  {
    new(@"HKEY_LOCAL_MACHINE\System\CurrentControlSet\Control\Remote Assistance", "fAllowToGetHelp", 0, 1),
  };
}
