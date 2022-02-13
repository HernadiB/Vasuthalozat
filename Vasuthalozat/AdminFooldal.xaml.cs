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
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.ObjectModel;

namespace Vasuthalozat
{
    /// <summary>
    /// Interaction logic for AdminFooldal.xaml
    /// </summary>
    public partial class AdminFooldal : Window
    {
        public AdminFooldal()
        {
            InitializeComponent();
            cb_modosit_kiindulo();
            cb_modosit_cel();
            cb_modosit_varos();
            cb_torol_varos();
            torol_cb_kiindulo();
            torol_cb_cel();
            //torol_tb_km();
        }

        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["VasuthalozatConnectionString"].ConnectionString);
        SqlCommand comHozzaad = new SqlCommand();
        SqlCommand comModosit = new SqlCommand();
        SqlCommand comTorol = new SqlCommand();

        private void btn_beolvas(object sender, RoutedEventArgs e)
        {
            varos_beolvas();
            jarat_beolvas();
        }

        private void varos_beolvas()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM varosok ORDER BY nev ASC", connection);
                DataTable dt = new DataTable();
                connection.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                connection.Close();
                varosok.ItemsSource = dt.DefaultView;
                sdr.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            connection.Close();
        }

        private void jarat_beolvas()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM jaratok ORDER BY kiindulo ASC", connection);
                DataTable dt = new DataTable();
                connection.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                connection.Close();
                jaratok.ItemsSource = dt.DefaultView;
                sdr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connection.Close();
        }

        private void btn_hozzad_varos(object sender, RoutedEventArgs e)
        {
            try
            {
                string tb_h_varos = tb_hozzaad_varos.Text.ToString();
                if (tb_h_varos == String.Empty)
                {
                    MessageBox.Show("A város hozzáadása mező kitöltése kötelező!");
                }
                else
                {
                    comHozzaad.Connection = connection;
                    connection.Open();
                    comHozzaad.CommandText = @"INSERT INTO varosok (nev) VALUES (@nev)";
                    comHozzaad.Parameters.AddWithValue("@nev", tb_hozzaad_varos.Text);
                    comHozzaad.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("A város sikeresen hozzáadva!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connection.Close();
        }

        private void btn_modosit_varos(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Open();
                comModosit.Connection = connection;
                comModosit.CommandText = @"UPDATE varosok SET nev = '" + this.tb_modosit_varos_uj.Text + "' WHERE nev = '" + this.cb_varos_modosit.SelectedItem + "'";
                comModosit.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("A város sikeresen módosítva!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connection.Close();
        }

        private void btn_torles_varos(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Open();
                cb_varos_torol.SelectedIndex = 0;
                comTorol.Connection = connection;
                comTorol.CommandText = @"DELETE FROM varosok WHERE nev = '" + this.cb_varos_torol.SelectedItem + "'";
                comTorol.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("A város sikeresen törölve!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connection.Close();
        }

        private void btn_hozzad_jarat(object sender, RoutedEventArgs e)
        {
            try
            {
                string tb_kiindulo = tb_hozzaad_kiindulo.Text.ToString();
                string tb_cel = tb_hozzaad_cel.Text.ToString();
                if (tb_hozzaad_km.Text == String.Empty || tb_hozzaad_kiindulo.Text == String.Empty || tb_hozzaad_cel.Text == String.Empty)
                {
                    MessageBox.Show("Az összes mező kitöltése kötelező!");
                }
                comHozzaad.Connection = connection;
                connection.Open();
                comHozzaad.CommandText = @"INSERT INTO jaratok (kiindulo, cel, km) VALUES (@kiindulo, @cel, @km)";
                comHozzaad.Parameters.AddWithValue("@kiindulo", tb_hozzaad_kiindulo.Text);
                comHozzaad.Parameters.AddWithValue("@cel", tb_hozzaad_cel.Text);
                comHozzaad.Parameters.AddWithValue("@km", Convert.ToInt32(tb_hozzaad_km.Text));
                comHozzaad.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Az adatok sikeresen elmentve");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void btn_modosit_jarat(object sender, RoutedEventArgs e)
        {

        }

        private void btn_torol_jarat(object sender, RoutedEventArgs e)
        {
            ObservableCollection<string> list = new ObservableCollection<string>();
            SqlCommand cmd_kiindulo_torol = new SqlCommand("SELECT * FROM jaratok", connection);
            connection.Open();
            SqlDataReader dr = cmd_kiindulo_torol.ExecuteReader();
            while (dr.Read())
            {
                string name = dr.GetString(1);
                list.Add(name);
            }
            cb_kiindulo_torol.ItemsSource = list;
            cb_kiindulo_torol.SelectedIndex = 0;
            connection.Close();
            dr.Close();

        }

        public void cb_modosit_varos()
        {
            try
            {
                ObservableCollection<string> list = new ObservableCollection<string>();
                SqlCommand cmd_varos_modosit = new SqlCommand("SELECT * FROM varosok ORDER BY nev ASC", connection);
                connection.Open();
                SqlDataReader dr = cmd_varos_modosit.ExecuteReader();
                while (dr.Read())
                {
                    string nev = dr.GetString(0);
                    list.Add(nev);
                }
                cb_varos_modosit.ItemsSource = list;
                cb_varos_modosit.SelectedIndex = 0;
                connection.Close();
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        public void cb_torol_varos()
        {
            try
            {
                ObservableCollection<string> list = new ObservableCollection<string>();
                SqlCommand cmd_varos_torol = new SqlCommand("SELECT nev FROM varosok ORDER BY nev ASC", connection);
                connection.Open();
                SqlDataReader dr = cmd_varos_torol.ExecuteReader();
                while (dr.Read())
                {
                    string nev = dr.GetString(0);
                    list.Add(nev);
                }
                cb_varos_torol.ItemsSource = list;
                cb_varos_torol.SelectedIndex = 0;
                connection.Close();
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
            connection.Close();
        }

        private void cb_modosit_kiindulo()
        {
            try
            {
                ObservableCollection<string> list = new ObservableCollection<string>();
                SqlCommand cmd_kiindulo_modosit = new SqlCommand("SELECT * FROM jaratok", connection);
                connection.Open();
                SqlDataReader dr = cmd_kiindulo_modosit.ExecuteReader();
                while (dr.Read())
                {
                    string kiindulo = dr.GetString(1);
                    list.Add(kiindulo);
                }
                cb_kiindulo_modosit.ItemsSource = list;
                cb_kiindulo_modosit.SelectedIndex = 0;
                connection.Close();
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
            connection.Close();
        }

        private void cb_modosit_cel()
        {
            try
            {
                ObservableCollection<string> list = new ObservableCollection<string>();
                SqlCommand cmd_cel_modosit = new SqlCommand("SELECT * FROM jaratok", connection);
                connection.Open();
                SqlDataReader dr = cmd_cel_modosit.ExecuteReader();
                while (dr.Read())
                {
                    string cel = dr.GetString(1);
                    list.Add(cel);
                }
                cb_cel_modosit.ItemsSource = list;
                cb_cel_modosit.SelectedIndex = 1;
                connection.Close();
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
            connection.Close();
        }

        public void torol_cb_kiindulo()
        {
            ObservableCollection<string> list = new ObservableCollection<string>();
            SqlCommand cmd_kiindulo_torol = new SqlCommand("SELECT * FROM jaratok", connection);
            connection.Open();
            SqlDataReader dr = cmd_kiindulo_torol.ExecuteReader();
            while (dr.Read())
            {
                string name = dr.GetString(1);
                list.Add(name);
            }
            cb_kiindulo_torol.ItemsSource = list;
            cb_kiindulo_torol.SelectedIndex = 0;
            connection.Close();
            dr.Close();
        }

        public void torol_cb_cel()
        {

            ObservableCollection<string> list = new ObservableCollection<string>();
            SqlCommand cmd_cel_torol = new SqlCommand("SELECT * FROM jaratok", connection);
            connection.Open();
            SqlDataReader dr = cmd_cel_torol.ExecuteReader();
            while (dr.Read())
            {
                string cel = dr.GetString(1);
                list.Add(cel);
            }
            cb_cel_torol.ItemsSource = list;
            //cb_cel_torol.SelectionChanged += Cb_cel_torol_SelectionChanged;
            cb_cel_torol.SelectedIndex = 1;
            connection.Close();
            dr.Close();
        }

        //private void Cb_cel_torol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //private void torol_tb_km()
        //{
        //    int km = int.Parse(tb_torol_km.Text);
        //}

        private void btn_kilep(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Admin adminbejelentkezes = new Admin();
            adminbejelentkezes.ShowDialog();
        }

    }
}
