using System;

public class Store
{
    private string _id;
    private string _area;
    private string _addres;

    public string Id { get; set; }
    public string Area { get; set; }
    public string Addres { get; set; }

    public Store(string id, string area, string addres)
    {
        this.Id = id;
        this.Area = area;
        this.Addres = addres;
    }

    public Store(Store store)
    {
        _id = store.Id;
        _area = store.Area;
        _addres = store.Addres;
    }

    public override string ToString()
    {
        return $"ID: {_id}, Area: {_area}, Addres: {_addres}";
    }
}
