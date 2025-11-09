using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Utils;
using Evo.api.plugin;
using Evo.plugin.extensions;
using MAULActainShared.maul.enums;

namespace Evo.plugin.commands;

public class NoBlockCmd(IEvo plugin) : Command(plugin) {
  public override void OnCommand(CCSPlayerController? executor,
    CommandInfo info) {
    if (executor != null && executor.GetRank() < MaulPermission.Manager) {
      info.ReplyLocalized(Plugin.GetBase().Localizer, "no_permission",
        "Manager", "rank");
      return;
    }

    if (info.ArgCount is > 2 or < 1) {
      executor.PrintLocalizedChat(Plugin.GetBase().Localizer, "usage",
        "css_noblock <true/false>");
      return;
    }
    
    var setter  = 0;
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
      _  => throw new ArgumentOutOfRangeException(nameof(setter))
    };

    switch (setter) {
      case 1:
        Plugin.GetNoBlockService().Enable();
        break;
      case -1:
        Plugin.GetNoBlockService().Disable();
        break;
      case 0:
        Plugin.GetNoBlockService().Toggle();
        break;
    }

    info.ReplyLocalized(Plugin.GetBase().Localizer, "command_noblock_confirm",
      toggleLabel);
    Plugin.GetAnnouncer()
     .Announce(executor!.PlayerName, "announce_noblock_change", toggleLabel);
  }
}