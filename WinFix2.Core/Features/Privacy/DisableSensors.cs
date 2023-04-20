using System.ServiceProcess;
using WinFix2.Core.Helpers;

namespace WinFix2.Core.Features.Privacy;

public class DisableSensors : FeatureBase
{
  public override string Name => "Disable Sensors";
  public override string Description =>
    "While this is useful for tablets to recognize different conditions such as screen orientation," +
    "\n\nit is not required for laptops or PC's and safe to disable to free up resources.";

  public override bool Default => false;
  public override bool? Recommended => true;
  public override bool? Optimized => true;

  protected override List<SvcStateHelper> SvcStates { get; } = new()
  {
    new("SensorService",     ServiceStartMode.Disabled, ServiceStartMode.Manual),
    new("SensrSvc",          ServiceStartMode.Disabled, ServiceStartMode.Manual),
    new("SensorDataService", ServiceStartMode.Disabled, ServiceStartMode.Manual),
  };

  protected override List<RegValueHelper> RegValues { get; } = new()
  {
    new(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors", "DisableSensors", 1, 0),
  };
}
