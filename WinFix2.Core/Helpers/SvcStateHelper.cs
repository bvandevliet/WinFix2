using System.ServiceProcess;

namespace WinFix2.Core.Helpers;

public class SvcStateHelper : IStateHelper
{
  public string SvcName { get; }
  public string SvcDisplayName { get; }

  public ServiceStartMode WantedStartMode { get; }
  public ServiceStartMode DefaultStartMode { get; }

  public SvcStateHelper(
    string svcName,
    ServiceStartMode wantedStartMode,
    ServiceStartMode defaultStartMode)
  {
    SvcName = svcName;

    try
    {
      SvcDisplayName = new ServiceController(svcName).DisplayName;
    }
    catch (Exception)
    {
      SvcDisplayName = svcName;
    }

    WantedStartMode = wantedStartMode;
    DefaultStartMode = defaultStartMode;
  }

  public bool IsWanted()
  {
    ServiceController svcController = new(SvcName);

    try
    {
      return svcController.StartType == WantedStartMode;
    }
    catch (InvalidOperationException)
    {
      return WantedStartMode == ServiceStartMode.Disabled;
    }
  }

  public bool IsDefault() => throw new NotImplementedException();

  public bool SetWanted()
  {
    ServiceController svcController = new(SvcName);

    if (WantedStartMode == ServiceStartMode.Disabled)
    {
      svcController.Kill();
    }

    return svcController.ChangeStartMode(WantedStartMode);
  }

  public bool SetDefault()
  {
    ServiceController svcController = new(SvcName);

    return svcController.ChangeStartMode(DefaultStartMode);
  }
}
