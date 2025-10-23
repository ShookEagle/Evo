using Evo.api.plugin;
using Evo.plugin.menus.components;
using Evo.plugin.menus.theme;
using RMenu;

namespace Evo.plugin.menus;

public class ModesMenu : ListMenuBase {
  public ModesMenu(IEvo evo) : base("Modes") {
    Options = evo.GetModeService().All.Keys;
  }
}