﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using GameDev.Core.Graphics;
#if SILVERLIGHT
using PointAlias = SilverArcade.SilverSprite;
using SilverArcade.SilverSprite;
using SilverArcade.SilverSprite.Graphics;
using System.Diagnostics;
using System.Windows;
using System.Windows.Resources;
#else
using PointAlias = Microsoft.Xna.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endif

namespace KlimaKonflikt
{
    public static class LevelLoader
    {
        public static IEnumerable<KKGameBoard> GetLevels(Game game, GameImage tileImage, SpriteBatch spriteBatch, int tileSizeInPixels)
        {
#if SILVERLIGHT
            yield return GetLevel("Levels/Level1.kklevel", game, tileImage, spriteBatch, tileSizeInPixels);  
#else
            var xmlFiles =
                from file in Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, "Levels"), "*.kklevel")
                select file;

            foreach (string file in xmlFiles)
            {
                yield return GetLevel(file, game, tileImage, spriteBatch, tileSizeInPixels);  
            }
#endif
        }
#if SILVERLIGHT
        private static string GetContent(string fileName)
        {
            StreamResourceInfo info = Application.GetResourceStream(new Uri(fileName, UriKind.Relative));
            using (StreamReader reader = new StreamReader(info.Stream))
            {
                return reader.ReadToEnd();
            }
        }
#endif
        private static KKGameBoard GetLevel(string fileName, Game game, GameImage tileImage, SpriteBatch spriteBatch, int tileSizeInPixels)
        {
#if SILVERLIGHT
            XDocument doc = XDocument.Parse(GetContent(fileName));
#else
            XDocument doc = XDocument.Load(fileName);
#endif

            string name = (from e in doc.Descendants("LevelInfo")
                          select e.Attribute("Name").Value).First();

            int tilesHorizontally = Convert.ToInt32((from e in doc.Descendants("Columns")
                                         select e.Value).First());

            int tilesVertically = Convert.ToInt32((from e in doc.Descendants("Rows")
                                          select e.Value).First());

            string imageFileName = GetImageFileName((from e in doc.Descendants("Image")
                            select e.Value).First(), fileName);

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

            PointAlias.Point startPosFlowerSack = LoadPointElement(doc, "Flowersack");
            PointAlias.Point startPosOilBarrel = LoadPointElement(doc, "Oilbarrel");
            PointAlias.Point startPosWheelBarrow = LoadPointElement(doc, "Wheelbarrow");
            PointAlias.Point startPosOilTower = LoadPointElement(doc, "Oiltower");
            List<PointAlias.Point> startPosFires = LoadPointElements(doc, "Fire").ToList();

            var gameBoard = new KKGameBoard(tileImage, name, tilesHorizontally, tilesVertically, 
                tileSizeInPixels, imageFileName, startPosFlowerSack, startPosWheelBarrow, startPosOilBarrel, startPosOilTower,
                startPosFires);
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

        private static PointAlias.Point LoadPointElement(XDocument doc, string elementName)
        {
            return LoadPointElements(doc, elementName).First(); 
        }

        private static IEnumerable<PointAlias.Point> LoadPointElements(XDocument doc, string elementName)
        {
            return
                from e in doc.Descendants(elementName)
                 select new PointAlias.Point(
                     Convert.ToInt32(e.Attribute("Column").Value),
                     Convert.ToInt32(e.Attribute("Row").Value));
        }

        /// <summary>
        /// Calculates the filename of the imageFile by looking at the path of the kkLevel file. Because they are
        /// most likely placed in the same directory.
        /// </summary>
        /// <param name="imageName"></param>
        /// <param name="kkLevelfileName"></param>
        /// <returns></returns>
        private static string GetImageFileName(string imageName, string kkLevelfileName)
        {
#if SILVERLIGHT
            return imageName;
#else
            string fileName = Path.Combine(Path.GetDirectoryName(kkLevelfileName), imageName);
            if (File.Exists(fileName))
                return fileName;

            if (File.Exists(imageName))
                return imageName;

            return null;
#endif
        }
    }
}
