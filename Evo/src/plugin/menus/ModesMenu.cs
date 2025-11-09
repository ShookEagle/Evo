using CounterStrikeSharp.API.Modules.Utils;
using Evo.api.plugin;
using Evo.plugin.menus.models;
using RMenu;

namespace Evo.plugin.menus;

public class ModesMenu : ListMenuBase {
  private readonly IEvo evo;

  public ModesMenu(IEvo evo) : base("Modes") {
    this.evo = evo;
    Options = evo.GetModeService()
     .All.ToDictionary(kvp => kvp.Value.Name, kvp => kvp.Key);
    CurrentValue = evo.GetModeService().CurrentMode;
  }

  override protected void OnSelected(object? data, string? name) {
    if (data is not string s) return;
    if (evo.GetModeService().TrySetMode(s))
      evo.GetAnnouncer()
       .Announce(Player.PlayerName, "announce_mode_change", name ?? "ERROR");
  }
}