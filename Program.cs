using System;

public class Program
{
    static void Main(string[] args)
    {
        DataBase db = new DataBase("C:/Users/Vova/source/repos/laba5/laba5/bin/Debug/net6.0/LR5-var10.xls", new Logger("logs.txt", LogMode.NEW_FILE));
        db.Read();
        db.Display();
    }
}