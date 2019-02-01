/*
Болотов Михаил
Урок 3. Объектно-ориентированное программирование. Часть 3
	
5.* Добавить в пример Lesson3 обобщенный делегат.
*/
using System;

namespace Delegates_Observer
{
	public delegate void MyDelegate(object o);
	/// <summary>
	/// Обобщенный делегат
	/// </summary>
	public delegate void MyGenericDelegate<T>(T o);

	class Source
	{
		public event MyDelegate Run;
		/// <summary>
		/// Событие через обобщенный делегат
		/// </summary>
		public event MyGenericDelegate<Source> Jump;

		public void Start()
		{
			Console.WriteLine("RUN");
			Run?.Invoke(this);
		}

		public void MakeJump()
		{
			Console.WriteLine("JUMP");
			Jump?.Invoke(this);
		}
	}

	/// <summary>
	/// Наблюдатель 1
	/// </summary>
	class Observer1
	{
		public void RunDo(object o)
		{
			Console.WriteLine("Первый. Принял, что объект {0} побежал", o);
		}

		public void JumpDo(Source s)
		{
			Console.WriteLine("Generic Первый. Наблюдаю прыжок объекта {0}", s);
		}
	}

	/// <summary>
	/// Наблюдатель 2
	/// </summary>
	class Observer2
	{
		public void RunDo(object o)
		{
			Console.WriteLine("Второй. Принял, что объект {0} побежал", o);
		}

		public void JumpDo(Source s)
		{
			Console.WriteLine("Generic Второй. Наблюдаю прыжок объекта {0}", s);
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			var s = new Source();
			var o1 = new Observer1();
			var o2 = new Observer2();

			var d1 = new MyDelegate(o1.RunDo);
			s.Run += d1;
			s.Run += o2.RunDo;
			s.Start();

			s.Run -= d1;
			s.Start();

			Console.WriteLine();
			var gd = new MyGenericDelegate<Source>(o1.JumpDo);
			s.Jump += gd;
			s.Jump += o2.JumpDo;
			s.MakeJump();

			s.Jump -= o2.JumpDo;
			s.MakeJump();

			Console.ReadLine();
		}
	}
}

