using Microsoft.EntityFrameworkCore;
using SpendWise_DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SpendWise_DataAccess.Repositories
{
    public class CategoryRepository : BaseRepository<Category>
    {
        private readonly SpendWiseContext _context;

        public CategoryRepository(SpendWiseContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Category>> GetAllAsync()
        {
            try
            {
                return await _context.categories.Include(c => c.Products).ThenInclude(c => c.CartProducts).ThenInclude(c => c.Cart).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in BaseRepository.GetAllAsync()", ex);
            }
        }

        
        public override async Task<Category?> FindByIdAsync(int id)
        {
            try
            {
                return await _context.categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in BaseRepository.GetByIdAsync()", ex);
            }
        }
     
        
    }
}
