using System;
using System.Windows;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;

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
        public static string hashedPassword = "";

        private void btn_regisztracio(object sender, RoutedEventArgs e)
        {
            if (this.tb_jelszo_megerosites.Password != string.Empty || this.tb_jelszo.Password != string.Empty || this.tb_felhasznalonev.Text != string.Empty)
            {
                if (this.tb_felhasznalonev.Text == this.tb_jelszo.Password)
                {
                    MessageBox.Show("A felhasználónév és jelszó nem egyezhet!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (this.tb_jelszo.Password == this.tb_jelszo_megerosites.Password)
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
                            //string hashedPassword = Hashing.HashPassword(this.tb_jelszo.Password);
                            //hashedPassword = Hashing.HashPassword(this.tb_jelszo.Password);
                            //cmd.Parameters.AddWithValue("jelszo", hashedPassword);
                            cmd.Parameters.AddWithValue("jelszo", this.tb_jelszo.Password);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Sikeres regisztráció. Bejelentkezés!", "Kész", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Hide();
                            UserFooldal userfooldal = new UserFooldal();
                            userfooldal.ShowDialog();
                            connection.Close();
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
            connection.Close();
            this.Hide();
            MainWindow bejelenzes = new MainWindow();
            bejelenzes.ShowDialog();
        }
    }
}
