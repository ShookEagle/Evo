using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Utils;
using Evo.plugin.utils;
using MAULActainShared.maul.enums;
using Microsoft.Extensions.Localization;

namespace Evo.plugin.extensions;

public static class CommandExtensions {
  public static void ReplyLocalized(this CommandInfo cmd,
    IStringLocalizer localizer, string local, params object[] args) {
    if (local == "no_permission") { args[0] = ColorizeRank((string)args[0]); }

    string message = localizer[local, args];
    message = message.Replace("%prefix%", localizer["prefix"]);
    message = StringUtils.ReplaceChatColors(message);
    cmd.ReplyToCommand(message);
  }

  public static string ColorizeRank(string rank) {
    var color = rank switch {
      "=(e)="             => MaulPermission.E.GetChatColor(),
      "=(eG)="            => MaulPermission.EG.GetChatColor(),
      "=(eGO)="           => MaulPermission.EGO.GetChatColor(),
      "Advisor"           => MaulPermission.Advisor.GetChatColor(),
      "Manager"           => MaulPermission.Manager.GetChatColor(),
      "Senior Manager"    => MaulPermission.SeniorManager.GetChatColor(),
      "Community Manager" => MaulPermission.CommunityManager.GetChatColor(),
      "Director"          => MaulPermission.Director.GetChatColor(),
      "Executive"         => MaulPermission.Executive.GetChatColor(),
      "Root"              => MaulPermission.Root.GetChatColor(),
      _                   => ChatColors.Default,
    };

    return $"{color}{rank}";
  }

  public static void Reply(this CommandInfo cmd, string message,
    params object[] args) {
    message = string.Format(message, args);
    message = StringUtils.ReplaceChatColors(message);
    cmd.ReplyToCommand(message);
  }

}