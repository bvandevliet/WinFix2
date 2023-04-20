using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WinFix2.Core.Extensions;

public static class ServiceStartModeExtensions
{
  public static string ConvertToSc(this ServiceStartMode startMode)
  {
    return startMode switch
    {
      ServiceStartMode.Boot => "boot",
      ServiceStartMode.System => "system",
      ServiceStartMode.Automatic => "auto",
      ServiceStartMode.Manual => "demand",
      ServiceStartMode.Disabled => "disabled",
      _ => throw new ArgumentOutOfRangeException(nameof(startMode)),
    };
  }

  public static int ConvertToReg(this ServiceStartMode startMode)
  {
    return startMode switch
    {
      ServiceStartMode.Boot => 0,
      ServiceStartMode.System => 1,
      ServiceStartMode.Automatic => 2,
      ServiceStartMode.Manual => 3,
      ServiceStartMode.Disabled => 4,
      _ => throw new ArgumentOutOfRangeException(nameof(startMode)),
    };
  }

  //public static string ConvertToPs(this ServiceStartMode startMode)
  //{
  //  return startMode switch
  //  {
  //    ServiceStartMode.Boot => "Automatic",
  //    ServiceStartMode.System => "Automatic",
  //    ServiceStartMode.Automatic => "Automatic", // "AutomaticDelayedStart",
  //    ServiceStartMode.Manual => "Manual",
  //    ServiceStartMode.Disabled => "Disabled",
  //    _ => throw new ArgumentOutOfRangeException(nameof(startMode)),
  //  };
  //}
}
