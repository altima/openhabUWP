using System.Diagnostics;
using System.Text.RegularExpressions;
using Windows.UI.Xaml;
using Microsoft.Practices.Unity;
using openhabUWP.Events;
using Prism.Events;

namespace openhabUWP.UI.Widgets
{
    public sealed partial class TextWidget : IWidget
    {
        private static DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(string), typeof(TextWidget), new PropertyMetadata(""));
        private static DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(TextWidget), new PropertyMetadata(""));

        private const string LabelValuePattern = @"(?<label>.+)\s(\[(?<value>.+)\])$";
        private openhabUWP.Widgets.TextWidget _widget;

        public string Value
        {
            get { return (string)this.GetValue(ValueProperty); }
            set { this.SetValue(ValueProperty, value); }
        }

        public string Label
        {
            get { return (string)this.GetValue(LabelProperty); }
            set { this.SetValue(LabelProperty, value); }
        }


        public TextWidget()
        {
            this.InitializeComponent();
            this.DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (args.NewValue is openhabUWP.Widgets.TextWidget)
                RegisterForUpdate();
        }

        public void RegisterForUpdate()
        {
            _widget = this.DataContext as openhabUWP.Widgets.TextWidget;
            App.Current.Container.Resolve<IEventAggregator>().GetEvent<WidgetUpdateEvent>().Unsubscribe(WidgetUpdateReceived);
            App.Current.Container.Resolve<IEventAggregator>().GetEvent<WidgetUpdateEvent>().Subscribe(WidgetUpdateReceived);

            if (_widget != null)
            {
                var labelValueMatch = Regex.Match(_widget.Label, LabelValuePattern);
                if (labelValueMatch.Success)
                {
                    Label = labelValueMatch.Groups["label"].Value;
                    Value = labelValueMatch.Groups["value"].Value;
                }
                else
                {
                    Label = _widget.Label;
                }
            }
        }

        public void WidgetUpdateReceived(Interfaces.Widgets.IWidget widget)
        {
            if (Equals(_widget.WidgetId, widget.WidgetId))
            {
                this.DataContext = widget;
                Debug.WriteLine("TextWidget Udpate {0}", widget.WidgetId);
            }
        }
    }
}
