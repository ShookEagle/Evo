namespace Evo.api.plugin.services;

public interface IAnnouncerService {
  void Announce(string admin, string local, params object[] args);
}