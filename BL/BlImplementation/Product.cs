using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using System.Runtime.Serialization;
using DalApi;
using BO;

namespace BlImplementation;

internal class Product : BlApi.IProduct
{
   private DalApi.IDal dal = new Dal.DalList();
    //צריך לעדכן חריגות כנדרש בשתיהן



    /// <summary>
    /// GetListedProducts shows a list of products to the manager and for the client Catalogue
    /// </summary>
    /// <returns></returns> the new List products (type ProductForList)
    /// <exception cref="NullReferenceException"></exception>
    public IEnumerable<BO.ProductForList?> GetListedProducts()
    {

        return from DO.Product? doProduct in dal.Product.GetAll() // get a list of products
               select new BO.ProductForList //build a new List products (type ProductForList) 
               {
                   Id = doProduct?.Id ?? throw new NullReferenceException("Missing Id"), //חריגות..
                   Name = doProduct?.Name ?? throw new NullReferenceException("Missing Name"),
                   Category = (BO.Category?)doProduct?.Category ?? throw new NullReferenceException("No Matching Category"),
                   Price = doProduct?.Price ?? 0
               };
    }


    //MANAGER:


    /// <summary>
    /// Get products by their id for the manager
    /// </summary>
    /// <param name="productId"></param> gets a product id
    /// <returns></returns> the Product object
    /// <exception cref="Exception"></exception>
    public BO.Product GetByIdM(int productId)
    {
        try
        {
            //חריגות
            //productId.IdIsNegative
           

            DO.Product doProduct = dal.Product.GetById(productId); //find the wanted product by its id and copy it to a new one

            return new BO.Product() //create a new Product (type BO) and return it with the wanted values
            {
                //copy the details
                Id = doProduct.Id, 
                Category = (BO.Category)doProduct.Category, //take the doProduct Category, turn it into BO.Category
                Price = doProduct.Price,
                Name = doProduct.Name,
                InStock = doProduct.InStock
            };
        }
        
        //לעדכן חריגות
        catch(Exception ex)
        {
            throw new Exception("*********", ex); 
        }
    }

    // public void AddProduct(int productId, string productName, double price, int amount)
    public void AddProduct(BO.Product product)
    {
        //if (productId <= 0)
        //{
        //    throw new ArgumentException("Invalid id");
        //}
        //if (productName == null)
        //{
        //    throw new ArgumentException("Invalid name");
        //}
        //if (price <= 0)
        //{
        //    throw new ArgumentException("Invalid price");
        //}
        //if (amount <= 0)
        //{
        //    throw new ArgumentException("Invalid amount");
        //}
        try
        {

            //productId.IdIsNegative();
            //product.Name.NameIsNull();
            //product.Price.PriceIsNegative();
            //product.InStock.AmountIsNegative();

            DO.Product newDoProduct = new DO.Product() //create a new data layer product
            {
                //copy the fields
                Id = product.Id,
                Name = product.Name,
                Category = (DO.Category)product.Category, //take the newDoProduct Category, turn it into DO.Category
                Price = product.Price,
                InStock = product.InStock
            };
           
            int a= dal.Product.Add(newDoProduct);//add the product (DO type), and dal.Product.Add(newDoProduct) returns int type
        }
        catch(Exception ex)
        {
            throw new Exception("  **********  ",ex);
        }

    }
    public void UpdateProduct(BO.Product product) //update product details
    {
        //if (product.Id <= 0)
        //{
        //    throw new ArgumentException("Invalid id");
        //}
        //if (product.Name == null)
        //{
        //    throw new ArgumentException("Invalid name");
        //}
        //if (product.Price <= 0)
        //{
        //    throw new ArgumentException("Invalid price");
        //}
        //if (product.InStock <= 0)
        //{
        //    throw new ArgumentException("Invalid amount");
        //}


        //productId.IdIsNegative();
        //product.Name.NameIsNull();
        //product.Price.PriceIsNegative();
        //product.InStock.AmountIsNegative();

        DO.Product newDoProduct = new DO.Product() //create a new data layer product
        {
            //copy the fields
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            InStock = product.InStock,
            Category = (DO.Category)product.Category //take the newDoProduct Category, turn it into DO.Category
        };
        dal.Product.Update(newDoProduct);
    }

    public void DeleteProduct(int productId) //delete a product by its id
    {
        foreach (DO.Order order in dal.Order.GetAll())
        {
            var list = from item in dal.OrderItem.GetAll()// get a list of orders in order to check if the wanted product is there
                       where dal.Product.GetById(item.Value.Id).Id == productId //search which product.id is equal to the given product id
                       select item;
            if (list != null) //if there is a product.id that match the wanted one
            {
                dal.Product.Delete(productId); //delete this product
            }
        }
        
    }


    //CLIENT:

    public BO.ProductItem GetByIdC(int productId, Cart cart)
    {
        try
        {
            //productId.IdIsNegative

            DO.Product doProduct = dal.Product.GetById(productId); //find the wanted product by its id and copy it to a new one


            return new BO.ProductItem() //create a new Product (type BO) and return it withthe wanted values
            {
                Id = doProduct.Id,
                Category = (BO.Category)doProduct.Category, //take the doProduct Category, turn it into BO.Category
                Price = doProduct.Price,
                Name = doProduct.Name,
                InStock = doProduct.InStock > 0,
                Amount = doProduct.InStock    
            };
        }
        catch (Exception ex)
        {
            throw new Exception("******", ex);
        }
        

    }
}


///GetById is the same name for several methods. is that a problem in this case?
//4now I changed the methods here to GetByIdC & GetByIdM
