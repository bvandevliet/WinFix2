using WinFix2.Core.Helpers;

namespace WinFix2.Core.Features.Privacy;

public class DisableHiddenShares : FeatureBase
{
  public override string Name => "Disable unauthorized access";
  public override string Description =>
    "Disable hidden shared folders to prevent administrators from seeing your files" +
    "\n\nAnd deny anonymous user access to prevent unauthorized use of your computer.";

  public override bool Default => false;
  public override bool? Recommended => true;
  public override bool? Optimized => true;

  protected override List<RegValueHelper> RegValues { get; } = new()
  {
    new(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\LanmanServer\Parameters", "AutoShareWks", 0, 1),
    new(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Lsa", "RestrictAnonymous", 2, 0),
  };
}
