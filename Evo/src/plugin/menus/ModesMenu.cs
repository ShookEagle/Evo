using Evo.api.plugin;
using Evo.plugin.extensions;
using Evo.plugin.menus.components;
using Evo.plugin.menus.theme;
using RMenu;

namespace Evo.plugin.menus;

public class ModesMenu : ListMenuBase {
  private readonly IEvo evo;

  public ModesMenu(IEvo evo) : base("Modes") {
    Options  = evo.GetModeService().All.Keys;
    this.evo = evo;
  }

  override protected void OnSelected(object? data) {
    if (data is not string modeAlias) {
      Player.PrintLocalizedChat(evo.GetBase().Localizer, "error_try_again",
        "Invalid Menu Data.");
      return;
    }

    if (!evo.GetModeService().TrySetMode(modeAlias)) {
      Player.PrintLocalizedChat(evo.GetBase().Localizer, "error_try_again",
        "An error occured when setting mode.");
    }
  }
}