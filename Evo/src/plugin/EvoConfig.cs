using System.Text.Json.Serialization;
using CounterStrikeSharp.API.Core;

namespace Evo.plugin;

public class EvoConfig : BasePluginConfig {
  [JsonPropertyName("MODES_JSON_PATH")]
  public string? ModesJsonPath { get; set; }

  [JsonPropertyName("MAPS_JSON_PATH")]
  public string? MapsJsonPath { get; set; }

  [JsonPropertyName("SETTINGS_JSON_PATH")]
  public string? SettingsJsonPath { get; set; }
}