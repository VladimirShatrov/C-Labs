using System;

public class Store
{
    public string Id { get; set; }
    public string Area { get; set; }
    public string Addres { get; set; }

    public Store(string id, string area, string addres)
    {
        if (id == null || id.Trim() == string.Empty)
            throw new ArgumentException("Id пустое.");
        Id = id;
        Area = area;
        Addres = addres;
    }

    public Store(Store store)
    {
        Id = store.Id;
        Area = store.Area;
        Addres = store.Addres;
    }

    public override string ToString()
    {
        return $"ID: {Id}, Area: {Area}, Addres: {Addres}";
    }
}
