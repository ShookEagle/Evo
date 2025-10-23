using Evo.plugin.models;

namespace Evo.api.plugin.services;

public interface IModeService {
  IReadOnlyDictionary<string, Mode> All { get; }
  bool TrySetMode(string alias);
}