using WinFix2.Core.Helpers;

namespace WinFix2.Core.Features.Performance;

public class EnhanceNTFS : FeatureBase
{
  public override string Name => "Enhance NTFS";
  public override string Description =>
    "Increase the Master File Table zone reservation by one step to increase file access performance over a long term." +
    "\n\nSince disk defragmenters can't defragment the MFT," +
    "\nWindows reserves a certain amount of extra space for it to grow, in an effort to reduce its eventual fragmentation." +
    "\nThe more fragmented the MFT gets, the more it will hamper overall disk performance.";

  public override bool Default => false;
  public override bool? Recommended => true;
  public override bool? Optimized => true;

  protected override List<RegValueHelper> RegValues { get; } = new()
  {
    new(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\FileSystem", "NtfsDisableLastAccessUpdate", 1, 0),
    new(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\FileSystem", "NtfsMftZoneReservation", 2, 1),
    new(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\FileSystem", "NtfsDisable8dot3NameCreation", 1, 0),
  };
}
