using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Client.Services
{
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Image image = new Image();
            try
            {
                using (MemoryStream stream = new MemoryStream((byte[])value))
                {
                    image.Source = BitmapFrame.Create(stream,
                                                        BitmapCreateOptions.None,
                                                        BitmapCacheOption.OnLoad);
                }
            }
            catch
            {
            }
            return image.Source;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
