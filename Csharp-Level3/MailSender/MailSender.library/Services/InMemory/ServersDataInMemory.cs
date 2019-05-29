using MailSender.lib.Data;
using MailSender.lib.Entityes;
using MailSender.lib.Services.Interfaces;

namespace MailSender.lib.Services.InMemory
{
	public class ServersDataInMemory : DataInMemory<Server>, IServersData
	{
		public ServersDataInMemory() => _items.AddRange(TestData.Servers);

		public override void Edit(Server item)
		{
			var dbItem = GetById(item.Id);
			if (dbItem is null || ReferenceEquals(dbItem, item))
				return;

			dbItem.Address = item.Address;
			dbItem.Port = item.Port;
			dbItem.UseSSL = item.UseSSL;
			dbItem.Login = item.Login;
			dbItem.Password = item.Password;
		}
	}
}
