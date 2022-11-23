using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

public class DalProduct
{
    public int Add(Product product) //create
    {
        if (DataSource.indexProduct == DataSource.ProductsList.Length)
            throw new Exception("There is no more space for new products");
        for (int i = 0; i < DataSource.indexProduct; i++)
        {
            if (DataSource.ProductsList[i].Id == product.Id)
            {
                throw new Exception("The identifying number already exists");
            }
        }
        DataSource.ProductsList[DataSource.indexProduct++] = product;
        return product.Id;
    }
    public Product GetById(int id) //Request 
    {
        for (int i = 0; i < DataSource.indexProduct; i++)
        {            
            if (DataSource.ProductsList[i].Id == id)
            {
                return DataSource.ProductsList[i];
            }
        }
        throw new Exception("Id Number doesn't exist");
    }
    public void Update(Product product)
    {
        for (int i = 0; i < DataSource.indexProduct; i++)
        {
            if (DataSource.ProductsList[i].Id == product.Id)
            {
                DataSource.ProductsList[i] = product;
                return;
            }
        }
        throw new Exception("The identifying number doesn't exist");
    }
    public void Delete(int id)
    {
        for (int i = 0; i < DataSource.indexProduct; i++)
        {
            if (DataSource.ProductsList[i].Id == id)
            {
                DataSource.ProductsList[i] = DataSource.ProductsList[--DataSource.indexProduct];
                return;
            }
        }
        throw new Exception("Product doesn't exist");


    }
    public Product[] GetAll()
    {
        Product[] onlyProducts = new Product[DataSource.indexProduct];
        for (int i = 0; i < DataSource.indexProduct; i++)
        {
            onlyProducts[i] = DataSource.ProductsList[i];
        }
        return onlyProducts;
    }
}