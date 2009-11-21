using System.Collections.Generic;
using GameDev.Core.Graphics;
using GameDev.GameBoard;
#if SILVERLIGHT
using SilverArcade.SilverSprite;
#else
using Microsoft.Xna.Framework;
#endif

namespace KlimaKonflikt
{
    public class KKGameBoard : GameBoard
    {
        private Point m_StartPositionFlowerSack;
        public Point StartPositionFlowerSack
        {
            get { return m_StartPositionFlowerSack; }
        }

        private Point m_StartPositionWheelBarrow;
        public Point StartPositionWheelBarrow
        {
            get { return m_StartPositionWheelBarrow; }
        }

        private Point m_StartPositionOilBarrel;
        public Point StartPositionOilBarrel
        {
            get { return m_StartPositionOilBarrel; }
        }

        private Point m_StartPositionOilTower;
        public Point StartPositionOilTower
        {
            get { return m_StartPositionOilTower; }
        }

        private List<Point> m_StartPositionsFire;
        public List<Point> StartPositionsFire
        {
            get { return m_StartPositionsFire; }
        }

        public KKGameBoard(GameImage baseGameImage, string name, int tilesHorizontally, int tilesVertically,
            int tileSizeInPixels, string levelImageFileName,
            Point startPositionFlowerSack, Point startPositionWheelBarrow, Point startPositionOilBarrel,
            Point startPositionOilTower, List<Point> startPositionsFire) 
            : base(baseGameImage, name, tilesHorizontally, tilesVertically, tileSizeInPixels, levelImageFileName)
        {
            m_StartPositionFlowerSack = startPositionFlowerSack;
            m_StartPositionWheelBarrow = startPositionWheelBarrow;
            m_StartPositionOilBarrel = StartPositionOilBarrel;
            m_StartPositionOilTower = startPositionOilTower;
            m_StartPositionsFire = startPositionsFire;
        }
    }
}
