using CounterStrikeSharp.API.Modules.Utils;

namespace Evo.api.plugin.services;

public interface IDynamicSpawnService {
  bool DynamicSpawns { get; }
  void Enable();
  void Disable();
  void Toggle();
  bool TryCreateSpawn(CsTeam team);
}