namespace MailSender.lib.Entityes.Base
{
	public abstract class NamedEntity : BaseEntity
	{
		public string Name { get; set; }
	}

	public abstract class Human : NamedEntity
	{
		public string Email { get; set; }
	}
}
