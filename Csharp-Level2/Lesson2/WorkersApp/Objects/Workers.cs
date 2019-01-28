using System.Collections;

namespace WorkersApp
{
	/// <summary>
	/// Список работников
	/// </summary>
	public class Workers : IEnumerable
	{
		private readonly BaseWorker[] _elements;

		public Workers(int count)
		{
			_elements = new BaseWorker[count];
		}

		public BaseWorker this[int idx]
		{
			get { return _elements[idx]; }
			set { _elements[idx] = value; }
		}

		public IEnumerator GetEnumerator()
		{
			for (int i = 0; i < _elements.Length; i++)
			{
				yield return _elements[i];
			}
		}
	}
}
