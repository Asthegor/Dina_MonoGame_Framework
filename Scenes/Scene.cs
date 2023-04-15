using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Pong_DirectX.DinaFramework.Interfaces;

namespace Pong_DirectX.DinaFramework.Scenes
{
    abstract class Scene : ILoad, IUpdate, IDraw, IValue
    {
        private readonly SceneManager _sceneManager;
        public Scene(SceneManager sceneManager)
        {
            _sceneManager = sceneManager;
        }
        public SceneManager GetSceneManager() { return _sceneManager; }
        public abstract void Draw(SpriteBatch spritebatch);
        public abstract void Load(ContentManager content);
        public abstract void Update(GameTime gameTime);
        public void AddValue(string name, object value) { _sceneManager.AddValue(name, value); }
        public T GetValue<T>(string name) { return _sceneManager.GetValue<T>(name); }
        public void RemoveValue(string name) { _sceneManager.RemoveValue(name); }
        public void Exit() { _sceneManager.Exit(); }
    }
}
