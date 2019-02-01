using System;

namespace AsteroidsGame
{
	/// <summary>
	/// Класс исключения генерируемого при создании игровых объектов с некорректными параметрами
	/// </summary>
	public class GameObjectException : ArgumentException
	{
		/// <summary>
		/// Инициализирует новый объект исключения
		/// </summary>
		/// <param name="message">Сообщение описывающие исключение</param>
		/// <param name="paramName">Имя параметра с некорректным значением</param>
		public GameObjectException(string message, string paramName) : base(message, paramName)
		{
		}
	}
}
