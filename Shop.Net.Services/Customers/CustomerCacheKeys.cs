using Shop.Net.Core.Caching;

namespace Shop.Net.Services.Customers;

public static class CustomerCacheKeys
{
    public const string CUSTOMER_PREFIX = "Shop.Net.Customers";

    public static CacheKey GetCustomerByIdKey => new CacheKey(
        "Shop.Net.Customers.GetCustomerById.{0}", CUSTOMER_PREFIX);

    public static CacheKey GetPasswordByCustomerIdKey => new CacheKey(
        "Shop.Net.Customers.GetPasswordByCustomerId.{0}", CUSTOMER_PREFIX);

    public static CacheKey GetCustomerByEmailKey => new CacheKey(
        "Shop.Net.Customers.GetCustomerByEmail.{0}.{1}", CUSTOMER_PREFIX);

    public static CacheKey GetCustomerRolesCacheKey => new CacheKey(
        "Shop.Net.Customers.GetCustomerRoles.{0}", CUSTOMER_PREFIX);

    public static CacheKey GetCustomerPermissionsCacheKey => new CacheKey(
    "Shop.Net.Customers.GetCustomerPermissions.{0}", CUSTOMER_PREFIX);
}

public static class RoleCacheKeys
{
    public const string PREFIX = "Shop.Net.Roles";

    public static CacheKey GetRoleByIdCacheKey => new CacheKey(
        "Shop.Net.Roles.GetRoleById.{0}", PREFIX);

    public static CacheKey GetRoleByNameCacheKey => new CacheKey(
        "Shop.Net.Roles.GetRoleByName.{0}", PREFIX);

    public static CacheKey GetRolePermissionsCacheKey => new CacheKey(
        "Shop.Net.Roles.GetRolePermissions.{0}", PREFIX);

    public static CacheKey GetAllRolesCacheKey => new CacheKey(
        "Shop.Net.Roles.GetAllRoles", PREFIX);
}