using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Pong_DirectX.DinaFramework.Enums;

namespace Pong_DirectX.DinaFramework.Controls
{
    internal interface IControllerKey
    {
        public abstract bool IsPressed();
        public abstract bool IsReleased();
    }
    internal class KeyboardKey : IControllerKey
    {
        private Keys Key { get; set; }
        private KeyboardState oldState = Keyboard.GetState();
        public KeyboardKey(Keys key) { Key = key; }
        public bool IsPressed()
        {
            bool result = !oldState.IsKeyDown(Key) && Keyboard.GetState().IsKeyDown(Key);
            oldState = Keyboard.GetState();
            return result;
        }
        public bool IsReleased() => Keyboard.GetState().IsKeyUp(Key);
    }
    internal class GamepadButton : IControllerKey
    {
        private Buttons Button { get; set; }
        private GamePadState oldState = GamePad.GetState(PlayerIndex.One);
        public GamepadButton(Buttons button) { Button = button; }
        public bool IsPressed()
        {
            bool result = !oldState.IsButtonDown(Button) && GamePad.GetState(PlayerIndex.One).IsButtonDown(Button);
            oldState = GamePad.GetState(PlayerIndex.One);
            return result;
        }
        public bool IsReleased() => GamePad.GetState(PlayerIndex.One).IsButtonUp(Button);
    }

}
