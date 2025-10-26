using CounterStrikeSharp.API.Core;
using Evo.plugin.menus.components;
using Evo.plugin.menus.theme;
using RMenu;
using RMenu.Enums;

namespace Evo.plugin.menus;

public abstract class EvoMenuBase(string header, MenuOptions? options = null)
  : MenuBase(new Header(header), new Footer(), options ?? Theme.MENU_OPTIONS) {
  public void Show(CCSPlayerController player, bool subMenu = false) {
    Menu.Display(player, this, subMenu, Callback);
  }
  virtual protected void Callback(MenuBase menu, MenuAction action) { }
}