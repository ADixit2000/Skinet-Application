
namespace Core.Entities;

public class Product : BaseEntitiy
{
    public  int Id {get; set;}
    public required string Name {get; set;}

    public string Description {get; set;}

    public decimal Price {get; set;}

    public string PictureUrl {get; set;}

    public required string Type { get; set; }

    public string Brand { get; set; }

    public int QuantityInStock { get; set; }

}
