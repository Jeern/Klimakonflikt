﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace GameDev.Core.Graphics
{
    public class GameDevTexture
    { 
        public Texture2D Texture
        { 
            get; 
            set; 
        }

        public static implicit operator GameDevTexture(Texture2D texture)
        {
            var gdTexture = new GameDevTexture();
            gdTexture.Texture = texture;
            return gdTexture;
        }
    }
}
