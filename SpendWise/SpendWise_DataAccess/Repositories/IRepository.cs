using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendWise_DataAccess.Repositories
{
    public interface IRepository<IEntity> where IEntity : class
    {
        Task<IEnumerable<IEntity>> GetAllAsync();

        Task<IEntity?> FindByIdAsync(int id);

        Task<IEntity> PostAsync(IEntity entity);

        Task<IEntity> UpdateAsync(IEntity entity);

        Task DeleteAsync(int id);
    }
}
