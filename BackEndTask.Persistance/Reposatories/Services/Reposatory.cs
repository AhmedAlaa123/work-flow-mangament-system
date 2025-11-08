using BackEndTask.Data.BaseModels;
using BackEndTask.Persistance.DbContext;
using BackEndTask.Persistance.Reposatories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTask.Persistance.Reposatories.Services
{
    public class Reposatory<Entity> : IReposatory<Entity> where Entity : class
    {
        private readonly WorkFlowTrakDbContext _dbcontext;

        public Reposatory(WorkFlowTrakDbContext dbcontext)
        {
            _dbcontext=dbcontext;
        }

        public void Delete(Entity input)
        {
            if (input is null)
            {
                throw new NullReferenceException("Input Is Null");
            }
            this._dbcontext.Set<Entity>().Remove(input);
        }

        public Task DeleteAsync(Entity input)
        {
           return Task.FromResult(this._dbcontext.Set<Entity>().Remove(input));
        }

        public IQueryable<Entity> GetAll()
        {
            return this._dbcontext.Set<Entity>().AsQueryable();
        }

        public Task<IQueryable<Entity>> GetAllAsync()
        {
            return Task.FromResult<IQueryable<Entity>>(this._dbcontext.Set<Entity>());
        }

        public void Insert(Entity input)
        {
            if (input is null)
            {
                throw new NullReferenceException("Input Is Null");
            }
            if (input is FullAudit)
            {
                (input as FullAudit).CreationTime=DateTime.Now;
            }
            var entry= _dbcontext.Set<Entity>().Add(input);
            
        }

        public async Task InsertAsync(Entity input)
        {
            if (input is null)
            {
                throw new NullReferenceException("Input Is Null");
            }
            if (input is FullAudit)
            {
                (input as FullAudit).CreationTime=DateTime.Now;
            }
            _dbcontext.Set<Entity>().AddAsync(input);
        }

        public int SaveChanges()
        {
           return this._dbcontext.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return this._dbcontext.SaveChangesAsync();
        }

        public void Update(Entity input)
        {
            if (input is null)
            {
                throw new NullReferenceException("Input Is Null");
            }
            if (input is FullAudit)
            {
                (input as FullAudit).UpdateTime=DateTime.Now;
            }
            this._dbcontext.Set<Entity>().Update(input);
        }

        public Task UpdateAsync(Entity input)
        {
            if (input is null)
            {
                throw new NullReferenceException("Input Is Null");
            }
            if (input is FullAudit)
            {
                (input as FullAudit).UpdateTime=DateTime.Now;
            }
            return Task.FromResult(this._dbcontext.Set<Entity>().Update(input));
        }
    }
}
