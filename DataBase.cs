using Microsoft.Office.Interop.Excel;
using System;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using OfficeOpenXml;

public class DataBase
{
    private string _path;
    private Logger _logger;
    private List<Product> _products;
    private List<Store> _stores;
    private List<Transaction> _transactions;

    public string Path { get; set; }
    public List<Product> Products { get; }
    public Store Store { get; }
    public Transaction Transaction { get; }


    public DataBase(string path, Logger logger)
    {
        if (File.Exists(path))
            if (System.IO.Path.GetExtension(path)?.ToLower() == ".xlsx")
            {
                _path = path;
            }
            else
            {
                throw new InvalidDataException("Файл должен иметь расширение .xlsx");
            }
        else
        {
            throw new FileNotFoundException("Не удалось найти файл по этому пути: " + path);
        }

        _logger = logger;
        _products = new List<Product>();
        _stores = new List<Store>();
        _transactions = new List<Transaction>();
    }

    public void Read()
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        try
        {
            ReadProducts();
            ReadTransaction();
            ReadStores();
        }
        catch (Exception e)
        {
            _logger.Log("Ошибка при чтении: " + e.Message);
        }
    }

    private static void DeleteEmptyRows(ExcelWorksheet worksheet)
    {
        int rows = worksheet.Dimension?.Rows ?? 0;
        for (int i = rows; i >= 1; i--)
        {
            if (worksheet.Cells[i, 1].Text == string.Empty)
            {
                worksheet.DeleteRow(i);
            }
        }
    }

    public void ReadProducts()
    {
        using (var package = new ExcelPackage(_path))
        {
            try
            {
                var worksheet = package.Workbook.Worksheets[1];
                DeleteEmptyRows(worksheet);
                var rows = worksheet.Dimension.Rows;

                for (int row = 2; row <= rows; row++)
                {
                    _products.Add(new Product(
                        int.Parse(worksheet.Cells[row, 1].Text),
                        worksheet.Cells[row, 2].Text,
                        worksheet.Cells[row, 3].Text,
                        worksheet.Cells[row, 4].Text,
                        double.Parse(worksheet.Cells[row, 5].Text),
                        double.Parse(worksheet.Cells[row, 6].Text)
                    ));
                }

                _logger.Log("Товары успешно считаны.");
            }
            catch (Exception e)
            {
                _logger.Log("Ошибка при чтении товаров: " + e.Message);
            }
        } 
    }

    public void ReadTransaction()
    {
        using (var package = new ExcelPackage(_path))
        {
            try
            {
                var worksheet = package.Workbook.Worksheets[0];
                DeleteEmptyRows(worksheet);
                var rows = worksheet.Dimension.Rows;

                for (int row = 2; row <= rows; row++)
                {
                    DateTime time = DateTime.Parse(worksheet.Cells[row, 2].Text);

                    _transactions.Add(new Transaction(
                        int.Parse(worksheet.Cells[row, 1].Text),
                        new DateOnly(time.Year, time.Month, time.Day),
                        worksheet.Cells[row, 3].Text,
                        int.Parse(worksheet.Cells[row, 4].Text),
                        int.Parse(worksheet.Cells[row, 5].Text),
                        worksheet.Cells[row, 6].Text.Equals("Поступление")
                            ? TransactionType.Поступление
                            : TransactionType.Продажа
                    ));
                }

                _logger.Log("Операции успешно считаны.");
            }
            catch (Exception e)
            {
                _logger.Log("Ошибка при чтении операций: " + e.Message);
            }
        }
    }

    public void ReadStores()
    {
        using (var package = new ExcelPackage(_path))
        {
            try
            {
                var worksheet = package.Workbook.Worksheets[2];
                DeleteEmptyRows(worksheet);
                var rows = worksheet.Dimension.Rows;

                for (int row = 2; row <= rows; row++)
                {
                    _stores.Add(new Store(
                        worksheet.Cells[row, 1].Text,
                        worksheet.Cells[row, 2].Text,
                        worksheet.Cells[row, 3].Text
                    ));
                }

                _logger.Log("Магазины успешно считаны.");
            }
            catch (Exception e)
            {
                _logger.Log("Ошибка при чтении магазинов: " + e.Message);
            }
        }
    }

    public void Display()
    {
        DisplayProduct();
        Console.WriteLine();
        DisplayStore();
        Console.WriteLine();
        DisplayTransaction();
    }

    public void DisplayProduct()
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
    }

    public void DisplayStore()
    {
        Console.WriteLine("Магазины (Stores):");
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
    }

    public void DisplayTransaction()
    {
        Console.WriteLine("Транзакции (Transactions):");
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

    public void Save()
    {
        try
        {
            using (var package = new ExcelPackage(new FileInfo(_path)))
            {
                var productWorksheet = package.Workbook.Worksheets[1];
                for (int i = 0; i < _products.Count; i++)
                {
                    var product = _products[i];
                    productWorksheet.Cells[i + 2, 1].Value = product.Article;
                    productWorksheet.Cells[i + 2, 2].Value = product.Department;
                    productWorksheet.Cells[i + 2, 3].Value = product.Title;
                    productWorksheet.Cells[i + 2, 4].Value = product.Measurement;
                    productWorksheet.Cells[i + 2, 5].Value = product.Amount;
                    productWorksheet.Cells[i + 2, 6].Value = product.Price;
                }

                var transactionWorksheet = package.Workbook.Worksheets[0];
                for (int i = 0; i < _transactions.Count; i++)
                {
                    var transaction = _transactions[i];
                    DateTime date = transaction.Date.ToDateTime(new TimeOnly(0, 0));
                    transactionWorksheet.Cells[i + 2, 1].Value = transaction.Id;
                    transactionWorksheet.Cells[i + 2, 2].Value = date.ToOADate();
                    transactionWorksheet.Cells[i + 2, 3].Value = transaction.StoreId;
                    transactionWorksheet.Cells[i + 2, 4].Value = transaction.ProductArticle;
                    transactionWorksheet.Cells[i + 2, 5].Value = transaction.Amount;
                    transactionWorksheet.Cells[i + 2, 6].Value = transaction.TransactionType.ToString();
                }

                var storeWorksheet = package.Workbook.Worksheets[2];
                for (int i = 0; i < _stores.Count; i++)
                {
                    var store = _stores[i];
                    storeWorksheet.Cells[i + 2, 1].Value = store.Id;
                    storeWorksheet.Cells[i + 2, 2].Value = store.Area;
                    storeWorksheet.Cells[i + 2, 3].Value = store.Addres;
                }

                package.Save();

                _logger.Log("Изменения успешно сохранены в Excel файл.");
            }
        }
        catch (Exception e)
        {
            _logger.Log("Ошибка при сохранении изменений: " + e.Message);
        }
    }

    public void DeleteProduct(int article)
    {
        var productToRemove = (from Product in _products where Product.Article == article select Product).FirstOrDefault();

        if (productToRemove != null)
        {
            _products.Remove(productToRemove);
            _logger.Log($"Продукт с артикулом {article} удален.");
        }
        else
        {
            _logger.Log($"Продукт с артикулом {article} не найден.");
        }
    }

    public void DeleteStore(string id)
    {
        var storeToRemove = (from Store in _stores where Store.Id == id select Store).FirstOrDefault();

        if (storeToRemove != null)
        {
            _stores.Remove(storeToRemove);
            _logger.Log($"Магазин с ID {id} удален.");
        }
        else
        {
            _logger.Log($"Магазин с ID {id} не найден.");
        }
    }

    public void DeleteTransaction(int id)
    {
        var transactionToRemove = (from Transaction in _transactions where Transaction.Id == id select Transaction).FirstOrDefault();
        if (transactionToRemove != null)
        {
            _transactions.Remove(transactionToRemove);
            _logger.Log($"Транзакция с ID {id} удалена.");
        }
        else
        {
            _logger.Log($"Транзакция с ID {id} не найдена.");
        }
    }

    public void ChangeProduct(int article, Product newProduct)
    {
        var oldProduct = (from Product in _products where Product.Article == article select Product).FirstOrDefault();

        if (oldProduct != null)
        {
            try
            {
                var index = _products.IndexOf(oldProduct);
                var sameArticle = (from Product in _products
                                   where (Product.Article == newProduct.Article) && (Product.Article != oldProduct.Article)
                                   select Product.Article).FirstOrDefault(-1);
                if (sameArticle != -1)
                {
                    _products[index] = newProduct;
                    _logger.Log("Товар успешно изменен.");
                }
                else
                {
                    _logger.Log($"Товар с артикулом {newProduct.Article} уже существует.");
                }
            }
            catch (Exception e)
            {
                _logger.Log("Ошибка при изменении товара: " + e.Message);
            }
        }
        else
        {
            _logger.Log($"Товар c артикулом {article} не найден.");
        }
    }

    public void ChangeTransaction(int id, Transaction newTransaction)
    {
        var oldTransaction = (from Transaction in _transactions where (Transaction.Id == id) select Transaction).FirstOrDefault();

        if (oldTransaction != null)
        {
            try
            {
                var index = _transactions.IndexOf(oldTransaction);
                var sameId = (from Transaction in _transactions
                              where (Transaction.Id == newTransaction.Id) && (Transaction.Id != oldTransaction.Id)
                              select Transaction.Id).FirstOrDefault(-1);
                if (sameId != -1)
                {
                    _transactions[index] = newTransaction;
                    _logger.Log("Операция успешно изменена.");
                }
                else
                {
                    _logger.Log($"Операция с id {newTransaction.Id} уже сущесвтует.");
                }
            }
            catch (Exception e)
            {
                _logger.Log("Ошибка при изменении операции: " + e.Message);
            }
        }
        else
        {
            _logger.Log($"Операция с id {id} не найдена.");
        }
    }

    public void ChangeStore(string id, Store newStore)
    {
        var oldStore = (from Store in _stores where (Store.Id == id) select Store).FirstOrDefault();
        if (oldStore != null)
        {
            try
            {
                var index = _stores.IndexOf(oldStore);
                var sameId = (from Store in _stores
                              where (Store.Id == id) && (Store.Id != oldStore.Id)
                              select Store.Id).FirstOrDefault("");
                if (sameId == "")
                {
                    _stores[index] = newStore;
                    _logger.Log("Магащин успешно изменен.");
                }
            }
            catch (Exception e)
            {
                _logger.Log("Ошибка при изменении магащина: " + e.Message);
            }

        }
        else
        {
            _logger.Log($"Магазин с id {id} не найден.");
        }
    }

    public void Add(Object obj)
    {
        if (obj is Product)
        {
            try
            {
                var product = obj as Product;
                var id = product.Article;
                var sameId = (from Product in _products
                              where (Product.Article == id)
                              select Product.Article).FirstOrDefault(-1);
                if (sameId != -1)
                {
                    _products.Add(product);
                }
                else
                {
                    _logger.Log($"Товар с артикулом {id} уже сущесвтует.");
                }
            }
            catch (Exception e)
            {
                _logger.Log("Ошибка при добавлении товара: " + e.Message);
            }
        }
        else if (obj is Store)
        {
            try
            {
                var store = obj as Store;
                var id = Store.Id;
                var sameId = (from Store in _stores
                              where (Store.Id == id)
                              select Store.Id).FirstOrDefault("");
                if (sameId != "")
                {
                    _stores.Add(store);
                }
                else
                {
                    _logger.Log($"Магазин с id {id} уже существует.");
                }
            }
            catch (Exception e)
            {
                _logger.Log("Ошибка при добавлении магазина: " + e.Message);
            }
        }
        else if (obj is Transaction)
        {
            try
            {
                var transaction = obj as Transaction;
                var id = Transaction.Id;
                var sameId = (from Transaction in _transactions
                              where (Transaction.Id == id)
                              select Transaction.Id).FirstOrDefault(-1);
                if (sameId != -1)
                {
                    _transactions.Add(transaction);
                }
                else
                {
                    _logger.Log($"Операция с id {id} уже сущесвтует.");
                }
            }
            catch (Exception e)
            {
                _logger.Log("Ошибка при добавлении операции: " + e.Message);
            }
        }
        else
        {
            throw new ArgumentException("Переданный объект не представлен в базе данных.");
        }
    }

    //товары отдела x с наименьшей ценной
    public List<Product> LessPriceProductsOfDepartment(string department)
    {
        try
        {
            List<Product> products = (from Product in _products
                            where (Product.Department == department)
                            orderby Product.Price ascending
                            select Product).ToList();
            if (products != null)
            {
                _logger.Log("Запрос 1 успешно выполнен.");
                return products;
            }
            else
            {
                _logger.Log("Запрос 1 успешно выполнен, не было найдено товаров.");
                return new List<Product>();
            }
        }
        catch (Exception e)
        {
            _logger.Log("Ошибка при выполнении запроса 1: " + e.Message);
            return new List<Product>();
        }
    }

    //товар, который принес больше всего прибыли, если таких несколько, то вернуть товар с наименьшим артикулом
    public Product? GetTopProfitableProducts()
    {
        try
        {
            var product = (from Product in _products
                                let profit = (from Transaction in _transactions
                                              where Transaction.ProductArticle == Product.Article
                                              select Transaction.Amount * Product.Price).Sum()
                                orderby profit descending, Product.Article ascending
                                select Product).FirstOrDefault();
                                
                                                 
            if (product != null)
            {
                _logger.Log("Запрос 2 успешно выполнен.");
                return product;
            }
            else
            {
                _logger.Log("Запрос 2 успешно выполнен, не было найдено товаров.");
                return null;
            }
        }
        catch (Exception e)
        {
            _logger.Log("Ошибка при выполнении запроса 2: " + e.Message);
            return null;
        }
    }

    //магазины в которые поступило больше 1500 упаковок товаров отдела x за период с y по z июля включительно
    public List<Store> GetStoresWithMoreThen1500ReceiptedProducts(string department, short start, short end)
    { 
        if (start < 1 || end > 31 || end < start)
        {
            throw new ArgumentException("Неверные границы выборки.");
        }
        if (string.IsNullOrEmpty(department))
        {
            throw new ArgumentNullException("Отдел должен быть задан.");
        }
        try
        {
            List<Store> stores = (from Store in _stores
                                  let SellPackages = (from Transaction in _transactions
                                                      join Product in _products on Transaction.ProductArticle equals Product.Article
                                                      where Transaction.StoreId == Store.Id &&
                                                            Transaction.TransactionType == TransactionType.Поступление &&
                                                            Product.Department == department &&
                                                            Transaction.Date.Day >= start &&
                                                            Transaction.Date.Day <= end
                                                      select Transaction.Amount).Sum()
                                  where SellPackages > 1500
                                  select Store).ToList();
            if (stores != null)
            {
                _logger.Log("Запрос 3 выполнен успешно.");
                return stores;
            }
            else
            {
                _logger.Log("Запрос 3 выполнен успешно, но небыло найдено подходящих магазинов.");
                return new List<Store>();
            }
        }
        catch (Exception e)
        {
            _logger.Log("Ошибка при выполнении запроса 3: " + e.Message);
            return new List<Store>();
        }
    }

    /*Определите общий объем (в литрах) всех видов шампуней для волос, проданных магазинами,
    расположенными на улице Гагарина, за период с 14 по 22 июля включительно.В ответ запишите
    целую часть числа.*/
    public int GetTotalShampooVolume()
    {
        try
        {
            var totalVolume = (from Transaction in _transactions
                               join Store in _stores on Transaction.StoreId equals Store.Id
                               join Product in _products on Transaction.ProductArticle equals Product.Article
                               where Store.Addres.Contains("Гагарина") &&
                                     Product.Title.ToLower().Contains("шампунь") &&
                                     Product.Title.ToLower().Contains("волос") &&
                                     Transaction.TransactionType == TransactionType.Продажа &&
                                     Transaction.Date.Day >= 1 &&
                                     Transaction.Date.Day <= 31 &&
                                     Transaction.Date.Month == 7
                               let volumeInLiters = Product.Measurement.ToLower() == "мл"
                                    ? (Product.Amount / 1000.0)
                                    : Product.Amount
                               select Transaction.Amount * volumeInLiters).Sum();

            _logger.Log("Запрос 4 успешно выполнен.");
            return (int)Math.Floor(totalVolume);
        }
        catch (Exception e)
        {
            _logger.Log("Ошибка при выполнении запроса 4." + e.Message);
            return 0;
        }
    }
}
