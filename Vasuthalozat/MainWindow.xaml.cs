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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Controls.Primitives;
using System.Diagnostics;
using Vasuthalozat.Properties;
using Vasuthalozat;
using System.Configuration;

namespace Vasuthalozat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["VasuthalozatConnectionString"].ConnectionString);
        SqlCommand cmd;
        SqlDataReader dr;

        private void btn_bezaras(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btn_bejelentkezes(object sender, RoutedEventArgs e)
        {
            if (this.tb_jelszo.Text != string.Empty || this.tb_felhasznalonev.Text != string.Empty)
            {
                connection.Open();
                cmd = new SqlCommand("SELECT * FROM felhasznalo WHERE felhasznalonev = '" + this.tb_felhasznalonev.Text + "' AND jelszo = '" + this.tb_jelszo.Text + "'");
                cmd.Connection = connection;
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    dr.Close();
                    this.Hide();
                    UserFooldal userfooldal = new UserFooldal();
                    userfooldal.ShowDialog();
                }
                else
                {
                    dr.Close();
                    MessageBox.Show("Nincs ilyen felhasználónév jelszó páros!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("A összes mező kitöltése kötelező!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_admin(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Admin admin = new Admin();
            admin.ShowDialog();
        }

        private void btn_regisztracio(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Regisztracio regisztracio = new Regisztracio();
            regisztracio.ShowDialog();
        }
    }
}
