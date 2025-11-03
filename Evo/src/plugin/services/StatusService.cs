using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using Evo.api.plugin;
using Evo.api.plugin.services;
using Evo.plugin.extensions;
using Evo.plugin.models;

namespace Evo.plugin.services;

public class StatusService(IEvo evo) : IStatusService {
  private readonly struct NowProvider {
    public static DateTime Utc => DateTime.UtcNow;
  }

  public bool Running { get; private set; }
  private readonly Dictionary<ulong, PlayerStat> stats = new();
  private DateTime? startUtc;
  private DateTime? stopUtc;
  private int onlineCount;
  private int peakPlayers;
  private TimeSpan Duration {
    get {
      if (startUtc is null) return TimeSpan.Zero;
      var end = stopUtc ?? NowProvider.Utc;
      return end - startUtc.Value;
    }
  }

  public void Start() {
    clear();

    Running  = true;
    startUtc = NowProvider.Utc;

    var players = Utilities.GetPlayers().Where(p => p.IsReal()).ToList();
    peakPlayers = players.Count;
    foreach (var player in players) {
      stats[player.SteamID] = new PlayerStat {
        Name = player.PlayerName, JoinedAtUtc = NowProvider.Utc,
      };
      onlineCount++;
    }
  }

  public void Stop() {
    Running     = false;
    stopUtc     = NowProvider.Utc;
    onlineCount = 0;

    foreach (var stat in stats.Select(kv => kv.Value)) {
      if (stat.JoinedAtUtc is null) continue;
      var delta = NowProvider.Utc - stat.JoinedAtUtc.Value;
      if (delta.Ticks > 0) stat.Accumulated += delta;
      stat.JoinedAtUtc = null;
    }
  }

  private void clear() {
    stats.Clear();
    startUtc    = null;
    stopUtc     = null;
    peakPlayers = 0;
    onlineCount = 0;
  }

  public void AddPlayer(CCSPlayerController player) {
    if (!stats.TryGetValue(player.SteamID, out var stat)) {
      stat                  = new PlayerStat { Name = player.PlayerName };
      stats[player.SteamID] = stat;
    }
    // ReSharper disable once RedundantCheckBeforeAssignment
    else if (stat.Name != player.PlayerName) stat.Name = player.PlayerName;

    if (!Running || stat.JoinedAtUtc is not null) return;
    stat.JoinedAtUtc = NowProvider.Utc;
    onlineCount++;
    if (onlineCount > peakPlayers) peakPlayers = onlineCount;
  }

  public void OnLeave(CCSPlayerController player) {
    if (!stats.TryGetValue(player.SteamID, out var stat)
      || stat.JoinedAtUtc is null)
      return;

    var delta = NowProvider.Utc - stat.JoinedAtUtc.Value;
    if (delta.Ticks > 0) stat.Accumulated += delta;

    stat.JoinedAtUtc = null;
    if (onlineCount > 0) onlineCount--;
  }

  public void PrintStatus(CCSPlayerController player) {
    var list =
      new List<(int Rank, ulong Id, string Name, uint Seconds)>(stats.Count);

    foreach (var (id, stat) in stats) {
      var secs = (uint)stat.Accumulated.TotalSeconds;
      if (Running && stat.JoinedAtUtc is not null) {
        var live                 = NowProvider.Utc - stat.JoinedAtUtc.Value;
        if (live.Ticks > 0) secs += (uint)live.TotalSeconds;
      }

      list.Add((0, id, stat.Name, secs));
    }

    list.Sort((a, b) => b.Seconds.CompareTo(a.Seconds));
    for (var i = 0; i < list.Count; ++i)
      list[i] = (i + 1, list[i].Id, list[i].Name, list[i].Seconds);

    player.PrintToConsole($"=== Event Status ===");
    player.PrintToConsole(
      $"Duration: {fmtTime(Duration)} | Peak: {peakPlayers} | Unique: {stats.Count}");
    foreach (var row in list)
      player.PrintToConsole(
        $"{row.Rank}. {row.Id} {row.Name} {fmtTime(row.Seconds)}");
    player.PrintToConsole("====================");
  }

  private static string fmtTime(TimeSpan t) {
    if (t.TotalHours >= 1) return $"{t.TotalHours:F2} hours";
    return t.TotalMinutes >= 1 ?
      $"{t.TotalMinutes:F2} minutes" :
      $"{t.TotalSeconds:F0} seconds";
  }

  private static string fmtTime(uint seconds) {
    return seconds switch {
      >= 3600 => $"{seconds / 3600f:F2} hours",
      >= 60   => $"{seconds / 60f:F2} minutes",
      _       => $"{seconds} seconds"
    };
  }
}