using Holy_Man_API.Models;
using Holy_Man_API.ModelsView;
using Holy_Man_API.ServerResponse;


namespace Holy_Man_API.Services.UserServices
{
    public interface UserInterface
    {
        Task<ServiceResponse<List<UserModel>>> GetUsers();
        Task<ServiceResponse<List<UserModel>>> CreateUser( UserView newUser);
        Task<ServiceResponse<UserModel>> FindUSER(int id);
        Task<ServiceResponse<List<UserModel>>> UpdateUser(UserView UpdateUser);
        Task<ServiceResponse<List<UserModel>>> InactivateUSER(int id);
        Task<ServiceResponse<List<UserModel>>> ActivateUSER(int id);
        Task<ServiceResponse<UserModel>> AuthenticateUser(string email, string password);
    



    }
}
