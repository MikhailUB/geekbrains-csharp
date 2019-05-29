using MailSender.lib.Entityes;
using MailSender.lib.Services.Interfaces;

namespace MailSender.lib.Services.InMemory
{
	public class SchedulerTasksDataInMemory : DataInMemory<SchedulerTask>, ISchedulerTasksData
	{
		public override void Edit(SchedulerTask item)
		{
			var dbItem = GetById(item.Id);
			if (dbItem is null || ReferenceEquals(dbItem, item))
				return;

			dbItem.Time = item.Time;
			dbItem.Sender = item.Sender;
			dbItem.Server = item.Server;
			dbItem.Messages = item.Messages;
			dbItem.Recipients = item.Recipients;
		}
	}
}
