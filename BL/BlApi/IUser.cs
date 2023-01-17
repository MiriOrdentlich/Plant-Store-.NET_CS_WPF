namespace BlApi;

public interface IUser
{
    public void Add(BO.User? user); //add a new user
    public BO.User? Get(string userName, string password);
}
