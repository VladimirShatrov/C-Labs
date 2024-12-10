using System;

public class Product
{
    public int Article { get; set; }
    public string Department { get; set; }
    public string Title { get; set; }
    public string Measurement { get; set; }
    public double Amount { get; set; }
    public double Price { get; set; }

    public Product(int article, string department, string title, string measurement, double amount, double price)
    {
        Article = article;
        Department = department;
        Title = title;
        Measurement = measurement;
        Amount = amount;
        Price = price;
    }

    public Product(Product product)
    {
        Amount = product.Amount;
        Article = product.Article;
        Department = product.Department;
        Title = product.Title;
        Measurement = product.Measurement;
        Price = product.Price;
    }

    public override string ToString()
    {
        return $"Artcile: {Article}, Department: {Department}, Title: {Title}, Measurement: {Measurement}, Amount: {Amount}, Price: {Price}";
    }
}
