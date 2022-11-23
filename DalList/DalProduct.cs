using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using DalApi;
using System.Diagnostics;
using System.Xml.Linq;


namespace Dal;

internal class DalProduct : IProduct
{
    public int Add(Product product) //create
    {
        // search for product in list:
        if (DataSource.ProductsList.Contains(product)) // if found product -> throw exception
            throw new Exception("Product already exists");
        DataSource.ProductsList.Add(product); // if product isn't in list, add product to list
        return product.Id;
    }

    public Product GetById(int id) //Request 
    {
        //search for the wanted product
        Product p = DataSource.ProductsList.Find(x => x?.Id == id) ?? throw new Exception("Id Number doesn't exist"); //throw if doesn't exist
        return p;
    }
    public void Update(Product product)
    {
        //search for the wanted product on ProductsList that match the wanted id
        Product p = DataSource.ProductsList.Find(x => x?.Id == product.Id) ?? throw new Exception("The identifying number doesn't exist"); //throw if doesn't exist
        p = product;
    }
    public void Delete(int id)
    {
        //search for the wanted product on ProductsList that match the wanted id and delete it
        if (DataSource.ProductsList.RemoveAll(x => x?.Id == id) == 0)
            throw new Exception("Product doesn't exist"); //throw if doesn't exist


    }
    public IEnumerable<Product?> GetAll()
    {
        List<Product?> onlyProducts = new List<Product?>(); //create a new list
        foreach (var item in DataSource.ProductsList)
        {
            onlyProducts.Add(item);  //copy the existing list to the new one
        }
        return onlyProducts; //return the new list
    }
}