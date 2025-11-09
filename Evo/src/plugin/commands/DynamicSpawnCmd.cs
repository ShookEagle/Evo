using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Utils;
using Evo.api.plugin;
using Evo.plugin.extensions;
using MAULActainShared.maul.enums;

namespace Evo.plugin.commands;

public class DynamicSpawnCmd(IEvo plugin) : Command(plugin) {
  public override void OnCommand(CCSPlayerController? executor,
    CommandInfo info) {
    if (executor != null && executor.GetRank() < MaulPermission.Manager) {
      info.ReplyLocalized(Plugin.GetBase().Localizer, "no_permission",
        "Manager", "rank");
      return;
    }

    if (info.ArgCount is > 2 or < 1) {
      executor.PrintLocalizedChat(Plugin.GetBase().Localizer, "usage",
        "css_dynamicspawns <true/false>");
      return;
    }

    var setter = 0;
    if (info.ArgCount == 2) {
      var query = info.GetArg(1).ToLower();
      setter = query switch {
        "true" or "1" or "on"   => 1,
        "false" or "0" or "off" => -1,
        _                       => setter
      };
    }

    var toggleLabel = setter switch {
      1  => $"{ChatColors.Lime}Enabled",
      -1 => $"{ChatColors.LightRed}Disabled",
      0  => $"{ChatColors.Yellow}Toggled",
      _  => throw new ArgumentOutOfRangeException()
    };

    switch (setter) {
      case 1:
        Plugin.GetDynamicSpawnService().Enable();
        break;
      case -1:
        Plugin.GetDynamicSpawnService().Disable();
        break;
      case 0:
        Plugin.GetDynamicSpawnService().Toggle();
        break;
    }

    info.ReplyLocalized(Plugin.GetBase().Localizer,
      "command_dynamic_spawns_confirm", toggleLabel);
    Plugin.GetAnnouncer()
     .Announce(executor!.PlayerName, "announce_dynamic_spawns_change",
        toggleLabel);
  }
}