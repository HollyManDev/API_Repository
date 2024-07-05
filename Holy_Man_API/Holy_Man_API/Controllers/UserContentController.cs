using Holy_Man_API.Services.UserContentServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Holy_Man_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserContentController : ControllerBase
    {
        private readonly UserContentInterfacecs _userContentService;

        public UserContentController(UserContentInterfacecs userContentService)
        {
            _userContentService = userContentService;
        }

        [HttpGet("{userId}/content/")]
        public async Task<IActionResult> GetUserContent(int userId)
        {
            var userContent = await _userContentService.GetConversationContent(userId);
            if (!userContent.Success)
            {
                return NotFound(userContent);
            }
            return Ok(userContent);
        }
    }
}
