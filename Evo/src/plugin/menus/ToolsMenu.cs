using CounterStrikeSharp.API.Core;
using Evo.api.plugin;
using Evo.plugin.extensions;
using Evo.plugin.menus.models;
using Evo.plugin.menus.theme;
using RMenu;
using RMenu.Enums;

namespace Evo.plugin.menus;

public class ToolsMenu(IEvo evo) : EvoMenuBase("Tools") {
  override protected void Build() {
    var obValue = evo.GetSettingService().ObtrusiveSettings;
    var running = evo.GetStatusService().Running;
    
    // Toggle Status Button
    Items.Add(new MenuItem(MenuItemType.Button,
      new MenuValue("Event Status", Theme.TEXT_PRIMARY.ToMenuFormat()),
      tail: new MenuValue($" [{(running ? "✔" : "✘")}]",
        running ?
          Theme.ACCENT_GREEN.ToMenuFormat() :
          Theme.ACCENT_RED.ToMenuFormat())));
    
    // Print Status Button
    Items.Add(new MenuItem(MenuItemType.Button,
      new MenuValue("Print Status", Theme.TEXT_PRIMARY.ToMenuFormat())));
    
    // Spacer
    Items.Add(new MenuItem(MenuItemType.Spacer));
    
    //Obtrusive Settings Button
    Items.Add(new MenuItem(MenuItemType.Button,
      new MenuValue("Obtrusive Settings", Theme.TEXT_PRIMARY.ToMenuFormat()),
      tail: new MenuValue($" [{(obValue ? "✔" : "✘")}]",
        obValue ?
          Theme.ACCENT_GREEN.ToMenuFormat() :
          Theme.ACCENT_RED.ToMenuFormat())));
  }

  override protected void Callback(MenuBase menu, MenuAction action) {
    if (action != MenuAction.Select || menu.SelectedItem == null) return;

    switch (menu.SelectedItem.Index) {
      case 0:
        handleEventStatus(menu.Player);
        break;
      case 1:
        handlePrintStatus(menu.Player);
        break;
      case 3:
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

  private void handleObtrusiveSelection() {
    evo.GetSettingService().ObtrusiveSettings =
      !evo.GetSettingService().ObtrusiveSettings;
  }

  private void handleEventStatus(CCSPlayerController? player) {
    if (player == null) return;

    var toSet = !evo.GetStatusService().Running;
    if (toSet) {
      evo.GetStatusService().Start();
      evo.GetAnnouncer().Announce(player.PlayerName, "Started", "the Event");
      return;
    }

    evo.GetStatusService().Stop();
    evo.GetAnnouncer().Announce(player.PlayerName, "Stopped", "the Event");
  }

  private void handlePrintStatus(CCSPlayerController? player) {
    if (player == null) return;
    evo.GetStatusService().PrintStatus(player);
    player.PrintLocalizedChat(evo.GetBase().Localizer,
      "command_status_printed");
  }
}