using BlApi;


namespace BlImplementation;

internal class Cart : ICart
{
    /// <summary>
    /// add new item to cart
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="productId"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    /// <exception cref="NotImplementedException"></exception>
    public Cart AddItem(BO.Cart cart, int productId)
    {
        // search for product in OrderItems list:

        //if (DataSource.ProductsList.Contains(product)) // if found product -> throw exception
        //    throw new Exception("Product already exists");
        //DataSource.ProductsList.Add(product); // if product isn't in list, add product to list
        //return product.Id;

        throw new NotImplementedException();
    }

    public Cart UpdateItemAmount(BO.Cart cart, int productId, int amount)
    {

        throw new NotImplementedException();
    }

    public void ConfirmCart(BO.Cart cart, string name, string email, string adress)
    {
        throw new NotImplementedException();
    }
}
