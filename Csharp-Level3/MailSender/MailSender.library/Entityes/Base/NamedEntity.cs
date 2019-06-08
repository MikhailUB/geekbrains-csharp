using System.ComponentModel.DataAnnotations;

namespace MailSender.lib.Entityes.Base
{
	public abstract class NamedEntity : BaseEntity
	{
		[Required]
		public string Name { get; set; }
	}
}
