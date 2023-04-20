using System.ServiceProcess;
using WinFix2.Core.Helpers;

namespace WinFix2.Core.Features.Privacy;

public class DisableTelemetry : FeatureBase
{
  public override string Name => "Disable Telemetry";
  public override string Description =>
    "Stop user tracking, collecting advertising info and other data and sending it to Microsoft.";

  public override bool Default => false;
  public override bool? Recommended => true;
  public override bool? Optimized => true;

  protected override List<SvcStateHelper> SvcStates { get; } = new()
  {
    new("DiagTrack",                                ServiceStartMode.Disabled, ServiceStartMode.Automatic),
    new("diagsvc",                                  ServiceStartMode.Disabled, ServiceStartMode.Manual),
    new("dmwappushsvc",                             ServiceStartMode.Disabled, ServiceStartMode.Automatic),
    new("dmwappushservice",                         ServiceStartMode.Disabled, ServiceStartMode.Automatic),
    new("diagnosticshub.standardcollector.service", ServiceStartMode.Disabled, ServiceStartMode.Automatic),
    new("DPS",                                      ServiceStartMode.Disabled, ServiceStartMode.Automatic),
    new("WdiServiceHost",                           ServiceStartMode.Disabled, ServiceStartMode.Manual),
    new("WdiSystemHost",                            ServiceStartMode.Disabled, ServiceStartMode.Manual),
    new("pla",                                      ServiceStartMode.Disabled, ServiceStartMode.Automatic),
  };

  protected override List<RegValueHelper> RegValues { get; } = new()
  {
    new(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\DataCollection", "AllowTelemetry", 0, 1),
    new(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\SQMClient\Windows", "CEIPEnable", 0, 1),
    new(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\AppCompat", "AITEnable", 0, 1),
    new(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\TabletPC", "PreventHandwritingDataSharing", 1, 0),
    new(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\WMI\Autologger\AutoLogger-Diagtrack-Listener", "Start", 0, 1),
    new(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Privacy", "TailoredExperiencesWithDiagnosticDataEnabled", 0, 1),
    new(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Privacy", "AllowTailoredExperiences", 0, 1),
    new(@"HKEY_CURRENT_USER\Software\Microsoft\Input\TIPC", "Enabled", 0, 1),
  };
}
