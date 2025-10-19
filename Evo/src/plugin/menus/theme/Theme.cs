using System.Drawing;
using RMenu;
using RMenu.Enums;

namespace Evo.plugin.menus.theme;

public static class Theme {
  public static readonly MenuOptions MENU_OPTIONS = new MenuOptions {
    DisplayItemsInHeader = true,
    BlockMovement        = true,
    HeaderFontSize       = MenuFontSize.M,
    ItemFontSize         = MenuFontSize.SM,
    FooterFontSize       = MenuFontSize.S,
    
    // ReSharper disable once StaticMemberInitializerReferesToMemberBelow
    Cursor   = CURSOR!,
    // ReSharper disable once StaticMemberInitializerReferesToMemberBelow
    Selector = SELECTOR!
  };

  public static readonly Color
    TEXT_PRIMARY = Color.FromArgb(255, 255, 255); // white

  public static readonly Color
    PLACEHOLDER_TEXT = Color.FromArgb(136, 136, 136); // #888

  public static readonly Color TEXT_DARK = Color.FromArgb(68, 68, 68); // #444

  public static readonly Color
    PRIMARY_BLUE = Color.FromArgb(58, 110, 165); // #3a6ea5

  public static readonly Color
    ACCENT_BLUE = Color.FromArgb(93, 157, 253); // #5d9dfd

  public static readonly Color
    ACCENT_YELLOW = Color.FromArgb(244, 197, 66); // #f4c542

  public static readonly Color
    ACCENT_GREEN = Color.FromArgb(59, 165, 93); // #3ba55d

  public static readonly Color
    ACCENT_RED = Color.FromArgb(255, 102, 102); // #ff6666

  public static readonly Color
    ACCENT_DARK_RED = Color.FromArgb(255, 51, 51); // #ff3333

  public static readonly MenuFormat HEADER_FORMAT =
    new MenuFormat(PRIMARY_BLUE, MenuStyle.Bold, false);

  private static readonly MenuObject[] CURSOR = [
    new("►", Theme.PRIMARY_BLUE.ToMenuFormat()),
    new("◄", Theme.PRIMARY_BLUE.ToMenuFormat())
  ];

  private static readonly MenuObject[] SELECTOR = [
    new("[ ", Theme.PRIMARY_BLUE.ToMenuFormat()),
    new(" ]", Theme.PRIMARY_BLUE.ToMenuFormat())
  ];
}

public static class ThemeExtensions {
  public static MenuFormat ToMenuFormat(this Color color) {
    return new MenuFormat(color);
  }
}