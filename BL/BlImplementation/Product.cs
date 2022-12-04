using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using DO;
using OtherFunctions;
using System.Runtime.Serialization;
using DalApi;

namespace BlImplementation;

internal class Product : BlApi.IProduct
{
    DalApi.IDal dal = new Dal.DalList();
    //צריך לעדכן חריגות כנדרש בשתיהן


    //MANAGER

    //GetListedProducts shows a list of products to the manager
    public IEnumerable<BO.ProductForList?> GetListedProducts()
    {
        //לעדכן חריגות
        return from DO.Product? doProduct in dal.Product.GetAll()
               select new BO.ProductForList
               {
                   Id = doProduct?.Id ?? throw new NullReferenceException("Missing Id"), //חריגות..
                   Name = doProduct?.Name ?? throw new NullReferenceException("Missing Name"),
                   Category = (BO.Category?)doProduct?.Category ?? throw new NullReferenceException("No Matching Category"),
                   Price = doProduct?.Price ?? 0
               };

    }

    //
    public BO.Product GetByIdM(int productId)
    {
        //חריגות
        DO.Product doProduct = dal.Product.GetById(productId);

        return new BO.Product() //create a new Product (type BO) and return it withthe wanted values
        {
            Id = doProduct.Id,
            Category = (BO.Category)doProduct.Category,
            Price = doProduct.Price,
            Name = doProduct.Name,
            InStock = doProduct.InStock
        };

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
        DO.Product newProduct = new DO.Product() //create a new data layer product
        {
            //copy the fields
            Id = product.Id,
            Name = product.Name,
            Category = (DO.Category)product.Category!, //! ???
            Price = product.Price,
            InStock = product.InStock
        };
       
        dal.Product.Add(newProduct);//add the product (DO type)

    }
    public void UpdateProduct(BO.Product product)
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
        DO.Product newProduct = new DO.Product() //create a new data layer product
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            InStock = product.InStock,
            Category = (DO.Category)product.Category! //! ???
        };
        dal.Product.Update(newProduct);
    }

    public void DeleteProduct(int productId)
    {
        var list = from item in dal.Product.GetAll()
                   where dal.Product.GetById(item.Value.Id).Id == productId
                   select item;
        if (list == null)
        {
            dal.Product.Delete(productId);
        }
    }

    //CLIENT
    public BO.ProductItem GetByIdC(int productId, Cart cart)
    {
        DO.Product doProduct = dal.Product.GetById(productId);
        return new BO.ProductItem() //create a new Product (type BO) and return it withthe wanted values
        {
            Id = doProduct.Id,
            Category = (BO.Category)doProduct.Category,
            Price = doProduct.Price,
            Name = doProduct.Name,
            InStock = doProduct.InStock > 0,
            Amount = //חסר
        };

    }
}///GetById is the same name for several methods. is that a problem in this case?
//4now I changed the methods here to GetByIdC & GetByIdM
