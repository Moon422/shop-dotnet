using Shop.Net.Core.Caching;

namespace Shop.Net.Services.Customers;

public static class CustomerCacheKeys
{
    public const string PREFIX = "Shop.Net.Customers";

    public static CacheKey GetCustomerByIdKey => new CacheKey(
        "Shop.Net.Customers.GetCustomerById.{0}", PREFIX);

    public static CacheKey GetPasswordByCustomerIdKey => new CacheKey(
        "Shop.Net.Customers.GetPasswordByCustomerId.{0}", PREFIX);
}