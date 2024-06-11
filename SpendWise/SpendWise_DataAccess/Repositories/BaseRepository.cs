using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendWise_DataAccess.Repositories
{
    public class BaseRepository<IEntity> : IRepository<IEntity> where IEntity : class
    {

        private readonly SpendWiseContext _context;

        public BaseRepository(SpendWiseContext context)
        {
            _context = context;
        }


        public virtual async Task<IEntity> PostAsync(IEntity entity)
        {
            try
            {
                await _context.Set<IEntity>().AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;

            }
            catch(Exception ex)
            {
                throw new Exception($"Error when adding data to DB: {ex.Message}", ex);
            }
        }

        public virtual async Task DeleteAsync(int id)
        {
            try
            {
                var entity = await _context.Set<IEntity>().FindAsync(id);
                if(entity == null)
                {
                    return;
                }

                _context.Set<IEntity>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception("Error in BaseRepository.DeleteAsync()", ex);
            }
        }

        public virtual async Task<IEntity?> FindByIdAsync(int id)
        {
            try
            {
                return await _context.Set<IEntity>().FindAsync(id);
            }
            catch(Exception ex)
            {
                throw new Exception($"Error when retrieving entity by id - {id}", ex);
            }
        }

        public virtual async Task<IEnumerable<IEntity>> GetAllAsync()
        {
            try
            {
                return await _context.Set<IEntity>().ToListAsync();
            }
            catch(Exception ex)
            {
                throw new Exception("Error in BaseRepository.GetAllAsync()", ex);
            }
        }

        public virtual async Task<IEntity> UpdateAsync(IEntity entity)
        {
            try
            {
                _context.Set<IEntity>().Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return entity;
            }
            catch(Exception ex)
            {
                throw new Exception("Error in BaseRepository.UpdateAsync()", ex);
            }
        }
    }
}
