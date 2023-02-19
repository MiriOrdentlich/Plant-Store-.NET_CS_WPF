using DO;

namespace DalApi;

public interface IProduct : ICrud<Product> //define empty CRUD interface for Product entity
{
    public Product GetById(int id);

}
