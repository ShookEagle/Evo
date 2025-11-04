using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using Evo.api.plugin;
using Evo.plugin.menus.models;
using Evo.plugin.menus.theme;
using RMenu;
using RMenu.Enums;

namespace Evo.plugin.menus;

public class ResetMenu(IEvo evo) : EvoMenuBase("Reset Server?") {
  override protected void Build() {
    Items.AddRange([
      new MenuItem(MenuItemType.Text,
        new MenuValue(
          "Are you sure you would like to reset the server to it's default config and map?",
          Theme.ACCENT_RED.ToMenuFormat())),
      new MenuItem(MenuItemType.Spacer),
      new MenuItem(MenuItemType.Button,
        new MenuValue("Confirm", Theme.TEXT_PRIMARY.ToMenuFormat()))
    ]);
  }

  override protected void Callback(MenuBase menu, MenuAction action) {
    if (menu.SelectedItem == null) return;
    switch (menu.SelectedItem.Index) {
      case 2:
        resetServer(menu.Player);
        break;
    }
  }

  private void resetServer(CCSPlayerController player) {
    evo.GetModeService().TrySetMode("default");
    Server.ExecuteCommand("exec utils/unload_plugins.cfg");
    Server.ExecuteCommand("exec utils/server_default.cfg");

    evo.GetAnnouncer()
     .Announce(player.PlayerName, string.Empty, "Reset", "the server",
        "lightred");

    evo.GetBase()
     .AddTimer(3f, () => { Server.ExecuteCommand("changelevel de_dust2"); });
  }
}