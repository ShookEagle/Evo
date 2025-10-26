using Evo.api.plugin;
using Evo.plugin.menus.components;
using RMenu;

namespace Evo.plugin.menus;

public class EcMenu: EvoMenuBase {
  private readonly IEvo evo;

  public EcMenu(IEvo evo) : base("Ec Menu") {
    this.evo = evo;
  }
}