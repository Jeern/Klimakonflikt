using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace GameDev.Core
{
    public static class KeyboardExtensions
    {
        public static bool IsArrowKeyDown(this KeyboardState state)
        {
            return state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.Left);
        }

        public static bool IsWASDKeyDown(this KeyboardState state)
        {
            return state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.D);
        }


        
        public static Direction GetDirectionFromArrowKeys(this KeyboardState state)
        {
            if (IsArrowKeyDown(state))
            {
                DirectionChanger result = new DirectionChanger(0,0);
                if (state.IsKeyDown(Keys.Up)) { result.Offset(DirectionHelper4.Up); }
                if (state.IsKeyDown(Keys.Right)) { result.Offset(DirectionHelper4.Right); }
                if (state.IsKeyDown(Keys.Down)) { result.Offset(DirectionHelper4.Down); }   
                if (state.IsKeyDown(Keys.Left)) { result.Offset(DirectionHelper4.Left); }
                //Console.WriteLine(result);
                return DirectionHelper8.GetDirection(result.DeltaX, result.DeltaY);
            }
            return Direction.None;
        }


        public static Direction GetDirectionFromWASDKeys(this KeyboardState state)
        {
            if (IsWASDKeyDown(state))
            {
                DirectionChanger result = new DirectionChanger(0, 0);
                if (state.IsKeyDown(Keys.W)) { result.Offset(DirectionHelper4.Up); }
                if (state.IsKeyDown(Keys.D)) { result.Offset(DirectionHelper4.Right); }
                if (state.IsKeyDown(Keys.S)) { result.Offset(DirectionHelper4.Down); }
                if (state.IsKeyDown(Keys.A)) { result.Offset(DirectionHelper4.Left); }
                //Console.WriteLine(result);
                return DirectionHelper8.GetDirection(result.DeltaX, result.DeltaY);
            }
            return Direction.None;
        }

    }
}
