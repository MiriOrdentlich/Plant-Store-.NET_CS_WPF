using DalApi;

namespace Dal;

internal class DalUser : IUser
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <exception cref="DO.DalAlreadyExistsUserException"></exception>
    public void Add(DO.User user) //create
    {
        // search for user in list:

        //if found user -> throw exception
        if (Get(user.Name!, user.Password!) != null)
            throw new DO.DalAlreadyExistsUserException(user.Name!, "User");
        DataSource.UsersList.Add(user); // if user isn't in list, add user to list
    }

    /// <summary>
    /// return user by name and password
    /// </summary>
    /// <param name="name"></param>
    /// <param name="psw"></param>
    /// <returns></returns>
    public DO.User? Get(string name, string psw) //Request 
    {
        //search for the wanted user, return null if doesn't exist
        return DataSource.UsersList.Find(x => x?.Name == name && x?.Password == psw);
    }
}
