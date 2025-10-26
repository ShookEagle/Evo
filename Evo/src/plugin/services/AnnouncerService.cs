using CounterStrikeSharp.API;
using Evo.api.plugin;
using Evo.api.plugin.services;
using Evo.plugin.extensions;
using MAULActainShared.maul.enums;

namespace Evo.plugin.services;

public class AnnouncerService(IEvo evo) : IAnnouncerService {
  public void Announce(string admin, string action, string value,
    string suffix = "", string valueColor = "bluegrey") {
    foreach (var player in
      Utilities.GetPlayers().Where(player => player.IsReal())) {
      player.PrintLocalizedChat(evo.GetBase().Localizer, "admin_action_format",
        player.GetRank() >= MaulPermission.EG ? admin : "EC", action,
        "{" + valueColor + "}", value, suffix);
    }
  }
}