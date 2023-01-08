using BlApi;

namespace BlImplementation;

sealed internal class Bl : IBl //Bl inherited from IBl
{

    /// <summary>
    /// implementation of IBl with public constructor
    /// </summary>

    public IOrder Order => new Order();
    public IProduct Product => new Product();
    public ICart Cart => new Cart();
    //public IUsers User => new Users();


}
