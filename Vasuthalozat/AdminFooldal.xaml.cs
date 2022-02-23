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
            cb_regi_modosit_kiindulo();
            cb_regi_modosit_cel();
            cb_modosit_varos();
            cb_torol_varos();
            torol_cb_kiindulo();
            torol_cb_cel();
        }

        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["VasuthalozatConnectionString"].ConnectionString);
        SqlCommand comHozzaad = new SqlCommand();
        SqlCommand comModosit = new SqlCommand();
        SqlCommand comTorol = new SqlCommand();

        private void btn_beolvas(object sender, RoutedEventArgs e)
        {
            varos_beolvas();
            jarat_beolvas();
            felhasznalo_beolvas();
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

        private void felhasznalo_beolvas()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM felhasznalo ORDER BY felhasznalonev ASC", connection);
                DataTable dt = new DataTable();
                connection.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                connection.Close();
                felhasznalok.ItemsSource = dt.DefaultView;
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
                if (tb_hozzaad_km.Text == String.Empty || tb_hozzaad_kiindulo.Text == String.Empty || tb_hozzaad_cel.Text == String.Empty)
                {
                    MessageBox.Show("Az összes járat hozzáadása mező kitöltése kötelező!");
                }
                else if (tb_hozzaad_kiindulo.Text == tb_hozzaad_cel.Text)
                {
                    MessageBox.Show("A kiinduló és cél állomás nem lehet azonos");
                }
                else
                {
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
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void btn_modosit_jarat(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Open();
                comModosit.Connection = connection;
                comModosit.CommandText = @"UPDATE jaratok SET kiindulo = '" + tb_uj_kiindulo_modosit.Text + "', cel = '" + tb_uj_cel_modosit.Text + "', km = '" + tb_modosit_km_uj.Text + "' WHERE kiindulo = '" + cb_regi_kiindulo_modosit.SelectedItem + "' AND cel = '" + cb_regi_cel_modosit.SelectedItem + "'";
                comModosit.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("A járat sikeresen módosítva!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_torol_jarat(object sender, RoutedEventArgs e)
        {
            if (cb_kiindulo_torol.SelectedItem == cb_cel_torol.SelectedItem)
            {
                MessageBox.Show("A kiinduló és cél város nem lehet azonos!");
            }
            else
            {
                try
                {
                    connection.Open();
                    comTorol.Connection = connection;
                    comTorol.CommandText = @"DELETE FROM jaratok WHERE kiindulo = '" + this.cb_kiindulo_torol.SelectedItem + "' AND cel = '" + cb_cel_torol.SelectedItem + "'";
                    comTorol.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("A járat sikeresen törölve!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                connection.Close();
            }
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

        private void cb_regi_modosit_kiindulo()
        {
            try
            {
                ObservableCollection<string> list = new ObservableCollection<string>();
                SqlCommand cmd_kiindulo_modosit = new SqlCommand("SELECT kiindulo FROM jaratok GROUP BY kiindulo ORDER BY kiindulo", connection);
                connection.Open();
                SqlDataReader dr = cmd_kiindulo_modosit.ExecuteReader();
                while (dr.Read())
                {
                    string kiindulo = dr.GetString(0);
                    list.Add(kiindulo);
                }
                cb_regi_kiindulo_modosit.ItemsSource = list;
                cb_regi_kiindulo_modosit.SelectedIndex = 0;
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

        private void cb_regi_modosit_cel()
        {
            try
            {
                ObservableCollection<string> list = new ObservableCollection<string>();
                SqlCommand cmd_cel_modosit = new SqlCommand("SELECT cel FROM jaratok GROUP BY cel ORDER BY cel", connection);
                connection.Open();
                SqlDataReader dr = cmd_cel_modosit.ExecuteReader();
                while (dr.Read())
                {
                    string cel = dr.GetString(0);
                    list.Add(cel);
                }
                cb_regi_cel_modosit.ItemsSource = list;
                cb_regi_cel_modosit.SelectedIndex = 1;
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
            try
            {
                ObservableCollection<string> list = new ObservableCollection<string>();
                SqlCommand cmd_kiindulo_torol = new SqlCommand("SELECT kiindulo FROM jaratok GROUP BY kiindulo ORDER BY kiindulo", connection);
                connection.Open();
                SqlDataReader dr = cmd_kiindulo_torol.ExecuteReader();
                while (dr.Read())
                {
                    string name = dr.GetString(0);
                    list.Add(name);
                }
                cb_kiindulo_torol.ItemsSource = list;
                cb_kiindulo_torol.SelectedIndex = 0;
                connection.Close();
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void torol_cb_cel()
        {

            try
            {
                ObservableCollection<string> list = new ObservableCollection<string>();
                SqlCommand cmd_cel_torol = new SqlCommand("SELECT cel FROM jaratok GROUP BY cel ORDER BY cel", connection);
                connection.Open();
                SqlDataReader dr = cmd_cel_torol.ExecuteReader();
                while (dr.Read())
                {
                    string cel = dr.GetString(0);
                    list.Add(cel);
                }
                cb_cel_torol.ItemsSource = list;
                cb_cel_torol.SelectedIndex = 1;
                connection.Close();
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void torol_cb_felhasznalo()
        {
            try
            {
                ObservableCollection<string> list = new ObservableCollection<string>();
                SqlCommand cmd_felhasznalo_torol = new SqlCommand("SELECT felhasznalonev FROM felhasznalo ORDER BY felhasznalonev", connection);
                connection.Open();
                SqlDataReader dr = cmd_felhasznalo_torol.ExecuteReader();
                while (dr.Read())
                {
                    string felhasznalo = dr.GetString(0);
                    list.Add(felhasznalo);
                }
                cb_cel_torol.ItemsSource = list;
                cb_cel_torol.SelectedIndex = 0;
                connection.Close();
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_kilep(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Admin adminbejelentkezes = new Admin();
            adminbejelentkezes.ShowDialog();
        }

        private void btn_felhasznalo_torles(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Open();
                cb_felhasznalo_torles.SelectedIndex = 0;
                comTorol.Connection = connection;
                comTorol.CommandText = @"DELETE FROM felhasznalo WHERE felhasznalonev = '" + this.cb_felhasznalo_torles.SelectedItem + "'";
                comTorol.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("A felhasználó sikeresen törölve!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connection.Close();
        }

        private void btn_frissites(object sender, RoutedEventArgs e)
        {
            //this.DataContext = new MyDataContext();
        }
    }
}
