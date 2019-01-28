using System;

namespace WorkersApp
{
	/// <summary>
	/// Работник с почасовой оплатой
	/// </summary>
	class WorkerWithHourlyPayment : BaseWorker
	{
		/// <summary>
		/// Почасовая ставка
		/// </summary>
		public decimal HourlyRate { get; set; }

		public WorkerWithHourlyPayment(string name, DateTime dateOfBirth, decimal hourlyRate) : base(name, dateOfBirth)
		{
			HourlyRate = hourlyRate;
		}

		/// <summary>
		/// Расчитывает среднемесячную зарплату
		/// </summary>
		public override decimal CalcMonthlyPayment()
		{
			return 20.8m * 8 * HourlyRate;
		}
	}
}
