using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BO;


[Serializable]
public class BlAlreadyExistEntityException: Exception //If Already Exists
{
    public string? EntityName;
    public int EntityID;
    public int Choice;
    public BlAlreadyExistEntityException(string name, int id, int choice = 0)
    : base() { EntityName = name; EntityID = id; Choice = choice; }
    public BlAlreadyExistEntityException(string name, int id, string message, int choice = 0)
    : base(message) { EntityName = name; EntityID = id; Choice = choice; }
    public BlAlreadyExistEntityException(int id, string name, string message, Exception exception, int choice = 0)
    : base(message, exception) { EntityName = name; EntityID = id; Choice = choice; }
    public BlAlreadyExistEntityException(string message, Exception exception, int choice = 1)
    : base(message, exception) { Choice = choice; }
    override public string ToString()
    {
        if (Choice == -1)
            return $"{EntityName} number {EntityID} exists in an Order";
        if (Choice == 0)
            return $"{EntityID} already exists";
        else // => (choice == 1)
            return "";
    }

}

[Serializable]
public class BlMissingEntityException : Exception //If doesn't Exists
{
    public string? Name;
    public string? EntityName;
    public int EntityID;
    public int Choice;
    public BlMissingEntityException(string name, int id, int choice=0)
    : base() { EntityName = name; EntityID = id; Choice = choice; }
    public BlMissingEntityException(string name, string userName, int choice = 1)
    : base() { EntityName = name; Name = userName; Choice = choice; }
    public BlMissingEntityException(string name, int id, string message,int choice=0)
    : base(message) { EntityName = name; EntityID = id; Choice = choice; }
    public BlMissingEntityException(int id, string name, string message, Exception exception, int choice=0)
    : base(message, exception) { EntityName = name; EntityID = id; Choice = choice; }
    public BlMissingEntityException(string message, Exception exception, int choice = 2) //if (choice == 2)
    : base(message, exception) { Choice = choice; }
    override public string ToString()
    {
        if (Choice == 0)
            return $"{EntityID} of type {EntityName}, doesn't exist";
        else if(Choice == 1)
            return $"{Name} of type {EntityName}, doesn't exist";
        else
            return base.ToString();
    }
}


[Serializable]
public class BlInvalidEntityException : Exception //If Invalid
{
    public int EntityId;
    public string? EntityName;
    public int EntityChoice;
    public string? Status;

    public BlInvalidEntityException(string name, int entityChoice) :base()
    {
        EntityChoice = entityChoice;
        EntityName = name;
    }
    public BlInvalidEntityException(string name, int entityChoice, string status) : base()
    {
        EntityChoice = entityChoice;
        EntityName = name;
        Status = status;
    }
    public BlInvalidEntityException(int id, string name, int entityChoice)
        : base()
    {
        EntityId = id; EntityName = name; EntityChoice = entityChoice;
    }
    public BlInvalidEntityException(int id, string name, int entityChoice, string message)
        : base(message) { EntityId = id; EntityName = name; EntityChoice = entityChoice; }
    public BlInvalidEntityException(string message, Exception exception)
    : base(message, exception) { }

    public override string ToString()
    {
        if (EntityChoice == 0) //if negative
            return $" The {EntityName} is negative";
        if (EntityChoice == 1) //if null
            return $" The {EntityName} isn't valid";
        if (EntityChoice == 2)
            return $" The {EntityName} has not {Status} yet"; //used for incorrect update on order status
        if (EntityChoice == 3)
            return $" The {EntityName} has already {Status}";//used for incorrect update on order status
        else
            return $"{EntityId} of type {EntityName}, isn't valid";

    }
    
        
}


[Serializable]
public class BlNotInStockException : Exception //If Not In Stock
{
    public int Entityamount;
    public string EntityName;
    public BlNotInStockException(int amount, string name)
        : base() { Entityamount = amount; EntityName = name; }

    public override string ToString()
    {
        if(Entityamount == 0)
            return $" {EntityName} isn't in stock";
        else
            return $" Not enough of {EntityName} in stock";
    }
}

[Serializable]
public class BlIncorrectDateException : Exception //If Incorrect Date
{
    public BlIncorrectDateException(string message, Exception exception)
    : base(message, exception) {}
    override public string ToString() => 
        base.ToString() /*+ $" Incorrect Date"*/;
}

[Serializable]
public class BlWrongCategoryException : Exception //If the Category is Wrong
{
    public BlWrongCategoryException() 
        : base() { }
    public BlWrongCategoryException(string message) 
        : base(message) { }
    public BlWrongCategoryException(string message, Exception exception)
        : base(message, exception) { }
    override public string ToString() =>
        $" Wrong Category";
}


