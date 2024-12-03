using System;
using static TransactionType;

public class Transaction
{
    private int _id;
    private DateOnly _date;
    private string _storeId;
    private int _productArticle;
    private int _amount;
    private TransactionType _transactionType;

    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public string StoreId { get; set; }
    public int ProductArticle { get; set; }
    public TransactionType TransactionType { get; set; }
    public int Amount { get; set; }

    public Transaction(int id, DateOnly date, string storeId, int productArticle, int amount, TransactionType transactionType)
    {
        this.Id = id;
        this.Date = date;
        this.StoreId = storeId;
        this.ProductArticle = productArticle;
        this.TransactionType = transactionType;
        this.Amount = amount;
    }

    public Transaction(Transaction transaction)
    {
        _id=transaction.Id;
        _date = transaction.Date;
        _storeId = transaction.StoreId;
        _productArticle = transaction.ProductArticle;
        _transactionType = transaction.TransactionType;
        _amount = transaction.Amount;
    }

    public override string ToString()
    {
        return $"Id: {_id}, Date: {_date}, Store Id: {_storeId}, Product Article: {_productArticle}, Transaction type: {_transactionType}, Amount: {_amount}";
    }
}
