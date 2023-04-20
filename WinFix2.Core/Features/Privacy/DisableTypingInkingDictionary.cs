using WinFix2.Core.Helpers;

namespace WinFix2.Core.Features.Privacy;

public class DisableTypingInkingDictionary : FeatureBase
{
  public override string Name => "Disable typing and inking dictionary";
  public override string Description =>
    "Your personal typing and inking dictionary will be cleared and disabled.";

  public override bool Default => false;
  public override bool? Recommended => true;
  public override bool? Optimized => true;

  protected override List<RegValueHelper> RegValues { get; } = new()
  {
    new(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Language", "Enabled", 0, 1),
    new(@"HKEY_CURRENT_USER\Software\Microsoft\Personalization\Settings", "AcceptedPrivacyPolicy", 0, 1),
    new(@"HKEY_CURRENT_USER\Software\Microsoft\InputPersonalization", "RestrictImplicitTextCollection", 1, 0),
    new(@"HKEY_CURRENT_USER\Software\Microsoft\InputPersonalization", "RestrictImplicitInkCollection", 1, 0),
    new(@"HKEY_CURRENT_USER\Software\Microsoft\InputPersonalization\TrainedDataStore", "HarvestContacts", 0, 1),
  };

  public override void Enable(bool Enable)
  {
    base.Enable(Enable);

    if (Enable)
    {
      try
      {
        Directory.Delete($@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\Microsoft\Spelling", true);
      }
      catch (Exception)
      {
      }
    }
  }
}
