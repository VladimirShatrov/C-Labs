using System;

public class Product
{
    private int _article;
    private string _department;
    private string _title;
    private string _measurement;
    private decimal _amount;
    private decimal _price;

    public int Article { get; set; }
    public string Department { get; set; }
    public string Title { get; set; }
    public string Measurement { get; set; }
    public decimal Amount { get; set; }
    public decimal Price { get; set; }

    public Product(int article, string department, string title, string measurement, decimal amount, decimal price)
    {
        this.Article = article;
        this.Department = department;
        this.Title = title;
        this.Measurement = measurement;
        this.Amount = amount;
        this.Price = price;
    }

    public Product(Product product)
    {
        _amount = product.Amount;
        _article = product.Article;
        _department = product.Department;
        _title = product.Title;
        _measurement = product.Measurement;
        _price = product.Price;
    }

    public override string ToString()
    {
        return $"Artcile: {_article}, Department: {_department}, Title: {_title}, Measurement: {_measurement}, Amount: {_amount}, Price: {_price}";
    }
}
