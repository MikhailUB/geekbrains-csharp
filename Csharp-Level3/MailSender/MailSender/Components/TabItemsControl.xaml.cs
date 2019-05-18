using System;
using System.Windows;
using System.Windows.Controls;

namespace MailSender.Components
{
	public partial class TabItemsControl : UserControl
	{
		public event EventHandler LeftButtonClick;
		protected virtual void OnLeftButtonClick(EventArgs e) => LeftButtonClick?.Invoke(this, e);

		public event EventHandler RightButtonClick;
		protected virtual void OnRightButtonClick(EventArgs e) => RightButtonClick?.Invoke(this, e);

		public bool LeftButtonEnabled
		{
			get => LeftArrowButton.IsEnabled;
			set
			{
				LeftArrowButton.IsEnabled = value;
				RefreshButtonOpacity(LeftArrowButton);
			}
		}

		public bool RightButtonEnabled
		{
			get => RightArrowButton.IsEnabled;
			set
			{
				RightArrowButton.IsEnabled = value;
				RefreshButtonOpacity(RightArrowButton);
			}
		}

		private void RefreshButtonOpacity(Button button)
		{
			button.Opacity = button.IsEnabled ? 1 : 0.5;
		}

		public TabItemsControl()
		{
			InitializeComponent();
		}

		private void OnButtonClick(object sender, RoutedEventArgs e)
		{
			if (!(e.Source is Button button))
				return;

			switch (button.Name)
			{
				case "LeftArrowButton":
					OnLeftButtonClick(EventArgs.Empty);
					break;
				case "RightArrowButton":
					OnRightButtonClick(EventArgs.Empty);
					break;
			}
		}
	}
}
