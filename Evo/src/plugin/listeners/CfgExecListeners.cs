using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Events;
using Evo.api.plugin;

namespace Evo.plugin.listeners;

public class CfgExecListeners {
  private readonly IEvo evo;

  /// <summary>
  ///   Over Redundant Settings Force due to maps being incredibly
  ///   inconsistent with when they run their cfg's
  /// </summary>
  public CfgExecListeners(IEvo evo) {
    this.evo = evo;

    evo.GetBase().RegisterEventHandler<EventRoundPrestart>(forceSettings);
    evo.GetBase().RegisterEventHandler<EventRoundStart>(forceSettings);
    evo.GetBase().RegisterEventHandler<EventRoundPoststart>(forceSettings);
  }

  private HookResult forceSettings<T>(T @event, GameEventInfo info) {
    var settingService = evo.GetSettingService();
    if (!settingService.ObtrusiveSettings) return HookResult.Continue;
    
    var modeService = evo.GetModeService();
    modeService.TryGet(modeService.CurrentMode, out var def);

    Server.ExecuteCommand($"exec modes/{def.File}.cfg");
    foreach (var setting in settingService.All)
      settingService.TrySetting(setting.Key,
        settingService.TryGetBool(setting.Key));
    return HookResult.Continue;
  }
}