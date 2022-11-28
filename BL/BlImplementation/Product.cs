using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;

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


}
