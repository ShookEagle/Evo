using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using Evo.api.plugin;

namespace Evo.plugin.commands;

public abstract class Command(IEvo plugin) {
  protected readonly IEvo Plugin = plugin;
  public string? Description => null;

  public abstract void OnCommand(CCSPlayerController? executor,
    CommandInfo info);
}