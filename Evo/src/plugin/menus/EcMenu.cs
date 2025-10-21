using CounterStrikeSharp.API.Core;
using Evo.api.plugin;
using Evo.plugin.extensions;
using Evo.plugin.menus.components;
using Evo.plugin.menus.theme;
using RMenu;
using RMenu.Enums;

namespace Evo.plugin.menus;

public class EcMenu : MenuBase {
  public EcMenu(IEvo evo) : base(
    new Header("EC Menu"), new Footer(), Theme.MENU_OPTIONS) {
    Items.AddRange([
      new MenuItem(MenuItemType.Button, new MenuValue("Modes")),
      new MenuItem(MenuItemType.Button, new MenuValue("Maps")),
      new MenuItem(MenuItemType.Button, new MenuValue("Settings")),
      new MenuItem(MenuItemType.Button, new MenuValue("Tools")),
      new MenuItem(MenuItemType.Button,
        new MenuValue("Reset", Theme.ACCENT_RED.ToMenuFormat()))
    ]);
    
    this.SetRootCallback((menu, action) => {
      if (action != MenuAction.Select) return;
      if (menu.SelectedItem == null) return;
      switch (menu.SelectedItem.Index) {
        case 0:   //Modes
          Menu.Display(menu.Player, new EcMenu(evo), true);
          break;
        case 1:   //Maps
          Menu.Display(menu.Player, new EcMenu(evo), true);
          break;
        case 2:   //Settings
          Menu.Display(menu.Player, new EcMenu(evo), true);
          break;
        case 3:   //Tools
          Menu.Display(menu.Player, new EcMenu(evo), true);
          break;
        case 4:   //Reset
          Menu.Display(menu.Player, new EcMenu(evo), true);
          break;
        default:
          menu.Player.PrintLocalizedChat(evo.GetBase().Localizer,
            "error_try_again", "Invalid menu selection.");
          break;
      }
    });
  }
}