using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;
using LevelEditor.Core.Helpers;

namespace LevelEditor.Core.UI
{
    public class BackgroundImages : ObservableCollection<FileInfo>
    {
        public BackgroundImages()
        {
            string path = Folders.GetBackgroundsFolder();
            var liste =
                from f in Directory.GetFiles(path, "*.png")
                select new FileInfo(f);

            foreach (var fileInfo in liste)
            {
                Add(fileInfo);
            }
        }
    }
}
