using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Pong_DirectX.DinaFramework.Interfaces;
using Pong_DirectX.DinaFramework.GUI;
using Pong_DirectX.DinaFramework.Enums;

namespace Pong_DirectX.DinaFramework.Menus
{
    class MenuItem : IDraw, IPosition, IDimensions, IElement, IColor
    {
        private readonly Text _text;
        private Func<MenuItem, MenuItem> _selection;
        private Func<MenuItem, MenuItem> _deselection;
        private Func<MenuItem, MenuItem> _activation;

        internal Func<MenuItem, MenuItem> Selection
        {
            get { return _selection; }
            set { _selection = value; }
        }

        internal Func<MenuItem, MenuItem> Deselection
        {
            get { return _deselection; }
            set { _deselection = value; }
        }

        internal Func<MenuItem, MenuItem> Activation
        {
            get { return _activation; }
            set { _activation = value; }
        }

        public MenuItem(SpriteFont font, string text, Color color, 
                        Func<MenuItem, MenuItem> selection,
                        Func<MenuItem, MenuItem> deselection,
                        Func<MenuItem, MenuItem> activation,
                        Vector2 position = default,
                        HorizontalAlignment halign = HorizontalAlignment.Left, VerticalAlignment valign = VerticalAlignment.Top)
        {
            if (selection is null) { throw new ArgumentNullException(nameof(selection)); }
            if (deselection is null) { throw new ArgumentNullException(nameof(deselection)); }
            if (activation is null) { throw new ArgumentNullException(nameof(activation)); }

            _text = new Text(font, text, color, position, halign, valign, 0);
            _selection = selection;
            Deselection = deselection;
            Activation = activation;
        }
        public void SetPosition(Vector2 position)
        {
            _text.SetPosition(position);
        }
        public void SetDimensions(Vector2 dimensions)
        {
            _text.SetDimensions(dimensions);
        }
        public void Draw(SpriteBatch spritebatch)
        {
            _text.Draw(spritebatch);
        }
        public Vector2 GetPosition() { return _text.GetPosition(); }
        public Vector2 GetDimensions() { return _text.GetDimensions(); }
        public int GetZOrder() { return _text.GetZOrder(); }
        public void SetZOrder(int zorder) { _text.SetZOrder(zorder); }
        public Color GetColor() { return _text.GetColor(); }
        public void SetColor(Color color) { _text.SetColor(color); }
    }
}
