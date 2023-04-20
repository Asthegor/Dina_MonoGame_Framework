using Microsoft.Xna.Framework;

using DinaFramework.Interfaces;

namespace DinaFramework.Core
{
    abstract class Base : IElement, IPosition, IDimensions
    {
        private Vector2 _position;
        private Vector2 _dimensions;
        private int _zorder;
        public Base(Vector2 position = new Vector2(), Vector2 dimensions = new Vector2(), int zorder = 0)
        {
            SetPosition(position);
            SetDimensions(dimensions);
            SetZOrder(zorder);
        }
        public int GetZOrder() { return _zorder; }
        public void SetZOrder(int zorder) {_zorder = zorder; }
        public Vector2 GetDimensions() { return _dimensions; }
        public Vector2 GetPosition() { return _position; }
        public void SetDimensions(Vector2 dimensions) { _dimensions = dimensions; }
        public void SetPosition(Vector2 position) { _position = position; }
    }
}
