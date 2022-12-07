using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    //קיים כבר
    //לא קיים
    //לא תקין
    //נגמר מלאי
    //תאריך שגוי
    //קטגוריה שגויה
    [Serializable]
    public class BlAlreadyExistEntityException: Exception //If Already Exists
    {

        public BlAlreadyExistEntityException(string message)
        : base(message) { }
        public BlAlreadyExistEntityException(string message, Exception exception)
        : base(message, exception){ }

        public override string ToString()
        {
            return base.ToString() + $"Entity is already exists.";
        }
    }

    [Serializable]
    public class BlMissingEntityException : Exception //If doesn't Exists
    {
        public BlMissingEntityException(string message)
        : base(message) { }

        public BlMissingEntityException(string message, Exception exception)
        : base(message, exception) { }

        public override string ToString()
        {
            return base.ToString() + $"Missing Entity";
        }
    }

    [Serializable]
    public class BlInvalidEntityException : Exception //If Invalid
    {
        public int EntityId;
        public string EntityName;
        public int EntityChoice;

        public BlInvalidEntityException(string name, int entityChoice) :base()
        {
            EntityId = entityChoice;
            EntityName = name;
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
                return $" The {EntityName}  is null";
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

        //public BlNotInStockException(int id, string name, string message)
        //    : base(message) { EntityId = id; EntityName = name; }

        public override string ToString()
        {
            if(Entityamount == 0)
                return $" {EntityName} isn't in stock";
            else
                return $" Not enough in stock";
        }
    }

    [Serializable]
    public class BlIncorrectDateException : Exception //If Incorrect Date
    {

        override public string ToString() =>
            $" Incorrect Date";
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


}
