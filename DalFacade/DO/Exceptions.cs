using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DO;

//throw new Exception("what you asked for doesn't exist");
//throw new Exception("object already exists");

public class DalDoesNotExistException : Exception
{
    public DalDoesNotExistException(string? message) : base(message) { }
}

/// <summary>
/// Exception class for when the dal we tried to enter already exists.
/// the class inherits from the build-in class Exception.
/// we define in the class one method that set in base 
/// </summary>
public class DalAlreadyExistsException : Exception
{
    public DalAlreadyExistsException() : base() { }
    public DalAlreadyExistsException(string message) : base(message) { }
    public DalAlreadyExistsException(string message, Exception inner) : base(message, inner) { }
    protected DalAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    //public OverloadCapacityException(int capacity, string message) : base(message) => this.capacity = capacity;
    //override public string ToString() =>
   //"DalAlreadyExistsException: DAL capacity of " + capacity + " overloaded\n" + Message;
}

public class OverloadCapacityException : Exception
{
    public int capacity { get; private set; }
    public OverloadCapacityException() : base() { }
    public OverloadCapacityException(string message) : base(message) { }
    public OverloadCapacityException(string message, Exception inner) : base(message, inner) { }
    protected OverloadCapacityException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    // special constructor for our custom exception
    public OverloadCapacityException(int capacity, string message) : base(message) =>
    this.capacity = capacity;
    override public string ToString() =>
    "OverloadCapacityException: DAL capacity of " + capacity + " overloaded\n" + Message;
}




/*
public class DalDataCorruptionException : Exception
{
    public DalDataCorruptionException(string? message) : base(message) { }
}
*/