using CounterStrikeSharp.API;
using Evo.plugin.extensions;
using Evo.plugin.menus.theme;
using RMenu;
using RMenu.Enums;

namespace Evo.plugin.menus.models;

public abstract class ListMenuBase(string header, MenuOptions? options = null)
  : EvoMenuBase(header, options) {
  new protected Dictionary<string, string> Options { get; init; } = [];
  public string? CurrentValue;

  override protected void Build() {
    foreach (var option in Options) {
      Items.Add(new MenuItem(MenuItemType.Button,
        new MenuValue(option.Key,
          option.Value != CurrentValue ?
            Theme.TEXT_PRIMARY.ToMenuFormat() :
            Theme.ACCENT_GREEN.ToMenuFormat())));
    }
  }

  override protected void Callback(MenuBase menu, MenuAction action) {
    if (action != MenuAction.Select || menu.SelectedItem is null) return;
    var selected = Options.ElementAt(menu.SelectedItem.Index);
    OnSelected(selected.Value,
      selected.Key ?? throw new InvalidOperationException());
    Menu.Close(Player);
    Show(Player, true);
  }

  abstract protected void OnSelected(object? data, string name);
}