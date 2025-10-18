using Evo.api.plugin;
using Evo.plugin.models;

namespace Evo.plugin.services;

public class SettingService {
  private readonly Dictionary<string, Setting> byKey;
  public IReadOnlyDictionary<string, Setting> All => byKey;

  public SettingService(IEvo evo) {
    var path = evo.Config.SettingsJsonPath ?? "settings.json";
    byKey = JsonCfg.Load<Dictionary<string, Setting>>(path);
  }

  public bool TryGet(string key, out Setting def)
    => byKey.TryGetValue(key, out def!);

  public bool TryGetDefault(string key, out bool value) {
    if (byKey.TryGetValue(key, out var def)) {
      value = def.Default;
      return true;
    }

    value = false;
    return false;
  }
}