using System;

namespace WorkersApp
{
	/// <summary>
	/// Работник с фиксированной ежемесячной оплатой
	/// </summary>
	class WorkerWithFixedPayment : BaseWorker
	{
		/// <summary>
		/// Фиксированная месячная оплата
		/// </summary>
		public decimal MonthlyRate { get; set; }

		public WorkerWithFixedPayment(string name, DateTime dateOfBirth, decimal monthlyRate) : base(name, dateOfBirth)
		{
			MonthlyRate = monthlyRate;
		}

		/// <summary>
		/// Расчитывает среднемесячную зарплату
		/// </summary>
		public override decimal CalcMonthlyPayment()
		{
			return MonthlyRate;
		}
	}
}
