using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using Evo.api.plugin;
using Evo.plugin.extensions;
using MAULActainShared.maul.enums;

namespace Evo.plugin.commands.status;

public class EStartCmd(IEvo evo) : Command(evo) {
  public override void OnCommand(CCSPlayerController? executor,
    CommandInfo info) {
    if (executor.IsReal(false) && executor != null
      && executor.GetRank() < MaulPermission.Manager) {
      info.ReplyLocalized(Plugin.GetBase().Localizer, "no_permission",
        "Manager", "rank");
      return;
    }

    if (executor == null) return;

    if (Plugin.GetStatusService().Running) {
      info.ReplyLocalized(Plugin.GetBase().Localizer,
        "command_status_err_event_started");
      return;
    }

    Plugin.GetStatusService().Start();
    info.ReplyLocalized(Plugin.GetBase().Localizer,
      "command_status_event_started");
    Plugin.GetAnnouncer().Announce(executor.PlayerName, "Started", "the Event");
  }
}