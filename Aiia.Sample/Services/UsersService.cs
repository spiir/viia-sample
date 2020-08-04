using System;
using Aiia.Sample.Repositories;

namespace Aiia.Sample.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
        }
    }

    public interface IUsersService { }
}
