using System.Windows.Controls;

namespace MailSender.View
{
	/// <summary>
	/// Interaction logic for RecipientInfoEditor.xaml
	/// </summary>
	public partial class RecipientInfoEditor : UserControl
	{
		public RecipientInfoEditor()
		{
			InitializeComponent();
		}

		private void OnDataValidationError(object sender, ValidationErrorEventArgs e)
		{
			if (!(e.Source is Control control))
				return;

			if (e.Action == ValidationErrorEventAction.Added)
				control.ToolTip = e.Error.ErrorContent.ToString();
			else
				control.ClearValue(ToolTipProperty);
		}

	}
}
