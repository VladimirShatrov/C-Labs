using Microsoft.Office.Interop.Excel;
using System;
using static EnterNum;

public class Program
{
    static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Как вести логгирование: 0 - создать новый файл, 1 - дополнять существующий.");
            LogMode logMode;
            logMode = (LogMode)enterNum(0, 1);

            Console.WriteLine("Введите путь к файлу логов: ");
            string logPath = null;
            while (logPath == null) {
                logPath = Console.ReadLine();
            }
            Logger logger = new Logger(logPath, logMode);

            Console.WriteLine("Введите путь к файлу базы данных.");
            string dataBasePath = null;
            while (dataBasePath == null)
            {
                dataBasePath = Console.ReadLine();
            }
            DataBase dataBase = new DataBase(dataBasePath, logger);

            int task = -1;
            while (task != 0)
            {
                Console.WriteLine("Выбор заданий:" +
                                  "\n1 - чтение базы данных" +
                                  "\n2 - просмотр базы данных" +
                                  "\n3 - удаление элемента" +
                                  "\n4 - изменение элемента" +
                                  "\n5 - добавление элемента" +
                                  "\n6 - выполнение запросов" +
                                  "\n7 - сохранить изменения" +
                                  "\n0 - прекращение работы");
                task = enterNum(0, 7);

                switch (task)
                {
                    case 1: dataBase.Read(); break;
                    case 2: dataBase.Display(); break;
                    case 3: Console.WriteLine("Введите из какой таблицы удалить элемент: 1 - товары, 2 - операции, 3 - магазины.");
                        Console.WriteLine("Введите ключ эдемента для удаления.");
                        switch (enterNum(1, 3))
                        {
                            case 1: dataBase.DeleteProduct(enterNum()); break;
                            case 2: dataBase.DeleteTransaction(enterNum()); break;
                            case 3: dataBase.DeleteStore(Console.ReadLine()); break;
                        }
                        break;
                    case 4: Console.WriteLine("Введите в какой таблице изменить элемент: 1 - товары, 2 - операции, 3 - магазины.");
                        switch(enterNum(1, 3))
                        {
                            case 1: Console.WriteLine("Введите артикул, отдел, название, единицы измерения, количесвто в упаковке и цену измененного товара:");
                                Product product = new Product(enterNum(),
                                                              Console.ReadLine(),
                                                              Console.ReadLine(),
                                                              Console.ReadLine(),
                                                              enterDoubleNum(0),
                                                              enterDoubleNum(0));
                                Console.WriteLine("Введите артикул старого товара: ");
                                dataBase.ChangeProduct(enterNum(), product);
                                break;
                            case 2: Console.WriteLine("Введите id, дату, id магазина, артикул товара, тип операции (0 - поступление, 1 - продажа), количесвто товара, для измененной операции: ");
                                Transaction transaction = new Transaction(enterNum(),
                                                                         DateOnly.FromDateTime(DateTime.Parse(Console.ReadLine())),
                                                                         Console.ReadLine(),
                                                                         enterNum(),
                                                                         enterNum(0),
                                                                         (TransactionType)enterNum(0,1));
                                Console.WriteLine("Введите id старой операции:");
                                dataBase.ChangeTransaction(enterNum(), transaction);
                                break;
                            case 3: Console.WriteLine("Введите id, район и адрес изменного магазина: ");
                                Store stroe = new Store(Console.ReadLine(),
                                                        Console.ReadLine(),
                                                        Console.ReadLine());
                                Console.WriteLine("Введите id старого магазина:");
                                dataBase.ChangeStore(Console.ReadLine(), stroe);
                                break;
                        }
                        break;
                    case 5: Console.WriteLine("Введите в какую таблицу добавить элемент: 1 - товары, 2 - операции, 3 - магазины.");
                        switch (enterNum(1, 3))
                        {
                            case 1: Console.WriteLine("Введите артикул, отдел, название, единицы измерения, количесвто в упаковке и цену нового товара: ");
                                Product product = new Product(enterNum(),
                                                              Console.ReadLine(),
                                                              Console.ReadLine(),
                                                              Console.ReadLine(),
                                                              enterDoubleNum(0),
                                                              enterDoubleNum(0));
                                dataBase.Add(product);
                                break;
                            case 2: Console.WriteLine("Введите id, дату, id магазина, артикул товара, тип операции (0 - поступление, 1 - продажа), количесвто товара, для новой операции: ");
                                Transaction transaction = new Transaction(enterNum(),
                                         DateOnly.FromDateTime(DateTime.Parse(Console.ReadLine())),
                                         Console.ReadLine(),
                                         enterNum(),
                                         enterNum(0),
                                         (TransactionType)enterNum(0, 1));
                                dataBase.Add(transaction);
                                break;
                            case 3: Console.WriteLine("Введите id, район и адрес нового магазина: ");
                                Store stroe = new Store(Console.ReadLine(),
                                                        Console.ReadLine(),
                                                        Console.ReadLine());
                                dataBase.Add(stroe);
                                break;
                        }
                        break;
                    case 6: Console.WriteLine("список запросов:" +
                        "\n1 - возвращает список товаров отдела x отсортированный по цене по возрастанию." +
                        "\n2 - возвращает товар, который принес больше всего прибыли, если таких несколько, то возврашает товар с наименьшим артикулом." +
                        "\n3 - возвращает магазины в которые поступило больше 1500 упаковок товаров отдела x за период с y по z июля включительно." +
                        "\n4 - Возврашает целую часть общего объема (в литрах) всех видов шампуней для волос, проданных магазинами, расположенными на улице Гагарина, за период с 14 по 22 июля включительно.");
                        Console.WriteLine("Введите какой запрос нужно выполнить.");
                        switch(enterNum(1, 4))
                        {
                            case 1: Console.WriteLine("Введите название отдела: ");
                                List<Product> t1 = dataBase.LessPriceProductsOfDepartment(Console.ReadLine());
                                Console.WriteLine("Результат запроса 1:");
                                foreach (var item in t1)
                                {
                                    Console.WriteLine(item);
                                }
                                break;
                            case 2: Console.WriteLine($"Результат запроса 2: {dataBase.GetTopProfitableProducts()}");
                                break;
                            case 3: Console.WriteLine("Введите назавние отдела и границы выборки: ");
                                short y = (short)enterNum(1, 31);
                                short z = (short)enterNum(y, 31);
                                List<Store> t2 = dataBase.GetStoresWithMoreThen1500ReceiptedProducts(Console.ReadLine(), y, z);
                                Console.WriteLine("Результат запроса 3:");
                                foreach (var item in t2)
                                {
                                    Console.WriteLine(item);
                                }
                                break;
                            case 4: Console.WriteLine($"Результат запроса 4: {dataBase.GetTotalShampooVolume()}");
                                break;
                        }
                        break;
                    case 7: dataBase.Save();
                        break;
                }
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}