using System;
using System.Text.RegularExpressions;
using Windows.UI.Xaml.Data;

namespace openhabUWP.Converters
{
    public static class StringPatterns
    {
        public static string GetLabelValuePattern(this IValueConverter self)
        {
            return @"(?<label>.+)\s(\[(?<value>.+)\])$";
        }
    }

    public sealed class LabelSplitConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool isLeft = parameter == null;
            if (Regex.IsMatch(value.ToString(), this.GetLabelValuePattern()))
            {
                var match = Regex.Match(value.ToString(), this.GetLabelValuePattern());
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
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (Regex.IsMatch(value.ToString(), this.GetLabelValuePattern()))
            {
                var match = Regex.Match(value.ToString(), this.GetLabelValuePattern());
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