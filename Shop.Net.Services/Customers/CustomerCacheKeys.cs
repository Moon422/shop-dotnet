using Shop.Net.Core.Caching;

namespace Shop.Net.Services.Customers;

public static class CustomerCacheKeys
{
    public const string PREFIX = "Shop.Net.Customers";

    public static CacheKey GetCustomerByIdKey => new CacheKey(
        "Shop.Net.Customers.GetCustomerById.{0}", PREFIX);

    public static CacheKey GetPasswordByCustomerIdKey => new CacheKey(
        "Shop.Net.Customers.GetPasswordByCustomerId.{0}", PREFIX);

    public static CacheKey GetCustomerByEmailKey => new CacheKey(
        "Shop.Net.Customers.GetCustomerByEmail.{0}.{1}", PREFIX);

    public static CacheKey GetCustomerRolesCacheKey => new CacheKey(
        "Shop.Net.Customers.GetCustomerRoles.{0}", PREFIX);
}