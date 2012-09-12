using System;
using System.Collections.Generic;
using System.Data;
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
using Dimond;

namespace NetworkView
{
	/// <summary>
	/// Interaction logic for AdapterSelector.xaml
	/// </summary>
	public partial class AdapterSelector : UserControl
	{
		private NetworkAdapter _adapterSelected;
		public NetworkAdapter NetworkAdapter { get { return _adapterSelected; } }
		public AdapterSelector(IEnumerable<NetworkAdapter> adapters)
		{
			InitializeComponent();

			gridAdapters.ItemsSource = adapters;
			_adapterSelected = adapters.First();
		}

		private void gridAdapters_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
		{
			IInputElement element = e.MouseDevice.DirectlyOver;
			if (element != null && element is FrameworkElement)
			{
				if (((FrameworkElement)element).Parent is DataGridCell)
				{
					var grid = sender as DataGrid;
					if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
					{
						_adapterSelected = grid.SelectedItem as NetworkAdapter;
						ConfigHelper.AdapterName = _adapterSelected.Name;
						var win = this.Parent as Window;
						win.Close();
					}
				}
			}
		}


	}
}

