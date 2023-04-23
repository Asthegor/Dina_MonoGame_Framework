using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DinaFramework.Interfaces
{
    interface IElement
    {
        public abstract int ZOrder { get; set; }
    }
    interface ILoad
    {
        public abstract void Load(ContentManager content);
    }
    interface IUpdate
    {
        public abstract void Update(GameTime gameTime);
    }
    interface IDraw
    {
        public abstract void Draw(SpriteBatch spritebatch);
    }
    interface IPosition
    {
        public abstract Vector2 Position { get; set; }
    }
    interface IDimensions
    {
        public abstract Vector2 Dimensions { get; set; }
    }
    interface IValue
    {
        public abstract void AddValue(string name, Object value);
        public abstract T GetValue<T>(string name);
        public abstract void RemoveValue(string name);
    }
    interface IColor
    {
        public abstract Color Color { get; set; }
    }
    interface IVisible
    {
        public abstract bool Visible { get; set; }
    }
    interface ICollide : IPosition, IDimensions
    {
        public abstract bool Collide(ICollide item);
        public Rectangle Rectangle { get; }
    }
}
