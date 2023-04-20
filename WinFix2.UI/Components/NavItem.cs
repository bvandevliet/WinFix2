using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WinFix2.UI.Components;

public class NavItem : ListBoxItem
{
  static NavItem()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(NavItem), new FrameworkPropertyMetadata(typeof(NavItem)));
  }

  /// <summary>
  /// The title to show in the navigation button.
  /// </summary>
  public string Title
  {
    get { return (string)GetValue(TitleProperty); }
    set { SetValue(TitleProperty, value); }
  }
  public static readonly DependencyProperty TitleProperty =
    DependencyProperty.Register("Title", typeof(string), typeof(NavItem), new PropertyMetadata(null));

  /// <summary>
  /// The icon to show in the navigation button.
  /// </summary>
  public Geometry Icon
  {
    get { return (Geometry)GetValue(IconProperty); }
    set { SetValue(IconProperty, value); }
  }
  public static readonly DependencyProperty IconProperty =
    DependencyProperty.Register("Icon", typeof(Geometry), typeof(NavItem), new PropertyMetadata(null));

  /// <summary>
  /// Reference to the static instance of a Singleton page. Use like `{x:Static pages:PageName.Instance}`
  /// </summary>
  public Page NavPage
  {
    get { return (Page)GetValue(NavPageProperty); }
    set { SetValue(NavPageProperty, value); }
  }
  public static readonly DependencyProperty NavPageProperty =
    DependencyProperty.Register("NavPage", typeof(Page), typeof(NavItem), new PropertyMetadata(null));
}
