using CounterStrikeSharp.API.Modules.Utils;

namespace Evo.plugin.utils;

internal class StringUtils {
  private static readonly List<string> STRINGS_TO_REMOVE = [];

  static StringUtils() {
    ChatColorUtils.ALL_COLORS.ToList()
     .ForEach(c => STRINGS_TO_REMOVE.Add(c.ToString()));
    typeof(ChatColors).GetFields()
     .Select(f => f.Name)
     .ToList()
     .ForEach(c => STRINGS_TO_REMOVE.Add($"{{{c}}}"));
  }

  public static string ReplaceChatColors(string message) {
    if (!message.Contains('{')) return message;
    var modifiedValue = message;
    foreach (var field in typeof(ChatColors).GetFields()) {
      var pattern = $"{{{field.Name}}}";
      if (message.Contains(pattern, StringComparison.OrdinalIgnoreCase))
        modifiedValue = modifiedValue.Replace(pattern,
          field.GetValue(null)!.ToString(), StringComparison.OrdinalIgnoreCase);
    }

    return modifiedValue;
  }

  private static string removeStrings(string message,
    List<string> stringsToRemove) {
    return stringsToRemove.Aggregate(message,
      (current, s)
        => current.Replace(s, string.Empty,
          StringComparison.OrdinalIgnoreCase));
  }

  public static string StripChatColors(string message) {
    return removeStrings(message, STRINGS_TO_REMOVE);
  }
}