using System;
using System.Collections.Generic;
using System.Diagnostics;

using DinaFramework.Scenes;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using DinaFramework.Controls;
using DinaFramework.Interfaces;

namespace DinaFramework.Scenes
{
    class SceneManager : IValue
    {
        static SceneManager _instance;
        static readonly Object _mutex = new Object();
        private readonly Dictionary<string, Object> _values;
        readonly Game _game;
        readonly ContentManager _content;
        readonly Dictionary<string, Scene> _scenes;
        Scene _currentscene;
        public static SceneManager GetInstance(Game game)
        {
            if (_instance == null)
            {
                lock (_mutex)
                {
                    _instance ??= new SceneManager(game);
                }
            }
            return _instance;
        }
        // Gestion des scènes
        public void AddScene(string name, Type type)
        {
            if (name == "")
            {
                Debug.WriteLine("The 'name' must not be empty.");
                return;
            }
            if (type == null)
            {
                Debug.WriteLine("The 'type' must not be null.");
                return;
            }
            if (!typeof(Scene).IsAssignableFrom(type))
            {
                Debug.WriteLine("The type '" + type.Name + "' is not a valid Scene type.");
                return;
            }
            if (_scenes.ContainsKey(name))
            {
                Debug.WriteLine("The 'name' already exists.");
                return;
            }
            _scenes[name] = (Scene)Activator.CreateInstance(type, this);
        }
        public void RemoveScene(string name)
        {
            if (name == "")
            {
                Debug.WriteLine("The 'name' must not be empty.");
                return;
            }
            if (_scenes.ContainsKey(name) && _currentscene == _scenes[name])
            {
                Debug.WriteLine("The current scene cannot be removed.");
                return;
            }
            _scenes.Remove(name);
        }
        public void SetCurrentScene(string name)
        {
            ControllerKey.ResetAllKeys();
            if (name == "")
            {
                Debug.WriteLine("The 'name' must not be empty.");
                return;
            }
            if (!_scenes.ContainsKey(name))
            {
                Debug.WriteLine("The scene '" + name + "' does not exists.");
                return;
            }
            _currentscene = _scenes[name];
            if (!_currentscene.Loaded)
            {
                _currentscene.Load(_content);
                _currentscene.Loaded = true;
            }
            _currentscene.Reset();
        }
        public void AddValue(string name, Object value)
        {
            if (_values.ContainsKey(name))
                _values[name] = value;
            else
                _values.Add(name, value);
        }
        public T GetValue<T>(string name) { return _values.ContainsKey(name) ? (T)_values[name] : default; }
        public void RemoveValue(string name) { if (_values.ContainsKey(name)) { _values.Remove(name); } }
        public void Exit() { _game.Exit(); }

        // Fonctions génériques
        public void Update(GameTime gameTime) { _currentscene?.Update(gameTime); }
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin();
            _currentscene?.Draw(spritebatch);
            spritebatch.End();
        }

        // Fonctions privées
        SceneManager(Game game)
        {
            _game = game;
            _content = game.Content;
            _scenes = new Dictionary<string, Scene>();
            _currentscene = null;
            _values = new Dictionary<string, Object>();
        }
    }
}
