using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameDev.Core.SceneManagement
{
    public class AnyKeySceneLink : SceneLink
    {
        public AnyKeySceneLink(IScene targetScene) : base(targetScene) { }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().GetPressedKeys().Length > 0)
            {
                GoToLink();
            }
        }
    }
}
