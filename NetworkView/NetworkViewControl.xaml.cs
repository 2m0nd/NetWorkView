using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Dimond;

namespace NetworkView
{
    /// <summary>
    /// Interaction logic for NetworkViewControl.xaml
    /// </summary>
    public partial class NetworkViewControl : UserControl
    {
		private List<NetworkAdapter> _adapters;
	    private Timer _timer;
	    private NetworkMonitor monitor;
	    private NetworkAdapter _adapter;
	    private int refreshInterval;
        public NetworkViewControl()
        {
            InitializeComponent();

			refreshInterval = ConfigHelper.RefreshInterval;
			_timer = new Timer(refreshInterval);
			monitor = new NetworkMonitor(refreshInterval);

            checkAutostart.IsChecked = AutoRunHelper.AutoStartEnabled();

            RootGrid.Background = new SolidColorBrush(new Color
            {
                A = ConfigHelper.BackgroundColor.Alpha,
                R = ConfigHelper.BackgroundColor.Red,
                G = ConfigHelper.BackgroundColor.Green,
                B = ConfigHelper.BackgroundColor.Blue
            });

			dspeed.Foreground = new SolidColorBrush(new Color
			{
				A = ConfigHelper.DownloadColor.Alpha,
				R = ConfigHelper.DownloadColor.Red,
				G = ConfigHelper.DownloadColor.Green,
				B = ConfigHelper.DownloadColor.Blue
			});

			upspeed.Foreground = new SolidColorBrush(new Color
			{
				A = ConfigHelper.UploadColor.Alpha,
				R = ConfigHelper.UploadColor.Red,
				G = ConfigHelper.UploadColor.Green,
				B = ConfigHelper.UploadColor.Blue
			});

            this.Loaded += (sender, args) => InitAdapter();
        }

		void InitAdapter()
		{
			_adapters = monitor.Adapters.ToList();

            var adapter = _adapters.FirstOrDefault(a => a.Name == ConfigHelper.AdapterName);

			if (adapter == null)
			{
				ChoiseAdapter();
			}

			SetAdapterToMonitor();
		}

		void ChoiseAdapter()
		{
			var window = new Window
			{
                Content = new AdapterSelector(_adapters),
                Topmost = true,
                Title = "Choise adapter",
				WindowStyle = WindowStyle.ToolWindow,
				ResizeMode = ResizeMode.NoResize,
                SizeToContent = SizeToContent.WidthAndHeight
				
			};

            window.ShowDialog();
		}

        void ConfigureBackground()
        {
            var window = new Window
            {
                Topmost = true,
                Title = "Choise colors",
                WindowStyle = WindowStyle.ToolWindow,
                ResizeMode = ResizeMode.NoResize,
                SizeToContent = SizeToContent.WidthAndHeight
            };
            window.Content = new SelectColorControl();
            window.ShowDialog();
        }



	    void SetAdapterToMonitor()
	    {
            _adapter = _adapters.FirstOrDefault(a => a.Name == ConfigHelper.AdapterName);
	        try
	        {
                monitor.StartMonitoring(_adapter);
                _timer.Elapsed += RefreshView;
                _timer.Start();
	        }
	        catch (Exception)
	        {
                MessageBox.Show("Not selected adapter.");
	        }

	    }

	    private void RefreshView(object sender, ElapsedEventArgs elapsedEventArgs)
        {
           Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => dspeed.Text = _adapter.DownloadDisplay));
           Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => upspeed.Text = _adapter.UploadDisplay));
        }

		private void MenuItem_Click_SelectAdapter(object sender, RoutedEventArgs e)
		{
			ChoiseAdapter();
			SetAdapterToMonitor();
		}

		private void MenuItem_Click_Config(object sender, RoutedEventArgs e)
		{
		    ConfigureBackground();
		}

		private void MenuItem_Click_Exit(object sender, RoutedEventArgs e)
		{
			var win = this.Parent as Window;
			win.Close();
		}

        private void MenuItem_Click_About(object sender, RoutedEventArgs e)
        {
            (new WindowAbout()).ShowDialog();
        }

        private void MenuItem_Click_Autostart(object sender, RoutedEventArgs e)
        {
            if(checkAutostart.IsChecked)
            {
                AutoRunHelper.EnableAutoStart();
            }
            else
            {
                AutoRunHelper.DisableAutoStart();
            }
        }
  
    }
}
