using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace LevelEditor
{
    public static class ExceptionHandler
    {
        public static void Handle(Exception ex)
        {
            var sb = new StringBuilder();
            if (ex == null)
            {
                sb.AppendLine("Something weird happened.");
            }
            else
            {
                sb.AppendLine("Error: " + ex.Message);
                sb.AppendLine();
                sb.AppendLine(ex.ToString());
            }
            MessageBox.Show(sb.ToString(), "KKLevel Editor", MessageBoxButton.OK, MessageBoxImage.Stop);
        }
    }
}
