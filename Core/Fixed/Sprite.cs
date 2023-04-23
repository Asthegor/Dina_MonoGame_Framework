﻿using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using DinaFramework.Interfaces;

namespace DinaFramework.Core.Fixed
{
    class Sprite : Base, IColor, IVisible, IDraw, ICollide
    {
        Rectangle _rectangle;
        Color _color;
        Vector2 _origin;
        bool _visible;
        Texture2D _texture;
        SpriteEffects _effects;
        Vector2 _flip;
        public Sprite(Texture2D texture, Color color, int zorder = default) : base(default, default, zorder)
        {
            Texture = texture;
            Color = color;
            Visible = true;
        }
        public Sprite(Texture2D texture, Color color, Vector2 position, int zorder) : this(texture, color, zorder)
        {
            Position = position;
        }
        public Sprite(Texture2D texture, Color color, Vector2 position, Vector2 dimensions, int zorder = default) : this(texture, color, position, zorder)
        {
            Dimensions = dimensions;
        }
        public Sprite(Texture2D texture, Color color, Vector2 position, Vector2 dimensions, float rotation = default, Vector2 origin = default,
                      Vector2 flip = default, int zorder = default) : this(texture, color, position, dimensions, zorder)
        {
            Rectangle = new Rectangle(Convert.ToInt32(Position.X), Convert.ToInt32(Position.Y), Convert.ToInt32(Dimensions.X), Convert.ToInt32(Dimensions.Y)); ;
            Rotation = rotation;
            Origin = origin;
            Flip = flip == default ? Vector2.One : flip;
        }
        public Sprite(Texture2D texture, Color color, Vector2 position, float rotation, Vector2 origin = default, Vector2 scale = default,
                      Vector2 flip = default, int zorder = default) : this(texture, color, position, zorder)
        {
            Scale = scale;
            Rotation = rotation;
            Origin = origin;
            Flip = flip == default ? Vector2.One : flip;
            Rectangle = new Rectangle(Convert.ToInt32(Position.X), Convert.ToInt32(Position.Y), Convert.ToInt32(Dimensions.X), Convert.ToInt32(Dimensions.Y));
        }
        public Sprite(Sprite sprite)
        {
            Texture = sprite.Texture;
            Rectangle = sprite.Rectangle;
            Color = sprite.Color;
            Position = sprite.Position;
            Dimensions = sprite.Dimensions;
            Origin = sprite.Origin;
            Visible = sprite.Visible;
            Rotation = sprite.Rotation;
            Scale = sprite.Scale;
            Flip = sprite.Flip;
            _effects = sprite._effects;
        }
        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }
        public Rectangle Rectangle
        {
            get { return _rectangle; }
            private set { _rectangle = value; }
        }
        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }
        public override Vector2 Position
        {
            get { return base.Position; }
            set
            {
                base.Position = value;
                _rectangle.Location = new Point(Convert.ToInt32(value.X - Origin.X), Convert.ToInt32(value.Y - Origin.Y));
            }
        }
        public new Vector2 Dimensions
        {
            get
            {
                if (base.Dimensions == default)
                    return new Vector2(_texture.Width, _texture.Height) * Scale;
                return base.Dimensions;
            }
            set
            {
                base.Dimensions = value;
                _rectangle.Size = new Point(Convert.ToInt32(value.X), Convert.ToInt32(value.Y));
            }
        }
        public Vector2 Origin
        {
            get { return _origin; }
            set { _origin = value; }
        }
        public void CenterOrigin() { _origin = new Vector2(Texture.Width, Texture.Height) * 0.5f; }
        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }
        public float Rotation { get; set; }
        public Vector2 Scale { get; set; }
        public Vector2 Flip
        {
            get { return _flip; }
            set
            {
                if (value.X != 0)
                    value.X /= Math.Abs(value.X);
                if (value.Y != 0)
                    value.Y /= Math.Abs(value.Y);
                _effects = SpriteEffects.None;
                if (value.X < 0)
                    _effects |= SpriteEffects.FlipHorizontally;
                if (value.Y < 0)
                    _effects |= SpriteEffects.FlipVertically;
                _flip = value;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                if (Dimensions == default)
                    spriteBatch.Draw(Texture, Position, new Rectangle(0, 0, _texture.Width, _texture.Height), Color, Rotation, Origin, Scale, _effects, ZOrder);
                else
                    spriteBatch.Draw(Texture, Rectangle, new Rectangle(0, 0, _texture.Width, _texture.Height), Color, Rotation, Origin, _effects, ZOrder);
            }
        }

        public bool Collide(ICollide item)
        {
            return Rectangle.Intersects(item.Rectangle);
        }
    }
}
