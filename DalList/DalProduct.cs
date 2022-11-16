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
        if (DataSource.Config.indexProduct == DataSource.ProductArr.Length)
            throw new Exception("There is no more space for new products");
        for (int i = 0; i < DataSource.Config.indexProduct; i++)
        {
            if (DataSource.ProductArr[i].Id == product.Id)
            {
                throw new Exception("The identifying number already exists");
            }
        }
        DataSource.ProductArr[DataSource.Config.indexProduct++] = product;
        return product.Id;
    }
    public Product GetById(int id) //Request 
    {
        for (int i = 0; i < DataSource.Config.indexProduct; i++)
        {            
            if (DataSource.ProductArr[i].Id == id)
            {
                return DataSource.ProductArr[i];
            }
        }
        throw new Exception("Id Number doesn't exist");
    }
    public void Update(Product product)
    {
        for (int i = 0; i < DataSource.Config.indexProduct; i++)
        {
            if (DataSource.ProductArr[i].Id == product.Id)
            {
                DataSource.ProductArr[i] = product;
                return;
            }
        }
        throw new Exception("The identifying number doesn't exist");
    }
    public void Delete(int id)
    {
        for (int i = 0; i < DataSource.Config.indexProduct; i++)
        {
            if (DataSource.ProductArr[i].Id == id)
            {
                DataSource.ProductArr[i] = DataSource.ProductArr[--DataSource.Config.indexProduct];
                return;
            }
        }
        throw new Exception("Product doesn't exist");


        // NOTE: your function doesnt work. i didnt feel like working up my head over this so i used the less precise function.
        // anyway, if u want to figure out the problem be my guest..

        //int a = -1;
        //for (int i = 0; i < DataSource.Config.indexProduct; i++)
        //{

        //    if (DataSource.ProductArr[i].Id == id)
        //    {
        //        a = i;
        //    }
        //}
        //if (a != -1)
        //{
        //    int j = a;
        //    for (; j < DataSource.Config.indexProduct - 2; j++)
        //    {
        //        DataSource.ProductArr[j] = DataSource.ProductArr[j + 1];

        //    }
        //    DataSource.Config.indexProduct--;
        //}
        //throw new Exception("The identifying number doesn't exists");
    }
    public Product[] GetAll()
    {
        Product[] onlyProducts = new Product[DataSource.Config.indexProduct];
        for (int i = 0; i < DataSource.Config.indexProduct; i++)
        {
            onlyProducts[i] = DataSource.ProductArr[i];
        }
        return onlyProducts;
    }
}