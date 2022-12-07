using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;
public interface IProduct 
{
    public IEnumerable<ProductForList?> GetListedProducts(); //MANAGER and CLIENT   

    /// <summary>
    /// MANAGER
    /// base on the given Product code, build a Product object
    /// that match the needed product type
    /// </summary>
    /// <param name="productId"></param>
    public BO.Product GetByIdM(int productId);
    /// <summary>
    /// get product item details, create a product
    /// and add the product to the list
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="productName"></param>
    /// <param name="price"></param>
    /// <param name="amount"></param>
    public void AddProduct(int productId, string productName, BO.Category category, double price, int amount); //add a new DO product
    public void DeleteProduct(int productId); //Delete a product
    public void UpdateProduct(BO.Product product); //update a DO product
    public BO.ProductItem GetByIdC(int productId, BO.Cart cart); //get product item details, create a product and add the product to the list

}

