using DalApi;
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
        List<DO.Product?> listProducts = XmlTools.LoadListFromXMLSerializer<DO.Product>(s_Products);

        if (filter == null)
            return listProducts.Select(pro => pro).OrderBy(pro => pro?.Id);
        else
            return listProducts.Where(filter).OrderBy(pro => pro?.Id);
    }

    public DO.Product Get(Func<Product?, bool> filter)
    {
        List<DO.Product?> listProducts = XmlTools.LoadListFromXMLSerializer<DO.Product>(s_Products);

        DO.Product pro = listProducts.Where(filter).FirstOrDefault() ??
            throw new DalDoesNotExistIdException(-1, "Product");
        return pro;
    }

    public int Add(DO.Product product)
    {
        List<DO.Product?> listProducts = XmlTools.LoadListFromXMLSerializer<DO.Product>(s_Products);

        if (listProducts.FirstOrDefault(pro => pro?.Id == product.Id) != null)
            throw new DalAlreadyExistsIdException(product.Id, "Product");

        product.Id = Config.GetNextProductId();


        listProducts.Add(product);
        Config.SetNextOrderId(product.Id + 1);

        XmlTools.SaveListToXMLSerializer(listProducts, s_Products);

        return product.Id;
    }

    public void Delete(int id)
    {
        List<DO.Product?> listProducts = XmlTools.LoadListFromXMLSerializer<DO.Product>(s_Products);

        if (listProducts.RemoveAll(pro => pro?.Id == id) == 0)
            throw new DO.DalDoesNotExistIdException(id, "Product");

        XmlTools.SaveListToXMLSerializer(listProducts, s_Products);
    }

    public void Update(DO.Product product)
    {
        Delete(product.Id);
        Add(product);
    }
}