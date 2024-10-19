using System;

namespace Shop.Net.Core;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
public class ScopeDependencyAttribute : Attribute
{
    private readonly Type interfaceType;

    public ScopeDependencyAttribute(Type interfaceType)
    {
        this.interfaceType = interfaceType;
    }

    public Type InterfaceType => interfaceType;
}