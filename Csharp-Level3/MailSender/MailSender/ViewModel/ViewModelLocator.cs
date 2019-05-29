using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using MailSender.lib.Data.Linq2Sql;
using MailSender.lib.Services;
using MailSender.lib.Services.InMemory;
using MailSender.lib.Services.Interfaces;
using System;

namespace MailSender.ViewModel
{
	public class ViewModelLocator
	{
		public ViewModelLocator()
		{
			ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

			SimpleIoc.Default.TryRegister(() => new MailSenderDB());

			SimpleIoc.Default
			   .TryRegister<IRecipientsData, RecipientsDataInMemory>()
			   .TryRegister<IRecipientsListsData, RecipientsListsDataInMemory>()
			   .TryRegister<IServersData, ServersDataInMemory>()
			   .TryRegister<IMailMessagesData, MailMessagesDataInMemory>()
			   .TryRegister<IMailsListsData, MailsListDataInMemory>()
			   .TryRegister<ISchedulerTasksData, SchedulerTasksDataInMemory>()
			   .TryRegister<ISendersData, SendersDataInMemory>();

			SimpleIoc.Default.TryRegister<MainWindowViewModel>();

			SimpleIoc.Default.Register<MainWindowViewModel>();
		}

		public MainWindowViewModel MainWindowModel => ServiceLocator.Current.GetInstance<MainWindowViewModel>();

		public static void Cleanup()
		{
			// TODO Clear the ViewModels
		}
	}

	internal static class SimpleIocExtensions
	{
		public static SimpleIoc TryRegister<T>(this SimpleIoc container, Func<T> factory) where T : class
		{
			if (container.IsRegistered<T>()) return container;

			container.Register(factory);
			return container;
		}

		public static SimpleIoc TryRegister<T>(this SimpleIoc container) where T : class
		{
			if (container.IsRegistered<T>()) return container;

			container.Register<T>();
			return container;
		}

		public static SimpleIoc TryRegister<TInterface, TClass>(this SimpleIoc container)
			where TInterface : class
			where TClass : class, TInterface
		{
			if (container.IsRegistered<TInterface>()) return container;
			container.Register<TInterface, TClass>();
			return container;
		}
	}

}
