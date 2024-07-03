using UserModule.Models;

namespace UserModule.Service
{
    public interface IUserRepository
    {
        Task<IEnumerable<Users>> GetAll();
        Task<Users> GetById(int id);
        Task<Users> Add(Users user);
        Task<Users> Update(int id, Users user);
        Task<Users> Delete(int id);
    }
}
