using System;
using System.Windows;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.ObjectModel;

namespace Vasuthalozat
{
    /// <summary>
    /// Interaction logic for UserFooldal.xaml
    /// </summary>
    public partial class UserFooldal : Window
    {
        public UserFooldal()
        {
            InitializeComponent();
            cb_kiindulo();
            cb_cel();
        }
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["VasuthalozatConnectionString"].ConnectionString);
        protected static int koltseg => 15;

        public void cb_kiindulo()
        {
            try
            {
                ObservableCollection<string> list = new ObservableCollection<string>();
                SqlCommand cmd_kiindulo = new SqlCommand("SELECT kiindulo FROM jaratok GROUP BY kiindulo ORDER BY kiindulo ASC", connection);
                connection.Open();
                SqlDataReader dr = cmd_kiindulo.ExecuteReader();
                while (dr.Read())
                {
                    string name = dr.GetString(0);
                    list.Add(name);
                }
                kiindulo_cb.ItemsSource = list;
                kiindulo_cb.SelectedIndex = 0;
                connection.Close();
                dr.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public void cb_cel()
        {
            try
            {
                ObservableCollection<string> list = new ObservableCollection<string>();
                SqlCommand cmd_cel = new SqlCommand("SELECT cel FROM jaratok GROUP BY cel ORDER BY cel ASC", connection);
                connection.Open();
                SqlDataReader dr = cmd_cel.ExecuteReader();
                while (dr.Read())
                {
                    string name = dr.GetString(0);
                    list.Add(name);
                }
                cel_cb.ItemsSource = list;
                cel_cb.SelectedIndex = 1;
                connection.Close();
                dr.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btn_beolvasas(object sender, RoutedEventArgs e)
        {

        }

        private void btn_megjelenit(object sender, RoutedEventArgs e)
        {

        }

        private void btn_foglal(object sender, RoutedEventArgs e)
        {

        }

        private void btn_kilep(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow userbejelentkezes= new MainWindow();
            userbejelentkezes.ShowDialog();
        }
    }
}
