using MailSender.lib.Entityes;
using System.Collections.Generic;

namespace MailSender.lib.Data
{
	public static class TestData
	{
		public static List<Sender> Senders { get; } = new List<Sender>
		{
			new Sender{ Name = "Сидоров", Email = "sidorov@mail.ru" },
			new Sender{ Name = "Петров", Email = "petrov@yandex.ru" },
			new Sender{ Name = "Иванов", Email = "ivanov@gmail.com" }
		};

		public static List<Server> Servers { get; } = new List<Server>
		{
			new Server{ Name="Mail.ru", Address ="smtp.mail.ru", Port = 25, UseSSL = true, Login="Mail.User", Password ="Pwd1"},
			new Server{ Name="Yandex.ru", Address ="smtp.yandex.ru", Port = 25, UseSSL = true, Login="Yandex.User", Password ="Pwd123"},
			new Server{ Name="Gmail.com", Address ="smtp.gmail.com", Port = 25, UseSSL = true, Login="Gmail.User", Password ="Pwd123456"}
		};
	}
}
