using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using DinaFramework.Interfaces;
using DinaFramework.Core.Fixed;
using DinaFramework.Enums;
using SharpDX.Direct2D1.Effects;

namespace DinaFramework.Menus
{
    class MenuItem : IDraw, IPosition, IDimensions, IElement, IColor
    {
        readonly Text _text;
        Func<MenuItem, MenuItem> _selection;
        Func<MenuItem, MenuItem> _deselection;
        Func<MenuItem, MenuItem> _activation;
        public Func<MenuItem, MenuItem> Selection
        {
            get { return _selection; }
            set { _selection = value; }
        }
        public Func<MenuItem, MenuItem> Deselection
        {
            get { return _deselection; }
            set { _deselection = value; }
        }
        public Func<MenuItem, MenuItem> Activation
        {
            get { return _activation; }
            set { _activation = value; }
        }

        public Vector2 Position
        {
            get { return _text.Position; }
            set { _text.Position = value; }
        }
        public Vector2 Dimensions
        {
            get { return _text.Dimensions; }
            set { _text.Dimensions = value; }
        }
        public int ZOrder
        {
            get { return _text.ZOrder; }
            set { _text.ZOrder = value; }
        }
        public Color Color
        {
            get { return _text.Color; }
            set { _text.Color = value; }
        }
        public string Content
        {
            get { return _text.Content; }
            set { _text.Content = value; }
        }

        public MenuItem(SpriteFont font, string text, Color color, 
                        Func<MenuItem, MenuItem> selection = null,
                        Func<MenuItem, MenuItem> deselection = null,
                        Func<MenuItem, MenuItem> activation = null,
                        Vector2 position = default,
                        HorizontalAlignment halign = HorizontalAlignment.Left, VerticalAlignment valign = VerticalAlignment.Top)
        {
            _text = new Text(font, text, color, position, halign, valign, 0);
            Selection = selection;
            Deselection = deselection;
            Activation = activation;
        }
        public void Draw(SpriteBatch spritebatch)
        {
            _text.Draw(spritebatch);
        }
        public override string ToString() => _text.Content;
    }
}
