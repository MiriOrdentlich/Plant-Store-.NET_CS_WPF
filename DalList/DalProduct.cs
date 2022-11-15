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
        if (DataSource.Config.indexProduct == DataSource.ProductArr.Length - 1)        
            throw new Exception("There is no more space for new products\n");
        for (int i = 0; i < DataSource.Config.indexProduct; i++)
        {

            if (DataSource.productArr[i].Id == product.Id)
            {
                throw new Exception("The identifying number already exists");
            }
        }
        DataSource.productArr[DataSource.Config.indexProduct] = product;
        return product.Id;
    }
    public Product GetById(int id) //Request 
    {
        Product p;
        for (int i = 0; i < DataSource.Config.indexProduct; i++)
        {
            p = DataSource.ProductArr[i];
            if (p.Id == id)
            {
                return p;
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
            }
            else
                throw new Exception("The identifying number doesn't exists");

        }

    }

    public void Delete(int id)
    {
        int a = -1;
        for (int i = 0; i < DataSource.Config.indexProduct; i++)
        {

            if (DataSource.ProductArr[i].Id == id)
            {
                a = i;
            }
        }
        if (a != -1)
            {
                //DataSource.productArr[a] = null; //לא מדויק, צריך למחוק את מה שהוא באמת מכיל ולא רק להגיד שהוא לא מכיל כלום
                int j = a;
                for (; j < DataSource.Config.indexProduct - 2; j++)
                {
                    DataSource.productArr[j] = DataSource.productArr[j + 1];

            }
            DataSource.Config.indexProduct--;
            //DataSource.ProductArr[j+1] = null; //כנל
        }
        else
            throw new Exception("The identifying number doesn't exists");
    }

    public IEnumerable<Product?>[] GetAll()
    //מתודת בקשה\קריאה של רשימת כל האובייקטים של הישות (ללא פרמטרים)
    {
        Product[] onlyProducts = new string[DataSource.Config.indexProduct];
            for (int i = 0; i < onlyProducts.Length; i++)
            {
                onlyProducts[i] = DataSource.productArr[i];
            }
            return onlyProducts;
        }

    
}
