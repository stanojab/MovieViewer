 using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieViewer
{
  public  class Movie : INotifyPropertyChanged
    {
        private bool isFavorite;

        public double Rating {  get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public string ReleaseYear { get; set; }
        private ObservableCollection<string> _actors;
        public ObservableCollection<string> Actors
        {
            get => _actors;
            set
            {
                _actors = value;
                OnPropertyChanged(nameof(Actors));
            }
        }
        public ObservableCollection<string> Genres { get; set; }
        public string ImagePath { get; set; }

        public bool IsFavorite
        {
            get => isFavorite;
            set
            {
                if (isFavorite != value)
                {
                    isFavorite = value;
                    OnPropertyChanged(nameof(IsFavorite));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ToggleFavorite()
        {
            IsFavorite = !IsFavorite;
        }
        public string GenresString => string.Join(", ", Genres);
    }
}
