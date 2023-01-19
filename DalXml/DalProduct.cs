using DO;
namespace Dal;

///////////////////////////////////////////
//implement IProduct with XML Serializer
//////////////////////////////////////////
class DalProduct : DalApi.IProduct
{
    //const string s_Products = @"products"; //XML Serializer
    readonly string s_Products = "products";

    public IEnumerable<DO.Product?> GetAll(Func<DO.Product?, bool>? filter = null)
    {
        List<DO.Product?> listProducts = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_Products);

        if (filter == null)
            return listProducts.Select(pro => pro).OrderBy(pro => pro?.Id);
        else
            return listProducts.Where(filter).OrderBy(pro => pro?.Id);
    }

    public DO.Product Get(Func<Product?, bool> filter)
    {
        List<DO.Product?> listProducts = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_Products);

        DO.Product pro = listProducts.Where(filter).FirstOrDefault() ??
            throw new DalDoesNotExistIdException(-1, "Product");
        return pro;
    }

    public int Add(DO.Product product)
    {
        List<DO.Product?> listProducts = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_Products);

        if (listProducts.FirstOrDefault(pro => pro?.Id == product.Id) != null)
            throw new Exception("id already exist"); //new DalAlreadyExistIdException(pr.ID, "Product");

        listProducts.Add(product);

        XMLTools.SaveListToXMLSerializer(listProducts, s_Products);

        return product.Id;
    }

    public void Delete(int id)
    {
        List<DO.Product?> listProducts = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_Products);

        if (listProducts.RemoveAll(pro => pro?.Id == id) == 0)
            throw new Exception("missing id"); //new DalMissingIdException(id, "Product");

        XMLTools.SaveListToXMLSerializer(listProducts, s_Products);
    }
    public void Update(DO.Product product)
    {
        Delete(product.Id);
        Add(product);
    }
}