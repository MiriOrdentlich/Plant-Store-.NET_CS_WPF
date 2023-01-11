using System.Runtime.Serialization;
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
        override public string ToString() =>
            $"Id: {EntityId} of type {EntityName}, doesn't exist";
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
            $"{EntityName} of type {EntityType}, doesn't exist";
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
            : base() { EntityId= id; EntityName = name; }
        public DalAlreadyExistsIdException(int id, string name, string message) 
            : base(message) { EntityId = id; EntityName = name; }
        public DalAlreadyExistsIdException(int id, string name,string message, Exception inner) 
            : base(message, inner) { EntityId = id; EntityName = name; }
        //protected DalAlreadyExistsIdException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        //public OverloadCapacityException(int capacity, string message) : base(message) => this.capacity = capacity;
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
            $"{EntityName} of type {EntityType}, is already exists";
    }

    //[Serializable]
    //public class OverloadCapacityException : Exception
    //{
    //    public int capacity { get; private set; }
    //    public OverloadCapacityException() : base() { }
    //    public OverloadCapacityException(string message) : base(message) { }
    //    public OverloadCapacityException(string message, Exception inner) : base(message, inner) { }
    //    protected OverloadCapacityException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    //    // special constructor for our custom exception
    //    public OverloadCapacityException(int capacity, string message) : base(message) =>
    //    this.capacity = capacity;
    //    override public string ToString() =>
    //    "OverloadCapacityException: DAL capacity of " + capacity + " overloaded\n" + Message;
    //}


}
