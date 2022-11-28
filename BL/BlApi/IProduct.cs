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
    //need to get a list of products!!

    /// <summary>
    /// MANAGER
    /// base on the given Product code, build a Product object
    /// that match the needed product type
    /// </summary>
    /// <param name="productId"></param>
    BO.Product GetById(int productId);
    /// <summary>
    /// get product item details, create a product
    /// and add the product to the list
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="productName"></param>
    /// <param name="price"></param>
    /// <param name="amount"></param>
    void AddProduct(int productId, string productName, double price, int amount); //add a new DO product
    void DeleteProduct(int productId); //Delete a product
    void UpdateProduct(BO.Product product); //update a DO product



    // CLIENT METHODS:


    IEnumerable<ProductItem?> GetProducts();
    BO.ProductItem GetById(int productId, Cart cart); //get product item details, create a product and add the product to the list

}

