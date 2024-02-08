using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CachingKataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        public static Dictionary<string, CacheItem> Cache { get; set; } = [];

        [HttpPost]
        public void Add([FromBody] CacheItem item)
        {
            Cache[item.Key] = item;
        }

        [HttpGet]
        public CacheItem Get(string key)
        {
            var cachedItem = Cache.GetValueOrDefault(key);
            if (cachedItem != null && cachedItem.ExpirationDate >= DateTime.Now)
            {
                return cachedItem;
            }

            return null;
        }

        [HttpPut]
        public void Replace(CacheItem item)
        {
            Cache[item.Key] = item;
        }

        [HttpDelete]
        public void Purge(string key)
        {
            Cache.Remove(key);
        }
    }

    public class CacheItem
    {
        public string Key { get; set; }

        public CacheItem(string key, string value, DateTime expirationDate)
        {
            Key = key;
            Value = value;
            ExpirationDate = expirationDate;
        }

        public string Value { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
