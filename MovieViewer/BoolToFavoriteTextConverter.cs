using System;
using System.Globalization;
using System.Windows.Data;

namespace MovieViewer
{
    public class BoolToFavoriteTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isFavorite)
                return isFavorite ? "Odstrani iz priljubljenih" : "Dodaj v priljubljene";

            return "Dodaj v priljubljene"; // Default
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
