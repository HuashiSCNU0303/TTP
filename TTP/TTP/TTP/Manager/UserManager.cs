using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTP.Model;

namespace TTP.Data
{
    public class UserManager
    {
        IUserRestService restService;
		public UserManager(IUserRestService service)
		{
			restService = service;
		}

		public Task<User> GetUserTasksAsync(long id)
		{
			return restService.GetUserAsync(id);
		}

		public Task<User> GetUserByNameTasksAsync(String userName)
		{
			return restService.GetUserByNameAsync(userName);
		}

		public Task<User> AddUserTaskAsync(User user)
		{
			return restService.AddUserAsync(user);
		}

		public Task DeleteUserTaskAsync(User user)
		{
			return restService.DeleteUserAsync(user.UserId);
		}

		public Task ModifyUserTaskAsync(User user)
		{
			return restService.ModifyUserAsync(user);
		}
	}
}
