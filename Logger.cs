using System;

public class Logger
{
	private string _filePath;
	private LogMode _mode;

	public Logger(string filePath, LogMode mode)
	{
		if (mode == LogMode.NEW_FILE)
		{
			_filePath = filePath;
			_mode = mode;
			File.Create(filePath).Close();
		}
        else
        {
			if (File.Exists(filePath))
			{
				_filePath = filePath;
				_mode = mode;
			}
			else
			{
				throw new FileNotFoundException("Не удалось найти файл по этому пути.");
			}
        }
    }

	public void Log(string message)
	{
		File.AppendAllText(_filePath, $"{DateTime.Now}: {message}\n");
	}
}
