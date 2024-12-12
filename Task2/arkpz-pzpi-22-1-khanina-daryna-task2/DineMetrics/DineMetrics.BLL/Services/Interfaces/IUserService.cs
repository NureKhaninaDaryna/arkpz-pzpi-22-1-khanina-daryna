using DineMetrics.Core.Models;

namespace DineMetrics.BLL.Services.Interfaces;

public interface IUserService
{
    Task<User?> GetUserByEmail(string email);
    Task<User?> GetUserById(Guid id);
    Task<bool> IsFreeEmail(string email);
}