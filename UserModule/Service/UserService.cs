using Microsoft.AspNetCore.Http.HttpResults;
using System.Xml;
using UserModule.Controllers;
using UserModule.Models;

namespace UserModule.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserController> _logger;
        public UserService(IUserRepository userRepository, ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Users>> GetAll()
        {
            _logger.LogDebug("Inside GetAll UserService endpoint");
            try
            {
                var result = await _userRepository.GetAll();
                if (result != null)
                {
                    _logger.LogDebug($"The response for the GetAll UserService is .{(result.Count())}");
                    return result;
                }
                else
                {
                    _logger.LogDebug("Data not found");
                    return null;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<Users> GetById(int id)
        {
            _logger.LogDebug("Inside GetById UserService endpoint");
            try
            {
                var res = await _userRepository.GetById(id);
                if (res == null)
                {
                    _logger.LogDebug("Id not found");
                    return null;

                }
                _logger.LogDebug($"The response for the GetById UserService is .{res.UserId}");
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<Users> Add(Users user)
        {
            _logger.LogDebug("Inside Add UserService endpoint");
            try
            {
                var res = await _userRepository.Add(user);
                _logger.LogDebug($"The response for the Add UserService is .{res.UserId}");
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new Users();
            }

        }

        public async Task<Users> Update(int id, Users user)
        {
            _logger.LogDebug("Inside Update UserService endpoint");
            try
            {
                var res = await _userRepository.Update(id, user);
                if (res == null)
                {
                    _logger.LogDebug("Id not found");
                    return null;

                }
                _logger.LogDebug($"Updated {id} ");
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }

        }

        public async Task<Users> Delete(int id)
        {
            _logger.LogDebug("Inside Delete UserService endpoint");
            try
            {
                var res = await _userRepository.Delete(id);
                if (res == null)
                {
                    _logger.LogDebug("Id not found");
                    return null;

                }
                _logger.LogDebug($"deleted: {res.UserId}");
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }


    }
}
