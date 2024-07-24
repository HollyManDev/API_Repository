using Holy_Man_API.Models;
using Holy_Man_API.ModelsView;
using Holy_Man_API.ServerResponse;
using Microsoft.EntityFrameworkCore;


namespace Holy_Man_API.Services.DepartmentService
{
    public class DepartmentService: DepartmentInterface
    {

        private readonly ApplicationDbContext _context;

        public DepartmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<DepartmentModel>>> AddDepartment(DepartmentView newDepartment)
        {
            var serviceResponse = new ServiceResponse<List<DepartmentModel>>();

            var dep= new DepartmentModel();

            try
            {
                if (newDepartment == null)
                {
                    serviceResponse.menssage = "Please provide data for the Department.";
                    serviceResponse.Success = false;
                    return serviceResponse;
                }
                else
                {
                    
                    dep.Department = newDepartment.Department;
                    dep.Status = true;

                    _context.Departments.Add(dep);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = await _context.Departments.ToListAsync();
                    serviceResponse.Success = true;
                }

            }
            catch (Exception ex)
            {
                serviceResponse.menssage = $"Error creating conversation: {ex.Message}";
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<DepartmentModel>>> DeleteDepartment(int id)
        {
            var serviceResponse = new ServiceResponse<List<DepartmentModel>>();

            try
            {
                var dep = await _context.Departments.FindAsync(id);

                if (dep == null)
                {
                    serviceResponse.menssage = "Department not found.";
                    serviceResponse.Success = false;
                    return serviceResponse;
                }
                else
                {
                    dep.Status = false;
                    _context.Departments.Update(dep);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = await _context.Departments.ToListAsync();
                    serviceResponse.Success = true;
                }


            }
            catch (Exception ex)
            {
                serviceResponse.menssage = $"Error deleting Participant: {ex.Message}";
                serviceResponse.Success = false;
            }

            return serviceResponse;

        }

        public Task<ServiceResponse<DepartmentModel>> GetDepartment(int id)
        {
            throw new NotImplementedException();
        }

        public async  Task<ServiceResponse<List<DepartmentModel>>> GetDepartments()
        {
            var serviceResponse = new ServiceResponse<List<DepartmentModel>>();

            try
            {
                serviceResponse.Data = await _context.Departments.ToListAsync();
                serviceResponse.Success = true;

                if (serviceResponse.Data.Count == 0)
                {
                    serviceResponse.menssage = "No Department found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.menssage = $"Error retrieving Participants: {ex.Message}";
                serviceResponse.Success = false;
            }
            return serviceResponse; 
        }

        public async Task<ServiceResponse<List<DepartmentModel>>> UpdateDepartment(DepartmentView updatedDept)
        {
            var serviceResponse = new ServiceResponse<List<DepartmentModel>>();

            try
            {
                var gotDept = await _context.Departments.AsNoTracking().FirstOrDefaultAsync(x => x.Id == updatedDept.Id);

                if (gotDept == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.menssage = "Message not found!";
                    serviceResponse.Success = false;
                }
                else
                {
                    gotDept.Department = updatedDept.Department;
                    gotDept.Status = updatedDept.Status;

                    _context.Departments.Update(gotDept);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = await _context.Departments.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                serviceResponse.menssage = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }
    }
}
