using Microsoft.EntityFrameworkCore;
using tevo_service.Entities;

namespace tevo_service.Services
{
    public class TestService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConfiguration configuration;
        public TestService(IConfiguration configuration, AppDbContext appDbContext)
        {
            this.configuration = configuration;
            this.appDbContext = appDbContext;
        }
        public async Task<List<Test>> GetAllAsync()
        {
            return await appDbContext.Test.ToListAsync();
        }

        public async Task<Test?> GetByIdAsync(long id)
        {
            return await appDbContext.Test.FindAsync(id);
        }

        public async Task<Test> AddAsync(Test test)
        {
            appDbContext.Test.Add(test);
            await appDbContext.SaveChangesAsync();
            return test;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var test = await appDbContext.Test.FindAsync(id);
            if (test == null) return false;

            appDbContext.Test.Remove(test);
            await appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Test?> UpdateAsync(Test test)
        {
            var existing = await appDbContext.Test.FindAsync(test.TestId);
            if (existing == null) return null;

            existing.TestName = test.TestName;
            await appDbContext.SaveChangesAsync();
            return existing;
        }

    }
}
