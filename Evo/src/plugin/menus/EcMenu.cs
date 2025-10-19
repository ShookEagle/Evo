using CounterStrikeSharp.API.Core;
using Evo.api.plugin;
using Evo.plugin.menus.components;
using Evo.plugin.menus.theme;
using RMenu;
using RMenu.Enums;

namespace Evo.plugin.menus;

public class EcMenu : MenuBase {
  public EcMenu(IEvo evo, CCSPlayerController player) :
    base(new Header("EC Menu"), new Footer(), Theme.MENU_OPTIONS) {
    Items.AddRange([
      new MenuItem(MenuItemType.Button, new MenuValue("Modes")),
      new MenuItem(MenuItemType.Button, new MenuValue("Maps")),
      new MenuItem(MenuItemType.Button, new MenuValue("Settings")),
      new MenuItem(MenuItemType.Button, new MenuValue("Tools")),
      new MenuItem(MenuItemType.Button,
        new MenuValue("Reset", Theme.ACCENT_RED.ToMenuFormat()))
    ]);
  }
}