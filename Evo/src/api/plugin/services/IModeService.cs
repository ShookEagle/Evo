using Evo.plugin.models;

namespace Evo.api.plugin.services;

public interface IModeService {
  IReadOnlyDictionary<string, Mode> All { get; }
  string CurrentMode { get; }
  bool TrySetMode(string alias);
}