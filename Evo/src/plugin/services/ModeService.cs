using System.Text.Json;
using Evo.api.plugin;
using Evo.api.plugin.services;
using Evo.plugin.models;

namespace Evo.plugin.services;

public class ModeService : IModeService {
  private readonly Dictionary<string, Mode> byId;
  private readonly Dictionary<string, string> aliasToId; // alias/tag -> modeId

  public IReadOnlyDictionary<string, Mode> All => byId;

  public ModeService(IEvo evo) {
    var path = evo.Config.ModesJsonPath ?? "modes.json";
    byId = JsonCfg.Load<Dictionary<string, Mode>>(path);
    aliasToId =
      new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    foreach (var (id, def) in byId) {
      aliasToId[id] = id;
      aliasToId.TryAdd(def.File, id);
      aliasToId.TryAdd(def.Name, id);
      foreach (var t in def.Tags) aliasToId.TryAdd(t, id);
    }
  }

  public bool TryGet(string id, out Mode def) => byId.TryGetValue(id, out def!);

  public bool TryResolve(string aliasOrId, out string modeId)
    => aliasToId.TryGetValue(aliasOrId, out modeId!);

  public string GetGroup(string modeId)
    => byId[modeId].Mapgroup; // throws if bad id
}