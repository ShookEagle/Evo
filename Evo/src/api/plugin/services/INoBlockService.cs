using CounterStrikeSharp.API.Core;

namespace Evo.api.plugin.services;

public interface INoBlockService {
  bool IsEnabled { get; }
  void Enable();
  void Disable();
  void Toggle();
  void ApplyNoBlock(CCSPlayerController player);
}