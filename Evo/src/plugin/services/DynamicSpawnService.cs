using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Utils;
using Evo.api.plugin;
using Evo.api.plugin.services;

namespace Evo.plugin.services;

public class DynamicSpawnService() : IDynamicSpawnService {
  private readonly Random rng = new();
  
  // ReSharper disable once InconsistentNaming
  private const string T_KEY = "info_player_terrorist";
  private const string CT_KEY = "info_player_counterterrorist";
  private static readonly Vector ZERO = new Vector(0f, 0f, 0f);

  public bool DynamicSpawns { get; private set; } = true;
  
  public void Enable() => DynamicSpawns = true;
  public void Disable() => DynamicSpawns = false;
  public void Toggle() => DynamicSpawns = !DynamicSpawns;

  public bool TryCreateSpawn(CsTeam team)
{
    var key = team == CsTeam.Terrorist ? T_KEY : CT_KEY;
    
    var spawns = Utilities.FindAllEntitiesByDesignerName<SpawnPoint>(key).ToArray();
    if (spawns.Length == 0) return false;
    
    var src = spawns[rng.Next(spawns.Length)];
    var origin = src.AbsOrigin;
    var angle  = src.AbsRotation;
    if (origin is null || angle is null) return false;
    
    SpawnPoint? entity = team == CsTeam.Terrorist
        ? Utilities.CreateEntityByName<CInfoPlayerTerrorist>(T_KEY)
        : Utilities.CreateEntityByName<CInfoPlayerCounterterrorist>(CT_KEY);

    if (entity is null) return false;
    
    entity.Teleport(origin, angle, ZERO);
    entity.DispatchSpawn();
    return true;
}
}