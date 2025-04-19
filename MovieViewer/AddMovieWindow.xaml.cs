using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


namespace MovieViewer
{
    public partial class AddMovieWindow : Window
    {
        public Movie? NewMovie { get; private set; }
        private string _imagePath = "";

        public AddMovieWindow()
        {
            InitializeComponent();
        }

        private void MovieImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpeg;*.jpg;*.png)|*.jpeg;*.jpg;*.png";

            if (openFileDialog.ShowDialog() == true)
            {
                _imagePath = openFileDialog.FileName;
                MovieImage.Source = new BitmapImage(new Uri(_imagePath));
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameBox.Text) ||
                string.IsNullOrWhiteSpace(DirectorBox.Text) ||
                string.IsNullOrWhiteSpace(YearBox.Text) ||
                string.IsNullOrWhiteSpace(DescriptionBox.Text) ||
                string.IsNullOrWhiteSpace(GenresBox.Text) ||
                string.IsNullOrWhiteSpace(ActorsBox.Text) ||
                string.IsNullOrWhiteSpace(RatingBox.Text) ||
                string.IsNullOrWhiteSpace(_imagePath))
                
            {
                MessageBox.Show("Please fill all fields and select an image.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!double.TryParse(RatingBox.Text, out double rating) || rating<0)
            {
                MessageBox.Show("Rating must be a positive number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NewMovie = new Movie
            {
                Name = NameBox.Text,
                Director = DirectorBox.Text,
                ReleaseYear = YearBox.Text,
                Description = DescriptionBox.Text,
                Genres = new ObservableCollection<string>(GenresBox.Text.Split(',')),
                Actors = new ObservableCollection<string>(ActorsBox.Text.Split(',')),
                Rating = rating,
                ImagePath = _imagePath,
                IsFavorite = false
            };

            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
