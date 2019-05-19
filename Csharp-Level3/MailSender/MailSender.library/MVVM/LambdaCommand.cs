using System;
using System.Windows.Input;

namespace MailSender.lib.MVVM
{
	public class LambdaCommand : ICommand
	{
		private readonly Action<object> _onExecute;
		private readonly Func<object, bool> _canExecute;

		public event EventHandler CanExecuteChanged;

		public LambdaCommand(Action<object> onExecute, Func<object, bool> canExecute = null)
		{
			_onExecute = onExecute ?? throw new ArgumentNullException(nameof(onExecute));
			_canExecute = canExecute;
		}

		public bool CanExecute(object parameter)
		{
			return _canExecute?.Invoke(parameter) ?? true;
		}

		public void Execute(object parameter) => _onExecute(parameter);
	}
}
