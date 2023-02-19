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

    public class cartVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// if value==0 set visibility as hidden, else set it as visible
        /// * will be used to make checkout option unavailabe in case cart is empty
        /// </summary>
        /// <param name="value">cart totalPrice</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)value == "0" || (string)value == "")
                return Visibility.Hidden;
            else
                return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class addVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// if value==0 set visibility as visible , else set it as hidden
        /// will be used to prevent manager(<=> value!=0) user from accessing irrelevant options
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value != 0)
                return Visibility.Hidden;
            else
                return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class updateVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// if value==0 set visibility as hidden, else set it as visible
        /// will be used to prevent manager(<=> value!=0) user from accessing irrelevant options
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value != 0)
                return Visibility.Visible;
            else
                return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class managerVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// if value==0 set visibility as hidden, else set it as visible
        /// will be used to prevent manager(<=> value!=0) user from accessing irrelevant options
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value == 0)
                return Visibility.Hidden;
            else
                return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class clientVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// if value==0 set visibility as visible , else set it as hidden
        /// will be used to prevent client(<=> value==0) user from accessing irrelevant options
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value == 0)
                return Visibility.Visible;
            else
            {
                return Visibility.Hidden;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class followOrderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
                return Visibility.Visible;
            else
                return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class statusOrderConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)value[0] == "" && (string)value[1] == "") //if stil haven't updated shipping and delivery date then 
                return Visibility.Visible;
            if ((string)value[1] != "")//if delivery date have been updated then we want to hide the update button
                return Visibility.Hidden;
            return Visibility.Visible;
        }
        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsReadOnlyConverter: IValueConverter
    {
        /// <summary>
        /// if value==0 set visibility as visible , else set it as hidden
        /// will be used to prevent manager(<=> value!=0) user from accessing irrelevant options
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value != 0)
                return true;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}


