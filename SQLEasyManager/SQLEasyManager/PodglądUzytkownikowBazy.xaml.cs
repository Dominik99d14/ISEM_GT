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
    public partial class PodglądUzytkownikowBazy : Window
    {
        private string _databaseName;
        private string connectionString;

        public PodglądUzytkownikowBazy(string databaseName, string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            this.Title = $"Użytkownicy SQL bazy {databaseName}";
            _databaseName = databaseName;
            LoadDatabaseUsers(databaseName);
            LoadUsers();
            this.connectionString = connectionString;
        }

        private void LoadDatabaseUsers(string databaseName)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $@"
                        USE [{databaseName}];
                        SELECT dp.name
                        FROM sys.database_principals dp
                        JOIN sys.database_role_members drm ON dp.principal_id = drm.member_principal_id
                        JOIN sys.database_principals r ON drm.role_principal_id = r.principal_id
                        WHERE r.name = 'db_owner'";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    List<string> dbOwners = new List<string>();
                    while (reader.Read())
                    {
                        dbOwners.Add(reader["name"].ToString());
                    }

                    UsersListBox.ItemsSource = dbOwners;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
                        users.Add(reader["name"].ToString());
                    }

                    UsersComboBox.ItemsSource = users;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddDbOwner_Click(object sender, RoutedEventArgs e)
        {
            if (UsersComboBox.SelectedItem == null)
            {
                MessageBox.Show("Wybierz użytkownika", "Uwaga", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string selectedUser = UsersComboBox.SelectedItem.ToString();                            

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $@"
                        USE [{_databaseName}];
                        ALTER ROLE db_owner ADD MEMBER [{selectedUser}]";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();

                    MessageBox.Show($"Użytkownik {selectedUser} został dodany do bazy {_databaseName}.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadDatabaseUsers(_databaseName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RemoveDbOwner_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
                return;

            string selectedUser = button.Tag as string;
            if (string.IsNullOrEmpty(selectedUser))
                return;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $@"
                        USE [{_databaseName}];
                        ALTER ROLE db_owner DROP MEMBER [{selectedUser}]";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();

                    MessageBox.Show($"Użytkownik {selectedUser} został usuniuęty z bazy {_databaseName}.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadDatabaseUsers(_databaseName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
