using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LevelEditor
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
            string folder = Path.Combine(Directory.GetCurrentDirectory(), "Levels");
            if (Directory.Exists(folder))
                return folder;

            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Klimakonflikt\\Levels");
        }
    }
}
