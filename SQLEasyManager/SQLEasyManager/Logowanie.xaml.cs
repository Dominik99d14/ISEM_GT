using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
using System.Security.Cryptography;

namespace SQLEasyManager
{
    /// <summary>
    /// Logika interakcji dla klasy Logowanie.xaml
    /// </summary>
    public partial class Logowanie : Window
    {
        public Logowanie()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            ServerTextBox.Text = ConfigurationManager.AppSettings["Server"];
            UsernameTextBox.Text = ConfigurationManager.AppSettings["Login"];
            string encryptedPassword = ConfigurationManager.AppSettings["Password"];
            if (!string.IsNullOrEmpty(encryptedPassword))
            {
                PasswordBox.Password = DecryptString(encryptedPassword);
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string server = ServerTextBox.Text;
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string encryptedPassword = EncryptString(password);

            string connectionString = $"Server={server};User Id={username};Password={password};";

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["Server"].Value = server;
            config.AppSettings.Settings["Login"].Value = username;
            config.AppSettings.Settings["Password"].Value = encryptedPassword;
            config.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    MainWindow mainWindow = new MainWindow(connectionString);
                    mainWindow.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login failed: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string EncryptString(string plainText)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] encryptedBytes = ProtectedData.Protect(plainBytes, null, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encryptedBytes);
        }

        private string DecryptString(string encryptedText)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            byte[] plainBytes = ProtectedData.Unprotect(encryptedBytes, null, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(plainBytes);
        }
    }
}