using System.Diagnostics;
using System.Management.Automation;

namespace WinFix2.Core.Helpers;

internal static class ProcessHelper
{
  public static int Run(string fileName, string arguments, out string output)
  {
    using Process? process = Process.Start(new ProcessStartInfo(fileName, arguments)
    {
      CreateNoWindow = true,
      UseShellExecute = false,
      Verb = "runas",
      RedirectStandardOutput = true,
    });

    process?.WaitForExit();

    output = process?.StandardOutput.ReadToEnd() ?? "";

    return process?.ExitCode ?? 1;
  }

  public static void RunPS(string command)
  {
    return; // !!

    using PowerShell ps = PowerShell.Create();

    ps.AddScript(command).Invoke();
  }

  public static void ForceKill(this Process process)
  {
    try
    {
      process.Kill();
    }
    catch (Exception)
    {
      Run("taskkill.exe", $"/f /t /im {process.ProcessName}.exe", out _); // !!
    }
    finally
    {
      process.Close();
    }
  }

  public static void ForceKill(string processName)
  {
    Process[] processes = Process.GetProcessesByName(processName);

    foreach (Process process in processes)
    {
      process.ForceKill();
    }
  }
}
