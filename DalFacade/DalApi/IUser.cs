using DO;

namespace DalApi;

public interface IUser /*: ICrud<User>*/ //define empty CRUD interface for User entity
{
    void Add(User user);

    User? Get(string name, string psw);

}
