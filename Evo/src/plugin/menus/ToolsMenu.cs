using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Utils;
using Evo.api.plugin;
using Evo.plugin.extensions;
using Evo.plugin.menus.models;
using Evo.plugin.menus.theme;
using RMenu;
using RMenu.Enums;

namespace Evo.plugin.menus;

public class ToolsMenu(IEvo evo) : EvoMenuBase("Tools") {
  override protected void Build() {
    # region Event Status
    // 0 - Title
    Items.Add(new MenuItem(MenuItemType.Text,
      new MenuValue("--- Event Status ---", Theme.TEXT_DARK.ToMenuFormat())));

    // 1 - Toggle Status Button
    var running = evo.GetStatusService().Running;
    Items.Add(new MenuItem(MenuItemType.Button,
      new MenuValue("Event Status", Theme.TEXT_PRIMARY.ToMenuFormat()),
      tail: new MenuValue($" [{(running ? "✔" : "✘")}]",
        running ?
          Theme.ACCENT_GREEN.ToMenuFormat() :
          Theme.ACCENT_RED.ToMenuFormat())));

    // 2 - Print Status Button
    Items.Add(new MenuItem(MenuItemType.Button,
      new MenuValue("Print Status", Theme.TEXT_PRIMARY.ToMenuFormat())));

    # endregion

    // 3 - Spacer
    Items.Add(new MenuItem(MenuItemType.Spacer));

    # region Plugin Settings
    // 4 - Title
    Items.Add(new MenuItem(MenuItemType.Text,
      new MenuValue("--- Plugin Settings ---",
        Theme.TEXT_DARK.ToMenuFormat())));

    // 5 - No-Block
    var noBlock = evo.GetNoBlockService().IsEnabled;
    Items.Add(new MenuItem(MenuItemType.Button,
      new MenuValue("No-Block", Theme.TEXT_PRIMARY.ToMenuFormat()),
      tail: new MenuValue($" [{(noBlock ? "✔" : "✘")}]",
        noBlock ?
          Theme.ACCENT_GREEN.ToMenuFormat() :
          Theme.ACCENT_RED.ToMenuFormat())));
    
    // 6 - Dynamic Spawns
    var dSpawns = evo.GetDynamicSpawnService().DynamicSpawns;
    Items.Add(new MenuItem(MenuItemType.Button,
      new MenuValue("Dynamic Spawns", Theme.TEXT_PRIMARY.ToMenuFormat()),
      tail: new MenuValue($" [{(dSpawns ? "✔" : "✘")}]",
        dSpawns ?
          Theme.ACCENT_GREEN.ToMenuFormat() :
          Theme.ACCENT_RED.ToMenuFormat())));

    // 7 - Obtrusive Settings Button
    var obValue = evo.GetSettingService().ObtrusiveSettings;
    Items.Add(new MenuItem(MenuItemType.Button,
      new MenuValue("Obtrusive Settings", Theme.TEXT_PRIMARY.ToMenuFormat()),
      tail: new MenuValue($" [{(obValue ? "✔" : "✘")}]",
        obValue ?
          Theme.ACCENT_GREEN.ToMenuFormat() :
          Theme.ACCENT_RED.ToMenuFormat())));

    #endregion
  }

  override protected void Callback(MenuBase menu, MenuAction action) {
    if (action != MenuAction.Select || menu.SelectedItem == null) return;

    switch (menu.SelectedItem.Index) {
      case 1:
        handleEventStatus(menu.Player);
        break;
      case 2:
        handlePrintStatus(menu.Player);
        break;
      case 5:
        handleNoBlock(menu.Player);
        break;
      case 6:
        handleDynamicSpawns(menu.Player);
        break;
      case 7:
        handleObtrusiveSelection();
        break;
      default:
        menu.Player.PrintLocalizedChat(evo.GetBase().Localizer,
          "error_try_again", "Invalid Menu Operation");
        break;
    }

    Menu.Close(Player);
    Show(Player, true);
  }

  private void handleEventStatus(CCSPlayerController? player) {
    if (player == null) return;

    var toSet = !evo.GetStatusService().Running;
    if (toSet) {
      evo.GetStatusService().Start();
      evo.GetAnnouncer().Announce(player.PlayerName, "announce_event_started");
      return;
    }

    evo.GetStatusService().Stop();
    evo.GetAnnouncer().Announce(player.PlayerName, "announce_event_stopped");
  }

  private void handlePrintStatus(CCSPlayerController? player) {
    if (player == null) return;
    evo.GetStatusService().PrintStatus(player);
    player.PrintLocalizedChat(evo.GetBase().Localizer,
      "command_status_printed");
  }

  private void handleNoBlock(CCSPlayerController? player) {
    if (player == null) return;
    var toSet = !evo.GetNoBlockService().IsEnabled;
    if (toSet) { evo.GetNoBlockService().Enable(); } else {
      evo.GetNoBlockService().Disable();
    }

    evo.GetAnnouncer()
     .Announce(player.PlayerName, "announce_noblock_change",
        toSet ? $"{ChatColors.Lime}Enabled" : $"{ChatColors.LightRed}Disabled");
  }

  private void handleDynamicSpawns(CCSPlayerController? player) {
    if (player == null) return;
    var toSet = !evo.GetDynamicSpawnService().DynamicSpawns;
    if (toSet) { evo.GetDynamicSpawnService().Enable(); } else {
      evo.GetDynamicSpawnService().Disable();
    }

    evo.GetAnnouncer()
     .Announce(player.PlayerName, "announce_dynamic_spawns_change",
        toSet ? $"{ChatColors.Lime}Enabled" : $"{ChatColors.LightRed}Disabled");
  }

  private void handleObtrusiveSelection() {
    evo.GetSettingService().ObtrusiveSettings =
      !evo.GetSettingService().ObtrusiveSettings;
  }
}