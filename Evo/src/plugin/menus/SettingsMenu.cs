using System.Reflection.Metadata;
using Evo.api.plugin;
using Evo.plugin.menus.models;
using Evo.plugin.menus.theme;
using RMenu;
using RMenu.Enums;

namespace Evo.plugin.menus;

public class SettingsMenu : ListMenuBase {
  private readonly IEvo evo;

  public SettingsMenu(IEvo evo) : base("Modes") {
    this.evo = evo;
    Options = evo.GetSettingService()
     .All.ToDictionary(kvp => kvp.Value.Name, kvp => kvp.Key);
  }

  override protected void Build() {
    Items.Clear();
    foreach (var option in Options) {
      var value = evo.GetSettingService().TryGetBool(option.Value);

      Items.Add(new MenuItem(MenuItemType.Button,
        new MenuValue(option.Key, Theme.TEXT_PRIMARY.ToMenuFormat()),
        tail: new MenuValue($" [{(value ? "✔" : "✘")}]",
          value ?
            Theme.ACCENT_GREEN.ToMenuFormat() :
            Theme.ACCENT_RED.ToMenuFormat())));
    }
  }

  override protected void OnSelected(object? data, string? name) {
    if (data is not string s) return;
    var value = !evo.GetSettingService().TryGetBool(s);
    if (!evo.GetSettingService().TrySetting(s, value)) return;
    //evo.GetAnnouncer()
     //.Announce(Player.PlayerName, $"{(value ? "Enabled" : "Disabled")}",
        //name ?? "ERROR", actionColor: value ? "lime" : "lightred");
  }
}