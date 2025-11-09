using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using Evo.api.plugin;
using Evo.api.plugin.services;

namespace Evo.plugin.listeners;

public class NoBlockListener {
  private readonly INoBlockService noBlock;

  public NoBlockListener(IEvo evo) {
    noBlock = evo.GetNoBlockService();
    evo.GetBase().RegisterListener<Listeners.OnTick>(onTick);
  }

  private void onTick() {
    if (!noBlock.IsEnabled) return;
    
    foreach (var player in Utilities.GetPlayers()
     .Where(player => player.IsValid)) { noBlock.ApplyNoBlock(player); }
  }
}