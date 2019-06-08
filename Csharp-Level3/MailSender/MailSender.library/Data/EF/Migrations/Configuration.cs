namespace MailSender.lib.Data.EF.Migrations
{
	using MailSender.lib.Entityes;
	using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MailSenderDBContext>
    {
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
			AutomaticMigrationDataLossAllowed = true;
			MigrationsDirectory = @"Data\EF\Migrations";
		}

        protected override void Seed(MailSenderDBContext context)
        {
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data.

			if (!context.MailMessages.Any())
				context.MailMessages.AddRange(Enumerable.Range(1, 10).Select(i => new MailMessage
				{
					Subject = $"Message {i}",
					Body = $"Message to send body {i}"
				}));

			if (!context.Senders.Any())
				context.Servers.AddRange(Enumerable.Range(1, 5).Select(i => new Server
				{
					Name = $"My server {i}",
					Address = $"smtp.server{i}.ru",
					Port = 25,
					Login = $"srvLogin{i}",
					Password = $"srvPassword{i}"
				}));

			if (!context.Senders.Any())
				context.Senders.AddRange(Enumerable.Range(1, 5).Select(i => new Sender
				{
					Name = $"Sender {i}",
					Email = $"sender{i}@server.ru"
				}));

			if (!context.Recipients.Any())
				context.Recipients.AddRange(Enumerable.Range(1, 5).Select(i => new Recipient
				{
					Name = $"Recipient {i}",
					Email = $"recipient{i}@server.ru"
				}));

			context.SaveChanges();

		}
	}
}
