//using BO;
//using DO;

//namespace BlImplementation;

//internal class Users : BlApi.IUsers
//{
//    private static readonly DalApi.IDal dal = DalApi.Factory.Get()!;
//    public void AddUser(Users user)
//    {
//        try
//        {
//            if (user.UserName is null)
//                throw new BO.BlInvalidEntityException("UserName", 1);
//            if (user.Name is null)
//                throw new BO.BlInvalidEntityException("Name", 1); 
//            if (user.Adress is null)
//                    throw new BO.BlInvalidEntityException("Adress", 1);
//            if (user.Phone is null)
//                throw new BO.BlInvalidEntityException("Phone", 0);
//            if (user.Email is null)
//                throw new BO.BlInvalidEntityException("Email", 1);
//            if (user.Password is null)
//                throw new BO.BlInvalidEntityException("Password", 1);

//            DO.Users newDoUser = new DO.Users() //create a new data layer user
//            {
//                //copy the fields
//                UserName = UserName,
//                Name =  Name ,
//                Adress = Adress,
//                Phone = Phone,
//                Email = Email,
//                Password = Password
//            };
//             dal.Users.Add(newDoUser);

//        }
//        catch (DO.DalAlreadyExistsIdException ex)
//        {
//            throw new BO.BlAlreadyExistEntityException("Data exception:", ex);
//        }
//    }

//    public void CheckUser(string UserName, string Password)
//    {
//        try
//        {
//            if (UserName is null)
//                throw new BO.BlInvalidEntityException("UserName", 1);
//            if (Password is null)
//                throw new BO.BlInvalidEntityException("Password", 1);
//            var UsersItemsList = from doUser in dal.Users.GetAll() // get a list of products and scan it
//                                   select Get(doUser?.UserName ?? null, doUser?.Password ?? null);
//            var list = dal.Users.GetAll(x => DalApi.IUsers.CheckUser(UserName, Password));
//            if (list.Any()) //if there is a user that match the wanted one
//                throw new BO.BlAlreadyExistEntityException("Product", productId, -1); //Exception: Product exists in an order
//            Users u = DataSource.UsersList.Find(Users => Users.UserName == name);
//            if (u.Password == psw) ;

//               return ;

//        }
//        catch (DO.DalAlreadyExistsIdException ex)
//        {
//            throw new BO.BlAlreadyExistEntityException("Data exception:", ex);
//        }
//    }

   
//    public Users Get(Func<Users?, bool> filter) //Request 
//    {
//        //search for the wanted user, throw if doesn't exist
//        return DataSource.UsersList.Find(x => filter(x)) ??
//            throw new DO.DalDoesNotExistUserNameException("", "Users"); //PROBLEM!!!!!!!!!
//    }
   
//    public IEnumerable<Users?> GetAll(Func<Users?, bool>? filter)
//    {
//        //create a new list, copy the existing list to the new one, return the new list.

//        if (filter == null)
//        {
//            return DataSource.UsersList.Select(x => x);
//        }
//        else
//        {
//            return from x in DataSource.UsersList
//                   where filter(x)
//                   select x;
//        }
//    }
//}

