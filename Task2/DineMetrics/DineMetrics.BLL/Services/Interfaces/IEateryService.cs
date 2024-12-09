using DineMetrics.Core.Dto;
using DineMetrics.Core.Shared;

namespace DineMetrics.BLL.Services.Interfaces;

public interface IEateryService
{
    Task<ServiceResult<EateryDto>> UpdateOperatingHours(Guid eateryId, string from, string to);
    Task<ServiceResult<EateryDto>> UpdateMaximumCapacity(Guid eateryId, int capacity);
    Task<ServiceResult<EateryDto>> UpdateTemperatureThreshold(Guid eateryId, double minTemperature);
}