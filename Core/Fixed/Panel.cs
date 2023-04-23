using Microsoft.Xna.Framework;

namespace DinaFramework.Core.Fixed
{
    class Panel : Base
    {
        public Color BackgroundColor { get; set; }
        public Panel(Color backgroundcolor, Vector2 position = default, Vector2 dimensions = default, int zorder = 0) : base(position, dimensions, zorder)
        {
            BackgroundColor = backgroundcolor;
        }
    }
}
