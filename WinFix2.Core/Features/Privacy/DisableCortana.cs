using Microsoft.Win32;
using WinFix2.Core.Helpers;

namespace WinFix2.Core.Features.Privacy;

public class DisableCortana : FeatureBase
{
  public override string Name => "Disable Cortana";
  public override string Description =>
    "Disable Cortana and improve your privacy.";

  public override bool Default => false;
  public override bool? Recommended => true;
  public override bool? Optimized => true;

  private readonly string cortanaPath = @"C:\Windows\SystemApps\Microsoft.Windows.Cortana_cw5n1h2txyewy";

  protected override List<RegValueHelper> RegValues { get; } = new()
  {
    new(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Search", "AllowCortana", 0, 1),
    new(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Cortana", "IsAvailable", 0, 1),
    new(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ShowCortanaButton", 0, 1),
  };

  public override bool? IsWanted() =>
    true == base.IsWanted() && !Directory.Exists(this.cortanaPath);

  public override void Enable(bool Enable)
  {
    base.Enable(Enable);

    return; // !!

    if (Enable)
    {
      // <= 1909
      ProcessHelper.RunPS("Get-AppxPackage -AllUsers Microsoft.Windows.Cortana | Remove-AppxPackage");

      // >= 2004
      ProcessHelper.RunPS("Get-AppxPackage -AllUsers Microsoft.549981C3F5F10 | Remove-AppxPackage");

      int attempts = 20;

      while (Directory.Exists(this.cortanaPath) && attempts > 0)
      {
        ProcessHelper.ForceKill("SearchUI"); // SearchApp.exe !!

        ProcessHelper.Run("cmd.exe", $"/c takeown /f \"{this.cortanaPath}\" /d Y && icacls \"{this.cortanaPath}\" /grant administrators:F /c", out _); // !!

        try
        {
          Directory.Move(this.cortanaPath, $@"{this.cortanaPath}.bak");
        }
        catch (Exception)
        {
        }

        attempts--;
      }

      if (Directory.Exists(this.cortanaPath))
      {
        Console.WriteLine("\nFailed to properly disable \"Cortana\"!");
      }
    }

    else // if !Enable
    {
      int WinVer = (int)(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", 2004) ?? 2004);

      try
      {
        if (WinVer <= 1909)
        {
          ProcessHelper.RunPS("Get-AppxPackage -AllUsers Microsoft.Windows.Cortana | Foreach {Add-AppxPackage -DisableDevelopmentMode -Register \"$($_.InstallLocation)\\AppXManifest.xml\"}");
        }
        else // WinVer >= 2004
        {
          ProcessHelper.RunPS("Get-AppxPackage -AllUsers Microsoft.549981C3F5F10 | Foreach {Add-AppxPackage -DisableDevelopmentMode -Register \"$($_.InstallLocation)\\AppXManifest.xml\"}");
        }
      }
      catch (Exception)
      {
      }

      try
      {
        Directory.Move($@"{this.cortanaPath}.bak", this.cortanaPath);
      }
      catch (Exception)
      {
        Console.WriteLine("\nFailed to properly re-enable \"Cortana\"!");
      }
    }
  }
}
