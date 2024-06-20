
using Holy_Man_API.Models;
using Holy_Man_API.ModelsView;
using Holy_Man_API.ServerResponse;
using Microsoft.EntityFrameworkCore;

namespace Holy_Man_API.Services.UserServices
{
    public class UserService : UserInterface
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<List<UserModel>>> ActivateUSER(int id)
        {
            ServiceResponse<List<UserModel>> serviceResponse = new ServiceResponse<List<UserModel>>();

            try
            {
                UserModel gotUser = _context.Users.FirstOrDefault(x => x.Id == id);

                if (gotUser == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.menssage = "User not found!";
                    serviceResponse.Success = false;
                }

                gotUser.Status = true;
                _context.Update(gotUser);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _context.Users.ToList();

            }
            catch (Exception ex)
            {
                serviceResponse.menssage = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }
    

        public async Task<ServiceResponse<List<UserModel>>> CreateUser(UserView newUser)
        {
            ServiceResponse<List<UserModel>> serviceResponse = new ServiceResponse<List<UserModel>>();

           var userModel = new UserModel();
            try
            {

                if (newUser == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.menssage = "Introduce data!";
                    serviceResponse.Success = false;

                    return serviceResponse;
                }
                else
                {
                    userModel.Id = newUser.Id; 
                    userModel.Lastname = newUser.Lastname;
                    userModel.Firstname = newUser.Firstname;
                    userModel.Gender = newUser.Gender;
                    userModel.Age = newUser.Age;
                    userModel.Email = newUser.Email;
                    userModel.Password = newUser.Password;
                    userModel.Status = newUser.Status;
                    _context.Add(userModel);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _context.Users.ToList(); 
                }


            }
            catch (Exception ex)
            {
                serviceResponse.menssage = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        
        }

        public async Task<ServiceResponse<UserModel>> FindUSER(int id)
        {
            ServiceResponse<UserModel> serviceResponse = new ServiceResponse<UserModel>();

            try
            {
                UserModel gotUser = _context.Users.FirstOrDefault(x => x.Id == id);

                if (gotUser == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.menssage = "User not found!";
                    serviceResponse.Success = false;
                }

                serviceResponse.Data = gotUser;
                
            }
            catch (Exception ex)
            {
                serviceResponse.menssage = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<UserModel>>> GetUsers()
        {
            ServiceResponse<List<UserModel>> serviceResponse = new ServiceResponse<List<UserModel>>();

            try
            {

                serviceResponse.Data = _context.Users.ToList(); 
                
                if(serviceResponse.Data.Count == 0)
                {
                    serviceResponse.menssage = "No data available!";
                }

            } catch (Exception ex)
            {
                serviceResponse.menssage = ex.Message;
                serviceResponse.Success = false; 
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<UserModel>>> InactivateUSER(int id)
        {
            ServiceResponse<List<UserModel>> serviceResponse = new ServiceResponse<List<UserModel>>();

            try
            {
                UserModel gotUser = _context.Users.FirstOrDefault(x => x.Id == id);

                if (gotUser == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.menssage = "User not found!";
                    serviceResponse.Success = false;
                }

                gotUser.Status = false;
                _context.Update(gotUser);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _context.Users.ToList();

            }
            catch (Exception ex)
            {
                serviceResponse.menssage = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<UserModel>>> UpdateUser(UserView UpdateUser)
        {
            ServiceResponse<List<UserModel>> serviceResponse = new ServiceResponse<List<UserModel>>();
           
            try
            {
                UserModel gotUser = _context.Users.AsNoTracking().FirstOrDefault(x => x.Id == UpdateUser.Id);

                if (gotUser == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.menssage = "User not found!";
                    serviceResponse.Success = false;
                }
                else
                {
                    gotUser.Firstname = UpdateUser.Firstname;
                    gotUser.Lastname = UpdateUser.Lastname;
                    gotUser.Age = UpdateUser.Age;
                    gotUser.Gender = UpdateUser.Gender;
                    gotUser.Status = UpdateUser.Status;
                    gotUser.Email = UpdateUser.Email;
                    gotUser.Password = UpdateUser.Password;

                    _context.Update(gotUser);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _context.Users.ToList();
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
