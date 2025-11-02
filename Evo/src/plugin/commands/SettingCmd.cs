using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using Evo.api.plugin;
using Evo.plugin.extensions;
using MAULActainShared.maul.enums;

namespace Evo.plugin.commands;

public class SettingCmd(IEvo evo) : Command(evo) {
  public override void OnCommand(CCSPlayerController? executor,
    CommandInfo info) {
    if (executor != null && executor.GetRank() < MaulPermission.Manager) {
      info.ReplyLocalized(Plugin.GetBase().Localizer, "no_permission",
        "Manager", "rank");
      return;
    }

    if (info.ArgCount is > 2 or < 1) {
      executor.PrintLocalizedChat(Plugin.GetBase().Localizer, "usage",
        "css_{setting} <true/false>");
      return;
    }
      
    var setting = info.ArgByIndex(0).Split("_").Last();
    var setter  = !Plugin.GetSettingService().TryGetBool(setting);

    if (info.ArgCount == 2) {
      var query = info.GetArg(1).ToLower();
      setter = query switch {
        "true" or "1" or "on"   => true,
        "false" or "0" or "off" => false,
        _                       => setter
      };
    }

    Plugin.GetSettingService().TrySetting(setting, setter);
  }
}