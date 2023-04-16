using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Pong_DirectX.DinaFramework.Interfaces;

namespace Pong_DirectX.DinaFramework.Core
{
    class Sprite : Base, IColor, IVisible
    {
        private readonly Texture2D _texture;
        private Rectangle _rectangle;
        private Color _color;
        private bool _visible;

        public Sprite(Texture2D texture, Color color, Vector2 position = default, Vector2 dimensions = default, int zorder = default) : base(position, dimensions, zorder)
        {
            _texture = texture;
            _rectangle = new Rectangle(Convert.ToInt32(position.X), Convert.ToInt32(position.Y), Convert.ToInt32(dimensions.X), Convert.ToInt32(dimensions.Y));
            _color = color;
            _visible = true;
        }
        public Rectangle GetRectangle() { return _rectangle; }
        public Color GetColor() { return _color; }
        public void SetColor(Color color) { _color = color; }
        public new void SetPosition(Vector2 position)
        {
            base.SetPosition(position);
            _rectangle.Location = new Point(Convert.ToInt32(position.X), Convert.ToInt32(position.Y));
        }
        public new void SetDimensions(Vector2 dimensions)
        {
            base.SetDimensions(dimensions);
            _rectangle.Size = new Point(Convert.ToInt32(dimensions.X), Convert.ToInt32(dimensions.Y));
        }
        public void Visible(bool visible) { _visible = visible; }
        public bool IsVisible() { return _visible; }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible())
                spriteBatch.Draw(_texture, _rectangle, _color);
        }
    }
}
