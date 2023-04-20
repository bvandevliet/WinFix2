using Microsoft.Win32;
using System.ServiceProcess;
using WinFix2.Core.Helpers;

namespace WinFix2.Core.Features.Services;

public class ErrorReporting : FeatureBase
{
  public override string Name => "Error Reporting";
  public override string Description =>
    "Allows errors to be reported when programs stop working or responding and allows existing solutions to be delivered." +
    "\nAlso allows logs to be generated for diagnostic and repair services." +
    "\n\nIf this service is stopped, error reporting might not work correctly " +
    "\nand results of diagnostic services and repairs might not be displayed.";

  public override bool Default => true;
  public override bool? Recommended => false;
  public override bool? Optimized => false;

  protected override List<SvcStateHelper> SvcStates { get; } = new()
  {
    new("WerSvc",        ServiceStartMode.Disabled, ServiceStartMode.Automatic),
    new("wercplsupport", ServiceStartMode.Disabled, ServiceStartMode.Automatic),
  };

  protected override List<RegValueHelper> RegValues { get; } = new()
  {
    new(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Windows Error Reporting", "Disabled", 1, 0),
  };

  public override bool? IsWanted() => !base.IsWanted();

  public override void Enable(bool Enable)
  {
    base.Enable(!Enable);

    // Always disable handwriting error reports.
    Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\HandwritingErrorReports", "PreventHandwritingErrorReports", 1);

    // Never send additional (potentially sensitive data) information in an error report (enhance privacy).
    Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Windows Error Reporting", "DontSendAdditionalData", 1);
    Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\Windows Error Reporting\Consent", "DefaultConsent", 3);
    Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Windows\Windows Error Reporting\Consent", "DefaultConsent", 3);
  }
}
