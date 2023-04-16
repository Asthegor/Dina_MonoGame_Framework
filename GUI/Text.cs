
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Pong_DirectX.DinaFramework.Core;
using Pong_DirectX.DinaFramework.Enums;
using Pong_DirectX.DinaFramework.Interfaces;

namespace Pong_DirectX.DinaFramework.GUI
{
    class Text : Base, IUpdate, IDraw, IColor
    {
        private readonly SpriteFont _font;
        private string _content;
        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
                SetDimensions(_font.MeasureString(value));
            }
        }
        private Color _color;

        private HorizontalAlignment _halign;
        private VerticalAlignment _valign;
        private Vector2 _displayposition;

        private float _waitTime;
        private float _displayTime;
        private int _nbLoops;
        private float _timerWaitTime;
        private float _timerDisplayTime;
        private bool _wait;
        private bool _display;


        public Text(SpriteFont font, string content, Color color, Vector2 position = default,
                    HorizontalAlignment horizontalalignment = HorizontalAlignment.Left, VerticalAlignment verticalalignment = VerticalAlignment.Top, int zorder = 0)
        {
            _font = font;
            Content = content;
            _color = color;
            _halign = horizontalalignment;
            _valign = verticalalignment;
            _wait = false;
            _display = true;
            _displayposition = position;
            SetPosition(position);
            SetDimensions(font.MeasureString(Content));
            SetZOrder(zorder);
        }
        public void SetTimers(float waitTime = -1.0f, float displayTime = -1.0f, int nbLoops = -1)
        {
            _waitTime = waitTime;
            _displayTime = displayTime;
            _nbLoops = nbLoops;

            _display = false;
            _wait = false;
            if (waitTime == 0.0f)
                _display = true;
            else if (waitTime > 0.0f)
                _wait = true;
        }
        public Vector2 GetTextDimensions() { return _font.MeasureString(Content); }
        public new void SetPosition(Vector2 position)
        {
            base.SetPosition(position);
            UpdateDisplayPosition(); // Must be after base.SetPosition
        }
        public new void SetDimensions(Vector2 dimension)
        {
            base.SetDimensions(dimension);
            UpdateDisplayPosition(); // Must be after base.SetDimensions
        }
        public void SetAlignments(HorizontalAlignment halign,  VerticalAlignment valign)
        {
            _halign = halign;
            _valign = valign;
        }
        public void Draw(SpriteBatch spritebatch)
        {
            if (_display)
                spritebatch.DrawString(_font, Content, _displayposition, _color);
        }
        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_wait)
            {
                _timerWaitTime += dt;
                if (_timerWaitTime > _waitTime)
                {
                    _timerWaitTime = 0.0f;
                    _wait = false;
                    _display = true;
                }
            }
            else if (_display)
            {
                if (_nbLoops != 0)
                {
                    _timerDisplayTime += dt;
                    if (_timerDisplayTime > _displayTime)
                    {
                        _timerDisplayTime = 0.0f;
                        _display = false;
                        _wait = true;
                        if (_nbLoops > 0)
                            _nbLoops--;
                        if (_nbLoops == 0)
                            _wait = false;
                    }
                }
            }
        }
        private void UpdateDisplayPosition()
        {
            Vector2 offset = new Vector2();
            Vector2 textdimension = GetTextDimensions();
            Vector2 dimensions = GetDimensions();

            if (_halign == HorizontalAlignment.Center)
                offset.X = (dimensions.X - textdimension.X) / 2.0f;
            else if (_halign == HorizontalAlignment.Right)
                offset.X = dimensions.X - textdimension.X;

            if (_valign == VerticalAlignment.Center)
                offset.Y = (dimensions.Y - textdimension.Y) / 2.0f;
            else if (_valign == VerticalAlignment.Bottom)
                offset.Y = dimensions.Y - textdimension.Y;

            _displayposition = base.GetPosition() + offset;
        }

        public Color GetColor() { return _color; }

        public void SetColor(Color color) { _color = color; }
    }
}
