using System;
using System.Collections.Generic;
using System.Linq;
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
    public DalAlreadyExistsException(string? message) : base(message) { }
}

/*
public class DalDataCorruptionException : Exception
{
    public DalDataCorruptionException(string? message) : base(message) { }
}
*/