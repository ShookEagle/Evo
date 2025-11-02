using Evo.plugin.models;

namespace Evo.api.plugin.services;

public interface IModeService {
  IReadOnlyDictionary<string, Mode> All { get; }
  bool TryGet(string id, out Mode def);
  string CurrentMode { get; }
  bool TrySetMode(string alias);
}