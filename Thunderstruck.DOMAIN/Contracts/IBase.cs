using System.Collections.Generic;
using System.Threading.Tasks;

namespace Thunderstruck.DOMAIN.Contracts
{
    public interface IBase<T>
    {
        //Get
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAsync(int skip, int take);
      
        //Crud
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);

    }
}