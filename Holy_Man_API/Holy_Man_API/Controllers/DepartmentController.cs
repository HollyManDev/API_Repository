using Holy_Man_API.Models;
using Holy_Man_API.ModelsView;
using Holy_Man_API.ServerResponse;
using Holy_Man_API.Services.ConversationParticipants;
using Holy_Man_API.Services.DepartmentService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Holy_Man_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentInterface _ParticipantInterface;
        public DepartmentController(DepartmentInterface PInterface)
        {
            _ParticipantInterface = PInterface;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<DepartmentModel>>>> AddDepartment(DepartmentView newDepartment)
        {
            var serviceResponse = await _ParticipantInterface.AddDepartment(newDepartment);
            return Ok(serviceResponse);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<DepartmentModel>>>> GetDepartments()
        {
            return Ok(await _ParticipantInterface.GetDepartments());
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<DepartmentModel>>>> UpdateDepartment(DepartmentView updatedDepartment)
        {
            var serviceResponse = await _ParticipantInterface.UpdateDepartment(updatedDepartment);
           return Ok(serviceResponse);
        }

        //[HttpGet("{id} Search")]
        //public async Task<ActionResult<ServiceResponse<ConversationParticipantsModel>>> GetConversationParticipants(int id)
        //{
        //    ServiceResponse<ConversationParticipantsModel> serviceResponse = await _ParticipantInterface.GetConversationParticipants(id);

        //    return Ok(serviceResponse);

        //}

        [HttpPut("Delete")]
        public async Task<ActionResult<ServiceResponse<List<DepartmentModel>>>> DeleteDepartment(DepartmentView dep)
        {
            ServiceResponse<List<DepartmentModel>> serviceResponse = await _ParticipantInterface.DeleteDepartment(dep.Id);

            return Ok(serviceResponse);

        }
    }
}
