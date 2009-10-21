using System;
namespace GameDev.Core.SceneManagement
{
    public interface IScene
    {
        System.Collections.Generic.List<Microsoft.Xna.Framework.DrawableGameComponent> DrawableGameComponents { get; set; }
        SceneManager Manager { get; }
        string Name { get; set; }
        Microsoft.Xna.Framework.Graphics.SpriteBatch SpriteBatch { get; }
    }
}
