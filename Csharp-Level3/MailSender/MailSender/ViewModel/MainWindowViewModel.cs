using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MailSender.lib.Data.Linq2Sql;
using MailSender.lib.Services.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MailSender.ViewModel
{
	public class MainWindowViewModel : ViewModelBase
	{
		private readonly IRecipientsData _recipientsData;

		private string _title = "Рассыльщик почты v1";
		private string _status = "К отправке почты готов!";
		private ObservableCollection<Recipient> _recipients;
		private Recipient _selectedRecipient;
		private string _searchName;

		public string Title
		{
			get => _title;
			set => Set(ref _title, value);
		}

		public string Status
		{
			get => _status;
			set => Set(ref _status, value);
		}

		public ObservableCollection<Recipient> Recipients
		{
			get => _recipients;
			private set => Set(ref _recipients, value);
		}

		public Recipient SelectedRecipient
		{
			get => _selectedRecipient;
			set => Set(ref _selectedRecipient, value);
		}

		public string SearchName
		{
			get => _searchName;
			set
			{
				if (Set(ref _searchName, value))
				{
					LoadData();
				}
			}
		}

		#region Commands
		public ICommand RefreshDataCommand { get; }
		private bool CanRefreshDataCommandExecute() => true;
		private void OnRefreshDataCommandExecuted() => LoadData();

		public ICommand WriteRecipientCommand { get; }
		private bool CanWriteRecipientCommandExecute(Recipient recipient)
		{
			return recipient != null;
		}
		private void OnWriteRecipientCommandExecuted(Recipient recipient)
		{
			_recipientsData.Write(recipient);
			_recipientsData.SaveChanges();
		}

		public ICommand CreateRecipientCommand { get; }
		private bool CanCreateRecipientCommandExecute() => true;
		private void OnCreateRecipientCommandExecuted()
		{
			var recipient = new Recipient { Name = "Новый получатель", Email = "new@address.com" };
			var id = _recipientsData.Create(recipient);
			if (id != 0)
			{
				Recipients.Add(recipient);
				SelectedRecipient = recipient;
			}
		}

		#endregion

		public MainWindowViewModel(IRecipientsData recipientsData)
		{
			_recipientsData = recipientsData;

			RefreshDataCommand = new RelayCommand(OnRefreshDataCommandExecuted, CanRefreshDataCommandExecute);
			WriteRecipientCommand = new RelayCommand<Recipient>(OnWriteRecipientCommandExecuted, CanWriteRecipientCommandExecute);
			CreateRecipientCommand = new RelayCommand(OnCreateRecipientCommandExecuted, CanCreateRecipientCommandExecute);
		}

		private void LoadData()
		{
			var recipients = _recipientsData.GetAll();
			if (!string.IsNullOrEmpty(_searchName))
				recipients = recipients.Where(r => r.Name.IndexOf(_searchName, StringComparison.OrdinalIgnoreCase) > -1);

			Recipients = new ObservableCollection<Recipient>(recipients);
		}
	}
}
