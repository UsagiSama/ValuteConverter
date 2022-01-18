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
	public sealed partial class ConvertPage : Page
	{
		private InterPageInfo info;
		private string leftValute;
		private string rightValute;
		private decimal leftValue;
		private decimal rightValue;

		public ConvertPage()
		{
			this.InitializeComponent();
			info = new InterPageInfo();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			PageStackEntry history = Frame.BackStack.Last();

			if (history.SourcePageType.Name == "MainPage")
			{
				leftValute = rightValute = "AUD";
				leftValue = rightValue = 0;
				info.Rates = e?.Parameter as RateData;
			}
			else if (history.SourcePageType.Name == "ValuteSelectionPage")
			{
				info = e?.Parameter as InterPageInfo;
				leftValute = info.LeftValute;
				rightValute = info.RightValute;
				leftValue = 0;
				rightValue = 0;

				if (info.LeftOrRight)
				{
					RightTextBox.Text = string.Format("{0:F2}", info.Rates.ConvertValute(leftValue, leftValute, rightValute));
					LeftTextBox.Text = string.Format("{0:F2}", leftValue);
				}
				else 
				{
					RightTextBox.Text = string.Format("{0:F2}", rightValue);
					LeftTextBox.Text = string.Format("{0:F2}", info.Rates.ConvertValute(rightValue, leftValute, rightValute));
				}

				//SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
			}
		}

		private void RightButtonClick(object sender, RoutedEventArgs e)
		{
			ClickButtonHelper(false);
			Frame.Navigate(typeof(ValuteSelectionPage), info);
		}

		private void LeftButtonClick(object sender, RoutedEventArgs e)
		{
			ClickButtonHelper(true);
			Frame.Navigate(typeof(ValuteSelectionPage), info);
		}

		private void ClickButtonHelper(bool leftOrRight)
		{
			info.LeftOrRight = leftOrRight;
			info.LeftValute = leftValute;
			info.RightValute = rightValute;
			//info.LeftValue = leftValue;
			//info.RightValue = rightValue;
		}

		private void RightTextChanged(object sender, TextChangedEventArgs e)
		{
			try 
			{
				leftValue = info.Rates.ConvertValute(Convert.ToDecimal(RightTextBox.Text), leftValute, rightValute);
				LeftTextBox.TextChanged -= LeftTextChanged;
				LeftTextBox.Text = string.Format("{0:F2}", leftValue);
				LeftTextBox.TextChanged += LeftTextChanged;
			}
			catch (FormatException) { TextChangedHelper(); }
		}

		private void LeftTextChanged(object sender, TextChangedEventArgs e)
		{
			try
			{
				rightValue = info.Rates.ConvertValute(Convert.ToDecimal(LeftTextBox.Text), rightValute, leftValute);
				RightTextBox.TextChanged -= RightTextChanged;
				RightTextBox.Text = string.Format("{0:F2}", rightValue);
				RightTextBox.TextChanged += RightTextChanged;
			}
			catch (FormatException) { TextChangedHelper(); }
		}

		private void TextChangedHelper()
		{
			rightValue = 0;
			leftValue = 0;
			LeftTextBox.Text = "";
			RightTextBox.Text = "";
		}
	}
}
