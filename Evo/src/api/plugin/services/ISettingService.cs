using Evo.plugin.models;

namespace Evo.api.plugin.services;

public interface ISettingService {
  IReadOnlyDictionary<string, Setting> All { get; }
  bool TryGet(string key, out Setting def);
  bool TryGetDefault(string key, out bool value);
}