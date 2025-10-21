using CounterStrikeSharp.API.Core;
using Evo.api.plugin.services;
using Evo.plugin;
using MAULActainShared.plugin;

namespace Evo.api.plugin;

public interface IEvo : IPluginConfig<EvoConfig> {
  BasePlugin GetBase();
  IActain GetActain();
  IAnnouncerService GetAnnouncer();
  IModeService GetModeService();
  IMapService GetMapService();
  ISettingService GetSettingService();
}