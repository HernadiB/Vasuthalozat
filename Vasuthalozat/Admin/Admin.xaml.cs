using System;
using System.Windows;
using System.Data.SqlClient;
using System.Configuration;

namespace Vasuthalozat
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["VasuthalozatConnectionString"].ConnectionString);
        SqlCommand cmd;
        SqlDataReader dr;

        private void btn_bejelentkezes(object sender, RoutedEventArgs e)
        {
            if (this.tb_jelszo.Password != string.Empty || this.tb_felhasznalonev.Text != string.Empty)
            {
                connection.Open();
                cmd = new SqlCommand("SELECT * FROM admin WHERE felhasznalonev = '" + this.tb_felhasznalonev.Text + "' AND jelszo = '" + this.tb_jelszo.Password + "'");
                cmd.Connection = connection;
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    dr.Close();
                    this.Hide();
                    AdminFooldal adminfooldal = new AdminFooldal();
                    adminfooldal.ShowDialog();
                }
                else
                {
                    dr.Close();
                    MessageBox.Show("Nincs ilyen felhasználónév jelszó páros!\nVegye fel a kapcsolatot egy rendszergazdával!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("A összes mező kitöltése kötelező!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_bezaras(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btn_user(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow userbejelentkezes = new MainWindow();
            userbejelentkezes.ShowDialog();
        }
    }
}
