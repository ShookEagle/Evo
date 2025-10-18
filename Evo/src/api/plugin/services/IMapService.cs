using Evo.plugin.models;

namespace Evo.api.plugin.services;

public interface IMapService {
  IReadOnlyDictionary<string, MapGroup> Groups { get; }
  bool TryGetGroup(string key, out MapGroup group);
  IReadOnlyList<(string Map, string? WorkshopId)> GetRotation(string groupKey);
}