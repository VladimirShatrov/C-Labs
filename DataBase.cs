using Microsoft.Office.Interop.Excel;
using System;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

public class DataBase
{
    private string _path;
    private Logger _logger;
    private readonly List<Product>? _products;
    private readonly List<Store>? _stores;
    private readonly List<Transaction>? _transactions;

    public string Path { get; set; }
    public List<Product> Products { get; }
    public Store Store { get; }
    public Transaction Transaction { get; }


    public DataBase(string path, Logger logger)
    {
        if (File.Exists(path))
        {
            Path = path;
        }
        else
        {
            throw new Exception("File with this path doesn`t exist.");
        }
        _logger = logger;
        _products = new List<Product>();
        _stores = new List<Store>();
        _transactions = new List<Transaction>();
    }

    /* public void Read()
     {
         Excel.Application application = new Excel.Application();
         Excel.Workbook workbook = null;
         Excel.Worksheet worksheet = null;
         Excel.Range range = null;
         Excel.Range rows = null;
         try
         {
             workbook = application.Workbooks.Open(Path);
             worksheet = workbook.Sheets[2];
             range = worksheet.UsedRange;

             rows = range.Rows;
             for (int row = 2; row <= rows.Count; row++)
             {
                 _products.Add(new Product
                 (
                     int.Parse(range.Cells[row, 1]?.Value2.ToString()),
                     range.Cells[row, 2]?.Value2,
                     range.Cells[row, 3]?.Value2,
                     range.Cells[row, 4]?.Value2,
                     decimal.Parse(range.Cells[row, 5]?.Value2.ToString()),
                     decimal.Parse(range.Cells[row, 6]?.Value2.ToString())
                 ));
             }
             _logger.Log("Товры успешо считаны.");
         }
         catch (Exception e)
         {
             _logger.Log("Ошибка при считывании товаров: " + e.Message);
         }
         finally
         {
             if (workbook != null)
             {
                 workbook.Close(false);
                 Marshal.ReleaseComObject(workbook);
                 workbook = null;
             }
             if (worksheet != null)
             {
                 Marshal.ReleaseComObject(worksheet);
                 worksheet = null;
             }
             if (range != null)
             {
                 Marshal.ReleaseComObject(range);
                 range = null;
             }
             if (rows != null)
             {
                 Marshal.ReleaseComObject(rows);
                 rows = null;
             }
         }

         try
         {
             workbook = application.Workbooks.Open(Path);
             worksheet = workbook.Sheets[1];
             range = worksheet.UsedRange;
             rows = range.Rows;

             for (int row = 2; row <= rows.Count; row++)
             {
                 DateTime time = DateTime.FromOADate(range.Cells[row, 2]?.Value2);

                 _transactions.Add(new Transaction
                 (
                     int.Parse(range.Cells[row, 1]?.Value2.ToString()),
                     new DateOnly(time.Year, time.Month, time.Day),
                     range.Cells[row, 3]?.Value2,
                     int.Parse(range.Cells[row, 4]?.Value2.ToString()),
                     int.Parse(range.Cells[row, 5]?.Value2.ToString()),
                     range.Cells[row, 6]?.Value2.ToString().Equals("Поступление")
                         ? TransactionType.Поступление
                         : TransactionType.Продажа
                 ));
             }
             _logger.Log("Операции успешо считаны.");
         }
         catch (Exception e)
         {
             _logger.Log("Ошибка при считывании операций: " + e.Message);
         }
         finally
         {
             if (workbook != null)
             {
                 workbook.Close(false);
                 Marshal.ReleaseComObject(workbook);
                 workbook = null;
             }
             if (worksheet != null)
             {
                 Marshal.ReleaseComObject(worksheet);
                 worksheet = null;
             }
             if (range != null)
             {
                 Marshal.ReleaseComObject(range);
                 range = null;
             }
             if (rows != null)
             {
                 Marshal.ReleaseComObject(rows);
                 rows = null;
             }
         }
         try
         {
             workbook = application.Workbooks.Open(Path);
             worksheet = workbook.Sheets[3];
             range = worksheet.UsedRange;
             rows = range.Rows;

             for (int row = 2; row <= rows.Count; row++)
             {
                 _stores.Add(new Store
                 (
                     range.Cells[row, 1]?.Value2,
                     range.Cells[row, 2]?.Value2,
                     range.Cells[row, 3]?.Value2
                 ));
             }
             _logger.Log("Магазины успешо считаны.");
         }
         catch (Exception e)
         {
             _logger.Log("Ошибка при считывании магазинов: " + e.Message);
         }
         finally
         {
             if (workbook != null)
             {
                 workbook.Close(false);
                 Marshal.ReleaseComObject(workbook);
                 workbook = null;
             }
             if (worksheet != null)
             {
                 Marshal.ReleaseComObject(worksheet);
                 worksheet = null;
             }
             if (range != null)
             {
                 Marshal.ReleaseComObject(range);
                 range = null;
             }
             if (rows != null)
             {
                 Marshal.ReleaseComObject(rows);
                 rows = null;
             }
         }
         _logger.Log("База данных считана.");
         application.Quit();
         Marshal.ReleaseComObject(application);

         workbook = null;
         worksheet = null;
         range = null;
         application = null;
         rows = null;

         GC.Collect();
         GC.WaitForPendingFinalizers();
     }*/

