using System.ServiceProcess;
using WinFix2.Core.Helpers;

namespace WinFix2.Core.Features.Privacy;

public class DisableLocation : FeatureBase
{
  public override string Name => "Disable Location";
  public override string Description =>
    "Disable tracking of your location and storing location history.";

  public override bool Default => false;
  public override bool? Recommended => null;
  public override bool? Optimized => true;

  protected override List<SvcStateHelper> SvcStates { get; } = new()
  {
    new("lfsvc", ServiceStartMode.Disabled, ServiceStartMode.Manual),
  };

  protected override List<RegValueHelper> RegValues { get; } = new()
  {
    new(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Search", "AllowSearchToUseLocation", 0, 1),
    new(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Privacy", "DisableGeoLocation", 1, 0),
    new(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors", "DisableLocation", 1, 0),
    new(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors", "DisableWindowsLocationProvider", 1, 0),
    new(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors", "DisableLocationScripting", 1, 0),
  };
}
