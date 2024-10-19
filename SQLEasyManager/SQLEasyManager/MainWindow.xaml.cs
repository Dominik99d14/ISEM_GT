using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SQLEasyManager
{
    public partial class MainWindow : Window
    {
        public static List<BazyDanych_class> allDatabases = new List<BazyDanych_class>();
        private string connectionString;
        private bool ZaznaczajBazy;

        public MainWindow(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            ZaznaczajBazy = bool.Parse(ConfigurationManager.AppSettings["Ustawienia-ZaznaczajBazy"]);
            LadowanieBaz();
            LadowanieUzytkownikow();
            Ladowanie_Label.Content = "Gotowe";
            Ladowanie_ProgresBar.Visibility = Visibility.Hidden;
        }

        private void LadowanieBaz()
        {
            Ladowanie_Label.Content = "Ładowanie danych";
            Ladowanie_ProgresBar.Visibility = Visibility.Visible;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    Ladowanie_Label.Content = "Połączono z bazą danych";
                    Ladowanie_ProgresBar.Value = 20;
                    connection.Open();
                    string query = "SELECT name FROM sys.databases";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    allDatabases.Clear();
                    Ladowanie_Label.Content = "Pobieranie danych z bazą danych";
                    Ladowanie_ProgresBar.Value = 50;
                    while (reader.Read())
                    {
                        string databaseName = reader["name"].ToString();
                        var database = new BazyDanych_class(databaseName);
                        allDatabases.Add(database);
                    }
                    Ladowanie_Label.Content = "Pobieranie danych z bazą danych";
                    Ladowanie_ProgresBar.Value = 75;
                    reader.Close();

                    Ladowanie_Label.Content = "Pobieranie danych z bazą danych";
                    Ladowanie_ProgresBar.Value = 100;

                    var filteredDatabases = new List<BazyDanych_class>();

                    Ladowanie_Label.Content = "ładowanie danych";
                    Ladowanie_ProgresBar.Value = 0;
                    int IloscZaladowanychBaz = 0;
                    int IloscBazDoZaladowania = allDatabases.Count;
                    Ladowanie_ProgresBar.Maximum = IloscBazDoZaladowania;
                    foreach (var database in allDatabases)
                    {
                        connection.ChangeDatabase(database.NazwaBazy);
                        SqlCommand checkTableCommand = new SqlCommand(
                            "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA + '.' + TABLE_NAME = 'dbo.adr__Ewid'", connection);
                        int tableCount = (int)checkTableCommand.ExecuteScalar();

                        if (tableCount > 0 && TableExists(connection, "dbo", "adr__Ewid"))
                        {
                            database.NIP = GetNIPFromAdrEwid(connection);
                            database.NazwaPodmiotu = GetNazwaPodmiotuFromAdrEwid(connection);
                            database.DbOwners = GetDbOwners(connection);
                            if (TableExists(connection, "dbo", "pd__Podmiot"))
                            {
                                database.Gratyfikant = GetGratyfikant(connection);
                                database.TypKsiegowosci = GetTypKsiegowosci(connection);
                            }
                            filteredDatabases.Add(database);
                        }
                        IloscZaladowanychBaz++;
                        Ladowanie_Label.Content = "Załadowano bazę: " + database.NazwaBazy + " Ładowanie " + IloscBazDoZaladowania.ToString() + "/" + IloscZaladowanychBaz.ToString();
                        Ladowanie_ProgresBar.Value = IloscZaladowanychBaz;
                    }

                    allDatabases = filteredDatabases;
                    Wyszukiwanie_Baz();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Ladowanie_Label.Content = "Gotowe";
            Ladowanie_ProgresBar.Visibility = Visibility.Hidden;
        }

        private string GetTypKsiegowosci(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("SELECT pd_TypKsiegowosci FROM pd__Podmiot WHERE pd_Id = 1", connection); // Upewnij się, że warunek WHERE jest poprawny dla twojego przypadku
            var result = command.ExecuteScalar();
            if (result != null)
            {
                int typKsiegowosci = Convert.ToInt32(result);
                if (typKsiegowosci == 1)
                {
                    return "Rachmistrz";
                }
                else if (typKsiegowosci == 2)
                {
                    return "Ryczałt";
                }
                else if (typKsiegowosci == 3)
                {
                    return "Rewizor";
                }
                else
                {
                    return "";
                }
            }
            return "Brak";
        }

        private string GetGratyfikant(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM pd__Podmiot WHERE pd_TypKadr = 2", connection);
            var result = command.ExecuteScalar();
            if (result != null)
            {
                int typKsiegowosci = Convert.ToInt32(result);
                if (typKsiegowosci == 1)
                {
                    return "Gratyfikant";
                }
                else if (typKsiegowosci == 2)
                {
                    return "MikroGratyfikant";
                }
                else
                {
                    return "";
                }
            }
            return "Brak";
        }

        private bool TableExists(SqlConnection connection, string schema, string tableName)
        {
            SqlCommand command = new SqlCommand($"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '{schema}' AND TABLE_NAME = '{tableName}'", connection);
            return (int)command.ExecuteScalar() > 0;
        }

        private void OdznaczWszystkieBazy_Click(object sender, RoutedEventArgs e)
        {
            foreach (var database in allDatabases)
            {
                database.IsChecked = false;
            }

            Wyszukiwanie_Baz(); // Odświeżenie widoku listy baz
        }

        private string GetNIPFromAdrEwid(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("SELECT adr_NIP FROM dbo.adr__Ewid where adr_Id = 1", connection);
            var result = command.ExecuteScalar();
            return result != null ? result.ToString() : string.Empty;
        }

        private string GetNazwaPodmiotuFromAdrEwid(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("SELECT adr_NazwaPelna FROM dbo.adr__Ewid where adr_Id = 1", connection);
            var result = command.ExecuteScalar();
            return result != null ? result.ToString() : string.Empty;
        }

        private List<string> GetDbOwners(SqlConnection connection)
        {
            var owners = new List<string>();
            SqlCommand command = new SqlCommand("SELECT dp.name FROM sys.database_principals dp JOIN sys.database_role_members drm ON dp.principal_id = drm.member_principal_id JOIN sys.database_principals r ON drm.role_principal_id = r.principal_id WHERE r.name = 'db_owner'", connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                owners.Add(reader["name"].ToString());
            }
            reader.Close();
            return owners;
        }

        private void LoadDatabases_Click(object sender, RoutedEventArgs e)
        {
            // LadowanieBaz();
        }

        private void LoadUsers_Click(object sender, RoutedEventArgs e)
        {
            // LadowanieUzytkownikow();
        }

        private void LadowanieUzytkownikow()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT name FROM sys.syslogins WHERE hasaccess = 1";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    UsersComboBox.Items.Clear();
                    while (reader.Read())
                    {
                        string userName = reader["name"].ToString();
                        UsersComboBox.Items.Add(userName);
                    }

                    if (UsersComboBox.Items.Count > 0)
                    {
                        UsersComboBox.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CheckUserDatabases_Click(object sender, RoutedEventArgs e)
        {
            if (UsersComboBox.SelectedItem == null)
            {
                MessageBox.Show("Wybierz użytkownika", "Uwaga", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string selectedUser = UsersComboBox.SelectedItem.ToString();

            var accessibleDatabases = new List<BazyDanych_class>();

            foreach (var database in allDatabases)
            {
                if (database.DbOwners.Contains(selectedUser))
                {
                    accessibleDatabases.Add(database);
                }
            }

            DatabasesDataGrid.Items.Clear();
            foreach (var db in accessibleDatabases)
            {
                DatabasesDataGrid.Items.Add(db);
            }
        }

        private void Wyszukiwanie_Baz()
        {
            string filter = Wyszukiwarka_baz.Text.ToLower();

            // Zakończ edycję przed rozpoczęciem jakichkolwiek zmian
            if (DatabasesDataGrid.IsReadOnly == false)
            {
                DatabasesDataGrid.CommitEdit(DataGridEditingUnit.Row, true);
                DatabasesDataGrid.CancelEdit();
            }

            var selectedDatabases = allDatabases.Where(db => db.IsChecked).ToList();

            var filteredDatabases = new List<BazyDanych_class>();

            if (SzukaniePoNazwieBazy_CheckBox.IsChecked == true && SzukanieUzytkownik_CheckBox.IsChecked == true && UsersComboBox.SelectedItem != null)
            {
                string selectedUser = UsersComboBox.SelectedItem.ToString();

                foreach (var database in allDatabases)
                {
                    if (database.DbOwners.Contains(selectedUser) && database.NazwaBazy.ToLower().Contains(filter))
                    {
                        filteredDatabases.Add(database);
                    }
                }
            }
            else if (SzukaniePoNazwieBazy_CheckBox.IsChecked == true)
            {
                foreach (var database in allDatabases)
                {
                    if (database.NazwaBazy.ToLower().Contains(filter))
                    {
                        filteredDatabases.Add(database);
                    }
                }
            }
            else if (SzukanieUzytkownik_CheckBox.IsChecked == true && UsersComboBox.SelectedItem != null)
            {
                string selectedUser = UsersComboBox.SelectedItem.ToString();

                foreach (var database in allDatabases)
                {
                    if (database.DbOwners.Contains(selectedUser))
                    {
                        filteredDatabases.Add(database);
                    }
                }
            }
            else
            {
                filteredDatabases = allDatabases;
            }

            // Nie modyfikuj elementów bezpośrednio - pracuj na kolekcji
            foreach (var database in filteredDatabases)
            {
                if (selectedDatabases.Any(db => db.NazwaBazy == database.NazwaBazy))
                {
                    database.IsChecked = true;
                }
            }

            DatabasesDataGrid.ItemsSource = null; // Odłącz źródło, aby odświeżyć
            DatabasesDataGrid.ItemsSource = filteredDatabases; // Przypisz nowe źródło
        }

        private void Wyszukiwarka_baz_TextChanged(object sender, TextChangedEventArgs e)
        {
            Wyszukiwanie_Baz();
        }

        private void DatabasesListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DatabasesDataGrid.SelectedItem != null)
            {
                var selectedDatabase = DatabasesDataGrid.SelectedItem as BazyDanych_class;
                if (selectedDatabase != null)
                {
                    PodglądUzytkownikowBazy newWindow = new PodglądUzytkownikowBazy(selectedDatabase.NazwaBazy, connectionString);
                    newWindow.Show();
                }
            }
        }

        private bool UzytkownikIstnieje(SqlConnection connection, string userName)
        {
            string query = "SELECT COUNT(*) FROM sys.database_principals WHERE name = @userName";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@userName", userName);
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        private void DodajZaznaczoneBazUzytkownikowi_Click(object sender, RoutedEventArgs e)
        {
            if (UsersComboBox.SelectedItem == null)
            {
                MessageBox.Show("Wybierz użytkownika", "Uwaga", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string selectedUser = UsersComboBox.SelectedItem.ToString();
            var selectedDatabases = allDatabases.Where(db => db.IsChecked).ToList();
            var databaseNames = selectedDatabases.Select(db => db.NazwaBazy).ToList();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    foreach (var database in selectedDatabases)
                    {
                        connection.ChangeDatabase(database.NazwaBazy);

                        // Sprawdzenie, czy użytkownik istnieje
                        if (!UzytkownikIstnieje(connection, selectedUser))
                        {
                            // Jeśli użytkownik nie istnieje, dodaj go
                            UtworzUzytkownika(connection, selectedUser);
                          //  MessageBox.Show($"Użytkownik '{selectedUser}' został utworzony w bazie danych {database.NazwaBazy}.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        // Dodawanie użytkownika do roli db_owner
                        string query = $@"
                    ALTER ROLE db_owner ADD MEMBER [{selectedUser}]";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.ExecuteNonQuery();
                    }
                }

                // Odświeżanie listy właścicieli baz danych
                OdświeżDbOwners();

                MessageBox.Show($"Wybrane bazy danych zostały dodane do użytkownika: {selectedUser}:\n{string.Join("\n", databaseNames)}", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UsunUzytkownika(SqlConnection connection, string userName)
        {
            string dropUserQuery = $"DROP USER [{userName}]";
            using (SqlCommand command = new SqlCommand(dropUserQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        private void UsunZaznaczoneBazUzytkownikowi_Click(object sender, RoutedEventArgs e)
        {
            if (UsersComboBox.SelectedItem == null)
            {
                MessageBox.Show("Wybierz użytkownika", "Uwaga", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string selectedUser = UsersComboBox.SelectedItem.ToString();
            var selectedDatabases = allDatabases.Where(db => db.IsChecked).ToList();
            var databaseNames = selectedDatabases.Select(db => db.NazwaBazy).ToList();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    foreach (var database in selectedDatabases)
                    {
                        connection.ChangeDatabase(database.NazwaBazy);

                        // Odpinanie użytkownika od roli db_owner
                        string query = $@"
                    ALTER ROLE db_owner DROP MEMBER [{selectedUser}]";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.ExecuteNonQuery();

                        // Usunięcie użytkownika z bazy danych
                        UsunUzytkownika(connection, selectedUser);

                      //  MessageBox.Show($"Użytkownik '{selectedUser}' został usunięty z bazy danych {database.NazwaBazy}.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }

                // Odświeżanie listy właścicieli baz danych
                MessageBox.Show("Użytkownik usunięty");
                OdświeżDbOwners();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void OProgramieMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Informacje OknoInfo = new Informacje();
            OknoInfo.Show();
        }

        private void UstawieniaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Ustawienia OknoUstawienia = new Ustawienia();
            OknoUstawienia.Show();
        }

        private void ZaznaczBazyButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsersComboBox.SelectedItem == null)
            {
                MessageBox.Show("Wybierz użytkownika", "Uwaga", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string selectedUser = UsersComboBox.SelectedItem.ToString();

            foreach (var database in allDatabases)
            {
                if (database.DbOwners.Contains(selectedUser))
                {
                    database.IsChecked = true;
                }
            }

            Wyszukiwanie_Baz();
        }

        private void KopiowanieUzytkownikaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            KopiowanieUprawnienUzytkownikow Okno = new KopiowanieUprawnienUzytkownikow(connectionString);
            Okno.Show();
        }

        private void UtworzUzytkownika(SqlConnection connection, string userName)
        {
            string createUserQuery = $"CREATE USER [{userName}] FOR LOGIN [{userName}]";
            using (SqlCommand command = new SqlCommand(createUserQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        private void OdświeżDbOwners()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                foreach (var database in allDatabases)
                {
                    connection.ChangeDatabase(database.NazwaBazy);
                    database.DbOwners = GetDbOwners(connection);
                }
            }
        }
    }
}
