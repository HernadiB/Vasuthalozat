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
using System.Data.SqlClient;
using System.Configuration;

namespace Vasuthalozat
{
    /// <summary>
    /// Interaction logic for Regisztracio.xaml
    /// </summary>
    public partial class Regisztracio : Window
    {
        public Regisztracio()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["VasuthalozatConnectionString"].ConnectionString);
        SqlCommand cmd;
        SqlDataReader dr;

        private void btn_regisztracio(object sender, RoutedEventArgs e)
        {
            if (this.tb_jelszo_megerosites.Text != string.Empty || this.tb_jelszo.Text != string.Empty || this.tb_felhasznalonev.Text != string.Empty)
            {
                if (this.tb_felhasznalonev.Text == this.tb_jelszo.Text)
                {
                    MessageBox.Show("A felhasználónév és jelszó nem egyezhet!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (this.tb_jelszo.Text == this.tb_jelszo_megerosites.Text)
                    {
                        connection.Open();
                        cmd = new SqlCommand("SELECT * FROM felhasznalo WHERE felhasznalonev = '" + this.tb_felhasznalonev.Text + "'", connection);
                        cmd.Connection = connection;
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            dr.Close();
                            MessageBox.Show("Ez a felhasználónév már foglalt!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            dr.Close();
                            cmd = new SqlCommand("INSERT INTO felhasznalo VALUES(@felhasznalonev, @jelszo)", connection);
                            cmd.Parameters.AddWithValue("felhasznalonev", this.tb_felhasznalonev.Text);
                            cmd.Parameters.AddWithValue("jelszo", this.tb_jelszo.Text);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Sikeres regisztráció . Bejelentkezés!", "Kész", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Hide();
                            UserFooldal userfooldal = new UserFooldal();
                            userfooldal.ShowDialog();
                        }
                    }
                    else
                    {
                        MessageBox.Show("A két jelszó nem egyezik!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
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

        private void btn_bejelentkezes(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow bejelenzes = new MainWindow();
            bejelenzes.ShowDialog();
        }
    }
}
