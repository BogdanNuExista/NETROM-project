using Microsoft.EntityFrameworkCore;
using SpendWise_DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendWise_DataAccess.Repositories
{
    public class ProductRepository : BaseRepository<Product>
    {
        private readonly SpendWiseContext _context;

        public ProductRepository(SpendWiseContext context) : base(context)
        {
            _context = context;
        }


        public override async Task<IEnumerable<Product>> GetAllAsync()
        {
            try
            {
                return await _context.products.Include(p => p.Categories).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in BaseRepository.GetAllAsync()", ex);
            }
        }

    }
}
