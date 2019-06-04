using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;
using MailSender.lib.Entityes.Base;

namespace MailSender.lib.Entityes
{
	//[Table(Name = "SchedulerTask")]
	public class SchedulerTask : BaseEntity
	{
		public DateTime Time { get; set; }
		[Required]
		public virtual Sender Sender { get; set; }
		[Required]
		public virtual Server Server { get; set; }
		[Required]
		public virtual RecipientsList Recipients { get; set; }
		[Required]
		public virtual MailsList Messages { get; set; }
	}
}
