using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace PL
{
	class ConvertImagePathToBitmap : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			try
			{
				string imageRelativeName = (string)value;
                string currentDir = Environment.CurrentDirectory[..^4];
				string imageFullName = currentDir + imageRelativeName;
				BitmapImage bitmapImage = new BitmapImage(new Uri(imageFullName));
                return bitmapImage;

			}
			catch (Exception ex)
			{
				string imageRelativeName = @"\pics\IMG_FAILURE.jpeg";
				string currentDir = Environment.CurrentDirectory[..^4];
				string imageFullName = currentDir + imageRelativeName;
                BitmapImage bitmapImage = new BitmapImage(new Uri(imageFullName));
                return bitmapImage;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

    public class visibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if((string)value == "0")
                    return Visibility.Hidden;
                else
                    return Visibility.Visible;
            }
            catch (Exception ex)
            {
                string imageRelativeName = @"\pics\IMG_FAILURE.jpeg";
                string currentDir = Environment.CurrentDirectory[..^4];
                string imageFullName = currentDir + imageRelativeName;
                BitmapImage bitmapImage = new BitmapImage(new Uri(imageFullName));
                return bitmapImage;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    //public class cartEmptyToTextConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        if ((string)value == "0")
    //            return Visibility.Hidden;
    //        else
    //            return Visibility.Visible;
    //    }
    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        throw new NotImplementedException();
    //    }

    //}
 
    internal class Converters
    {
    }
}


