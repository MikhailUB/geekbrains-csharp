using MailSender.lib.Data.EF.Migrations;
using MailSender.lib.Entityes;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics;

namespace MailSender.lib.Data.EF
{
	//Enable-Migrations -StartUpProjectName MailSender -MigrationsDirectory Data\EF\Migrations
	//Add-Migration Initial -StartUpProjectName MailSender -Verbose
	//Update-Database -StartUpProjectName MailSender -Verbose

	public class MailSenderDBContext : DbContext
	{
		static MailSenderDBContext()
		{
			// Database.SetInitializer(new DropCreateDatabaseAlways<MailSenderDBContext>());
			// Database.SetInitializer(new CreateDatabaseIfNotExists<MailSenderDBContext>());
			// Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MailSenderDBContext>());

			Database.SetInitializer(new MigrateDatabaseToLatestVersion<MailSenderDBContext, Migrations.Configuration>());
		}

		public void MigrateToLatestVersion()
		{
			var migrator = new DbMigrator(new Configuration());
			foreach (var pendingMigration in migrator.GetPendingMigrations())
			{
				Debug.WriteLine($"migration:{pendingMigration}");
			}
			//migrator.Update("нужная миграция");
			migrator.Update();
		}

		public DbSet<Sender> Senders { get; set; }
		public DbSet<Recipient> Recipients { get; set; }
		public DbSet<RecipientsList> RecipientsLists { get; set; }
		public DbSet<Server> Servers { get; set; }
		public DbSet<SchedulerTask> SchedulerTasks { get; set; }
		public DbSet<MailMessage> MailMessages { get; set; }
		public DbSet<MailsList> MailsLists { get; set; }

		public MailSenderDBContext() : this("name=MailSenderDBContext") { }
		public MailSenderDBContext(string ConnectionString) : base(ConnectionString) { }
	}
}
