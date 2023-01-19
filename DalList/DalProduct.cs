using DO;

using DalApi;
using System.Diagnostics;
using System.Xml.Linq;


namespace Dal;

internal class DalProduct : IProduct
{
    public int Add(Product product) //create
    {
        // search for product in list:

        //if found product -> throw exception
        if (DataSource.ProductsList.Find(x => x?.Id == product.Id) != null) 
            throw new DO.DalAlreadyExistsIdException(product.Id, "Product");
        DataSource.ProductsList.Add(product); // if product isn't in list, add product to list
        return product.Id;
    }

    public Product Get(Func<Product?, bool> filter) //Request 
    {
        //search for the wanted product, throw if doesn't exist
        return DataSource.ProductsList.Find(x => filter(x)) ??
            throw new DO.DalDoesNotExistIdException(-1, "Product");
    }

    public void Update(Product product)
    {
        //search for the wanted product on ProductsList that match the wanted id
        if (DataSource.ProductsList.RemoveAll(x => x?.Id == product.Id) == 0)
            throw new DO.DalDoesNotExistIdException(product.Id, "Product");
        DataSource.ProductsList.Add(product);
    }

    public void Delete(int id)
    {
        //search for the wanted product on ProductsList that match the wanted id and delete it
        if (DataSource.ProductsList.RemoveAll(x => x?.Id == id) == 0)
            throw new DO.DalDoesNotExistIdException(id, "Product"); //throw if doesn't exist
    }
    
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter)
    {
        //create a new list, copy the existing list to the new one, return the new list.

        if (filter == null)
        {
            return DataSource.ProductsList.Select(x => x);
        }
        else
        {
            return from x in DataSource.ProductsList
                   where filter(x)
                   select x;
        }
    }
}