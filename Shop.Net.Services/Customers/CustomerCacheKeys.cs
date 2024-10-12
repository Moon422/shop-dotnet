using Shop.Net.Core.Caching;

namespace Shop.Net.Services.Customers;

public static class CustomerCacheKeys
{
    public const string PREFIX = "Shop.Net.Customers.GetCustomerById";

    public static CacheKey GetCustomerByIdKey => new CacheKey(
        "Shop.Net.Customers.GetCustomerById.{0}", PREFIX);
}