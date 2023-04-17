using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Pong_DirectX.DinaFramework.Enums;

namespace Pong_DirectX.DinaFramework.Controls
{
    abstract class ControllerKey
    {
        private static List<ControllerKey> _controllers = new List<ControllerKey>();
        public static List<ControllerKey> Controllers
        {
            get { return _controllers; }
            protected set { _controllers = value; }
        }
        private ControllerAction _action;
        public ControllerAction Action
        {
            get { return _action; }
            set { _action = value; }
        }

        protected ControllerKey()
        { 
            Controllers.Add(this);
        }
        public static void ResetAllKeys()
        {
            foreach (var controller in Controllers)
                controller.Reset();
        }
        public abstract void Reset();
        public abstract bool IsPressed();
        public abstract bool IsReleased();
        public new abstract string ToString();
    }
    class KeyboardKey : ControllerKey
    {
        private Keys Key { get; set; }
        
        private KeyboardState oldState = Keyboard.GetState();
        public KeyboardKey(Keys key, ControllerAction action = ControllerAction.Pressed)
        {
            Key = key;
            Reset();
            Action = action;
        }
        public override void Reset() { oldState = Keyboard.GetState(); }
        public override bool IsPressed()
        {
            if (Action == ControllerAction.Released)
                return false;
            bool result = Keyboard.GetState().IsKeyDown(Key);
            if (Action == ControllerAction.Pressed)
                result = !oldState.IsKeyDown(Key) && Keyboard.GetState().IsKeyDown(Key);
            oldState = Keyboard.GetState();
            return result;
        }
        public override bool IsReleased() => Keyboard.GetState().IsKeyUp(Key);
        public override string ToString() { return Key.ToString(); }
    }
    class GamepadButton : ControllerKey
    {
        private readonly PlayerIndex _indexplayer;
        private Buttons Button { get; set; }
        private GamePadState oldState;
        public GamepadButton(Buttons button, ControllerAction action = ControllerAction.Pressed, PlayerIndex index = PlayerIndex.One)
        {
            Button = button;
            Action = action;
            _indexplayer = index;
            Reset();
        }
        public override void Reset() { oldState = GamePad.GetState(_indexplayer); }
        public override bool IsPressed()
        {
            if (Action == ControllerAction.Released)
                return false;
            bool result = GamePad.GetState(_indexplayer).IsButtonDown(Button);
            if(Action == ControllerAction.Pressed)
                result = !oldState.IsButtonDown(Button) && GamePad.GetState(_indexplayer).IsButtonDown(Button);
            oldState = GamePad.GetState(_indexplayer);
            return result;
        }
        public override bool IsReleased() => GamePad.GetState(_indexplayer).IsButtonUp(Button);
        public override string ToString() { return Button.ToString(); }
    }
}
