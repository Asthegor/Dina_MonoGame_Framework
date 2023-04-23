using System;
using System.Collections.Generic;

using DinaFramework.Controls;
using DinaFramework.Core;
using DinaFramework.Core.Fixed;
using DinaFramework.Enums;
using DinaFramework.Interfaces;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DinaFramework.Menus
{
    class MenuManager : IDraw, IUpdate
    {
        readonly List<IElement> _elements;
        readonly List<MenuItem> _items;
        Group _group = null;
        readonly int _itemspacing;
        ControllerKey _next_item_key;
        ControllerKey _previous_item_key;
        ControllerKey _active_item_key;
        int _currentitemindex = -1;
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
            float item_pos_y = _group.Dimensions.Y + (_group.Count() > 0 ? _itemspacing : 0.0f);

            MenuItem menuitem = new MenuItem(font, text, color, selection, deselection, activation,
                                             new Vector2(_group.Position.X, item_pos_y),
                                             halign, valign);

            _group.Add(menuitem);
            _group.Dimensions = new Vector2(_group.Dimensions.X, item_pos_y + menuitem.Dimensions.Y);

            _items.Add(menuitem);

            SortElements();
            return menuitem;
        }

        public Vector2 ItemsDimensions { get { return _group.Dimensions; } }
        public Vector2 ItemsPosition
        {
            get { return _group.Position; }
            set { _group.Position = value; }
        }
        public void SetNextItemKey(ControllerKey key) { _next_item_key = key; }
        public void SetPreviousItemKey(ControllerKey key) { _previous_item_key = key; }
        public void SetActivateItemKey(ControllerKey key) { _active_item_key = key; }
        public MenuItem CurrentItem
        {
            get { return _currentitemindex == -1 || _currentitemindex >= _items.Count ? null : _items[_currentitemindex]; }
            set { _currentitemindex = _items.IndexOf(value); }
        }
        public void Reset() { _currentitemindex = -1; }
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
        public void Update(GameTime gameTime)
        {
            if (_next_item_key != null && _next_item_key.IsPressed())
                ChangeCurrentItem(1);
            if (_previous_item_key != null && _previous_item_key.IsPressed())
                ChangeCurrentItem(-1);

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
        public void Draw(SpriteBatch spritebatch)
        {
            foreach (var element in _elements)
            {
                if (element is IDraw draw)
                    draw.Draw(spritebatch);
            }
        }
        private void SortElements()
        {
            _elements.Sort(delegate (IElement e1, IElement e2)
            {
                if (e1.ZOrder < e2.ZOrder)
                    return -1;
                if (e1.ZOrder > e2.ZOrder)
                    return 1;
                return 0;
            });
        }
    }
}
