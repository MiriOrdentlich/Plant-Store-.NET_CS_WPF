using DO;

using DalApi;


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
        return DataSource.ProductsList.Find(x => x?.Id == id) ?? throw new Exception("Id Number doesn't exist"); //throw if doesn't exist
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
        //create a new list, copy the existing list to the new one, return the new list.
        return new List<Product?>(DataSource.ProductsList);
    }
}