using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
            }
        }



        public ICommand AddStaticMovieCommand { get; }

        public ICommand RemoveMovieCommand { get; }

        public ICommand EditStaticMovieCommand { get; }

        public MovieViewModel()
        {
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

            AddStaticMovieCommand = new RelayCommand(AddStaticMovie);
            RemoveMovieCommand = new RelayCommand(
                 execute: RemoveSelectedMovie,
                 canExecute: CanRemoveMovie
             );
            EditStaticMovieCommand = new RelayCommand(execute:EditStaticMovie, canExecute: CanEditMovie);
        }


        private void AddStaticMovie(object obj)
        {
            {
                Movies.Add(new Movie
                {
                    Name = "The Shawshank Redemption",
                    Director = "Frank Darabont",
                    ReleaseYear = "1994",
                    Description = "A banker convicted of uxoricide forms a friendship over a quarter century with a hardened convict, while maintaining his innocence and trying to remain hopeful through simple compassion.",
                    Actors = new ObservableCollection<string> { "Tim Robbins", "Morgan Freeman" },
                    Genres = new ObservableCollection<string> { "Epic", "Prison Drama" },
                    ImagePath = "C:\\Users\\STANOJA\\Desktop\\faks2sem3god\\MovieViewer\\MovieViewer\\Assets\\MovieImages\\shawShankPoster.jpg",
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
                SelectedMovie.Actors = new ObservableCollection<string> { "New Actor 1", "New Actor 2" };
            }
        }

        private bool CanEditMovie(object? parameter)
        {
            return SelectedMovie != null;
        }

    }
}
