using WinFix2.Core.Helpers;

namespace WinFix2.Core.Features.Privacy;

public class DisableWifiSense : FeatureBase
{
  public override string Name => "Disable WifiSense";
  public override string Description =>
    "Wifi Sense is a feature in that allows you to connect to your friends shared Wifi connections.";

  public override bool Default => false;
  public override bool? Recommended => true;
  public override bool? Optimized => true;

  protected override List<RegValueHelper> RegValues { get; } = new()
  {
    new(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\WcmSvc\wifinetworkmanager\features\{6acfcf6e-f03a-41bd-9dce-97431ba3d3ea}", "Enabled", 0, 1),
    new(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\WcmSvc\wifinetworkmanager\features", "WifiSenseCredShared", 0, 1),
    new(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\WcmSvc\wifinetworkmanager\features", "WifiSenseOpen", 0, 1),
    new(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\WcmSvc\wifinetworkmanager\config", "AutoConnectAllowedOEM", 0, 1),
  };
}
