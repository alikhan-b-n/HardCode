namespace TechTaskHardCode.ViewModels.Responses;

public class ProductResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public decimal Price { get; set; }
    public string CategoryName { get; set; }
    public List<Property<object>> Properties { get; set; }
}

public class Property<T>
{
    public string Name { get; set; }
    public T Value { get; set; }
}