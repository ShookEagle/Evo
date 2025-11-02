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
}