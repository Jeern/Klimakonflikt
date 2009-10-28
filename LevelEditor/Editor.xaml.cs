using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;

namespace LevelEditor
{
    /// <summary>
    /// Interaction logic for Editor.xaml
    /// </summary>
    public partial class Editor : Window
    {
        public Editor()
        {
            InitializeComponent();
            m_LevelInfoEditor = new LevelInfoEditorPage();
            EditorFrame.Content = m_LevelInfoEditor;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                NextButton.IsEnabled = true;
                BackButton.IsEnabled = false;
                SaveButton.IsEnabled = false;
                if (EditorFrame.CanGoBack)
                {
                    EditorFrame.GoBack();
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        private MazeEditorPage m_MazeEditor;
        private LevelInfoEditorPage m_LevelInfoEditor;

        private void NextButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (m_LevelInfoEditor.Validate())
                {
                    NextButton.IsEnabled = false;
                    BackButton.IsEnabled = true;
                    SaveButton.IsEnabled = true;
                    Maze.Initialize((int)(LEConstants.DefaultTileSize * m_LevelInfoEditor.Columns),
                        (int)(LEConstants.DefaultTileSize * m_LevelInfoEditor.Rows),
                        m_LevelInfoEditor.Columns,
                        m_LevelInfoEditor.Rows,
                        m_LevelInfoEditor.BackgroundImageFullName,
                        m_LevelInfoEditor.LevelNumber,
                        m_LevelInfoEditor.LevelName,
                        m_LevelInfoEditor.SpeedFactor);

                    m_MazeEditor = new MazeEditorPage();
                    EditorFrame.Navigate(m_MazeEditor);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }

        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                m_MazeEditor.Save();
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }
    }
}
