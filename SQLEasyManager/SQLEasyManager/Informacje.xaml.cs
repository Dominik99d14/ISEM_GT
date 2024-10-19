using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows;
using System.ComponentModel;
using System.Reflection;
using System.IO;
using System.Windows;

namespace SQLEasyManager
{
    /// <summary>
    /// Logika interakcji dla klasy Informacje.xaml
    /// </summary>
    public partial class Informacje : Window
    {
        public Informacje()
        {
            InitializeComponent();
            DisplayVersion();
            LoadChangelog();
        }

        public void DisplayVersion()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            // Wyświetl wersję w kontrolce TextBlock o nazwie versionTextBlock
            WersjaLabel.Content = $"Wersja programu: {version}";
        }

        private void LoadChangelog()
        {
            try
            {
                string changelogPath = "changelog.txt"; // Ścieżka do pliku changelog
                if (File.Exists(changelogPath))
                {
                    string changelogText = File.ReadAllText(changelogPath);
                    ChangeLogTextBox.Text = changelogText;
                }
                else
                {
                    ChangeLogTextBox.Text = "Changelog brak pliku.";
                }
            }
            catch (Exception ex)
            {
                ChangeLogTextBox.Text = $"Błąd elementu: {ex.Message}";
            }
        }
    }
}
