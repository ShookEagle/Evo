using System.Reflection;
using RMenu;
using RMenu.Enums;

namespace Evo.plugin.extensions;

public static class MenuExtensions {
  public static void SetRootCallback(this MenuBase menu,
    Action<MenuBase, MenuAction> cb) {
    var prop = typeof(MenuBase).GetProperty("Callback",
      BindingFlags.Instance | BindingFlags.NonPublic);
    if (prop is null)
      throw new InvalidOperationException("MenuBase.Callback not found.");
    prop.SetValue(menu, cb);
  }
}