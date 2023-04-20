using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DinaFramework.Interfaces
{
    interface IElement
    {
        public abstract int GetZOrder();
        public abstract void SetZOrder(int zorder);
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
        public abstract Vector2 GetPosition();
        public abstract void SetPosition(Vector2 position);
    }
    interface IDimensions
    {
        public abstract Vector2 GetDimensions();
        public abstract void SetDimensions(Vector2 dimensions);
    }
    interface IValue
    {
        public abstract void AddValue(string name, Object value);
        public abstract T GetValue<T>(string name);
        public abstract void RemoveValue(string name);
    }
    interface IColor
    {
        public abstract Color GetColor();
        public abstract void SetColor(Color color);
    }
    interface IVisible
    {
        public abstract void Visible(bool visible);
        public abstract bool IsVisible();
    }
}
