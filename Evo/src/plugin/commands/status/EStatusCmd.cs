using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using Evo.api.plugin;
using Evo.plugin.extensions;
using MAULActainShared.maul.enums;

namespace Evo.plugin.commands.status;

public class EStatusCmd(IEvo plugin) : Command(plugin) {
  public override void OnCommand(CCSPlayerController? executor,
    CommandInfo info) {
    if (executor != null && executor.GetRank() < MaulPermission.Manager) {
      info.ReplyLocalized(Plugin.GetBase().Localizer, "no_permission",
        "Manager", "rank");
      return;
    }

    if (executor == null) return;

    Plugin.GetStatusService().PrintStatus(executor);
    info.ReplyLocalized(Plugin.GetBase().Localizer, "command_status_printed");
  }
}