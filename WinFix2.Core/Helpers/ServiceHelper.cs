using Microsoft.Win32;
using System.Diagnostics;
using System.Management;
using System.ServiceProcess;
using WinFix2.Core.Extensions;

namespace WinFix2.Core.Helpers;

public static class ServiceHelper
{
  public static void KillAssocProcesses(this ServiceController svcController)
  {
    int svcPID;

    try
    {
      svcPID = (int?)svcController.GetType().GetProperty("ServiceHandle")?.GetValue(svcController, Array.Empty<object>()) ?? 0;
    }
    catch (System.Reflection.TargetInvocationException)
    {
      return;
    }

    foreach (Process process in Process.GetProcesses().Where(p => p.Id == svcPID))
    {
      process.ForceKill();
    }
  }

  public static void Kill(this ServiceController svcController)
  {
    ServiceControllerStatus svcStatus;

    try
    {
      svcStatus = svcController.Status;
    }
    catch (InvalidOperationException)
    {
      return;
    }

    if (svcStatus != ServiceControllerStatus.Stopped)
    {
      try
      {
        svcController.Stop();
        //svcController.WaitForStatus(ServiceControllerStatus.Stopped);
      }
      catch (Exception)
      {
        //svcController.KillAssocProcesses();
      }
      //try
      //{
      //  svcController.Stop();
      //  //svcController.WaitForStatus(ServiceControllerStatus.Stopped);
      //}
      //catch (Exception Ex)
      //{
      //  Console.WriteLine("\n{0}\nService: {1}", Ex.ToString(), svcController.DisplayName);
      //}
    }
  }

  public static void Kill(string svcName)
  {
    using ServiceController svcController = new ServiceController(svcName);

    svcController.Kill();
  }

  public static bool IsEnabled(this ServiceController svcController)
  {
    return
      svcController.StartType != ServiceStartMode.Disabled ||
      4 != (int)(Registry.GetValue($@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\{svcController.ServiceName}", "Start", 4) ?? 4);
  }

  public static bool IsEnabled(string svcName)
  {
    using ServiceController svcController = new ServiceController(svcName);

    return svcController.IsEnabled();
  }

  public static bool ChangeStartMode(string svcName, ServiceStartMode startMode)
  {
    bool success = false;

    // Use the WMI method to change the start mode.
    using (ManagementObject managementObj = new ManagementObject("Win32_Service.Name='" + svcName + "'"))
    {
      success = 0 == (uint)managementObj.InvokeMethod("ChangeStartMode", new object[] { startMode.ToString() });
    }

    // Fallback: Use the sc.exe command to change the start mode.
    success = success || 0 == ProcessHelper.Run("sc.exe", $"config {svcName} start= {startMode.ConvertToSc()}", out _); // !!

    // Fallback: Modify the Windows Registry to change the start mode.
    if (!success)
    {
      try
      {
        using RegistryKey? key = Registry.LocalMachine.OpenSubKey($@"SYSTEM\CurrentControlSet\Services\{svcName}", true);

        key?.SetValue("Start", startMode.ConvertToReg(), RegistryValueKind.DWord);

        success = true;
      }
      catch (System.Security.SecurityException)
      {
      }
    }

    return success;
  }

  public static bool ChangeStartMode(this ServiceController svcController, ServiceStartMode startMode)
  {
    try
    {
      string svcName = svcController.ServiceName;
    }
    catch (InvalidOperationException)
    {
      return startMode == ServiceStartMode.Disabled;
    }

    return ChangeStartMode(svcController.ServiceName, startMode);
  }
}
