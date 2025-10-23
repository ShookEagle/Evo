using Evo.api.plugin;
using Evo.plugin.extensions;
using Evo.plugin.menus.components;
using Evo.plugin.menus.theme;
using RMenu;
using RMenu.Enums;

namespace Evo.plugin.menus;

public abstract class ListMenuBase : MenuBase {
  public new IEnumerable<string> Options { get; set; } = [];
  public ListMenuBase(string headerText) : base(
    new Header(headerText), new Footer(), Theme.MENU_OPTIONS) {
    foreach (var option in Options) {
      Items.Add(new MenuItem(MenuItemType.Button,
        new MenuValue(option, Theme.TEXT_PRIMARY.ToMenuFormat(),
          data: option)));
    }
    
    this.SetRootCallback(handleSelect);
  }
  
  private void handleSelect(MenuBase menu, MenuAction action)
  {
    if (action != MenuAction.Select || menu.SelectedItem is null) return;

    var idx  = menu.SelectedItem.Index;
    var item = menu.SelectedItem.Item;

    // Prefer MenuItem.Data; fall back to selected value’s data/text
    var data  = item.Data ?? item.SelectedValue?.Value?.Data ?? item.SelectedValue?.Value?.Text;

    OnSelected(idx, data, item, menu);
  }

  /// <summary>
  /// Child overrides this to implement per-selection logic.
  /// </summary>
  abstract protected void OnSelected(int index, object? data, MenuItem item, MenuBase menu);
}