using System;
using System.Collections.Generic;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using DinaFramework.Controls;
using DinaFramework.Enums;
using DinaFramework.GUI;
using DinaFramework.Interfaces;


namespace DinaFramework.Menus
{
    class MenuManager : IDraw, IUpdate
    {
        private readonly List<IElement> _elements;
        private readonly List<MenuItem> _items;
        private Group _group = null;
        private readonly int _itemspacing;
        private ControllerKey _next_item_key;
        private ControllerKey _previous_item_key;
        private ControllerKey _active_item_key;
        private int _currentitemindex = -1;
        public MenuManager(int itemspacing = 5)
        {
            _elements = new List<IElement>();
            _items = new List<MenuItem>();
            _itemspacing = itemspacing;
        }
        public void AddTitle(SpriteFont font, string text, Vector2 position, Color color, int zorder = 0)
        {
            _elements.Add(new Text(font, text, color, position, default, default, zorder));
        }
        public void AddTitle(SpriteFont font, string text, Vector2 position, Color color, Color shadowcolor, Vector2 shadowOffset, int zorder = 0)
        {
            _elements.Add(new ShadowText(font, text, color, position, shadowcolor, shadowOffset, default, default, zorder));
            SortElements();
        }
        public MenuItem AddItem(SpriteFont font, string text, Color color,
                                Func<MenuItem, MenuItem> selection = null,
                                Func<MenuItem, MenuItem> deselection = null,
                                Func<MenuItem, MenuItem> activation = null,
                                HorizontalAlignment halign = HorizontalAlignment.Left, VerticalAlignment valign = VerticalAlignment.Top
                               )
        {
            if (_group == null)
            {
                _group = new Group();
                _elements.Add(_group);
            }
            Vector2 groupdimension = _group.GetDimensions();
            float item_pos_y = groupdimension.Y + (_group.Count() > 0 ? _itemspacing : 0.0f);

            MenuItem menuitem = new MenuItem(font, text, color, selection, deselection, activation, default, halign, valign);
            menuitem.SetPosition(new Vector2(_group.GetPosition().X, item_pos_y));

            _group.Add(menuitem);
            _group.SetDimensions(new Vector2(groupdimension.X, item_pos_y + menuitem.GetDimensions().Y));

            _items.Add(menuitem);

            SortElements();
            return menuitem;
        }
        public Vector2 GetItemsDimensions() { return _group.GetDimensions(); }
        public void SetItemsPosition(Vector2 position) { _group.SetPosition(position); }
        public Vector2 GetItemsPosition() { return _group.GetPosition(); }
        public void SetNextItemKey(ControllerKey key) { _next_item_key = key; }
        public void SetPreviousItemKey(ControllerKey key) { _previous_item_key = key; }
        public void SetActivateItemKey(ControllerKey key) { _active_item_key = key; }
        public MenuItem GetCurrentItem()
        {
            if (_currentitemindex == -1)
                return null;
            return _items[_currentitemindex];
        }
        public void SetCurrentItem(MenuItem menuitem) { _currentitemindex = _items.IndexOf(menuitem); }
        public void Reset() { _currentitemindex = -1; }
        public void Draw(SpriteBatch spritebatch)
        {
            foreach (var element in _elements)
            {
                if (element is IDraw draw)
                    draw.Draw(spritebatch);
            }
        }
        public void Update(GameTime gameTime)
        {
            if (_next_item_key != null && _next_item_key.IsPressed())
            {
                ChangeCurrentItem(1);
            }
            if (_previous_item_key != null && _previous_item_key.IsPressed())
            {
                ChangeCurrentItem(-1);
            }
            if (_active_item_key != null && _active_item_key.IsPressed())
            {
                if (_currentitemindex >= 0 && _currentitemindex < _items.Count)
                    _items[_currentitemindex].Activation?.Invoke(_items[_currentitemindex]);
            }

            foreach (var element in _elements)
            {
                if (element is IUpdate update)
                    update.Update(gameTime);
            }
        }
        private void ChangeCurrentItem(int offset)
        {
            if (_currentitemindex >= 0 && _currentitemindex < _items.Count)
                _items[_currentitemindex].Deselection?.Invoke(_items[_currentitemindex]);

            _currentitemindex += offset;
            if (_currentitemindex >= _items.Count)
                _currentitemindex = 0;
            else if (_currentitemindex < 0)
                _currentitemindex = _items.Count - 1;

            _items[_currentitemindex].Selection?.Invoke(_items[_currentitemindex]);
        }
        private void SortElements()
        {
            _elements.Sort(delegate (IElement e1, IElement e2)
            {
                int e1zorder = e1.GetZOrder();
                int e2zorder = e2.GetZOrder();
                if (e1zorder < e2zorder)
                    return -1;
                if (e1zorder > e2zorder)
                    return 1;
                return 0;
            });
        }
    }
}
