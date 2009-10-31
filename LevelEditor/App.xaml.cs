using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.IO;

namespace LevelEditor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Maze.LoadXmlFile(GetInputFile(e.Args));
            AppDomain.CurrentDomain.UnhandledException += DomainUnhandledException;
        }

        private string GetInputFile(string[] args)
        {
            if (args != null & args.Length > 0)
            {
                string file = args[0];
                if (!file.EndsWith(".kklevel"))
                    return null;

                if (!File.Exists(file))
                {
                    file = Path.Combine(Directory.GetCurrentDirectory(), file);
                }
                if (!File.Exists(file))
                    return null;

                return file;
            }
            return null;
        }

        private void DomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ExceptionHandler.Handle(e.ExceptionObject as Exception);
        }
    }
}
