using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ValuteConverter
{
	public sealed partial class MainPage : Page
	{
		private RateData rates;

		public MainPage()
		{
			this.InitializeComponent();
			rates = new RateData();
			TextBlock.Text = rates.LoadRateData();
			progressRing.IsActive = false;
			TextBlock.Visibility = Visibility.Collapsed;
			NavigateButton.Visibility = Visibility.Visible;	
		}

		private void ClickNavigateButton(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(ConvertPage), rates);
		}
	}
}
