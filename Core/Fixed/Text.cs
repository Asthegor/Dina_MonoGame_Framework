﻿
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using DinaFramework.Core;
using DinaFramework.Enums;
using DinaFramework.Interfaces;

namespace DinaFramework.Core.Fixed
{
    class Text : Base, IUpdate, IDraw, IColor, IVisible
    {
        readonly SpriteFont _font;
        string _content;
        Color _color;
        bool _visible;

        HorizontalAlignment _halign;
        VerticalAlignment _valign;

        Vector2 _displayposition;

        float _waitTime;
        float _displayTime;
        int _nbLoops;
        float _timerWaitTime;
        float _timerDisplayTime;
        bool _wait;

        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
                Dimensions = _font.MeasureString(value);
            }
        }
        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }
        public bool Visible
        {
            get { return _visible; } 
            set { _visible = value; }
        }
        public new Vector2 Position
        {
            get { return base.Position; }
            set
            {
                base.Position = value;
                UpdateDisplayPosition();
            }
        }
        public new Vector2 Dimensions
        {
            get { return base.Dimensions; }
            set
            {
                base.Dimensions = value;
                UpdateDisplayPosition();
            }
        }
        public Text(SpriteFont font, string content, Color color, Vector2 position = default,
                    HorizontalAlignment horizontalalignment = HorizontalAlignment.Left, VerticalAlignment verticalalignment = VerticalAlignment.Top, int zorder = 0)
        {
            _font = font;
            Content = content;
            _color = color;
            _halign = horizontalalignment;
            _valign = verticalalignment;
            _wait = false;
            _displayposition = position;
            Position = position;
            Dimensions = font.MeasureString(Content);
            ZOrder = zorder;
            Visible = true;
        }
        public Text(Text text)
        {
            _font = text._font;
            Content = text._content;
            Color = text.Color;
            Visible = text.Visible;
            _halign = text._halign;
            _valign = text._valign;
            _displayposition = text._displayposition;
            _waitTime = text._waitTime;
            _displayTime = text._displayTime;
            _nbLoops = text._nbLoops;
            _timerDisplayTime = text._timerDisplayTime;
            _wait = text._wait;
            Position = text.Position;
            Dimensions = text.Dimensions;
            ZOrder = text.ZOrder;
        }
        public void SetTimers(float waitTime = -1.0f, float displayTime = -1.0f, int nbLoops = -1)
        {
            _waitTime = waitTime;
            _displayTime = displayTime;
            _nbLoops = nbLoops;

            _visible = false;
            _wait = false;
            if (waitTime == 0.0f)
                _visible = true;
            else if (waitTime > 0.0f)
                _wait = true;
        }
        public Vector2 TextDimensions { get { return _font.MeasureString(Content); } }
        public void SetAlignments(HorizontalAlignment halign,  VerticalAlignment valign)
        {
            _halign = halign;
            _valign = valign;
        }
        public void Draw(SpriteBatch spritebatch)
        {
            if (_visible)
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
                    _visible = true;
                }
            }
            else if (_visible)
            {
                if (_nbLoops != 0)
                {
                    _timerDisplayTime += dt;
                    if (_timerDisplayTime > _displayTime)
                    {
                        _timerDisplayTime = 0.0f;
                        _visible = false;
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

            if (_halign == HorizontalAlignment.Center)
                offset.X = (Dimensions.X - TextDimensions.X) / 2.0f;
            else if (_halign == HorizontalAlignment.Right)
                offset.X = Dimensions.X - TextDimensions.X;

            if (_valign == VerticalAlignment.Center)
                offset.Y = (Dimensions.Y - TextDimensions.Y) / 2.0f;
            else if (_valign == VerticalAlignment.Bottom)
                offset.Y = Dimensions.Y - TextDimensions.Y;

            _displayposition = base.Position + offset;
        }
    }
}
