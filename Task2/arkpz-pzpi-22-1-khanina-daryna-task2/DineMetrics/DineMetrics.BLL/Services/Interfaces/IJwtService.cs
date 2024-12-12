using DineMetrics.Core.Models;

namespace DineMetrics.BLL.Services.Interfaces;

public interface IJwtService
{
    public string GenerateJwtToken(User user);
    public Guid? ValidateJwtToken(string? token);
}