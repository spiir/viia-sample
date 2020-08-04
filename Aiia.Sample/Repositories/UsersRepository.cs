using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Aiia.Sample.Extensions;
using Aiia.Sample.Models;
using Dapper;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace Aiia.Sample.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private const int DEFAULT_CACHE_TIME_IN_MILLIS_SECONDS = 1000 * 60 * 5;
        private readonly IDistributedCache _cache;
        private readonly OptionsMonitor<SampleOptions> _options;
        private static string Table => "Users";
        private IDbConnection Connection => new SqlConnection(_options.CurrentValue.ConnectionStrings.Main);

        public UsersRepository(OptionsMonitor<SampleOptions> options, IDistributedCache cache)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<User> GetByEmail(string email)
        {
            var cache = await _cache.GetObjectAsync<User>(email);
            if (cache != null)
                return cache;

            var query = $"SELECT * FROM {Table} WHERE UserName = @email";
            using var conn = Connection;
            var user = await conn.QueryFirstOrDefaultAsync<User>(query,
                                                                 new
                                                                 {
                                                                     userName = email
                                                                 });
            await _cache.SetObjectAsync(email,
                                        user,
                                        new DistributedCacheEntryOptions
                                        { AbsoluteExpirationRelativeToNow = TimeSpan.FromMilliseconds(DEFAULT_CACHE_TIME_IN_MILLIS_SECONDS) });
            return user;
        }

        public async Task<User> GetById(string id)
        {
            var cache = await _cache.GetObjectAsync<User>(id);
            if (cache != null)
                return cache;

            var query = $"SELECT * FROM {Table} WHERE Id = @id";
            using var conn = Connection;
            var user = await conn.QueryFirstOrDefaultAsync<User>(query, new { id });
            await _cache.SetObjectAsync(id,
                                        user,
                                        new DistributedCacheEntryOptions
                                        { AbsoluteExpirationRelativeToNow = TimeSpan.FromMilliseconds(DEFAULT_CACHE_TIME_IN_MILLIS_SECONDS) });
            return user;
        }
    }

    public interface IUsersRepository
    {
        Task<User> GetByEmail(string email);
        Task<User> GetById(string id);
    }
}
