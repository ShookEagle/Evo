using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using Evo.api.plugin;
using Evo.plugin.menus;

namespace Evo.plugin.commands;

public class EcCmd(IEvo evo) : Command(evo) {
  public override void OnCommand(CCSPlayerController? executor,
    CommandInfo info) {
    if (executor != null) new EcMenu(Plugin).Show(executor);
  }
}