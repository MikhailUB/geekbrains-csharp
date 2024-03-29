﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Models
{
	public class Employee
	{
		[DisplayName("ID")]
		public int Id { get; set; }

		[Display(Name = "Фамилия")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "Укажите фамилию от 2-х до 100 символов")]
		[StringLength(100, MinimumLength = 2, ErrorMessage = "Укажите фамилию от 2-х до 100 символов")]
		public string SurName { get; set; }

		[Display(Name = "Имя")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "Укажите имя от 2-х до 100 символов")]
		[StringLength(100, MinimumLength = 2, ErrorMessage = "Укажите имя от 2-х до 100 символов")]
		public string Name { get; set; }

		[Display(Name = "Отчество")]
		public string Patronymic { get; set; }

		[Display(Name = "Возраст")]
		[Range(18, 100, ErrorMessage = "Укажите возраст от 18-ти до 100 лет")]
		public int Age { get; set; } = 18;

		[Display(Name = "Дата приема")]
		[DataType(DataType.Date)]
		[Range(typeof(DateTime), "01.01.2000", "31.12.9999", ErrorMessage = "Укажите дату приема не ранее 01.01.2000г. ")]
		public DateTime StartWork { get; set; } = DateTime.Now.Date;
	}
}
