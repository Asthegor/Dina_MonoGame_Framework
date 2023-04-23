using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using DinaFramework.Core.Fixed;
using DinaFramework.Interfaces;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using SharpDX.Direct2D1.Effects;

namespace DinaFramework.Core.Animated
{
    class Animation : Base, IUpdate, IDraw, IColor
    {
        readonly List<Sprite> _frames = new List<Sprite>();
        readonly float _speed;
        float _currentframe;
        float _rotation;
        Color _color;
        Vector2 _origin;
        Vector2 _flip;
        public Animation(ContentManager content, string prefix, int nbframes, float speed, int start, Color color,
                         Vector2 position, Vector2 dimensions, float rotation = default, Vector2 origin = default,
                         Vector2 flip = default, int zorder = default) : base(position, dimensions, zorder)
        {
            _speed = speed;
            Color = color == default ? Color.White : color;
            Rotation = 0.0f;
            Origin = origin;
            Flip = flip == default ? Vector2.One : flip;
            Scale = Vector2.One;
            AddFrames(content, prefix, nbframes, start, Dimensions, rotation, Origin);
        }
        public Animation(ContentManager content, string prefix, int nbframes, float speed, int start, Color color,
                         Vector2 position, float rotation, Vector2 origin = default, Vector2 scale = default,
                         Vector2 flip = default, int zorder = default) : base(position, default, zorder)
        {
            _speed = speed;
            Color = color == default ? Color.White : color;
            Rotation = rotation;
            Origin = origin;
            Scale = scale == default ? Vector2.One : scale;
            Flip = flip == default ? Vector2.One : flip;
            AddFrames(content, prefix, nbframes, start, rotation, Origin, Scale);
        }
        public Animation(Animation animation, bool duplicate = true)
        {
            _frames = new List<Sprite>();
            foreach (Sprite item in animation._frames)
            {
                if (duplicate)
                    _frames.Add((Sprite)Activator.CreateInstance(typeof(Sprite), item));
                else
                    _frames.Add(item);
            }
            _speed = animation._speed;
            Color = animation.Color;
            Rotation = animation.Rotation;
            Origin = animation.Origin;
            Scale = animation.Scale;
            Flip = animation.Flip;
            _currentframe = 0;
            Position = animation.Position;
            Dimensions = animation.Dimensions;
            ZOrder = animation.ZOrder;
        }
        private void AddFrames(ContentManager content, string prefix, int nbframes, int start, Vector2 dimensions, float rotation, Vector2 origin)
        {
            Texture2D texture;
            for (int index = start; index < nbframes + start; index++)
            {
                texture = content.Load<Texture2D>(prefix + index.ToString());
                if (dimensions == Vector2.Zero)
                {
                    dimensions = new Vector2(texture.Width, texture.Height);
                }
                _frames.Add(new Sprite(texture, Color, Position, dimensions, rotation, origin, Flip, ZOrder));
            }
            Dimensions = dimensions;
        }
        private void AddFrames(ContentManager content, string prefix, int nbframes, int start, float rotation, Vector2 origin, Vector2 scale)
        {
            Texture2D texture;
            for (int index = start; index < nbframes + start; index++)
            {
                texture = content.Load<Texture2D>(prefix + index.ToString());
                _frames.Add(new Sprite(texture, Color, Position, rotation, origin, scale, Flip, ZOrder));
            }
        }
        public new Vector2 Position
        {
            get { return base.Position; }
            set
            {
                foreach (Sprite frame in _frames)
                    frame.Position = value + Origin;
                base.Position = value;
            }
        }
        public new Vector2 Dimensions
        {
            get { return base.Dimensions; }
            set
            {
                foreach (Sprite frame in _frames)
                    frame.Dimensions = value;
                base.Dimensions = value;
            }
        }
        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }
        public Vector2 Origin
        {
            get { return _origin; }
            set
            {
                foreach (Sprite frame in _frames)
                    frame.Origin = value;
                _origin = value;
            }
        }
        public void CenterOrigin()
        {
            foreach (Sprite frame in _frames)
                frame.CenterOrigin();
        }
        public float Rotation
        {
            get { return _rotation; }
            set
            {
                foreach (Sprite frame in _frames)
                    frame.Rotation = value;
                _rotation = value;
            }
        }
        public Vector2 Scale { get; set; }
        public Vector2 Flip
        {
            get { return _flip; }
            set
            {
                foreach (Sprite frame in _frames)
                    frame.Flip = value;
                if (value.X != 0)
                    value.X /= Math.Abs(value.X);
                if (value.Y != 0)
                    value.Y /= Math.Abs(value.Y);
                _flip = value;
            }
        }
        public void Update(GameTime gameTime)
        {
            _currentframe += Convert.ToSingle(gameTime.ElapsedGameTime.TotalSeconds) * _speed;
            if (_currentframe >= _frames.Count)
                _currentframe = 0;
        }
        public void Draw(SpriteBatch spritebatch)
        {
            _frames[(int)_currentframe].Draw(spritebatch);
        }
    }
}
