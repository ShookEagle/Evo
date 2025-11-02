using CounterStrikeSharp.API;
using Evo.api.plugin;
using Evo.api.plugin.services;
using Evo.plugin.models;

namespace Evo.plugin.services;

public class SettingService : ISettingService {
  private readonly Dictionary<string, Setting> byKey;
  private readonly Dictionary<string, bool> currentSettings = new();
  public bool ObtrusiveSettings { get; set; }
  public IReadOnlyDictionary<string, Setting> All => byKey;


  public SettingService(IEvo evo) {
    var path = $"{evo.GetBase().ModulePath}/../{evo.Config.SettingsJsonPath}";
    byKey = JsonCfg.Load<Dictionary<string, Setting>>(path);
    foreach (var kv in byKey) { currentSettings[kv.Key] = kv.Value.Default; }

    ObtrusiveSettings = true;
  }

  public bool TrySetting(string key, bool value) {
    if (!byKey.TryGetValue(key, out var def)) return false;

    Server.ExecuteCommand(
      $"exec settings/{def.Stem}_{(value ? "on" : "off")}.cfg");
    currentSettings[key] = value;

    return true;
  }

  public bool TryGetBool(string key) {
    return currentSettings.TryGetValue(key, out var value) && value;
  }
}