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
using System.Threading;
using System.Diagnostics;

namespace ValuteConverter
{
	public sealed partial class MainPage : Page
	{
		private RateData rates;
		//private bool tryAgain;

		public MainPage()
		{
			this.InitializeComponent();
		}

		private void ClickStartButton(object sender, RoutedEventArgs e)
		{
			//tryAgain = false;
			progressRing.IsActive = true;
			TextBlock.Visibility = Visibility.Visible;
			NavigateButton.Visibility = Visibility.Collapsed;

			rates = new RateData();
			TryLoadData();
		}

		private void ClickRetryButton(object sender, RoutedEventArgs e)
		{
			//tryAgain = true;
			progressRing.IsActive = true;
			TryLoadData();
		}

		private void TryLoadData()
		{
			if (rates.LoadRateData())
			{
				TextBlock.Text = "Ошибка подключения\n";
				progressRing.IsActive = false;
				RetryButton.Visibility = Visibility.Visible;
			}
			else Frame.Navigate(typeof(ConvertPage), rates);
		}
	}
}
