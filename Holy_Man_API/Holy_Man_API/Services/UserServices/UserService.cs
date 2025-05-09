﻿
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

                else
                {
                    gotUser.Status = true;
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
               
                    userModel.Lastname = newUser.Lastname;
                    userModel.Firstname = newUser.Firstname;
                    userModel.Gender = newUser.Gender;
                    userModel.Age = newUser.Age;
                    userModel.Email = newUser.Email;
                    userModel.Password = newUser.Password;
                    userModel.Status = newUser.Status;
                    userModel.DepartmentId = newUser.DepartmentId;
                    userModel.Acess = newUser.Acess;

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
                else
                {
                    gotUser.Status = false;
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
                    gotUser.DepartmentId = UpdateUser.DepartmentId;
                    gotUser.Acess = UpdateUser.Acess;

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
        public async Task<ServiceResponse<UserModel>> AuthenticateUser(string email, string password)
        {
            ServiceResponse<UserModel> serviceResponse = new ServiceResponse<UserModel>();

            try
            {
                // Procura o usuário pelo email na base de dados
                UserModel user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

                // Verifica se o usuário foi encontrado e se a senha está correta
                if (user == null || user.Password != password || user.Status != true)
                {
                    serviceResponse.Data = null;
                    serviceResponse.menssage = "Invalid email or password.";
                    serviceResponse.Success = false;
                }
                else
                {
                    // Se o usuário foi encontrado e a senha está correta, retorna o usuário
                    serviceResponse.Data = user;
                    serviceResponse.menssage = "User authenticated successfully.";
                    serviceResponse.Success = true;
                   
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
