using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using LevelEditor.Core.MazeItems;

namespace LevelEditor.Core.Helpers
{
    public static class Folders
    {
        public static string GetBackgroundsFolder()
        {
            string folder = Path.Combine(Directory.GetCurrentDirectory(), "Backgrounds");
            if (Directory.Exists(folder))
                return folder;

            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Klimakonflikt\\Backgrounds");
        }

        public static string GetLevelsFolder()
        {
            string folder = null;
            if (Maze.LevelFile != null)
            {
                folder = Path.GetDirectoryName(Maze.LevelFile);
                if (Directory.Exists(folder))
                    return folder;
            }
            folder = Path.Combine(Directory.GetCurrentDirectory(), "Levels");
            if (Directory.Exists(folder))
                return folder;

            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Klimakonflikt\\Levels");
        }
    }
}
