using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MovieViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Movie> Movies { get; set; }
        public Movie? SelectedMovie { get; set; }
        public MovieViewModel ViewModel { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MovieViewModel();
            DataContext = ViewModel;
        }

        private void MovieListView_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (MovieListView.SelectedItem is Movie selectedMovie)
            {
                MessageBox.Show($"Selected Movie: {selectedMovie.Name}", "Movie Info");
            }
        }

        private void ToggleFavorite_Click(object sender, RoutedEventArgs e)
        {
            
            if (sender is Button button && button.DataContext is Movie movie)
            {
                movie.ToggleFavorite(); 
            }
        }

        private void Izhod_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
       

    }

}