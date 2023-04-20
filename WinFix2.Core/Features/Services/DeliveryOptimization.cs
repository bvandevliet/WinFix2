using System.ServiceProcess;
using WinFix2.Core.Helpers;

namespace WinFix2.Core.Features.Services;

public class DeliveryOptimization : FeatureBase
{
  public override string Name => "Delivery Optimization Service";
  public override string Description =>
    "Helps to speed up the Windows Update process by sharing parts of downloaded updates with other PC's." +
    "\n\nThough, your system also serves as a channel for such P2P updates, which may lead to reduced bandwidth and network stability." +
    "\nIt may also cause you to use too much paid internet traffic.";

  public override bool Default => true;
  public override bool? Recommended => false;
  public override bool? Optimized => false;

  protected override List<SvcStateHelper> SvcStates { get; } = new()
  {
    new("DoSvc", ServiceStartMode.Disabled, ServiceStartMode.Automatic),
  };

  protected override List<RegValueHelper> RegValues { get; } = new()
  {
    new(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\DeliveryOptimization\Config", "DODownloadMode", 0, 1),
    new(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\DeliveryOptimization\Config", "DODownloadFromInternet", 0, 1),
  };

  public override bool? IsWanted() => !base.IsWanted();

  public override void Enable(bool Enable) => base.Enable(!Enable);
}
