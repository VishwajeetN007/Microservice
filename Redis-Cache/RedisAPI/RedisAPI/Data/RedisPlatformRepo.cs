using RedisAPI.Models;
using StackExchange.Redis;
using System.Text.Json;
namespace RedisAPI.Data
{
    public class RedisPlatformRepo : IPlatformRepo
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisPlatformRepo(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }
        public void CreatePlatform(Platform plat)
        {
            if (plat == null)
            {
                throw new ArgumentOutOfRangeException(nameof(plat));
            }

            var db = _redis.GetDatabase();

            var serialPlat = JsonSerializer.Serialize(plat);

            //// Set key to hold the string value. If it already holds a value. it is overwritten, regardless of its type.
            // db.StringSet(plat.Id, serialPlat);

            //// Add the specified member to the set stored at key.
            //db.SetAdd("PlatformSet",serialPlat);

            db.HashSet($"hashplatform", new HashEntry[]
               {new HashEntry(plat.Id, serialPlat)});
        }

        public Platform? GetPlatformById(string id)
        {
            // Obtain an interactive connection to a database inside reids.
            var db = _redis.GetDatabase();

            //// Get the value of key. ( If the key does not exist, the special value nil is returned.
            //// An error is returned, if the value stored at key is not a string, because GET handles string value 
            //var plat = db.StringGet(id);


            //// Return the value associated with field in the hash stored at key.
             var plat = db.HashGet("hashplatform", id);

            if (!string.IsNullOrEmpty(plat))
            {
                return JsonSerializer.Deserialize<Platform>(plat);
            }
            return null;
        }

        public IEnumerable<Platform?>? GetAllPlatforms()
        {
            // Obtain an interactive connection to a database inside reids.
            var db = _redis.GetDatabase();

            /* Return all the members of the set value stored at key.
            //var completeSet = db.SetMembers("PlatformSet");

            if (completeSet.Length > 0)
            {
                var obj = Array.ConvertAll(completeSet, val =>
                    JsonSerializer.Deserialize<Platform>(val)).ToList();
                return obj;
            }
            */

            //// Return all fields and values of the hash stored at key. 
            var completeSet = db.HashGetAll("hashplatform");

            if (completeSet.Length > 0)
            {
                var obj = Array.ConvertAll(completeSet, val =>
                    JsonSerializer.Deserialize<Platform>(val.Value)).ToList();
                return obj;
            }
            return null;
        }

    }
}
