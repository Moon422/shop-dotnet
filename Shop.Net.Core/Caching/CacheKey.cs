namespace Shop.Net.Core.Caching;

public class CacheKey
{
    public CacheKey(string key, string prefix)
    {
        Key = key;
        Prefix = prefix;
    }

    public string Key { get; set; }
    public string Prefix { get; set; }
}