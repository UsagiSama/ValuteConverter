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
	public sealed partial class ConvertPage : Page
	{
		private InterPageInfo info;
		private string fromValute;
		private string toValute;

		public ConvertPage()
		{
			this.InitializeComponent();
			info = new InterPageInfo();
			fromValute = toValute = "AUD";
			TextBlock.Text = "0";			
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			PageStackEntry history = Frame.BackStack.Last();

			if (history.SourcePageType.Name == "MainPage")
			{
				info.Rates = e?.Parameter as RateData;
			}
			else if (history.SourcePageType.Name == "ValuteSelectionPage")
			{
				info = e?.Parameter as InterPageInfo;

				if (info.FromOrTo) toValute = info.CodeValute;
				else fromValute = info.CodeValute;
			}
		}

		private void ClickButton1(object sender, RoutedEventArgs e)
		{
			info.FromOrTo = false; 
			info.CodeValute = fromValute;
			Frame.Navigate(typeof(ValuteSelectionPage), info);
		}

		private void ClickButton2(object sender, RoutedEventArgs e)
		{
			info.FromOrTo = true;
			info.CodeValute = toValute;
			Frame.Navigate(typeof(ValuteSelectionPage), info);
		}

		private void TextChanged(object sender, TextChangedEventArgs e)
		{
			if (TextBox.Text == "")
				TextBlock.Text = "0";

			try 
			{
				decimal result = info.Rates.ConvertValute(Convert.ToDecimal(TextBox.Text), fromValute, toValute);
				TextBlock.Text = string.Format("{0:F2}", result);
			}
			catch (FormatException)
			{
				TextBox.Text = "";
			}
		}
	}
}
