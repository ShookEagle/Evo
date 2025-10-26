using Evo.api.plugin;
using Evo.api.plugin.services;
using Evo.plugin.models;

namespace Evo.plugin.services;

public class MapService : IMapService {
  private readonly Dictionary<string, MapGroup> groups;
  private string currentGroup;

  public MapService(IEvo evo) {
    var path = $"{evo.GetBase().ModulePath}/../{evo.Config.MapsJsonPath}";
    groups = JsonCfg.Load<Dictionary<string, MapGroup>>(path);
    currentGroup = "mg_active";
  }
  
  public Dictionary<string, string> GetRotation() {
    var g = groups[currentGroup];
    return g.Maps;
  }
  
  public void SetMapGroup(string groupKey) => currentGroup =  groupKey;
  
}