using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameDev.Core.SceneManagement
{
    class MouseClickSceneLink : SceneLink
    {
        public MouseClickSceneLink(string targetSceneName, SceneManager sceneManager) : base(targetSceneName, sceneManager) { }

        public override void Update(GameTime gameTime)
        {
            MouseState state = Mouse.GetState();
            if (state.LeftButton == ButtonState.Pressed || state.MiddleButton == ButtonState.Pressed || state.RightButton == ButtonState.Pressed )
            {
                GoToLink();
            }
        }
    }
}
