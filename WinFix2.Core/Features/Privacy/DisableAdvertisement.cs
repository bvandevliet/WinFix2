using WinFix2.Core.Helpers;

namespace WinFix2.Core.Features.Privacy;

public class DisableAdvertisement : FeatureBase
{
  public override string Name => "Disable Advertisement Info";
  public override string Description =>
    ""; // !!

  public override bool Default => false;
  public override bool? Recommended => true;
  public override bool? Optimized => true;

  protected override List<RegValueHelper> RegValues { get; } = new()
  {
    new(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\AdvertisingInfo", "Enabled", 0, 1),
    new(@"HKEY_CURRENT_USER\Control Panel\International\User Profile", "HttpAcceptLanguageOptOut", 1, 0),
  };
}
