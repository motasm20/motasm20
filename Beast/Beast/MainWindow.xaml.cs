using Beast.Classes;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Beast
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DbHandler db = new DbHandler();

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Beast_nummer_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string beastnummer = tbRijnummer.Text;
                DataTable result = db.beastnummer(beastnummer);


                string kleur = result.Rows[0]["kleur"].ToString().ToLower();

                switch (kleur)
                {
                    case "red":
                        tbNumber.Background = Brushes.Red;
                        break;
                    case "blue":
                        tbNumber.Background = Brushes.Blue;
                        break;
                    case "yellow":
                        tbNumber.Background = Brushes.Yellow;
                        break;
                    case "black":
                        tbNumber.Background = Brushes.Black;
                        break;
                    case "green":
                        tbNumber.Background = Brushes.Green;
                        break;
                    default:
                        MessageBox.Show("Ongeldige kleur.");
                        return;
                }

                tbNumber.Text = result.Rows[0]["nummer"].ToString();



            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Er is een fout opgetreden bij het bijwerken van de gegevens: " + ex.Message);
            }

        }
        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable result = db.GetNumber();
                dataGrid.ItemsSource = result.DefaultView;
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Er is een fout opgetreden bij het bijwerken van de gegevens: " + ex.Message);
            }

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string rijnummer = tbRijnummer.Text;
            string waarde = tbValue.Text;
            string kleur = tbKleur.SelectedItem != null ? ((ComboBoxItem)tbKleur.SelectedItem).Content.ToString() : "";

            if (string.IsNullOrEmpty(rijnummer) || string.IsNullOrEmpty(waarde) || string.IsNullOrEmpty(kleur))
            {
                MessageBox.Show("Vul alle velden in voordat u kunt bijwerken.");
                return; // Stop de methode als een van de velden leeg is
            }

            int rowsAffected = db.UpdateNumber(waarde, rijnummer, kleur);
            if (rowsAffected < 0)

            {
                MessageBox.Show("Fout opgetreden bij het bijwerken van de database");
            }
            btnGo_Click(null, null);
            Beast_nummer_Click(null, null);
        }

        private void Toevoegen_Click(object sender, RoutedEventArgs e)
        {
            string waarde = tbValue.Text;
            string kleur = tbKleur.SelectedItem != null ? ((ComboBoxItem)tbKleur.SelectedItem).Content.ToString() : "";

            if (string.IsNullOrEmpty(waarde) || string.IsNullOrEmpty(kleur))
            {
                MessageBox.Show("Vul alle velden in voordat u kunt bijwerken.");
                return; // Stop de methode als een van de velden leeg is
            }

            int rowsAffected = db.toevoegen(waarde, kleur);
            if (rowsAffected < 0)

            {
                MessageBox.Show("Fout opgetreden bij het bijwerken van de database");
            }

            btnGo_Click(null, null);
        }

        private void Vrwijderen_Click(object sender, RoutedEventArgs e)
        {
            string rijnummer = tbRijnummer.Text;

            if (string.IsNullOrEmpty(rijnummer))
            {
                MessageBox.Show("Vul de rijnummer die u wilt verijderen");
                return; // Stop de methode als een van de velden leeg is
            }

            int rowsAffected = db.Verwijderen(rijnummer);
            if (rowsAffected < 0)

            {
                MessageBox.Show("Fout opgetreden bij het bijwerken van de database");
            }
            btnGo_Click(null, null);
        }
    }
}
