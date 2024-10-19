using System;
using System.Collections.Generic;
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

namespace SQLEasyManager
{
    public partial class KopiowanieUprawnienUzytkownikow : Window
    {
        private string connectionString;

        public KopiowanieUprawnienUzytkownikow(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            LoadUsers();
        }

        private void LoadUsers()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT name FROM sys.syslogins WHERE hasaccess = 1";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    List<string> users = new List<string>();
                    while (reader.Read())
                    {
                        string userName = reader["name"].ToString();
                        users.Add(userName);
                    }

                    SourceUserComboBox.ItemsSource = users;
                    TargetUserComboBox.ItemsSource = users;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void KopiujUprawnienia_Click(object sender, RoutedEventArgs e)
        {
            if (SourceUserComboBox.SelectedItem == null || TargetUserComboBox.SelectedItem == null)
            {
                MessageBox.Show("Wybierz obu użytkowników.", "Uwaga", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string sourceUser = SourceUserComboBox.SelectedItem.ToString();
            string targetUser = TargetUserComboBox.SelectedItem.ToString();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    foreach (var database in MainWindow.allDatabases)
                    {
                        if (database.DbOwners.Contains(sourceUser))
                        {
                            string query = $@"
                                USE [{database.NazwaBazy}];
                                ALTER ROLE db_owner ADD MEMBER [{targetUser}]";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.ExecuteNonQuery();

                            database.DbOwners.Add(targetUser); // Aktualizacja obiektu w pamięci
                        }
                    }

                    MessageBox.Show($"Uprawnienia użytkownika {sourceUser} zostały skopiowane do {targetUser}.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
