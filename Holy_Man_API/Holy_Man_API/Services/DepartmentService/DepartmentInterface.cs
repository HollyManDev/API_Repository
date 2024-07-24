using Holy_Man_API.Models;
using Holy_Man_API.ModelsView;
using Holy_Man_API.ServerResponse;

namespace Holy_Man_API.Services.DepartmentService
{
    public interface DepartmentInterface
    {
        
        Task<ServiceResponse<List<DepartmentModel>>> GetDepartments();
        Task<ServiceResponse<DepartmentModel>> GetDepartment(int id);
        Task<ServiceResponse<List<DepartmentModel>>> AddDepartment(DepartmentView newDepartment);
        Task<ServiceResponse<List<DepartmentModel>>> UpdateDepartment(DepartmentView updatedDepartment);
        Task<ServiceResponse<List<DepartmentModel>>> DeleteDepartment(int id);
    }
}
