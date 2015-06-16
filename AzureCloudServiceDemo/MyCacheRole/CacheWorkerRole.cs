using Microsoft.ApplicationServer.Caching;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace MyCacheRole
{
    public class CacheWorkerRole : RoleEntryPoint
    {
        public static DataCacheFactory MyFactory;
        public static DataCache MyCache;

        public string GetCacheMessage()
        {
            if (MyCache == null)
            {
                MyFactory = new DataCacheFactory();
                MyCache = MyFactory.GetDefaultCache();
                MyCache.Put("MyCache", "Welcome to use cache!");
            }

            return MyCache.Get("MyCache") as string;
        }
    }
}
