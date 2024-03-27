using UserAPI.Models.Dto;

namespace UserAPI.Repositories
{
    public interface IUserRepository
    {
        Task<UserDto?> GetUserById(string userId);
        Task<UserDto?> UpdateUserProfile(string id,UserDto userDto);
    }
}
