using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Modules.Timers;
using Evo.api.plugin;
using Evo.plugin.menus.models;

namespace Evo.plugin.menus;

public class MapsMenu : ListMenuBase {
  private readonly IEvo evo;

  public MapsMenu(IEvo evo) : base("Maps") {
    this.evo = evo;
    Options  = evo.GetMapService().GetRotation();
  }

  override protected void OnSelected(object? data, string? name) {
    if (data is not string s) return;
    evo.GetAnnouncer()
     .Announce(Player.PlayerName, "Changed the map to", name ?? "ERROR",
        "Changing in 3s", "yellow");
    evo.GetBase()
     .AddTimer(3f,
        () => {
          Server.ExecuteCommand((string)data == "" ?
            $"changelevel {name}" :
            $"host_workshop_map {data}");
        }, TimerFlags.STOP_ON_MAPCHANGE);
  }
}