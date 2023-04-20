using WinFix2.Core.Helpers;

namespace WinFix2.Core.Features.Privacy;

public class DisableShareAcrossDevices : FeatureBase
{
  public override string Name => "Disable share across devices";
  public override string Description =>
    "Disable sharing of messages between apps on other devices.";

  public override bool Default => false;
  public override bool? Recommended => null;
  public override bool? Optimized => true;

  protected override List<RegValueHelper> RegValues { get; } = new()
  {
    new(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\CDP", "CdpSessionUserAuthzPolicy", 0, 2),
    new(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\CDP", "EnableRemoteLaunchToast", 0, 1),
    new(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\CDP", "NearShareChannelUserAuthzPolicy", 0, 2),
    new(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\CDP", "RomeSdkChannelUserAuthzPolicy", 0, 1),
  };
}
