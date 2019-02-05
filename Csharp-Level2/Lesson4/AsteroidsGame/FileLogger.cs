using System;
using System.IO;

namespace AsteroidsGame
{
	/// <summary>
	/// Класс реализющий логирование в файл
	/// </summary>
	class FileLogger
	{
		/// <summary>
		/// Имя файла на диске
		/// </summary>
		public string FileName { get; private set; }

		public FileLogger(string fileName)
		{
			FileName = fileName;

			var folder = Path.GetDirectoryName(fileName);
			if (!Directory.Exists(folder))
				Directory.CreateDirectory(folder);

			if (File.Exists(FileName))
			{
				try
				{
					File.Delete(FileName);
				}
				catch { }
				
			}
		}

		/// <summary>
		/// Записать сообщение
		/// </summary>
		/// <param name="message">Сообщение для записи</param>
		public void Write(string message)
		{
			File.AppendAllText(FileName, DateTime.Now + ": " + message + Environment.NewLine);
		}
	}
}
