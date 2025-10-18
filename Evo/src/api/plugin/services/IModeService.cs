using Evo.plugin.models;

namespace Evo.api.plugin.services;

public interface IModeService {
  IReadOnlyDictionary<string, Mode> All { get; }
  bool TryGet(string id, out Mode def);
  bool TryResolve(string aliasOrId, out string modeId);
  string GetGroup(string modeId);
}