using Microsoft.EntityFrameworkCore;
using tevo_service.Entities;
using tevo_service.Models;
using tevo_service.Models.DTOs;

namespace tevo_service.Services
{
    public class ProductService
    {
        private readonly AppDbContext appDbContext;

        public ProductService(AppDbContext context)
        {
            appDbContext = context;
        }

        public async Task<ResultModel<List<Product>>> GetAllAsync()
        {
            var res = await appDbContext.Product
                .ToListAsync();
            return new ResultModel<List<Product>>().Success(res);

        }

        public async Task<ResultModel<bool>> AddAsync(ProductModel model)
        {
            var dbAdd = new Product()
            {
                ProductName = model.ProductName,
                Unit = model.Unit,
            };
            await appDbContext.Product.AddAsync(dbAdd);
            var result = await appDbContext.SaveChangesAsync();
            return new ResultModel<bool>().Success(true);
        }

        public async Task<ResultModel<bool>> DeleteAsync(ProductModel model)
        {
            var dbRes = await appDbContext.Product
                .FirstOrDefaultAsync(x => x.ProductId == model.ProductId);
            appDbContext.Product.Remove(dbRes);
            var result = await appDbContext.SaveChangesAsync();
            return new ResultModel<bool>().Success(true);
        }
    }
}
