using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using my_book_store_v1.Data.DTOs;
using my_book_store_v1.Data.Models;
using my_book_store_v1.Data.Services;

namespace my_book_store_v1.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    #region DI _userManger

    private readonly UserManager<ApiUser> _userManager;
    //private readonly SignInManager<ApiUser> _signInManager;
    private readonly ILogger<AccountController> _logger;
    private readonly IMapper _mapper;
    private readonly AuthManager _authManager;

    public AccountController(
        UserManager<ApiUser> userManager,
        //SignInManager<ApiUser> signInManager,
        ILogger<AccountController> logger,
        IMapper mapper,
        AuthManager authManager
        )
    {
        _userManager = userManager;
        //_signInManager = signInManager;
        _logger = logger;
        _mapper = mapper;
        _authManager = authManager;
    }

    #endregion

    [HttpPost]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] userDTO userDTO)
    {
        //Logger
        _logger.LogInformation($"Register Attemp For {userDTO.Email}");

        //check Model State
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var user = _mapper.Map<ApiUser>(userDTO);
            user.UserName = userDTO.Email;
            var result = await _userManager.CreateAsync(user, userDTO.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            await _userManager.AddToRolesAsync(user, userDTO.Roles);
            return Accepted();

        }
        catch (Exception ex)
        {
            //Logger Error
            _logger.LogError(ex, $"This is and error in {nameof(Register)} section");
            return Problem($"This is and error in {nameof(Register)} section", statusCode: 500);
            //return StatusCode(500, "his is and error in {nameof(Register)} section");
        }
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDTO userDTO)
    {
        //Logger
        _logger.LogInformation($"Login Attemp For {userDTO.Email}");

        //check Model State
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
           
            if (await _authManager.ValidateUser(userDTO) == false)
            {
                return Unauthorized();
            }

            return Accepted(new { token = await _authManager.CreateToken()});
        }
        catch (Exception ex)
        {
            //Logger Error
            _logger.LogError(ex, $"This is and error in {nameof(Login)} section");
            return Problem($"This is and error in {nameof(Login)} section", statusCode: 500);
            //return StatusCode(500, "his is and error in {nameof(Register)} section");
        }
    }
}
