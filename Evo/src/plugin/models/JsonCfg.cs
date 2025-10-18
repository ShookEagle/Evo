using System.Text.Json;

namespace Evo.plugin.models;

static class JsonCfg {
  public static readonly JsonSerializerOptions OPT =
    new(JsonSerializerDefaults.Web) {
      AllowTrailingCommas = true, ReadCommentHandling = JsonCommentHandling.Skip
    };

  public static T Load<T>(string path)
    => JsonSerializer.Deserialize<T>(File.ReadAllText(path), OPT)!;
}