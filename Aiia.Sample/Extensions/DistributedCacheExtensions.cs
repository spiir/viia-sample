using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Aiia.Sample.Extensions
{
    internal static class DistributedCacheExtensions
    {
        /// <summary>
        ///     Asynchronously gets a object from the specified cache with the specified key.
        /// </summary>
        /// <param name="cache">The cache in which to store the data.</param>
        /// <param name="key">The key to get the stored data for.</param>
        /// <param name="token">Optional. A <see cref="CancellationToken" /> to cancel the operation.</param>
        /// <returns>A task that gets the object value from the stored cache key.</returns>
        public static async Task<T> GetObjectAsync<T>(this IDistributedCache cache, string key, CancellationToken token = default)
            where T : class
        {
            var data = await cache.GetAsync(key, token);
            if (data == null)
                return null;

            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(data, 0, data.Length));
        }

        /// <summary>
        ///     Asynchronously sets a object in the specified cache with the specified key.
        /// </summary>
        /// <param name="cache">The cache in which to store the data.</param>
        /// <param name="key">The key to store the data in.</param>
        /// <param name="value">The data to store in the cache.</param>
        /// <param name="options">The cache options for the entry.</param>
        /// <param name="token">Optional. A <see cref="CancellationToken" /> to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous set operation.</returns>
        /// <exception cref="System.ArgumentNullException">
        ///     Thrown when <paramref name="key" /> or <paramref name="value" /> is
        ///     null.
        /// </exception>
        public static Task SetObjectAsync<T>(this IDistributedCache cache,
                                             string key,
                                             T value,
                                             DistributedCacheEntryOptions options,
                                             CancellationToken token = default)
            where T : class
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            return cache.SetAsync(key, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)), options, token);
        }
    }
}
