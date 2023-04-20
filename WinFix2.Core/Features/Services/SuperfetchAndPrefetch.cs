using System.ServiceProcess;
using WinFix2.Core.Helpers;

namespace WinFix2.Core.Features.Services;

public class SuperfetchAndPrefetch : FeatureBase
{
  public override string Name => "Superfetch and Prefetch";
  public override string Description =>
    "Prefetch groups application files together on the hard disk to reduce access time." +
    "\nThis is useful but it takes time for itself too." +
    "\n\nSuperfetch preloads often used applications into the memory." +
    "\nDisabling this can speed up a computer with low memory or a slower CPU," +
    "\nwhile its questionable if it's even needed on a super fast computer.";

  public override bool Default => true;
  public override bool? Recommended => false;
  public override bool? Optimized => false;

  protected override List<SvcStateHelper> SvcStates { get; } = new()
  {
    new("SysMain", ServiceStartMode.Disabled, ServiceStartMode.Automatic),
  };

  protected override List<RegValueHelper> RegValues { get; } = new()
  {
    new(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management\PrefetchParameters", "EnablePrefetcher", 0, 3),
    new(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management\PrefetchParameters", "EnableSuperfetch", 0, 3),
  };

  public override bool? IsWanted() => !base.IsWanted();

  public override void Enable(bool Enable) => base.Enable(!Enable);
}
