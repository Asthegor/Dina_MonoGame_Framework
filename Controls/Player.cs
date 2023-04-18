using Microsoft.Xna.Framework;

using Pong_DirectX.DinaFramework.Enums;

namespace Pong_DirectX.DinaFramework.Controls
{
    struct Player
    {
        public ControllerType controller;
        public PlayerIndex index;
        public ControllerKey up;
        public ControllerKey down;
        public ControllerKey left;
        public ControllerKey right;
        public ControllerKey validate;
        public ControllerKey pause;
    }
}
