using Microsoft.Xna.Framework;

using DinaFramework.Enums;

namespace DinaFramework.Controls
{
    class Player
    {
        public ControllerType controller = ControllerType.Keyboard;
        public PlayerIndex index = PlayerIndex.One;
        public ControllerKey up = null;
        public ControllerKey down = null;
        public ControllerKey left = null;
        public ControllerKey right = null;
        public ControllerKey pause = null;
        public ControllerKey validate = null;
    }
}
