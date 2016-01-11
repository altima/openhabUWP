using System;
using System.Text.RegularExpressions;
using Windows.UI.Xaml.Data;

namespace openhabUWP.Converters
{
    public sealed class LabelSplitConverter : IValueConverter
    {
        private const string LabelValuePattern = @"(?<label>.+)\s(\[(?<value>.+)\])$";
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool isLeft = parameter == null;
            if (Regex.IsMatch(value.ToString(), LabelValuePattern))
            {
                var match = Regex.Match(value.ToString(), LabelValuePattern);
                return isLeft ? match.Groups["label"].Value : match.Groups["value"].Value;
            }
            return isLeft ? value : string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public sealed class LabelCleanConverter : IValueConverter
    {
        private const string LabelValuePattern = @"(?<label>.+)\s(\[(?<value>.+)\])$";
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (Regex.IsMatch(value.ToString(), LabelValuePattern))
            {
                var match = Regex.Match(value.ToString(), LabelValuePattern);
                return string.Concat(match.Groups["label"].Value, " ", match.Groups["value"].Value);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}