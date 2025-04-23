using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public Movie EditableMovie { get; set; }
        private string _imagePath;

        public void UpdateMovie(Movie movie)
        {
            // Update the editable copy instead of replacing DataContext
            EditableMovie.Name = movie.Name;
            EditableMovie.Director = movie.Director;
            EditableMovie.ReleaseYear = movie.ReleaseYear;
            EditableMovie.Description = movie.Description;
            EditableMovie.Rating = movie.Rating;
            EditableMovie.ImagePath = movie.ImagePath;

            EditableMovie.Genres = new ObservableCollection<string>(movie.Genres);
            EditableMovie.Actors = new ObservableCollection<string>(movie.Actors);

            // Trigger property changes
            OnPropertyChanged(nameof(EditableMovie));
            OnPropertyChanged(nameof(GenresText));
            OnPropertyChanged(nameof(ActorsText));
        }

        public string GenresText
        {
            get => string.Join(", ", EditableMovie.Genres);
            set
            {
                EditableMovie.Genres = new System.Collections.ObjectModel.ObservableCollection<string>(
                    value.Split(',').Select(g => g.Trim()));
                OnPropertyChanged(nameof(GenresText));
            }
        }

        public string ActorsText
        {
            get => string.Join(", ", EditableMovie.Actors);
            set
            {
                EditableMovie.Actors = new System.Collections.ObjectModel.ObservableCollection<string>(
                    value.Split(',').Select(a => a.Trim()));
                OnPropertyChanged(nameof(ActorsText));
            }
        }

        private readonly Movie _originalMovie;

        public EditWindow(Movie movie)
        {
            InitializeComponent();

            _originalMovie = movie;
            _imagePath = movie.ImagePath;
            EditableMovie = new Movie
            {
                Name = movie.Name,
                Director = movie.Director,
                ReleaseYear = movie.ReleaseYear,
                Rating = movie.Rating,
                Description = movie.Description,
                Genres = new System.Collections.ObjectModel.ObservableCollection<string>(movie.Genres),
                Actors = new System.Collections.ObjectModel.ObservableCollection<string>(movie.Actors),
                ImagePath = movie.ImagePath,
                IsFavorite = movie.IsFavorite

            };

            DataContext = this;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)

        {
            _originalMovie.OnPropertyChanged(nameof(Movie.Name));
            _originalMovie.OnPropertyChanged(nameof(Movie.Director));
            _originalMovie.OnPropertyChanged(nameof(Movie.ReleaseYear));
            _originalMovie.OnPropertyChanged(nameof(Movie.Description));
            _originalMovie.OnPropertyChanged(nameof(Movie.Rating));
            _originalMovie.OnPropertyChanged(nameof(Movie.ImagePath));
            _originalMovie.OnPropertyChanged(nameof(Movie.Genres));
            _originalMovie.OnPropertyChanged(nameof(Movie.Actors));


            Close();
        }

       

        private void BrowseImage_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
            };

            if (dialog.ShowDialog() == true)
            {
                string appDir = AppDomain.CurrentDomain.BaseDirectory;
                string destFolder = System.IO.Path.Combine(appDir, "Assets", "MovieImages");
                Directory.CreateDirectory(destFolder);

                string fileName = System.IO.Path.GetFileName(dialog.FileName);
                string destPath = System.IO.Path.Combine(destFolder, fileName);

                try
                {
                    if (!File.Exists(destPath))
                        File.Copy(dialog.FileName, destPath);

                    EditableMovie.ImagePath = $"Assets\\MovieImages\\{fileName}";
                    OnPropertyChanged(nameof(EditableMovie));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to copy image: " + ex.Message);
                }
            }

        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
