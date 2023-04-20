using System.ServiceProcess;
using WinFix2.Core.Helpers;

namespace WinFix2.Core.Features.Services;

public class BackupAndShadowCopy : FeatureBase
{
  public override string Name => "Backup and Shadow Copy";
  public override string Description =>
    "Provides Windows Backup and Restore capabilities." +
    "\n\nKeep in mind that System Restore does not backup your personal files, " +
    "\nemails, documents or pictures, but only system files and settings." +
    "\nDisabling System Restore improves performance and saves disk space.";

  public override bool Default => true;
  public override bool? Recommended => null;
  public override bool? Optimized => false;

  protected override List<SvcStateHelper> SvcStates { get; } = new()
  {
    new("SDRSVC",   ServiceStartMode.Disabled, ServiceStartMode.Manual),
    new("wbengine", ServiceStartMode.Disabled, ServiceStartMode.Manual),
    new("VSS",      ServiceStartMode.Disabled, ServiceStartMode.Manual),
    new("swprv",    ServiceStartMode.Disabled, ServiceStartMode.Manual),
  };

  public override bool? IsWanted() => !base.IsWanted();

  public override void Enable(bool Enable)
  {
    base.Enable(!Enable);

    if (!Enable)
    {
      try
      {
        Directory.Delete($@"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\Microsoft\Windows\FileHistory", true);
      }
      catch (Exception)
      {
      }
    }
  }
}
