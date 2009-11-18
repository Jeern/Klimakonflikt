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
using GameDev.Core.Graphics;
using GameDev.Core.SceneManagement;
using GameDev.Core.Sequencing;

namespace GameDev.Core
{
    public static class BaseTextures
    {
        public static Texture2D Square_128x128 { get; internal set; }
        public static Texture2D Circle_128x128 { get; internal set; }
        public static Texture2D CheckMark_128x128 { get; internal set; }
        public static Texture2D Box_128x128 { get; internal set; }

        static BaseTextures()
        {
            BaseTextures.Circle_128x128 = GameDevGame.Current.Content.Load<Texture2D>("Circle_128x128");
            BaseTextures.Square_128x128 = GameDevGame.Current.Content.Load<Texture2D>("Square_128x128");
            BaseTextures.CheckMark_128x128 = GameDevGame.Current.Content.Load<Texture2D>("Checkmark_128x128");
            BaseTextures.Box_128x128 = GameDevGame.Current.Content.Load<Texture2D>("Box_128x128");
        }
    }
}
