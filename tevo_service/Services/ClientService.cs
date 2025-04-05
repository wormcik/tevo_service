using Microsoft.EntityFrameworkCore;
using tevo_service.Entities;

namespace tevo_service.Services
{
    public class ClientService
    {
        private readonly AppDbContext appDbContext;
        private readonly IConfiguration configuration;
        public ClientService(IConfiguration configuration, AppDbContext appDbContext)
        {
            this.configuration = configuration;
            this.appDbContext = appDbContext;
        }
        public async Task<List<Client>> GetAllAsync()
        {
            return await appDbContext.Client.ToListAsync();
        }

        public async Task<Client?> GetByIdAsync(long id)
        {
            return await appDbContext.Client.FindAsync(id);
        }

        public async Task<Client> AddAsync(Client client)
        {
            appDbContext.Client.Add(client);
            await appDbContext.SaveChangesAsync();
            return client;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var client = await appDbContext.Client.FindAsync(id);
            if (client == null) return false;

            appDbContext.Client.Remove(client);
            await appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Client?> UpdateAsync(Client client)
        {
            var existing = await appDbContext.Client.FindAsync(client.ClientId);
            if (existing == null) return null;

            existing.ClientName = client.ClientName;
            existing.ClientTelNo = client.ClientTelNo;
            existing.ClientSurname = client.ClientSurname;
            existing.ClientAdres = client.ClientAdres;
            existing.ClientRequestMilk = client.ClientRequestMilk;
            existing.ClientDeliverMilk = client.ClientDeliverMilk;
            existing.ClientPrice = client.ClientPrice;
            await appDbContext.SaveChangesAsync();
            return existing;
        }

        public async Task<List<Client>> FilterAsync(string? name, string? surname, string? tel, string? adres)
        {
            var query = appDbContext.Client.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(c => c.ClientName.ToLower().Contains(name.ToLower()));

            if (!string.IsNullOrWhiteSpace(surname))
                query = query.Where(c => c.ClientSurname.ToLower().Contains(surname.ToLower()));

            if (!string.IsNullOrWhiteSpace(tel))
                query = query.Where(c => c.ClientTelNo.Contains(tel));

            if (!string.IsNullOrWhiteSpace(adres))
                query = query.Where(c => c.ClientAdres.ToLower().Contains(adres.ToLower()));
            query = query.OrderBy(c => c.ClientName.ToLower());
            return await query.ToListAsync();
        }
    }
}
