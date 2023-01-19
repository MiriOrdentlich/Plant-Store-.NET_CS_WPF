using DalApi;
using DO;
using System.Xml.Linq;

namespace Dal;

internal class DalUser : IUser
{
    readonly string s_Users = "users";

    public void Add(DO.User doUser)
    {
        XElement usersRootElem = XmlTools.LoadListFromXMLElement(s_Users);

        XElement? user = (from usr in usersRootElem.Elements()
                          where (string?)usr.Element("Name") == doUser.Name && (string?)usr.Element("Password") == doUser.Password
                          select usr).FirstOrDefault();
        if (user != null)
            throw new DalAlreadyExistsUserException((string)user.Element("Name")!, "User");//Exception("id already exist"); // fix to: throw new DalMissingIdException(password);

        XElement userElem = new XElement("User",
                                   new XElement("isManager", doUser.isManager),
                                   new XElement("Name", doUser.Name),
                                   new XElement("Address", doUser.Address),
                                   new XElement("Email", doUser.Email),
                                   new XElement("Password", doUser.Password));

        usersRootElem.Add(userElem);

        XmlTools.SaveListToXMLElement(usersRootElem, s_Users);
    }

    static DO.User? createUserfromXElement(XElement usr)
    {
        return new DO.User()
        {
            //isManager = usr.ToIntNullable("isManager") == 1 ? true : false,
            isManager = (Boolean)usr.ToBooleanNullable("isManager")!,
            Name = (string?)usr.Element("Name"),
            Address = (string?)usr.Element("Address"),
            Email = (string?)usr.Element("Email"),
            Password = (string?)usr.Element("Password")
        };
    }

    public User? Get(string name, string psw)
    {
        XElement usersRootElem = XmlTools.LoadListFromXMLElement(s_Users);

        return (from usr in usersRootElem.Elements()
                 where (string?)usr.Element("Name") == name && (string?)usr.Element("Password") == psw
                 select (DO.User?)createUserfromXElement(usr)).FirstOrDefault()
                ?? throw new DalDoesNotExistUserNameException(name, "User"); // fix to: throw new DalMissingIdException(id);
    }
}
