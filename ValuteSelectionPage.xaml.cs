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
			dataList.ItemsSource = info?.Rates.GetValuteList().Values;
			dataList.SelectedIndex = info.Rates.GetIndex(info.CodeValute);
			//SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
		}

		private void DataList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Valute selectedValute = dataList.SelectedItem as Valute;
			info.CodeValute = selectedValute.CharCode;
			Frame.Navigate(typeof(ConvertPage), info);
		}
	}
}
