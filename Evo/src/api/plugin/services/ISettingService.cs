using Evo.plugin.models;

namespace Evo.api.plugin.services;

public interface ISettingService {
  IReadOnlyDictionary<string, Setting> All { get; }
  bool ObtrusiveSettings { get; set; }
  bool TrySetting(string key, bool value);
  bool TryGetBool(string key);
}