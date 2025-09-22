using UserManagement.API.Dtos;

namespace UserManagement.API.Services
{
    public class UserService : IUserService
    {
        private readonly List<UserDto> _users = new();
        private int _nextId = 1;

        public IEnumerable<UserDto> GetAll() => _users;

        public UserDto? GetById(int id) => _users.FirstOrDefault(u => u.Id == id);

        public UserDto Add(UserDto user)
        {
            user.Id = _nextId++;
            _users.Add(user);
            return user;
        }

        public bool Update(int id, UserDto updated)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null) return false;

            user.Name = updated.Name;
            user.Email = updated.Email;
            user.Phone = updated.Phone;
            user.EmployeeNumber = updated.EmployeeNumber;
            return true;
        }

        public bool Delete(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null) return false;
            _users.Remove(user);
            return true;
        }
    }
}
