using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Pong_DirectX.DinaFramework.Core;
using Pong_DirectX.DinaFramework.Interfaces;

namespace Pong_DirectX.DinaFramework.GUI
{
    class Group : Base, IDraw
    {
        private readonly List<IElement> _elements;
        public Group(Vector2 position = default, Vector2 dimensions = default) : base(position, dimensions)
        {
            _elements = new List<IElement>();
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
        public new void SetPosition(Vector2 position)
        {
            Vector2 groupposition = GetPosition();
            float offsetx = position.X - groupposition.X;
            float offsety = position.Y - groupposition.Y;
            foreach (var element in _elements)
            {
                if (element is IPosition item)
                {
                    Vector2 pos_item = item.GetPosition();
                    item.SetPosition(new Vector2(pos_item.X + offsetx, pos_item.Y + offsety));
                }
            }
            base.SetPosition(position);
        }
        public new void SetDimensions(Vector2 dimensions)
        {
            float max_width = 0.0f;
            foreach (var element in _elements)
            {
                if (element is IDimensions itemdim)
                {
                    Vector2 item_dimension = itemdim.GetDimensions();
                    if (item_dimension.X > max_width)
                        max_width = item_dimension.X;
                }
            }
            if (max_width < GetDimensions().X)
                max_width = GetDimensions().X;
            foreach (var element in _elements)
            {
                if (element is IDimensions itemdim)
                {
                    Vector2 item_dimension = itemdim.GetDimensions();
                    itemdim.SetDimensions(new Vector2(max_width, item_dimension.Y));
                }
            }
            if (dimensions.X < max_width) 
                dimensions.X = max_width;
            base.SetDimensions(dimensions);

        }
        public int Count()
        {
            return _elements.Count;
        }
        private void SortElements()
        {
            _elements.Sort(delegate (IElement e1, IElement e2)
            {
                int e1zorder = e1.GetZOrder();
                int e2zorder = e2.GetZOrder();
                if (e1zorder < e2zorder)
                    return -1;
                if (e1zorder > e2zorder)
                    return 1;
                return 0;
            });
        }
    }
}
