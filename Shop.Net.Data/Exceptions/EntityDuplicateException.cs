using System;

namespace Shop.Net.Data.Exceptions;

public class EntityDuplicateException : Exception
{
    public EntityDuplicateException() : base("Entity with same property exists in DB")
    { }

    public EntityDuplicateException(string? message) : base(message)
    { }
}