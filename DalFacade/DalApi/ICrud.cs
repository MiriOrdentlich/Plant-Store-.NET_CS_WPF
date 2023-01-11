using DO;

namespace DalApi;

public interface ICrud<T> where T : struct
{
    int Add(T item);
    T Get(Func<T?, bool> filter);
    void Update(T item);
    void Delete(int id);
    IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
}
