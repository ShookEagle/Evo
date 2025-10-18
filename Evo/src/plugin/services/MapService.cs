using Evo.api.plugin;
using Evo.api.plugin.services;
using Evo.plugin.models;

namespace Evo.plugin.services;

public class MapService : IMapService {
  private readonly Dictionary<string, MapGroup> groups;
  public IReadOnlyDictionary<string, MapGroup> Groups => groups;

  public MapService(IEvo evo) {
    var path = evo.Config.MapsJsonPath ?? "maps.json";
    groups = JsonCfg.Load<Dictionary<string, MapGroup>>(path);
  }

  public bool TryGetGroup(string key, out MapGroup group)
    => groups.TryGetValue(key, out group!);

  // Ordered by numeric keys "1","2","3",…
  public IReadOnlyList<(string Map, string? WorkshopId)> GetRotation(
    string groupKey) {
    var g = groups[groupKey];
    return g.Maps.OrderBy(kv => int.Parse(kv.Key))
     .Select(kv => (kv.Value.Name, kv.Value.Id))
     .ToList();
  }
}