using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NetworkView
{
    /// <summary>
    /// Interaction logic for SelectColorControl.xaml
    /// </summary>
    public partial class SelectColorControl : UserControl
    {
        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register("SelectedColor", typeof (ModelColor), typeof (SelectColorControl), new PropertyMetadata(default(ModelColor)));

        public ModelColor SelectedColor
        {
            get { return (ModelColor) GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }

        public SelectColorControl()
        {
            InitializeComponent();

	        SelectedColor = ConfigHelper.BackgroundColor;

            SelectedColor.PropertyChanged += SelectedColorOnPropertyChanged;


			this.Dispatcher.ShutdownStarted += (sender, args) =>
					{
						if (selectBackround.IsChecked.Value)
							ConfigHelper.BackgroundColor = SelectedColor;

						if (selectDownload.IsChecked.Value)
							ConfigHelper.DownloadColor = SelectedColor;

						if (selectUpload.IsChecked.Value)
							ConfigHelper.UploadColor = SelectedColor;
					};
			
        }

	    private void SelectedColorOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
	    {
			var windowWithView = Application.Current.Windows.OfType<Window>().Where(w => w.Content is NetworkViewControl).FirstOrDefault();
			if (windowWithView != null)
			{
				var netView = windowWithView.Content as NetworkViewControl;

				var brush = new SolidColorBrush(new Color
				{
					R = SelectedColor.Red,
					G = SelectedColor.Green,
					B = SelectedColor.Blue,
					A = SelectedColor.Alpha
				});
				if (selectBackround.IsChecked.Value)
					netView.RootGrid.Background = brush;

				if (selectDownload.IsChecked.Value)
					netView.dspeed.Foreground = brush;

				if (selectUpload.IsChecked.Value)
					netView.upspeed.Foreground = brush;
			}
	    }


	    private void selectBackround_Unchecked(object sender, RoutedEventArgs e)
		{
			ConfigHelper.BackgroundColor = SelectedColor;
		}

		private void selectDownload_Unchecked(object sender, RoutedEventArgs e)
		{
			ConfigHelper.DownloadColor = SelectedColor;
		}

		private void selectUpload_Unchecked(object sender, RoutedEventArgs e)
		{
			ConfigHelper.UploadColor = SelectedColor;
		}

		private void selectBackround_Checked(object sender, RoutedEventArgs e)
		{
			SelectedColor = ConfigHelper.BackgroundColor;
			SelectedColor.PropertyChanged += SelectedColorOnPropertyChanged;
		}

		private void selectDownload_Checked(object sender, RoutedEventArgs e)
		{
			SelectedColor = ConfigHelper.DownloadColor;
			SelectedColor.PropertyChanged += SelectedColorOnPropertyChanged;
		}

		private void selectUpload_Checked(object sender, RoutedEventArgs e)
		{
			SelectedColor = ConfigHelper.UploadColor;
			SelectedColor.PropertyChanged += SelectedColorOnPropertyChanged;
		}


        
    }

    public class ModelColor : DependencyObject, INotifyPropertyChanged
    {
		public override string ToString()
		{
			return String.Format("{0},{1},{2},{3}", this.Red, this.Green, this.Blue, this.Alpha);
		}

        public static readonly DependencyProperty RedProperty =
            DependencyProperty.Register("Red", typeof(byte), typeof(ModelColor), new PropertyMetadata(default(byte), RedPropertyChangedCallback));

        private static void RedPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var c = dependencyObject as ModelColor;
            if (c != null)
            {
                c.OnPropertyChanged("RedProperty");
            }
        }

        public byte Red
        {
            get { return (byte)GetValue(RedProperty); }
            set { SetValue(RedProperty, value); }
        }

        public static readonly DependencyProperty GreenProperty =
            DependencyProperty.Register("Green", typeof(byte), typeof(ModelColor), new PropertyMetadata(default(byte), GreenPropertyChangedCallback));

        private static void GreenPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var c = dependencyObject as ModelColor;
            if (c != null)
            {
                c.OnPropertyChanged("GreenProperty");
            }
        }

        public byte Green
        {
            get { return (byte)GetValue(GreenProperty); }
            set { SetValue(GreenProperty, value); }
        }

        public static readonly DependencyProperty BlueProperty =
            DependencyProperty.Register("Blue", typeof(byte), typeof(ModelColor), new PropertyMetadata(default(byte), BluePropertyChangedCallback));

        private static void BluePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var c = dependencyObject as ModelColor;
            if (c != null)
            {
                c.OnPropertyChanged("BlueProperty");
            }
        }

        public byte Blue
        {
            get { return (byte)GetValue(BlueProperty); }
            set { SetValue(BlueProperty, value); }
        }

        public static readonly DependencyProperty AlphaProperty =
            DependencyProperty.Register("Alpha", typeof(byte), typeof(ModelColor), new PropertyMetadata(default(byte), AlphaPropertyChangedCallback));

        private static void AlphaPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var c = dependencyObject as ModelColor;
            if (c != null)
            {
                c.OnPropertyChanged("AlphaProperty");
            }
        }

        public byte Alpha
        {
            get { return (byte)GetValue(AlphaProperty); }
            set { SetValue(AlphaProperty, value); }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
