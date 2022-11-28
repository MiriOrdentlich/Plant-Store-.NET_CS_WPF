using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using DO;

namespace BlImplementation;

internal class Product : IProduct
{
    DalApi.IDal dal = new Dal.DalList();
    //צריך לעדכן חריגות כנדרש בשתיהן
    public IEnumerable<BO.ProductForList?> GetListedProducts()
    {
        return from DO.Product? doProduct in dal.Product.GetAll()
               select new BO.ProductForList
               {
                   Id = doProduct?.Id ?? throw new NullReferenceException("Missing Id"),
                   Name = doProduct?.Name ?? throw new NullReferenceException("Missing Name"),
                   Category = (BO.Category?)doProduct?.Category ?? throw new NullReferenceException("No Matching Category"),
                   Price = doProduct?.Price ?? 0
               };

    }

    public BO.Product GetById(int productId)
    {
        DO.Product doProduct =dal.Product.GetById(productId);
        return new BO.Product()
        {
            Id = doProduct.Id,
            Category = (BO.Category)doProduct.Category,
            Price = doProduct.Price,
            Name = doProduct.Name,
            InStock = doProduct.InStock
        };

    }

   public void AddProduct(int productId, string productName, double price, int amount)
    {
        if(productId <= 0)
        {
            throw new ArgumentException("Invalid id");
        }
        if(productName == null)
        {
            throw new ArgumentException("Invalid name");
        }
        if(price <= 0)
        {
            throw new ArgumentException("Invalid price");
        }
        if(amount <= 0)
        {
            throw new ArgumentException("Invalid amount");
        }

    }
    public void UpdateProduct(BO.Product product)
    {
        if (product.Id <= 0)
        {
            throw new ArgumentException("Invalid id");
        }
        if (product.Name != null)
        {
            throw new ArgumentException("Invalid name");
        }
        //if (product<= 0)
        //{
        //    throw new ArgumentException("Invalid price");
        //}
        if (product.InStock <= 0)
        {
            throw new ArgumentException("Invalid amount");
        }
    }


}
