namespace Evo.plugin.models;

public sealed class PlayerStat {
  public string Name = "";
  public TimeSpan Accumulated = TimeSpan.Zero;
  public DateTime? JoinedAtUtc = null; // null = currently not in server
}