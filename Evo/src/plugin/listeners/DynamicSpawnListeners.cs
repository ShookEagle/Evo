using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Utils;
using Evo.api.plugin;
using Evo.plugin.enums;
using Evo.plugin.extensions;

namespace Evo.plugin.listeners;

public class DynamicSpawnListeners {
  private readonly IEvo evo;

  public DynamicSpawnListeners(IEvo evo) {
    this.evo = evo;
    
    evo.GetBase()
     .RegisterEventHandler<EventJointeamFailed>(onJoinFailed, HookMode.Pre);
  }

  private HookResult onJoinFailed(EventJointeamFailed @event,
    GameEventInfo info) {
    var player = @event.Userid;
    if (player == null || !player.IsReal()) { return HookResult.Continue; }
    var failReason = (JoinTeamReason)@event.Reason;

    switch (failReason) {
      case JoinTeamReason.TEAMS_FULL:
        evo.GetDynamicSpawnService().TryCreateSpawn(CsTeam.Terrorist);
        evo.GetDynamicSpawnService().TryCreateSpawn(CsTeam.CounterTerrorist);
        break;
      case JoinTeamReason.TERRORIST_TEAM_FULL:
        evo.GetDynamicSpawnService().TryCreateSpawn(CsTeam.Terrorist);
        break;
      case JoinTeamReason.CT_TEAM_FULL:
        evo.GetDynamicSpawnService().TryCreateSpawn(CsTeam.CounterTerrorist);
        break;
      case JoinTeamReason.TEAM_LIMIT:
      case JoinTeamReason.CT_TEAM_LIMIT:
      default:
        break;
    }
    
    return HookResult.Continue;
  }
}