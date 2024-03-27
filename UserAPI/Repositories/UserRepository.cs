using Microsoft.EntityFrameworkCore;
using UserAPI.DataAccess;
using UserAPI.Models;
using UserAPI.Models.Dto;

namespace UserAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository( AppDbContext context)
        {
            _context = context;
        }
        public async Task<UserDto?> GetUserById(string userId)
        {
            var user = await _context.ApplycationUsers.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) { return null;}
            UserDto userDto = new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.UserName,
                PhoneNumber = user.PhoneNumber,
                BirthDay = user.BirthDay
            };
            return userDto;
        }

        public async Task<UserDto?> UpdateUserProfile(string id, UserDto userDto)
        {
            var user = _context.ApplycationUsers.FirstOrDefault(u => u.Id == id);
            user.FullName = userDto.FullName;
            user.PhoneNumber = userDto.PhoneNumber;
            user.BirthDay = userDto.BirthDay;
            await _context.SaveChangesAsync();
            return userDto;
        }
    }
}
