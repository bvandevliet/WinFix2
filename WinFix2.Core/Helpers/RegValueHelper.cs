using Microsoft.Win32;
using System.ComponentModel;

namespace WinFix2.Core.Helpers;

public class RegValueHelper : IStateHelper
{
  public string KeyName { get; }
  public string ValueName { get; }

  public object? WantedValue { get; }
  public object? DefaultValue { get; }

  public RegValueHelper(
    string keyName,
    string valueName,
    object? wantedValue,
    object? defaultValue)
  {
    KeyName = keyName;
    ValueName = valueName;

    WantedValue = wantedValue;
    DefaultValue = defaultValue;
  }

  private bool SetValue(object? value)
  {
    try
    {
      if (null == value)
      {
        using RegistryKey? key = KeyName.Split(@"\")[0] switch
        {
          "HKEY_CLASSES_ROOT" => Registry.ClassesRoot.OpenSubKey(KeyName, true),
          "HKEY_CURRENT_USER" => Registry.CurrentUser.OpenSubKey(KeyName, true),
          "HKEY_LOCAL_MACHINE" => Registry.LocalMachine.OpenSubKey(KeyName, true),
          "HKEY_USERS" => Registry.Users.OpenSubKey(KeyName, true),
          "HKEY_CURRENT_CONFIG" => Registry.CurrentConfig.OpenSubKey(KeyName, true),
          _ => throw new InvalidEnumArgumentException(nameof(KeyName)),
        };

        key?.DeleteValue(ValueName);
      }
      else
      {
        Registry.SetValue(KeyName, ValueName, value);
      }

      return true;
    }
    catch (Exception)
    {
    }

    return false;
  }

  public bool SetWanted() => SetValue(WantedValue);

  public bool SetDefault() => SetValue(DefaultValue);

  public bool IsWanted()
  {
    object? current = Registry.GetValue(KeyName, ValueName, null);

    return null == current ? null == WantedValue : current.Equals(WantedValue);
  }

  public bool IsDefault()
  {
    object? current = Registry.GetValue(KeyName, ValueName, null);

    return null == current ? null == DefaultValue : current.Equals(DefaultValue);
  }
}
