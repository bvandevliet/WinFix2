namespace WinFix2.Core.Helpers;

public abstract class FeatureBase
{
  public abstract string Name { get; }
  public abstract string Description { get; }

  public abstract bool Default { get; }
  public abstract bool? Recommended { get; }
  public abstract bool? Optimized { get; }

  protected virtual List<SvcStateHelper> SvcStates { get; } = new();
  protected virtual List<RegValueHelper> RegValues { get; } = new();

  public virtual bool? IsWanted()
  {
    int servicesWanted = SvcStates.Count(stat => stat.IsWanted());
    int settingsWanted = RegValues.Count(val => val.IsWanted());

    if (
      (SvcStates.Count > 0 && servicesWanted == 0) ||
      (RegValues.Count > 0 && settingsWanted == 0))
    {
      return false;
    }

    if (
      servicesWanted == SvcStates.Count &&
      settingsWanted == RegValues.Count)
    {
      return true;
    }

    return null;
  }

  public virtual void Enable(bool Enable)
  {
    SvcStates.ForEach(svc =>
    {
      if (Enable ? svc.SetWanted() : svc.SetDefault())
      {
      }
      else
      {
        Console.WriteLine($"Failed to change service start mode.\nService: {svc.SvcDisplayName}");
      }
    });

    RegValues.ForEach(reg =>
    {
      if (Enable ? reg.SetWanted() : reg.SetDefault())
      {
      }
      else
      {
        Console.WriteLine($"Failed to modify Windows Registry value.\nValue: {reg.KeyName}\\{reg.ValueName}");
      }
    });
  }
}
