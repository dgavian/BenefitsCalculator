using Api.Dtos.Dependent;

namespace Api.Services
{
    public interface IDependentService
    {
        Task<List<GetDependentDto>> GetAllDependentsAsync();
        Task<GetDependentDto?> GetDependentAsync(int dependentId);
    }
}