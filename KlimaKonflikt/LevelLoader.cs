using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameDev.GameBoard;
using System.IO;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using GameDev.Core.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace KlimaKonflikt
{
    public static class LevelLoader
    {
        public static IEnumerable<GameBoard> GetLevels(Game game, GameImage tileImage, SpriteBatch spriteBatch, int tileSizeInPixels)
        {
            var xmlFiles =
                from file in Directory.GetFiles(Environment.CurrentDirectory, "*.xml")
                where new FileInfo(file).Name.StartsWith("Level")
                select file;

            foreach (string file in xmlFiles)
            {
                yield return GetLevel(file, game, tileImage, spriteBatch, tileSizeInPixels);  
            }
        }

        private static GameBoard GetLevel(string fileName, Game game, GameImage tileImage, SpriteBatch spriteBatch, int tileSizeInPixels)
        {
            XDocument doc = XDocument.Load(fileName);

            string name = (from e in doc.Descendants("LevelInfo")
                          select e.Attribute("Name").Value).First();

            int tilesHorizontally = Convert.ToInt32((from e in doc.Descendants("Columns")
                                         select e.Value).First());

            int tilesVertically = Convert.ToInt32((from e in doc.Descendants("Rows")
                                          select e.Value).First());

            var tiles =
                from e in doc.Descendants("Tile")
                select new
                {
                    Column = Convert.ToInt32(e.Attribute("Column").Value),
                    Row = Convert.ToInt32(e.Attribute("Row").Value),
                    Top = Convert.ToBoolean(e.Attribute("Top").Value),
                    Bottom = Convert.ToBoolean(e.Attribute("Bottom").Value),
                    Left = Convert.ToBoolean(e.Attribute("Left").Value),
                    Right = Convert.ToBoolean(e.Attribute("Right").Value)
                };

            var gameBoard = new GameBoard(game, tileImage, spriteBatch, name, tilesHorizontally, tilesVertically, tileSizeInPixels);
            foreach (var tile in tiles)
            {
                var gameTile = gameBoard.Tiles[tile.Column, tile.Row];
                gameTile.Walls.HasBottomBorder = tile.Bottom;
                gameTile.Walls.HasTopBorder = tile.Top;
                gameTile.Walls.HasLeftBorder = tile.Left;
                gameTile.Walls.HasRightBorder = tile.Right;
            }

            return gameBoard;

        }
    }
}
