using System;
using System.Collections.Generic;
using System.Linq;
using GameDev.Core;
using GameDev.Core.Graphics;
using GameDev.Core.SceneManagement;
using GameDev.Core.Sequencing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameDev.Core
{
    public static class Vector2Helper
    {
        public static Vector2 Right = new Vector2(1,0);
        public static Vector2 Up = new Vector2(0, -1);
        public static Vector2 Left = new Vector2(-1, 0);
        public static Vector2 Down = new Vector2(0, 1);

    }
}
