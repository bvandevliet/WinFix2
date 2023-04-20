namespace WinFix2.Core.Helpers;

internal interface IStateHelper
{
  bool IsWanted();
  bool IsDefault();
  bool SetWanted();
  bool SetDefault();
}