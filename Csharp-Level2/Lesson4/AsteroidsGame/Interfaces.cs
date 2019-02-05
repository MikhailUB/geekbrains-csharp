using System.Drawing;

namespace AsteroidsGame
{
	/// <summary>
	/// Интерфейс описывающий столкновения объектов
	/// </summary>
	interface ICollision
	{
		/// <summary>
		/// Проверяет произошло ли столкновение с другим объектом
		/// </summary>
		/// <param name="obj">Объект, с которым проверяется столкновение</param>
		bool Collision(ICollision obj);

		/// <summary>
		/// Свойство определяющее участок игровой поверхности занимаемый объектом
		/// </summary>
		Rectangle Rect { get; }
	}
}
