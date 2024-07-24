using Holy_Man_API.Models;
using Holy_Man_API.ModelsView;
using Holy_Man_API.ServerResponse;
using Holy_Man_API.Services.UserServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Holy_Man_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserInterface _userInterface;
        public UserController(UserInterface userInterface)
        { 
          _userInterface = userInterface;   
        } 

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<UserModel>>>> GetUsers()
        {
            return Ok(await _userInterface.GetUsers());    
        }

        [HttpGet("{id}")]
        public async Task<ActionResult< ServiceResponse<UserModel>>> FindUser(int id)
        {
            ServiceResponse<UserModel> serviceResponse = await _userInterface.FindUSER(id);
            
            return Ok(serviceResponse); 

        }

        [HttpPut("DeleteUser")]
        public async Task<ActionResult<ServiceResponse<List<UserModel>>>> InactivateUSER(UserView user)
         {
            ServiceResponse<List<UserModel>> serviceResponse = await _userInterface.InactivateUSER(user.Id);

            return Ok(serviceResponse);

        }

        [HttpPut("ActivateUser")]
        public async Task<ActionResult<ServiceResponse<List<UserModel>>>> ActivateUSER(int id)
        {
            ServiceResponse<List<UserModel>> serviceResponse = await _userInterface.ActivateUSER(id);

            return Ok(serviceResponse);

        }
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<UserModel>>>> UpdateUser(UserView UpdatedUser)
        {
            ServiceResponse<List<UserModel>> serviceResponse = await _userInterface.UpdateUser(UpdatedUser);

            return Ok(serviceResponse);

        }


        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<UserModel>>>> CreateUser(UserView newUser)
        {
            return Ok(await _userInterface.CreateUser(newUser));
        }

           [HttpPost("authenticate")]
        public async Task<ActionResult<ServiceResponse<UserModel>>> AuthenticateUser(UserAuthView userCredentials)
        {
            var response = await _userInterface.AuthenticateUser(userCredentials.Email, userCredentials.Password);
            
            if (response.Success && response.Data != null)
            {
                return Ok(new ServiceResponse<UserModel>
                {
                    Data = response.Data,
                    menssage = "User authenticated successfully.",
                    Success = true
                });
            }
            else
            {
                return Unauthorized(new ServiceResponse<UserModel>
                {
                    Data = null,
                    menssage = response.menssage, // Mensagem de erro da autenticação
                    Success = false
                });
            }
        }

    }
}
