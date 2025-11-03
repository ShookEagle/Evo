using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using Evo.api.plugin;
using Evo.plugin.extensions;
using Evo.plugin.menus;
using MAULActainShared.maul.enums;

namespace Evo.plugin.commands;

public class EcCmd(IEvo evo) : Command(evo) {
  public override void OnCommand(CCSPlayerController? executor,
    CommandInfo info) {
    if (executor != null && executor.GetRank() < MaulPermission.Manager) {
      info.ReplyLocalized(Plugin.GetBase().Localizer, "no_permission",
        "Manager", "rank");
      return;
    }

    if (executor == null) return;

    new EcMenu(Plugin).Show(executor);
  }
}