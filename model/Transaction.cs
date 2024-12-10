using System;

public class Transaction
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public string StoreId { get; set; }
    public int ProductArticle { get; set; }
    public TransactionType TransactionType { get; set; }
    public int Amount { get; set; }

    public Transaction(int id, DateOnly date, string storeId, int productArticle, int amount, TransactionType transactionType)
    {
        Id = id;
        Date = date;
        StoreId = storeId;
        ProductArticle = productArticle;
        TransactionType = transactionType;
        Amount = amount;
    }

    public Transaction(Transaction transaction)
    {
        Id = transaction.Id;
        Date = transaction.Date;
        StoreId = transaction.StoreId;
        ProductArticle = transaction.ProductArticle;
        TransactionType = transaction.TransactionType;
        Amount = transaction.Amount;
    }

    public override string ToString()
    {
        return $"Id: {Id}, Date: {Date}, Store Id: {StoreId}, Product Article: {ProductArticle}, Transaction type: {TransactionType}, Amount: {Amount}";
    }
}
