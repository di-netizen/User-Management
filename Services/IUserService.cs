using UserManagement.API.Dtos;

namespace UserManagement.API.Services
{
    public interface IUserService
    {
        IEnumerable<UserDto> GetAll();
        UserDto? GetById(int id);
        UserDto Add(UserDto user);
        bool Update(int id, UserDto user);
        bool Delete(int id);
    }
}
