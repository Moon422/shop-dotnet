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

    public const string ROLE_PREFIX = "Shop.Net.Customers.Roles";

    public static CacheKey GetRoleByNameCacheKey => new CacheKey(
        "Shop.Net.Customers.Roles.GetRoleByName.{0}", ROLE_PREFIX);

    public static CacheKey GetRolePermissionsCacheKey => new CacheKey(
        "Shop.Net.Customers.Roles.GetRolePermissions.{0}", ROLE_PREFIX);
}