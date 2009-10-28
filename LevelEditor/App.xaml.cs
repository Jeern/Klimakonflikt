using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

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
            AppDomain.CurrentDomain.UnhandledException += DomainUnhandledException;
        }

        private void DomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ExceptionHandler.Handle(e.ExceptionObject as Exception);
        }
    }
}
