using Api.Dtos.Dependent;
using Api.Models;
using Api.Repositories;

namespace Api.Services
{
    public class DependentService : IDependentService
    {
        private Repository<Dependent> _repository;

        public DependentService(Repository<Dependent> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetDependentDto>> GetAllDependentsAsync()
        {
            var dependents = await _repository.GetAllAsync();
            return ModelToDtoMapper.MapDependents(dependents);
        }

        public async Task<GetDependentDto?> GetDependentAsync(int dependentId)
        {
            var dependent = await _repository.GetAsync(dependentId);
            var result = dependent != null ? ModelToDtoMapper.MapDependent(dependent) : null;
            return result;
        }
    }
}
