
using BO;
using DalApi;
using DO;

namespace BlImplementation;

internal class User : BlApi.IUser
{
    private static readonly DalApi.IDal dal = DalApi.Factory.Get()!;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <exception cref="BO.BlInvalidEntityException"></exception>
    /// <exception cref="BO.BlAlreadyExistEntityException"></exception>
    public void Add(BO.User? user)
    {
        try
        {
            if (user?.Name is null)
                throw new BO.BlInvalidEntityException("Name", 1);
            if (user?.Address is null)
                throw new BO.BlInvalidEntityException("Address", 1);
            if (user?.Email is null)
                throw new BO.BlInvalidEntityException("Email", 1);
            if (user?.Password is null)
                throw new BO.BlInvalidEntityException("Password", 1);

            DO.User newDoUser = new DO.User() //create a new data layer user
            {
                //copy the fields
                isManager = user.isManager,
                Name = user.Name,
                Address = user.Address,
                Email = user.Email,
                Password = user.Password
            };
            dal.User.Add(newDoUser);
        }
        catch (DO.DalAlreadyExistsIdException ex)
        {
            throw new BO.BlAlreadyExistEntityException("Data exception:", ex);
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public BO.User? Get(string userName, string password) //Request 
    {
        //search for the wanted user, throw if doesn't exist

        DO.User doUser = dal.User.Get(userName, password) ??
            throw new BlMissingEntityException("User", userName);

        return new BO.User() //create a new data layer user
        {
            //copy the fields
            isManager = doUser.isManager,
            Name = doUser.Name,
            Address = doUser.Address,
            Email = doUser.Email,
            Password = doUser.Password
        };
    }
}

