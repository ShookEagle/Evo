using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using Evo.api.plugin;
using Evo.plugin.extensions;
using Evo.plugin.menus;
using MAULActainShared.maul.enums;
using RMenu;

namespace Evo.plugin.commands;

public class EcCmd(IEvo evo) : Command(evo) {
  public override void OnCommand(CCSPlayerController? executor,
    CommandInfo info) {
    if (!executor.IsReal() || executor == null) return;

    if (executor.GetRank() < MaulPermission.Manager) {
      info.ReplyLocalized(Plugin.GetBase().Localizer, "no_permission",
        "Manager", "rank");
      return;
    }
    
    Menu.Display(executor, new EcMenu(Plugin));
  }
}