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
    /// Logika interakcji dla klasy Ustawienia.xaml
    /// </summary>
    public partial class Ustawienia : Window
    {
        public Ustawienia()
        {
            InitializeComponent();
            LadowanieUstawien();
        }

        private void LadowanieUstawien()
        {
            string Ustawienie1 = ConfigurationManager.AppSettings["Ustawienia-ZaznaczajBazy"];
            if (Ustawienie1 == "true")
            {
                ZaznaczajBazyWybranegoUzytkownikaCheckBox.IsChecked = true;
            }
            else
            {
                ZaznaczajBazyWybranegoUzytkownikaCheckBox.IsChecked = false;
            }

        }

        private void ZapiszButton_Click(object sender, RoutedEventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if(ZaznaczajBazyWybranegoUzytkownikaCheckBox.IsChecked == true)
            {
                config.AppSettings.Settings["Ustawienia-ZaznaczajBazy"].Value = "true";
            }
            else
            {
                config.AppSettings.Settings["Ustawienia-ZaznaczajBazy"].Value = "false";
            }
            config.Save(ConfigurationSaveMode.Modified);
        }
    }
}
