using System.IO;
using System.Text;

namespace WinFix2.UI.Helpers;

internal class EventWriter : TextWriter
{
  public event EventHandler<string>? OnWrite;

  public override void Write(char value) =>
    OnWrite?.Invoke(this, value.ToString());

  public override void Write(string? value) =>
    OnWrite?.Invoke(this, value ?? "");

  public override Encoding Encoding => Encoding.ASCII;
}
