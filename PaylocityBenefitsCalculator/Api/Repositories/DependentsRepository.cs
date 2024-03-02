using Api.Models;

namespace Api.Repositories
{
    public class DependentsRepository : Repository<Dependent>
    {
        public override async Task<List<Dependent>> GetAllAsync()
        {
            var result = new List<Dependent>();
            var employees = await GetAllEmployeesAsync();
            employees.ForEach(e =>
            {
                if (e.Partner != null)
                {
                    result.Add(e.Partner);
                }
                result.AddRange(e.Children);
            });
            return result;
        }

        public override async Task<Dependent?> GetAsync(int id)
        {
            var dependents = await GetAllAsync();
            var dependent = dependents.FirstOrDefault(d => d.Id == id);
            return dependent;
        }
    }
}
