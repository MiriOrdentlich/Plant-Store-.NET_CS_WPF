using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IProduct
{

    //MANAGER METHODS:
    IEnumerable<ProductForList?> GetListedProducts(); //MANAGER

    /// <summary>
    /// MANAGER
    /// base on the given Product code, build a Product object
    /// that match the needed product type
    /// </summary>
    /// <param name="productId"></param>
    void GetProductInfo(int productId);

    /// <summary>
    /// get product item details, create a product
    /// and add the product to the list
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="productName"></param>
    /// <param name="price"></param>
    /// <param name="amount"></param>
    void AddProduct(int productId, string productName, double price, int amount);
    void DeleteProduct(int productId);
    void UpdateProduct(int productId, string productName, double price, int amount);

    // CLIENT METHODS:
    IEnumerable<ProductItem?> GetProducts();
    void AddProductITEM(int productItemId);

}

