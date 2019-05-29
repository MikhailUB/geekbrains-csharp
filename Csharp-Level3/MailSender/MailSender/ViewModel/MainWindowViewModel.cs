using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MailSender.lib.Entityes;
using MailSender.lib.Services.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace MailSender.ViewModel
{
	public class MainWindowViewModel : ViewModelBase
	{
		private readonly IRecipientsData _recipientsData;
		private readonly IRecipientsListsData _recipientsListsData;
		private readonly ISendersData _sendersData;
		private readonly IMailMessagesData _mailMessagesData;
		private readonly IMailsListsData _mailsListsData;
		private readonly IServersData _serversData;
		private readonly ISchedulerTasksData _schedulerTasksData;

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

		private CollectionViewSource _filtredRecipientsSource;

		private void OnRecipientsFiltred(object sender, FilterEventArgs e)
		{
			if (!(e.Item is Recipient recipient))
				return;

			e.Accepted = string.IsNullOrWhiteSpace(_searchName) ||
				(recipient.Name != null && recipient.Name.IndexOf(_searchName, StringComparison.OrdinalIgnoreCase) > -1);
		}

		public ICollectionView FiltredRecipients => _filtredRecipientsSource?.View;

		public ObservableCollection<Recipient> Recipients
		{
			get => _recipients;
			private set
			{
				if (!Set(ref _recipients, value))
					return;

				if (_filtredRecipientsSource != null)
					_filtredRecipientsSource.Filter -= OnRecipientsFiltred;

				_filtredRecipientsSource = new CollectionViewSource { Source = value };
				_filtredRecipientsSource.Filter += OnRecipientsFiltred;

				RaisePropertyChanged(nameof(FiltredRecipients));
			}
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
					_filtredRecipientsSource?.View?.Refresh();
					//LoadData();
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
			_recipientsData.Edit(recipient);
			_recipientsData.SaveChanges();
		}

		public ICommand CreateRecipientCommand { get; }
		private bool CanCreateRecipientCommandExecute() => true;
		private void OnCreateRecipientCommandExecuted()
		{
			var recipient = new Recipient { Name = "Новый получатель", Email = "new@address.com" };
			var id = _recipientsData.Add(recipient);
			if (id != 0)
			{
				Recipients.Add(recipient);
				SelectedRecipient = recipient;
			}
		}

		#endregion

		public MainWindowViewModel(
					IRecipientsData recipientsData,
					IRecipientsListsData recipientsListsData,
					ISendersData sendersData,
					IMailMessagesData mailMessagesData,
					IMailsListsData mailsListsData,
					IServersData serversData,
					ISchedulerTasksData schedulerTasksData)
		{
			_recipientsData = recipientsData;
			_recipientsListsData = recipientsListsData;
			_sendersData = sendersData;
			_mailMessagesData = mailMessagesData;
			_mailsListsData = mailsListsData;
			_serversData = serversData;
			_schedulerTasksData = schedulerTasksData;

			RefreshDataCommand = new RelayCommand(OnRefreshDataCommandExecuted, CanRefreshDataCommandExecute);
			WriteRecipientCommand = new RelayCommand<Recipient>(OnWriteRecipientCommandExecuted, CanWriteRecipientCommandExecute);
			CreateRecipientCommand = new RelayCommand(OnCreateRecipientCommandExecuted, CanCreateRecipientCommandExecute);
		}

		private void LoadData()
		{
			var recipients = _recipientsData.GetAll();
			//if (!string.IsNullOrEmpty(_searchName))
			//	recipients = recipients.Where(r => r.Name.IndexOf(_searchName, StringComparison.OrdinalIgnoreCase) > -1);

			Recipients = new ObservableCollection<Recipient>(recipients);
		}
	}
}
