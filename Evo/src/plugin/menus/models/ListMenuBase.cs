using CounterStrikeSharp.API;
using Evo.plugin.extensions;
using Evo.plugin.menus.theme;
using RMenu;
using RMenu.Enums;

namespace Evo.plugin.menus.models;

public abstract class ListMenuBase(string header, MenuOptions? options = null)
  : EvoMenuBase(header, options) {
  public new Dictionary<string, string> Options { get; set; } = [];

  override protected void Build() {
    foreach (var option in Options) {
      Items.Add(new MenuItem(MenuItemType.Button,
        new MenuValue(option.Key, Theme.TEXT_PRIMARY.ToMenuFormat())));
    }
  }

  override protected void Callback(MenuBase menu, MenuAction action) {
    if (action != MenuAction.Select || menu.SelectedItem is null) return;
    var selected = Options.ElementAt(menu.SelectedItem.Index);
    OnSelected(selected.Value, selected.Key ?? throw new InvalidOperationException());
    Menu.Close(Player);
    Show(Player, true);
  }

  abstract protected void OnSelected(object? data, string name);
}