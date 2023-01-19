using BO;
namespace BlApi;
public interface IProduct
{
    public IEnumerable<ProductForList?> GetListedProducts(Func<ProductForList?, bool>? filter = null); //MANAGER and CLIENT   
    public IEnumerable<BO.ProductItem?> GetListedProductItems(BO.Cart cart, Func<ProductItem?, bool>? filter = null);
    public BO.Product GetByIdM(int productId);
    public void AddProduct(int productId, string productName, BO.Category? category, double price, int amount); //add a new DO product
    public void DeleteProduct(int productId); //Delete a product
    public void UpdateProduct(BO.Product product); //update a DO product
    public BO.ProductItem GetByIdC(int productId, BO.Cart cart); //get product item details, create a product and add the product to the list
    public IEnumerable<ProductForList?> GetListedPopularItems(); //Get a list of 8 most popular items
}

