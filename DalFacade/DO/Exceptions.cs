﻿using System.Runtime.Serialization;
using System.Xml.Linq;


namespace DO
{

    [Serializable]
    public class DalConfigException : Exception
    {
        public DalConfigException(string msg) : base(msg) { }
        public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
    }

    [Serializable]
    public class DalDoesNotExistIdException : Exception
    {
        public int EntityId;
        public string EntityName;

        public DalDoesNotExistIdException(int id, string name) 
            : base() { EntityId = id; EntityName = name; }

        public DalDoesNotExistIdException(int id, string name, string message) 
            : base(message) { EntityId = id; EntityName = name; }

        public DalDoesNotExistIdException(int id, string name, string message, Exception inner) 
            : base(message, inner) { EntityId = id; EntityName = name; }
        override public string ToString()
        {
            if (EntityId == -1)
                return $"{EntityName} doesn't exist";
            else
                return $"Id: {EntityId} of type {EntityName}, doesn't exist";
        }
    }

    [Serializable]   
    public class DalDoesNotExistUserNameException : Exception
    {
        public string EntityName;
        public string EntityType;

        public DalDoesNotExistUserNameException(string name, string type)
            : base() { EntityName = name; EntityType = type; }
        public DalDoesNotExistUserNameException(string name, string type, string message)
            : base(message) { EntityName = name; EntityType = type; }
        public DalDoesNotExistUserNameException(string name, string type, string message, Exception inner)
            : base(message, inner) { EntityName = name; EntityType = type; }
        override public string ToString() =>
            $"{EntityType} {EntityName}, doesn't exist";
    }

    [Serializable]
    /// <summary>
    /// Exception class for when the dal we tried to enter already exists.
    /// the class inherits from the build-in class Exception.
    /// we define in the class one method that set in base 
    /// </summary>
    public class DalAlreadyExistsIdException : Exception
    {
        public int EntityId;
        public string EntityName;

        public DalAlreadyExistsIdException(int id, string name) 
            : base() { EntityId = id; EntityName = name; }
        public DalAlreadyExistsIdException(int id, string name, string message) 
            : base(message) { EntityId = id; EntityName = name; }
        public DalAlreadyExistsIdException(int id, string name,string message, Exception inner) 
            : base(message, inner) { EntityId = id; EntityName = name; }
        override public string ToString() =>
            $"Id: {EntityId} of type {EntityName}, is already exists";
    }

    [Serializable]
    public class DalAlreadyExistsUserException : Exception
    {
        public string EntityName; 
        public string EntityType;

        public DalAlreadyExistsUserException(string name, string type)
            : base() { EntityName = name; EntityType = type; }
        public DalAlreadyExistsUserException(string name, string type ,string message)
            : base(message) { EntityName = name; EntityType = type; }
        public DalAlreadyExistsUserException(string name, string type, string message, Exception inner)
            : base(message, inner) { EntityName = name; EntityType = type; }
        override public string ToString() =>
            $"{EntityType} {EntityName}, is already exists";
    }
}
