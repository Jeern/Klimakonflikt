#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

using GameDev.Core;
using GameDev.GameBoard;
using GameDev.Core.Graphics;
using GameDev.Core.SceneManagement;
using GameDev.Core.Sequencing;

	#endregion

namespace KlimaKonflikt.Scenes
{
    public class SceneBase : StaticImageScene
    {
        protected KeyboardState state;
        protected bool noKeysPressed = false;


        public SceneBase(string sceneName, string backgroundTextureName) : base (sceneName, backgroundTextureName)
        {}

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            state = Keyboard.GetState();

            //ensures that the user must have lifted fingers from the ESC that brought him/her here
            if (!noKeysPressed && state.GetPressedKeys().Length == 0)
            {
                noKeysPressed = true;
                return;
            }
        }

        public override void OnEntered()
        {
            base.OnEntered();
            noKeysPressed = false;

        }
    }
}
