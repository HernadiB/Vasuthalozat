﻿using System;
using System.Windows;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using BCrypt.Net;

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
            try
            {
                if (this.tb_jelszo.Password != string.Empty || this.tb_felhasznalonev.Text != string.Empty)
                {
                    //bool verified = BCrypt.Net.BCrypt.Verify(this.tb_jelszo.Password, Hashing.HashPassword(this.tb_jelszo.Password));
                    //if (verified)
                    //{
                        connection.Open();
                        //cmd = new SqlCommand("SELECT * FROM felhasznalo WHERE felhasznalonev = '" + this.tb_felhasznalonev.Text + "' AND jelszo = '" + this.tb_jelszo.Password + "'");
                        cmd = new SqlCommand("SELECT * FROM felhasznalo WHERE felhasznalonev = '" + this.tb_felhasznalonev.Text + "' AND jelszo = '" + this.tb_jelszo.Password + "'");
                        //cmd.Parameters.Add(new SqlParameter("@felhasznalonev", this.tb_felhasznalonev.Text));
                        //var hashed = (string)cmd.ExecuteScalar();
                        //string hashedPass = Hashing.ValidatePassword(this.tb_jelszo.Password, correctHash);
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
                    //}
                    //else
                    //{
                    //    connection.Close();
                    //    MessageBox.Show("Nincs ilyen felhasználónévhez tartozó jelszó!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    //}

                }
                else
                {
                    MessageBox.Show("A összes mező kitöltése kötelező!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
