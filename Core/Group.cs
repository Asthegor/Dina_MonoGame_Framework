using System;
using System.Collections.Generic;


using DinaFramework.Interfaces;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DinaFramework.Core
{
    [Serializable]
    class Group : Base, IDraw, IVisible
    {
        readonly List<IElement> _elements;
        public Group(Vector2 position = default, Vector2 dimensions = default) : base(position, dimensions)
        {
            _elements = new List<IElement>();
        }
        public Group(Group group, bool duplicate = true)
        {
            _elements = new List<IElement>();
            foreach (var item in group._elements)
            {
                if (duplicate)
                    _elements.Add((IElement)Activator.CreateInstance(item.GetType(), item));
                else
                    _elements.Add(item);
            }
            Position = group.Position;
            Dimensions = group.Dimensions;
            ZOrder = group.ZOrder;
            Visible = group.Visible;
        }
        public void Add(IElement element)
        {
            _elements.Add(element);
            SortElements();
        }
        public void Draw(SpriteBatch spritebatch)
        {
            foreach (var element in _elements)
            {
                if (element is IDraw draw)
                    draw.Draw(spritebatch);
            }
        }
        public new Vector2 Position
        {
            get { return base.Position; }
            set
            {
                Vector2 offset = value - base.Position;
                foreach (var element in _elements)
                {
                    if (element is IPosition item)
                        item.Position += offset;
                }
                base.Position = value;
            }
        }
        public new Vector2 Dimensions
        {
            get { return base.Dimensions; }
            set
            {
                float width = 0.0f;
                foreach (var element in _elements)
                {
                    if (element is IDimensions item)
                        width = item.Dimensions.X;
                }
                if (width < base.Dimensions.X)
                    width = base.Dimensions.X;
                foreach (var element in _elements)
                {
                    if (element is IDimensions item)
                        item.Dimensions = new Vector2(width, item.Dimensions.Y);
                }
                if (value.X < width)
                    value.X = width;
                base.Dimensions = value;
            }
        }
        private bool _visible;
        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        public int Count() { return _elements.Count; }
        void SortElements()
        {
            _elements.Sort(delegate (IElement e1, IElement e2)
            {
                if (e1.ZOrder < e2.ZOrder)
                    return -1;
                if (e1.ZOrder > e2.ZOrder)
                    return 1;
                return 0;
            });
        }
    }
}
