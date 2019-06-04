using System.ComponentModel.DataAnnotations;

namespace MailSender.lib.Entityes.Base
{
	public abstract class BaseEntity
	{
		[Key]
		public int Id { get; set; }
	}
}
