using CounterStrikeSharp.API;
using Evo.api.plugin;
using Evo.api.plugin.services;
using Evo.plugin.extensions;
using MAULActainShared.maul.enums;

namespace Evo.plugin.services;

public class AnnouncerService(IEvo evo) : IAnnouncerService {

  public void Announce(string admin, string local, params object[] args) {
    var message = evo.GetBase().Localizer[local, args];
    foreach (var player in Utilities.GetPlayers()) {
      player.PrintLocalizedChat(evo.GetBase().Localizer, "announcement_base",
        player.GetRank() >= MaulPermission.EG ? admin : "EC", message);
    }
  }
}