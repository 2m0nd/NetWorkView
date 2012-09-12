using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
    /// Interaction logic for NetworkViewWindow.xaml
    /// </summary>
    public partial class NetworkViewWindow : Window
    {
        public NetworkViewWindow()
        {
            InitializeComponent();
            this.MouseDown += OnMouseDown;

            this.Left = ConfigHelper.PositionLeft;
            this.Top = ConfigHelper.PositionTop;

            this.MouseUp += (sender, args) =>
                                {
                                    ConfigHelper.PositionLeft = (int)this.Left;
                                    ConfigHelper.PositionTop = (int)this.Top;
                                };
  
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (mouseButtonEventArgs.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
