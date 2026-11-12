using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FilmekWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Film> filmek = new List<Film>();

        ICollectionView filmNezet;
        public MainWindow()
        {
            InitializeComponent();
            filmNezet = CollectionViewSource.GetDefaultView(filmek);
            dgFilmek.ItemsSource = filmNezet;
        }

        private void btnBetoltes_Click(object sender, RoutedEventArgs e)
        {
            filmek.Clear();

            filmek.Add(new Film("Csillagok háborúja", "sci-fi", 121, 8.6));
            filmek.Add(new Film("Titanic", "dráma", 195, 7.8));
            filmek.Add(new Film("A Gyűrűk Ura", "fantasy", 201, 8.8));
            filmek.Add(new Film("Mátrix", "sci-fi", 136, 8.7));
            filmek.Add(new Film("Shrek", "animáció", 90, 7.9));

            filmNezet.Filter = null;
            filmNezet.SortDescriptions.Clear();

            dgFilmek.Columns[1].Visibility = Visibility.Visible;
            dgFilmek.Columns[2].Visibility = Visibility.Visible;
            dgFilmek.Columns[3].Visibility = Visibility.Visible;

            pbFilmek.Value = filmek.Count;
        }


        // Gombok linqval
        private void btnDarab_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Filmek száma: " + filmek.Count);
        }

        private void btnLeghosszabb_Click(object sender, RoutedEventArgs e)
        {
            if (filmek.Count == 0)
                return;

            var leghosszabb = filmek.OrderByDescending(f => f.Hossz).First();

            MessageBox.Show("Leghosszabb film: " + leghosszabb.Cim +"\nHossz: " + leghosszabb.Hossz + " perc");
        }

        private void btnAtlag_Click(object sender, RoutedEventArgs e)
        {
            if (filmek.Count == 0)
                return;

            double atlag = filmek.Average(f => f.Ertekeles);

            MessageBox.Show("Átlag értékelés: " + atlag.ToString("0.0"));
        }

        private void btnSciFi_Click(object sender, RoutedEventArgs e)
        {
            filmNezet.Filter = item =>((Film)item).Mufaj == "sci-fi";
        }

        private void btnCimek_Click(object sender, RoutedEventArgs e)
        {
            dgFilmek.Columns[1].Visibility = Visibility.Collapsed;
            dgFilmek.Columns[2].Visibility = Visibility.Collapsed;
            dgFilmek.Columns[3].Visibility = Visibility.Collapsed;
        }

        private void btnRendezes_Click(object sender, RoutedEventArgs e)
        {
            filmNezet.Filter = null;
            filmNezet.SortDescriptions.Clear();

            filmNezet.SortDescriptions.Add(new SortDescription("Cim", ListSortDirection.Ascending));
        }

        private void dgFilmek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgFilmek.SelectedItem is Film film)
            {
                MessageBox.Show("Kiválasztott film: " + film.Cim +"\nÉrtékelés: " + film.Ertekeles);
            }
        }


    }
}