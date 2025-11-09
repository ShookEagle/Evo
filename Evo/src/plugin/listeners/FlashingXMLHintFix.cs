using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using Evo.api.plugin;

namespace Evo.plugin.listeners;

//Thank you https://github.com/M-archand/CS2FlashingHtmlHudFix
public class FlashingXmlHintFix {
  private CCSGameRules? gameRules;
  private readonly IEvo evo;

  public FlashingXmlHintFix(IEvo evo) {
    this.evo = evo;
    evo.GetBase().RegisterListener<Listeners.OnTick>(onTick);
    evo.GetBase().RegisterListener<Listeners.OnMapStart>(onMapStart);
  }

  private void onMapStart(string mapName) { gameRules = null; }

  private void initializeGameRules() {
    var gameRulesProxy = Utilities
     .FindAllEntitiesByDesignerName<CCSGameRulesProxy>("cs_gamerules")
     .FirstOrDefault();
    gameRules = gameRulesProxy?.GameRules;
  }

  private void onTick() {
    if (gameRules == null) { initializeGameRules(); } else {
      gameRules.GameRestart = gameRules.RestartRoundTime < Server.CurrentTime;
    }
  }
}