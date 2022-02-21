using CloudCustomers.API.Models;
using CloudCustomers.API.Config;
using Microsoft.Extensions.Options;

namespace CloudCustomers.API.Services
{
    public interface IUsersService
    {
        Task<List<Users>> GetAllUsers();
    }

    public class UsersService : IUsersService
    {
        private readonly HttpClient _httpClient;
        private readonly UsersApiOptions _apiConfig;

        public UsersService(HttpClient httpClient, IOptions<UsersApiOptions> apiConfig)
        {
            _httpClient = httpClient;
            _apiConfig = apiConfig.Value;
        }

        public async Task<List<Users>> GetAllUsers()
        {
           var usersResponse = await _httpClient.GetAsync(_apiConfig.EndPoint);

           if (usersResponse.StatusCode == System.Net.HttpStatusCode.NotFound) {
               return new List<Users>();
           }

           var responseContent = usersResponse.Content;
           var allUsers = await responseContent.ReadFromJsonAsync<List<Users>>();

           return allUsers.ToList();
        }
    }
}