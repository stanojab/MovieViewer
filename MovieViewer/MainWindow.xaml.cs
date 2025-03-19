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
        public MainWindow()
        {
            InitializeComponent();
            Movies = new ObservableCollection<Movie>
            {
                new Movie
    {
        Name = "Inception",
        Director = "Christopher Nolan",
        ReleaseYear = "2010",
        Description = "A thief who enters the dreams of others to steal secrets from their subconscious.",
        Actors = new ObservableCollection<string> { "Leonardo DiCaprio", "Joseph Gordon-Levitt", "Elliot Page" },
        Genres = new ObservableCollection<string> { "Sci-Fi", "Thriller" },
        ImagePath = "C:\\Users\\STANOJA\\Desktop\\faks2sem3god\\MovieViewer\\MovieViewer\\inception.jpg",
        IsFavorite = false
    },

    new Movie
    {
        Name = "Interstellar",
        Director = "Christopher Nolan",
        ReleaseYear = "2014",
        Description = "A team of explorers travel through a wormhole in space in an attempt to ensure humanity's survival.",
        Actors = new ObservableCollection<string> { "Matthew McConaughey", "Anne Hathaway", "Jessica Chastain" },
        Genres = new ObservableCollection<string> { "Sci-Fi", "Adventure", "Drama" },
        ImagePath = "C:\\Users\\STANOJA\\Desktop\\faks2sem3god\\MovieViewer\\MovieViewer\\inception.jpg",
        IsFavorite = true
    },

    new Movie
    {
        Name = "The Dark Knight",
        Director = "Christopher Nolan",
        ReleaseYear = "2008",
        Description = "When a menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman, James Gordon and Harvey Dent must work together to put an end to the madness.",
        Actors = new ObservableCollection<string> { "Christian Bale", "Heath Ledger", "Aaron Eckhart" },
        Genres = new ObservableCollection<string> { "Action", "Crime", "Drama" },
        ImagePath = "C:\\Users\\STANOJA\\Desktop\\faks2sem3god\\MovieViewer\\MovieViewer\\inception.jpg",
        IsFavorite = false
    }
            };
            DataContext = this;
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
            // Get the clicked button
            if (sender is Button button && button.DataContext is Movie movie)
            {
                movie.ToggleFavorite(); // Call the method to toggle favorite
            }
        }

        private void Izhod_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }

}