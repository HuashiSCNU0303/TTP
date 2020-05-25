using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTP.Model;

namespace TTP.Data
{
    public interface IUserRestService
    {
        Task DeleteUserAsync(long id);

        Task<User> AddUserAsync(User user);

        Task ModifyUserAsync(User user);

        Task<User> GetUserAsync(long id);

        Task<User> GetUserByNameAsync(String userName);
    }
}
