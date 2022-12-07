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
using DO;
using System.Xml.Linq;

namespace BlImplementation;

internal class Product : BlApi.IProduct
{
    private DalApi.IDal dal = new Dal.DalList();


    /// <summary>
    /// GetListedProducts shows a list of products to the manager and for the client Catalogue
    /// </summary>
    /// <returns></returns> the new List products (type ProductForList)
    /// <exception cref="NullReferenceException"></exception>
    public IEnumerable<BO.ProductForList?> GetListedProducts()
    {
        return from doProduct in dal.Product.GetAll() // get a list of products and scan it
               select new BO.ProductForList //build a new List products (type ProductForList) 
               {
                   Id = doProduct?.Id ?? 0,
                   Name = doProduct?.Name ?? "",
                   Category = (BO.Category)doProduct?.Category!,
                   Price = doProduct?.Price ?? 0
               };
    }

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

            DO.Product doProduct = dal.Product.GetById(productId); //find the wanted product by its id and copy it to a new one

            if (doProduct.Id > 0)
            {
                return new BO.Product() //create a new Product (type BO) and return it with the wanted values
                {
                    //copy the details

                    Id = doProduct.Id,
                    Category = (BO.Category)doProduct.Category!, //take the doProduct Category, turn it into BO.Category
                    Price = doProduct.Price,
                    Name = doProduct.Name,
                    InStock = doProduct.InStock
                };
            }
            else
                throw new BlInvalidEntityException(doProduct.Id, doProduct.Name! , 0);
        }

        catch (DO.DalDoesNotExistIdException ex) //לא בטוח
        {
            throw new BO.BlMissingEntityException("Data exception:", ex);
        }
    }

    public void AddProduct(int productId, string productName, double price, int amount)
    {
        try
        {
            //product.InStock.AmountIsNegative();
            if (productId > 0)
            {
                if (productName is null)
                    throw new BO.BlInvalidEntityException("product Name",1); //will put EntityChoice = 4 and print - Name is null ;
                if (price < 0)
                    throw new BO.BlInvalidEntityException("product Name", 0);
                if (amount < 0)
                    throw new BO.BlInvalidEntityException("product Name", 0);

                DO.Product newDoProduct = new DO.Product() //create a new data layer product
                {
                    //copy the fields
                    Id = productId,
                    Name = productName,
                    Category = dal.Product.GetById(productId).Category,  //take the newDoProduct Category, turn it into DO.Category
                    Price = price,
                    InStock = amount
                };
                int check = dal.Product.Add(newDoProduct); //add the product (DO type), and dal.Product.Add(newDoProduct) returns int type
            }
            else
            {
                throw new BlMissingEntityException("Missing Id");
            }

        }

        catch (DO.DalAlreadyExistsIdException ex)
        {
            throw new BO.BlAlreadyExistEntityException("Data exception:", ex);
        }
    }

    public void UpdateProduct(BO.Product product) //update product details
    {
        try
        {
            //product.InStock.AmountIsNegative();
            if (product.Id > 0)
            {
                if (product.Name is null)
                    throw new BO.BlInvalidEntityException("product name", 1); //will put EntityChoice = 4 and print - Name is null ;
                if (product.Price < 0)
                    throw new BO.BlInvalidEntityException("price", 0);
                if (product.InStock < 0)
                    throw new BO.BlInvalidEntityException("amount", 0);
                DO.Product newDoProduct = new DO.Product() //create a new data layer product
                {
                    //copy the fields
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    InStock = product.InStock,
                    Category = (DO.Category)product.Category! //take the newDoProduct Category, turn it into DO.Category
                };
                dal.Product.Update(newDoProduct); //update product in 
            }
            else
            {
                throw new BlMissingEntityException("Missing Id");
            }

        }

        catch (DO.DalDoesNotExistIdException ex)
        {
            throw new BO.BlMissingEntityException("Data exception:", ex);
        }
    }

    public void DeleteProduct(int productId) //delete a product by its id
    {
        try
        {
            var tmp = dal.Product.GetById(productId); //if product doesn't exist get exception from data layer 
            var list = from item in dal.OrderItem.GetAll()// get a list of orders in order to check if the wanted product is there
                       select item?.ProductID == productId; //search which product.id is equal to the given product id

            if (list != null) //if there is a product.id that match the wanted one
                throw new BO.BlAlreadyExistEntityException("Product exists in an order"); //Exception: Product exists in an order
            dal.Product.Delete(productId); //delete this product
        }
        catch (DO.DalDoesNotExistIdException ex)
        {
            throw new BO.BlMissingEntityException("Data exception:", ex);
        }
    }


    public BO.ProductItem GetByIdC(int productId, BO.Cart cart)
    {
        try
        {

            DO.Product doProduct = dal.Product.GetById(productId); //find the wanted product by its id and copy it to a new one

            if (doProduct.Id > 0)
            {
                return new BO.ProductItem()//create a new Product (type BO) and return it withthe wanted values   
                {
                    Id = doProduct.Id,
                    Category = (BO.Category)doProduct.Category!, //take the doProduct Category, turn it into BO.Category
                    Price = doProduct.Price,
                    Name = doProduct.Name,
                    InStock = doProduct.InStock > 0,
                    Amount = cart.Items!.Sum(x => x.Amount)

                };

            }
            else
                throw new BlInvalidEntityException("Id", 0);
        }
        catch (DO.DalDoesNotExistIdException ex) 
        {
            throw new BO.BlMissingEntityException("Data exception:", ex);
        }

    }
}