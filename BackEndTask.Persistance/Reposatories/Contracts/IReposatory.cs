using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTask.Persistance.Reposatories.Contracts
{
    public interface IReposatory<Enitiy> where Enitiy:class
    {
        Task InsertAsync(Enitiy input);
        void Insert(Enitiy input);
        Task<IQueryable<Enitiy>> GetAllAsync();
        IQueryable<Enitiy> GetAll();
        int SaveChanges();
        Task<int> SaveChangesAsync();

        Task UpdateAsync(Enitiy input);
        void Update(Enitiy input);
        Task DeleteAsync(Enitiy input);
        void Delete(Enitiy input);



    }
}
