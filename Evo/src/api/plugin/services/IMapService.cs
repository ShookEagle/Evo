using Evo.plugin.models;

namespace Evo.api.plugin.services;

public interface IMapService {
  Dictionary<string, string> GetRotation();
  void SetMapGroup(string groupKey);
}