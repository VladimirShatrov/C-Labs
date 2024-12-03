using System;
using static LogMode;

public class Logger
{
	private string _filePath;
	private LogMode _mode;

	public Logger(string filePath, LogMode mode)
	{
		if (mode == LogMode.NEW_FILE)
		{
			_filePath = filePath;
			File.Create(filePath).Close();
		}
        else
        {
			if (File.Exists(filePath))
			{
				_filePath = filePath;
			}
			else
			{
				throw new Exception("File with this path doesn`t exist.");
			}
        }
    }

	public void Log(string message)
	{
		File.AppendAllText(_filePath, $"{DateTime.Now}: {message}\n");
	}
}
