using System.Collections.Generic;

namespace GameDev.GameBoard
{
    public static class Extensions
    {

        public static List<WalledTile> GetWalledTiles(this List<Tile> tiles)
        {
            List<WalledTile> wallTiles = new List<WalledTile>();
            foreach (var tile in tiles)
            {
                if (tile is WalledTile)
                {
                    wallTiles.Add((WalledTile)tile);
                }
            }
            return wallTiles;
        }

    }
}
