using System;
using System.ComponentModel;

namespace MailSender.lib.Data.Linq2Sql
{
	public partial class Recipient : IDataErrorInfo
	{
		string IDataErrorInfo.Error => "";

		string IDataErrorInfo.this[string PropertyName]
		{
			get
			{
				switch (PropertyName)
				{
					case nameof(Id):
						return "";
					case nameof(Name):
						if (Name is null)
							return "Имя не определено (пустая ссылка на строку)";
						if (Name.Length < 3)
							return "Длина имени не может быть меньше 3 символов";
						if (Name.Length > 35)
							return "Длина имени не может быть больше 35 символов";

						return "";
					case nameof(Email):
						return "";
					default:
						return "";
				}
			}
		}

		partial void OnNameChanging(string value)
		{
			if (value is null)
				throw new ArgumentNullException(nameof(value));
			if (value == string.Empty)
				throw new InvalidOperationException("Имя не может быть пустой строкой");
		}
	}
}
