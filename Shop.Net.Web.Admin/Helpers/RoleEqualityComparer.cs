using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Shop.Net.Web.Admin.Helpers;

public class RoleEqualityComparer : IEqualityComparer<string>
{
    public bool Equals(string? x, string? y)
    {
        if (string.IsNullOrWhiteSpace(x) && string.IsNullOrWhiteSpace(y))
        {
            return true;
        }
        else if (string.IsNullOrWhiteSpace(x))
        {
            return false;
        }
        else if (string.IsNullOrWhiteSpace(y))
        {
            return false;
        }

        return x.Equals(y, StringComparison.OrdinalIgnoreCase);
    }

    public int GetHashCode([DisallowNull] string obj)
    {
        return obj.GetHashCode();
    }
}
