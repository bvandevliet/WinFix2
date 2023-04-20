using System.ServiceProcess;
using WinFix2.Core.Helpers;

namespace WinFix2.Core.Features.Services;

public class WindowsSearch : FeatureBase
{
  public override string Name => "Windows Search";
  public override string Description =>
    "Disable indexing of files to improve overall performance." +
    "\nYou can still search for files and folders in the Windows Explorer, but it might take just a little more time.";

  public override bool Default => true;
  public override bool? Recommended => false;
  public override bool? Optimized => false;

  protected override List<SvcStateHelper> SvcStates { get; } = new()
  {
    new("WSearch", ServiceStartMode.Disabled, ServiceStartMode.Automatic),
  };

  protected override List<RegValueHelper> RegValues { get; } = new()
  {
    new(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows\Windows Search", "DisableWebSearch", 1, 0),
    new(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Search\Preferences", "WholeFileSystem", 0, 1),
    new(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\SearchSettings", "IsMSACloudSearchEnabled", 0, null),
    new(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\SearchSettings", "IsAADCloudSearchEnabled", 0, null),
    new(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\SearchSettings", "IsDeviceSearchHistoryEnabled", 0, null),
  };

  public override bool? IsWanted() => !base.IsWanted();

  public override void Enable(bool Enable) => base.Enable(!Enable);
}
