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
using Windows.UI.Core;

namespace ValuteConverter
{
	public sealed partial class ValuteSelectionPage : Page
	{
		private InterPageInfo info;

		public ValuteSelectionPage()
		{
			this.InitializeComponent();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			info = e?.Parameter as InterPageInfo;
			//dataList.ItemsSource = info?.Rates.GetValuteList().Values;
			dataList.ItemsSource = info.Rates.SortedDict.Values;

			if (info.LeftOrRight) dataList.SelectedIndex = info.Rates.GetIndex(info.RightValute);
			else dataList.SelectedIndex = info.Rates.GetIndex(info.LeftValute);
			//SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
		}

		private void DataListSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Valute selectedValute = dataList.SelectedItem as Valute;

			if (info.LeftOrRight) info.RightValute = selectedValute.CharCode;
			else info.LeftValute = selectedValute.CharCode;

			Frame.Navigate(typeof(ConvertPage), info);
		}
	}
}
