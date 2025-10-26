using Evo.plugin.menus.theme;
using RMenu;

namespace Evo.plugin.menus.components;

public class Footer : List<MenuObject> {
  public Footer() {
    Add(new MenuObject("Scroll- ", Theme.PLACEHOLDER_TEXT.ToMenuFormat()));
    Add(new MenuObject("WASD ", Theme.ACCENT_BLUE.ToMenuFormat()));
    Add(new MenuObject("| Select- ", Theme.PLACEHOLDER_TEXT.ToMenuFormat()));
    Add(new MenuObject("Jump ", Theme.ACCENT_GREEN.ToMenuFormat()));
    Add(new MenuObject("| Back- ", Theme.PLACEHOLDER_TEXT.ToMenuFormat()));
    Add(new MenuObject("Duck ", Theme.ACCENT_YELLOW.ToMenuFormat()));
    Add(new MenuObject("| Exit- ", Theme.PLACEHOLDER_TEXT.ToMenuFormat()));
    Add(new MenuObject("Tab ", Theme.ACCENT_DARK_RED.ToMenuFormat()));
  }
}