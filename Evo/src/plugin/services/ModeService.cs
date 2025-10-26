using CounterStrikeSharp.API;
using Evo.api.plugin;
using Evo.api.plugin.services;
using Evo.plugin.models;

namespace Evo.plugin.services;

public class ModeService : IModeService {
  private readonly IEvo evo;

  private readonly Dictionary<string, Mode> byId;
  private readonly Dictionary<string, string> aliasToId; // alias/tag -> modeId

  public IReadOnlyDictionary<string, Mode> All => byId;

  public ModeService(IEvo evo) {
    this.evo = evo;

    var path = $"{evo.GetBase().ModulePath}/../{evo.Config.ModesJsonPath}";
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

  public bool TrySetMode(string alias) {
    if (!TryResolve(alias, out var mode)) return false;
    if (!TryGet(mode, out var def)) return false;

    Server.ExecuteCommand("exec \"utils/unload_plugins.cfg\"");

    if (!evo.GetMapService().TrySetMapGroup(def.Mapgroup)) return false;

    Server.ExecuteCommand(
      $"hostname \"=(eGO)= | EVENTS | {def.Name.ToUpper()} | EdgeGamers.com\"");
    Server.ExecuteCommand(
      $"sv_tags events, ego, {string.Join(", ", def.Tags.ToArray())}");

    Server.ExecuteCommand($"exec modes/{def.File}.cfg");
    Server.ExecuteCommand("mp_restartgame 1");

    return true;
  }
}