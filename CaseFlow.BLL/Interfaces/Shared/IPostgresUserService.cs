namespace CaseFlow.BLL.Interfaces.Shared;

public interface IPostgresUserService
{
    Task CreateUserAsync(string username, string password, string role);
    
    Task UpdateUsernameAsync(string oldUsername, string newUsername);
    Task UpdatePasswordAsync(string oldPassword, string newPassword);
    Task DisableUserAsync(string username);
    Task EnableUserAsync(string username);
    Task DeleteUserAsync(string username);
}