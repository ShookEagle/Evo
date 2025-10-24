using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Capabilities;
using Evo.api.plugin;
using Evo.api.plugin.services;
using Evo.plugin.commands;
using Evo.plugin.services;
using MAULActainShared.plugin;

namespace Evo.plugin;

public class Evo : BasePlugin, IEvo {
  public override string ModuleName => "Evo";
  public override string ModuleVersion => "1.0.0";
  public override string ModuleAuthor => "ShookEagle";

  public override string ModuleDescription
    => "'Evo' or Events Organizer for the EdgeGamers Events Server";

  private static PluginCapability<IActain>? ActainCapability { get; } =
    new("maulactain:core");

  private readonly Dictionary<string, Command> commands = new();

  private IAnnouncerService? announcerService;
  private IModeService? modeService;
  private IMapService? mapService;
  private ISettingService? settingService;

#pragma warning disable CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
  public EvoConfig? Config { get; set; }
#pragma warning restore CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
  
  public void OnConfigParsed(EvoConfig? config) { Config = config; }

  public BasePlugin GetBase() { return this; }
  public IActain GetActain() { return ActainCapability!.Get()!; }
  public IAnnouncerService GetAnnouncer() { return announcerService!; }
  public IModeService GetModeService() { return modeService!; }
  public IMapService GetMapService() { return mapService!; }
  public ISettingService GetSettingService() { return settingService!; }

  public override void Load(bool hotReload) {
    announcerService = new AnnouncerService(this);
    modeService      = new ModeService(this);
    mapService       = new MapService(this);
    settingService   = new SettingService(this);

    loadCommands();
  }

  private void loadCommands() {
    commands.Add("css_ec", new EcCmd(this));

    foreach (var command in commands)
      AddCommand(command.Key,
        command.Value.Description ?? "No Description Provided",
        command.Value.OnCommand);
  }
}