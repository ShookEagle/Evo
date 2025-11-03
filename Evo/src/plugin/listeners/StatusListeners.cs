using CounterStrikeSharp.API.Core;
using Evo.api.plugin;
using Evo.api.plugin.services;
using Evo.plugin.extensions;

namespace Evo.plugin.listeners;

public class StatusListeners {
  private readonly IStatusService statusService;

  public StatusListeners(IEvo evo) {
    statusService = evo.GetStatusService();

    evo.GetBase()
     .RegisterEventHandler<EventPlayerConnectFull>(OnPlayerConnectFull);
    evo.GetBase()
     .RegisterEventHandler<EventPlayerDisconnect>(OnPlayerDisconnect);
  }

  private HookResult OnPlayerConnectFull(EventPlayerConnectFull @event,
    GameEventInfo info) {
    if (!statusService.Running) return HookResult.Continue;
    var player = @event.Userid;
    if (!player.IsReal(false) || player == null) return HookResult.Continue;
    
    statusService.AddPlayer(player);
    return HookResult.Continue;
  }
  
  private HookResult OnPlayerDisconnect(EventPlayerDisconnect @event,
    GameEventInfo info) {
    if (!statusService.Running) return HookResult.Continue;
    var player = @event.Userid;
    if (!player.IsReal(false) || player == null) return HookResult.Continue;
    
    statusService.OnLeave(player);
    return HookResult.Continue;
  }
}