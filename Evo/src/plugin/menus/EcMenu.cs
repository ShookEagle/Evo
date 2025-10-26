using CounterStrikeSharp.API;
using Evo.api.plugin;
using Evo.plugin.menus.models;
using RMenu;
using RMenu.Enums;

namespace Evo.plugin.menus;

public class EcMenu(IEvo evo) : EvoMenuBase("EC Menu") {
  override protected void Build() {
    Items.AddRange([
      new MenuItem(MenuItemType.Button, new MenuValue("Modes")),
      new MenuItem(MenuItemType.Button, new MenuValue("Maps")),
      new MenuItem(MenuItemType.Button, new MenuValue("Settings")),
      new MenuItem(MenuItemType.Button, new MenuValue("Tools")),
      new MenuItem(MenuItemType.Button, new MenuValue("Reset")),
    ]);
  }

  override protected void Callback(MenuBase menu, MenuAction action) {
    if (action != MenuAction.Select || menu.SelectedItem == null) return;

    switch (menu.SelectedItem.Index) {
      case 0:
        new ModesMenu(evo).Show(Player, true);
        break;
      case 1: new MapsMenu(evo).Show(Player, true);
        break;
      /*case 2: new EcMenu(evo).Show(Player, true);
        break;
      case 3: new EcMenu(evo).Show(Player, true);
        break;
      case 4: new EcMenu(evo).Show(Player, true);
        break;*/
      default:
        evo.GetAnnouncer()
         .Announce(menu.Player.PlayerName, "", "Reset", "The Server",
            "lightred");
        break;
    }
  }
}