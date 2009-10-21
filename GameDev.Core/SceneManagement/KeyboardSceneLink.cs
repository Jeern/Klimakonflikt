using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameDev.Core.SceneManagement
{
    public class KeyboardSceneLink : SceneLink
    {
        public List<Keys> TriggerKeys { get; set; }

        public KeyboardSceneLink(List<Keys> triggerKeys, string targetSceneName, SceneManager sceneManager): base(targetSceneName, sceneManager)
        {
            TriggerKeys = triggerKeys;
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            foreach (Keys key in TriggerKeys)
            {
                if (state.IsKeyDown(key))
                {
                    GoToLink();
                }
            }
        }

    }
}
