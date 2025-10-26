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
        new MenuValue(option.Key, Theme.TEXT_PRIMARY.ToMenuFormat()),
        data: option.Value));
    }
  }

  override protected void Callback(MenuBase menu, MenuAction action) {
    if (action != MenuAction.Select || menu.SelectedItem is null) return;

    var item = menu.SelectedItem.Item;
    var data = item.Data ?? item.Head;
    var name = item.Head?.ToString();

    OnSelected(data, name ?? throw new InvalidOperationException());
  }

  abstract protected void OnSelected(object? data, string name);
}