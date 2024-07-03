using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UserModule.ApiResponse;
using UserModule.Models;
using UserModule.Service;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UserModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        #region User

        /// <summary>
        /// Get User List
        /// </summary>
        /// <param></param>
        /// <returns>Return Bank User List</returns>
        [HttpGet]
        [Route("GetUser")]
        public async Task<ActionResult<IEnumerable<Users>>> GetUser()
        {
            _logger.LogDebug("Inside GetUser endpoint");
            var result = new List<Users>();
            try
            {
                result = (await _userService.GetAll()).ToList();
                if (result.Count > 0)
                {
                    var response = new ApiResponse<dynamic>
                    {
                        Status = Constants.Success,
                        Message = Constants.Get,
                        Data = result
                    };
                    _logger.LogDebug($"The response for the get user is .{result.Count}");
                    return Ok(response);
                }
                else
                {
                    var response = new ApiResponse<dynamic>
                    {
                        Status = Constants.Badrequest,
                        Message = Constants.Failure,
                    };
                    _logger.LogDebug($"The response for the get user is .{result.Count}");
                    return NotFound(response);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Find User Details By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return User Details</returns>
        [HttpGet]
        [Route("GetUserById/{id}")]
        public async Task<ActionResult<Users>> GetUserById(int id)
        {
            _logger.LogDebug("Inside Find User endpoint");

            try
            {
                if (id <= 0)
                {
                    var response = new ApiResponse<dynamic>
                    {
                        Status = Constants.Badrequest,
                        Message = Constants.Failure,
                    };
                    _logger.LogDebug($"The response for the get user is .{response.Status}");
                    return NotFound(response);
                }
                else
                {
                    var result = await _userService.GetById(id);
                    if (result != null)
                    {
                        var response = new ApiResponse<dynamic>
                        {
                            Status = Constants.Success,
                            Message = Constants.Get,
                            Data = result
                        };
                        _logger.LogDebug($"The response for the get user is .{response.Status}");
                        return Ok(response);
                    }
                    else
                    {
                        var response = new ApiResponse<dynamic>
                        {
                            Status = Constants.Badrequest,
                            Message = Constants.Failure,
                        };
                        _logger.LogDebug($"The response for the get user is .{response.Status}");
                        return NotFound(response);
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Add User Details
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Save User Details</returns>
        [HttpPost]
        [Route("AddNewUserDetails")]
        public async Task<ActionResult<Users>> AddUser(Users user)
        {
            _logger.LogDebug("Inside AddUser endpoint");
            try
            {
                var employees = await _userService.Add(user);
                if (employees == null)
                {
                    return NotFound("Employee is null here");
                }
                _logger.LogDebug($"The response for the Add user is .{user.UserId}");
                return CreatedAtAction(nameof(GetUser), new { id = user.UserId, Status = Constants.Success, Message = Constants.Add }, user);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return CreatedAtAction(nameof(GetUser), new { Status = Constants.Badrequest, Message = Constants.Failure });

            }
        }

        /// <summary>
        /// Update User Details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns>Return Updated User Details</returns>
        [HttpPut]
        [Route("UpdateUserDetails/{id}")]
        public async Task<ActionResult<Users>> UpdateUser(int id, Users user)
        {
            _logger.LogDebug("Inside UpdateUser endpoint");
            try
            {
                if (id == user.UserId)
                {
                    var result = await _userService.Update(id, user);
                    if (result != null)
                    {
                        var response = new ApiResponse<dynamic>
                        {
                            Status = Constants.Success,
                            Message = Constants.Update,
                            Data = user
                        };
                        _logger.LogDebug($"The response for the UpdateUser is .{user.UserId}");
                        return Ok(response);
                    }
                    else
                    {

                        var response = new ApiResponse<dynamic>
                        {
                            Status = Constants.Badrequest,
                            Message = Constants.Failure,
                        };
                        _logger.LogError("No Data Found");
                        return NotFound(response);
                    }
                }

                else
                {
                    var response = new ApiResponse<dynamic>
                    {
                        Status = Constants.Badrequest,
                        Message = Constants.Failure,
                    };
                    _logger.LogError("UserId not found");
                    return NotFound(response);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete User Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Data will be Deleted from records</returns>
        [HttpDelete]
        [Route("DeleteUserDetails/{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            _logger.LogDebug("Inside DeleteUser endpoint");
            try
            {
                if (id <= 0)
                {
                    var response = new ApiResponse<dynamic>
                    {
                        Status = Constants.Badrequest,
                        Message = Constants.Failure,
                    };
                    _logger.LogError("No Data Found");
                    return NotFound(response);
                }
                else
                {
                    var result = await _userService.Delete(id);
                    if (result != null)
                    {
                        var response = new ApiResponse<dynamic>
                        {
                            Status = Constants.Success,
                            Message = Constants.Delete,
                            Data = result
                        };
                        _logger.LogDebug($"The response for the DeleteUser is .{id}");
                        return Ok(response);
                    }
                    else
                    {
                        var response = new ApiResponse<dynamic>
                        {
                            Status = Constants.Badrequest,
                            Message = Constants.Failure,
                        };
                        _logger.LogError("No Data Found");
                        return NotFound(response);
                    }
                }


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        #endregion User
    }
}
