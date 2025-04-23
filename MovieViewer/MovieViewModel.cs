using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace MovieViewer
{
    public class MovieViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Movie> Movies { get; set; }
        private Movie? _selectedMovie;
        public Movie? SelectedMovie
        {
            get => _selectedMovie;
            set
            {
                _selectedMovie = value;
                OnPropertyChanged(nameof(SelectedMovie));
                CommandManager.InvalidateRequerySuggested();
                if (_editWindow != null && _editWindow.IsVisible)
                {
                    _editWindow.UpdateMovie(value);  
                }
            }
        }

        private EditWindow? _editWindow;

        public ICommand AddStaticMovieCommand { get; }

        public ICommand RemoveMovieCommand { get; }

        public ICommand EditStaticMovieCommand { get; }

        public ICommand OpenAddMovieCommand { get; }

        public ICommand OpenEditMovieWindowCommand { get; }

        public MovieViewModel()
        {
            Movies = new ObservableCollection<Movie>
            {
                new Movie
                {
                    Name = "Inception",
                    Director = "Christopher Nolan",
                    ReleaseYear = "2010",
                    Rating = 9.2,
                    Description = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O., but his tragic past may doom the project and his team to disaster.",
                    Actors = new ObservableCollection<string> { "Leonardo DiCaprio", "Joseph Gordon-Levitt", "Elliot Page" },
                    Genres = new ObservableCollection<string> { "Sci-Fi", "Thriller" },
                    ImagePath = "Assets\\MovieImages\\inception.jpg",
                    IsFavorite = false
                },

                new Movie
                {
                    Name = "Pulp Fiction",
                    Director = "Quentin Tarantino",
                    ReleaseYear = "1994",
                    Rating = 8.9,
                    Description = "The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
                    Actors = new ObservableCollection<string> { "John Travolta", "Uma Thurman", "Samuel L. Jackson" },
                    Genres = new ObservableCollection<string> { "Dark Comedy", "Drug Crime", "Gangster" },
                    ImagePath = "Assets\\MovieImages\\pulpFiction.jpg",
                    IsFavorite = true
                },

                new Movie
                {
                    Name = "The Dark Knight",
                    Director = "Christopher Nolan",
                    ReleaseYear = "2008",
                    Rating=9.1,
                    Description = "When a menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman, James Gordon and Harvey Dent must work together to put an end to the madness.",
                    Actors = new ObservableCollection<string> { "Christian Bale", "Heath Ledger", "Aaron Eckhart" },
                    Genres = new ObservableCollection<string> { "Action", "Crime", "Drama" },
                    ImagePath = "Assets\\MovieImages\\darkKnight.jpg",
                    IsFavorite = false
                }
            };

            AddStaticMovieCommand = new RelayCommand(AddStaticMovie);
            RemoveMovieCommand = new RelayCommand(
                  RemoveSelectedMovie,
                  CanRemoveMovie
             );
            EditStaticMovieCommand = new RelayCommand(EditStaticMovie,CanEditMovie);
            OpenAddMovieCommand = new RelayCommand(_ => OpenAddMovieDialog());
            OpenEditMovieWindowCommand = new RelayCommand(OpenEditMovieWindow, CanEditMovie);
        }


        private void AddStaticMovie(object obj)
        {
            {
                Movies.Add(new Movie
                {
                    Name = "The Shawshank Redemption",
                    Director = "Frank Darabont",
                    ReleaseYear = "1994",
                    Rating = 9.3,
                    Description = "A banker convicted of uxoricide forms a friendship over a quarter century with a hardened convict, while maintaining his innocence and trying to remain hopeful through simple compassion.",
                    Actors = new ObservableCollection<string> { "Tim Robbins", "Morgan Freeman" , "Bob Gunton" },
                    Genres = new ObservableCollection<string> { "Epic", "Prison Drama" },
                    ImagePath = "Assets\\MovieImages\\SShank.jpg",
                    IsFavorite = false
                });

                OnPropertyChanged(nameof(Movies));
            }
        }
        private bool CanRemoveMovie(object? parameter)
        {
            return SelectedMovie != null;
        }

        private void RemoveSelectedMovie(object? parameter)
        {
            if (SelectedMovie != null)
            {
                Movies.Remove(SelectedMovie);
                SelectedMovie = null;
            }
        }

        private void EditStaticMovie(object? parameter)
        {
            if (SelectedMovie != null)
            {
                SelectedMovie.Actors = new ObservableCollection<string> { "NewStar1 ", "NewStar2" , "NewStar3" };
            }
        }

        private bool CanEditMovie(object? parameter)
        {
            return SelectedMovie != null;
        }
        private void OpenAddMovieDialog()
        {
            var window = new AddMovieWindow();
           

            if (window.ShowDialog() == true && window.NewMovie != null)
            {
                Movies.Add(window.NewMovie);
            }
        }

        private void OpenEditMovieWindow(object? parameter)
        {
            if (_editWindow == null || !_editWindow.IsVisible)
            {

                _editWindow = new EditWindow(SelectedMovie);
           
                _editWindow.Owner = Application.Current.MainWindow;
                _editWindow.DataContext = this;
                _editWindow.Topmost = true;
                _editWindow.Show();
            }
            else
            {
                _editWindow.Activate();
                _editWindow.UpdateMovie(SelectedMovie);  
            }
        }




        public string GenresText
        {
            get => SelectedMovie != null ? string.Join(", ", SelectedMovie.Genres) : "";
            set
            {
                if (SelectedMovie != null)
                {
                    SelectedMovie.Genres = new ObservableCollection<string>(
                        value.Split(',').Select(g => g.Trim())
                    );
                    OnPropertyChanged(nameof(GenresText));
                }
            }
        }

        public string ActorsText
        {
            get => SelectedMovie != null ? string.Join(", ", SelectedMovie.Actors) : "";
            set
            {
                if (SelectedMovie != null)
                {
                    SelectedMovie.Actors = new ObservableCollection<string>(
                        value.Split(',').Select(a => a.Trim())
                    );
                    OnPropertyChanged(nameof(ActorsText));
                }
            }
        }

    }


}
