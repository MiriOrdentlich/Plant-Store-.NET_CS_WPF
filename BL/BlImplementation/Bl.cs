using BlApi;

namespace BlImplementation;

public class Bl : IBl //Bl inherited from IBl
{
    public Bl() { }

    /// <summary>
    /// implementation of IBl with public constructor
    /// </summary>
    public IOrder Order { get; set; } = new Order();
    public IProduct Product { get; set; } = new Product();
    public ICart Cart { get; set; } = new Cart();

}
