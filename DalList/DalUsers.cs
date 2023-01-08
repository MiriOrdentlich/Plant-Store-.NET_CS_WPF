//using DO;

//using DalApi;
//using System.Diagnostics;
//using System.Xml.Linq;


//namespace Dal;

//internal class DalUsers : IUsers
//{
//    public void Add(Users user) //create
//    {
//        // search for user in list:

//        //if found user -> throw exception
//        if (CheckUser(user.UserName, user.Password))
//            throw new DO.DalAlreadyExistsUserException(user.UserName, "Users");
//        DataSource.UsersList.Add(user); // if user isn't in list, add user to list
//        //return user;
//    }
//    public bool CheckUser(string name, string psw) 
//    {
        
//        Users u = DataSource.UsersList.Find(Users => Users.UserName == name);
//        if(u.Password== psw)
//        return true;

//        return false;
//    }
//    public Users Get(string name, string psw) //Request 
//    {
//        //search for the wanted user, throw if doesn't exist
//        if(!CheckUser(name, psw))
//            throw new DO.DalDoesNotExistUserNameException( "", "Users");
//        Users u = DataSource.UsersList.Find(Users=> Users.UserName == name);
//        return u;
//    }
//    public IEnumerable<Users?> GetAll(Func<Users?, bool> filter)
//        {
//            //create a new list, copy the existing list to the new one, return the new list.

//            return from users in DataSource.UsersList
//                   select users;
//        }
    
//}
