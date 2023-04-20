using WinFix2.Core.Helpers;

namespace WinFix2.Core.Features.Performance;

public class DisableHibernate : FeatureBase
{
  public override string Name => "Disable hibernate";
  public override string Description =>
    "In order to resume your work after booting from hibernate, Windows needs to store its memory in a file on the hard disk (hiberfil.sys)." +
    "\n\nThis file has the same size as your installed memory. If you don't use hibernate, there might be some extra storage space to win.";

  public override bool Default => false;
  public override bool? Recommended => true;
  public override bool? Optimized => true;

  protected override List<RegValueHelper> RegValues => new()
  {
    new(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Power", "HibernateEnabled", 0, 1),
  };

  public override void Enable(bool Enable)
  {
    ProcessHelper.Run("powercfg.exe", $"-h {(Enable ? "off" : "on")}", out _); // !!

    base.Enable(Enable);
  }
}
