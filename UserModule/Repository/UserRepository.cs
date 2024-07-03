using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using UserModule.Controllers;
using UserModule.Models;

namespace UserModule.Service
{
    public class UserRepository : IUserRepository
    {
        public readonly BankingServiceContext _bankingServiceContext;
        private readonly ILogger<UserController> _logger;
        public UserRepository(BankingServiceContext bankingServiceContext, ILogger<UserController> logger)
        {
            _bankingServiceContext = bankingServiceContext;
            _logger = logger;
        }
        public async Task<Users> Add(Users user)
        {
            _logger.LogDebug("Inside AddUserRepo endpoint");
            try
            {
                var result = _bankingServiceContext.Add(user);
                await _bankingServiceContext.SaveChangesAsync();
                _logger.LogDebug($"The response for the AddUserRepo is .{(user.UserId)}");
                return result.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
                return new Users();
            }
        }

        public async Task<Users> Delete(int id)
        {
            _logger.LogDebug("Inside DeleteUserRepo endpoint");
            try
            {
                var user = await GetById(id);
                if (user == null)
                {
                    _logger.LogDebug("Id not found");
                    return null;
                }
                _bankingServiceContext.Users.Remove(user);
                _logger.LogDebug($"The response for the DeleteUser is .{user.UserId}");
                await _bankingServiceContext.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<Users>> GetAll()
        {
            _logger.LogDebug("Inside GetAll UserRepo endpoint");
            try
            {
                var response = await _bankingServiceContext.Users.ToListAsync();
                if (response != null)
                {
                    _logger.LogDebug($"{response.Count} users");
                    return response;
                }
                else
                {
                    _logger.LogDebug("Data not found");
                    return null;
                }

            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
                return null;
            }
        }

        public async Task<Users> GetById(int id)
        {
            _logger.LogDebug("Inside GetById UserRepo endpoint");
            try
            {
                var response = await _bankingServiceContext.Users.FindAsync(id);
                if (response != null)
                {
                    _logger.LogDebug($"{response.UserId} GetById users");
                    return response;
                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
                return null; ;
            }
        }

        public async Task<Users> Update(int id, Users user)
        {
            _logger.LogDebug("Inside Update UserRepo endpoint");
            _bankingServiceContext.Entry(user).State = EntityState.Modified;
            try
            {
                await _bankingServiceContext.SaveChangesAsync();
                _logger.LogDebug($"{nameof(Update)}");
            }
            catch (DbUpdateConcurrencyException)
            {
                _logger.LogDebug("User not Updated");
                var findUser = await GetById(id);
                if (findUser == null)
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            _logger.LogDebug($"{user.UserId} Update users");
            return user;
        }
    }
}