    public void Read()
    {
        Excel.Application application = null;
        Excel.Workbook workbook = null;
        Excel.Worksheet worksheet = null;
        Excel.Range range = null;
        Excel.Range rows = null;
        try
        {
            application = new Excel.Application();
            application.DisplayAlerts = false; // Отключаем предупреждения Excel

            workbook = application.Workbooks.Open(Path);

            // Считывание данных о товарах (Sheet 2)
            worksheet = workbook.Sheets[2];
            range = worksheet.UsedRange;
            rows = range.Rows;
            for (int row = 2; row <= rows.Count; row++)
            {
                _products.Add(new Product
                (
                    int.Parse(range.Cells[row, 1]?.Value2.ToString()),
                    range.Cells[row, 2]?.Value2,
                    range.Cells[row, 3]?.Value2,
                    range.Cells[row, 4]?.Value2,
                    decimal.Parse(range.Cells[row, 5]?.Value2.ToString()),
                    decimal.Parse(range.Cells[row, 6]?.Value2.ToString())
                ));
            }
            _logger.Log("Товры успешо считаны.");

            // Считывание данных о транзакциях (Sheet 1)
            worksheet = workbook.Sheets[1];
            range = worksheet.UsedRange;
            rows = range.Rows;
            for (int row = 2; row <= rows.Count; row++)
            {
                DateTime time = DateTime.FromOADate(range.Cells[row, 2]?.Value2);
                _transactions.Add(new Transaction
                (
                    int.Parse(range.Cells[row, 1]?.Value2.ToString()),
                    new DateOnly(time.Year, time.Month, time.Day),
                    range.Cells[row, 3]?.Value2,
                    int.Parse(range.Cells[row, 4]?.Value2.ToString()),
                    int.Parse(range.Cells[row, 5]?.Value2.ToString()),
                    range.Cells[row, 6]?.Value2.ToString().Equals("Поступление")
                        ? TransactionType.Поступление
                        : TransactionType.Продажа
                ));
            }
            _logger.Log("Операции успешо считаны.");

            // Считывание данных о магазинах (Sheet 3)
            worksheet = workbook.Sheets[3];
            range = worksheet.UsedRange;
            rows = range.Rows;
            for (int row = 2; row <= rows.Count; row++)
            {
                _stores.Add(new Store
                (
                    range.Cells[row, 1]?.Value2,
                    range.Cells[row, 2]?.Value2,
                    range.Cells[row, 3]?.Value2
                ));
            }
            _logger.Log("Магазины успешо считаны.");

            _logger.Log("База данных считана.");
        }
        catch (Exception e)
        {
            _logger.Log("Ошибка при считывании данных: " + e.Message);
        }
        finally
        {
            // Освобождение COM объектов
            ReleaseComObject(worksheet);
            ReleaseComObject(range);
            ReleaseComObject(rows);
            if (workbook != null)
            {
                workbook.Close(false);
                Marshal.ReleaseComObject(workbook);
            }
            if (application != null)
            {
                application.Quit();
                Marshal.ReleaseComObject(application);
            }

            workbook = null;
            worksheet = null;
            range = null;
            rows = null;
            application = null;

            // Принудительная очистка
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();  // Второй вызов для более глубокого освобождения ресурсов
            GC.WaitForPendingFinalizers();
        }
    }

    // Вспомогательный метод для освобождения COM объектов
    private void ReleaseComObject(object obj)
    {
        if (obj != null)
        {
            Marshal.ReleaseComObject(obj);
            obj = null;
        }
    }




    public void Display()
    {
        Console.WriteLine("Товары (Products):");
        if (_products != null && _products.Any())
        {
            Console.WriteLine($"{"Article",-10}{"Department",-20}{"Title",-40}{"Measurement",-15}{"Amount",-10}{"Price",-10}");
            Console.WriteLine(new string('-', 105));
            foreach (var product in _products)
            {
                Console.WriteLine($"{product.Article,-10}{product.Department,-20}{product.Title,-40}{product.Measurement,-15}{product.Amount,-10}{product.Price,-10}");
            }
        }
        else
        {
            Console.WriteLine("Данные о товарах отсутствуют.");
        }

        Console.WriteLine("\nМагазины (Stores):");
        if (_stores != null && _stores.Any())
        {
            Console.WriteLine($"{"ID",-10}{"Area",-20}{"Address",-30}");
            Console.WriteLine(new string('-', 60));
            foreach (var store in _stores)
            {
                Console.WriteLine($"{store.Id,-10}{store.Area,-20}{store.Addres,-30}");
            }
        }
        else
        {
            Console.WriteLine("Данные о магазинах отсутствуют.");
        }

        Console.WriteLine("\nТранзакции (Transactions):");
        if (_transactions != null && _transactions.Any())
        {
            Console.WriteLine($"{"ID",-10}{"Date",-15}{"StoreID",-10}{"Product",-10}{"Type",-15}{"Amount",-10}");
            Console.WriteLine(new string('-', 80));
            foreach (var transaction in _transactions)
            {
                Console.WriteLine($"{transaction.Id,-10}{transaction.Date,-15}{transaction.StoreId,-10}{transaction.ProductArticle,-10}{transaction.TransactionType,-15}{transaction.Amount,-10}");
            }
        }
        else
        {
            Console.WriteLine("Данные о транзакциях отсутствуют.");
        }
    }


}
