using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    [Serializable]
    public class BlAlreadyExistEntityException: Exception
    {
        public BlAlreadyExistEntityException(string message, Exception exception)
        : base(message, exception){ }

        public override string ToString()
        {
            return base.ToString() + $"Entity is already exists.";
        }
    }

    [Serializable]
    public class BlMissingEntityException : Exception
    {
        public BlMissingEntityException(string message, Exception exception)
        : base(message, exception) { }

        public override string ToString()
        {
            return base.ToString() + $"Missing Entity";
        }
    }

    [Serializable]
    public class BlInvalidEntityException : Exception
    {
        public int EntityId;
        public string EntityName;
        public int EntityChoice;
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
            if(EntityChoice == 0) //if the amount is negative
                return $" Amount is negative";
            if(EntityChoice == 1) //if the price is negative
                return $" Price is negative";
            if (EntityChoice == 2) //if the Address is null
                return $"Address is null";
            else
                return $"{EntityId} of type {EntityName}, isn't valid";

        }
        
            
    }

    [Serializable]
    public class BlNotInStockException : Exception
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
    public class BlIncorrectDateException : Exception
    {

        override public string ToString() =>
            $" Incorrect Date";
    }

    [Serializable]
    public class BlWrongCategoryException : Exception
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
