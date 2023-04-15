using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Pong_DirectX.DinaFramework.Core;
using Pong_DirectX.DinaFramework.Enums;
using Pong_DirectX.DinaFramework.Interfaces;

namespace Pong_DirectX.DinaFramework.GUI
{
    class ShadowText : Base, IUpdate, IDraw, IColor
    {
        private readonly Text _text;
        private readonly Text _shadow;
        private Vector2 _offset;
        public ShadowText(SpriteFont font, string content, Color color, Vector2 position, Color shadowcolor, Vector2 offset,
                          HorizontalAlignment halign = default, VerticalAlignment valign = default, int zorder = 0)
        {
            _text = new Text(font, content, color, position, halign, valign, zorder);
            SetOffset(offset);
            _shadow = new Text(font, content, shadowcolor, position + offset, halign, valign, zorder - 1);
        }
        public Color GetColor() { return _text.GetColor(); }
        public void SetColor(Color color) { _text.SetColor(color); }
        public Color GetShadowColor() { return _shadow.GetColor(); }
        public void SetShadowColor(Color color) { _shadow.SetColor(color); }
        public void SetTimers(float waitTime = -1.0f, float displayTime = -1.0f, int nbLoops = -1)
        {
            _shadow.SetTimers(waitTime, displayTime, nbLoops);
            _text.SetTimers(waitTime, displayTime, nbLoops);
        }
        public void Update(GameTime gameTime)
        {
            _shadow.Update(gameTime);
            _text.Update(gameTime);
        }
        public void Draw(SpriteBatch spritebatch)
        {
            _shadow.Draw(spritebatch);
            _text.Draw(spritebatch);
        }
        public new Vector2 GetPosition() { return _text.GetPosition(); }
        public new void SetPosition(Vector2 position)
        {
            _text.SetPosition(position);
            _shadow.SetPosition(position + GetOffset());
        }
        public new Vector2 GetDimensions() { return _text.GetDimensions() + GetOffset(); }
        public new void SetDimensions(Vector2 dimensions)
        {
            _text.SetDimensions(dimensions);
            _shadow.SetDimensions(dimensions + GetOffset());
        }
        public Vector2 GetOffset() { return _offset; }
        public void SetOffset(Vector2 offset) { _offset = offset; }
        public new int GetZOrder() { return _text.GetZOrder(); }
        public new void SetZOrder(int zorder)
        {
            _text.SetZOrder(zorder);
            _shadow.SetZOrder(zorder - 1);
        }
    }
}
