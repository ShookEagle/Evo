using CounterStrikeSharp.API.Core;

namespace Evo.api.plugin.services;

public interface IStatusService {
  bool Running { get; }
  void Start();
  void Stop();
  void AddPlayer(CCSPlayerController player);
  void OnLeave(CCSPlayerController player);
  void PrintStatus(CCSPlayerController player);
}