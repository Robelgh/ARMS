using Application.Contracts.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly PNSDbContext _context;

        public GenericRepository(PNSDbContext context)
        {
            _context = context;
        }

        public async Task<T> Add(T entity)
        {
            try
            {
                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task Delete(T entity)
        {
            var entry = _context.Entry(entity);

            // After deleting related entities, remove the main entity
            _context.Set<T>().Remove(entity);

            // Save changes to the database
            await _context.SaveChangesAsync();
        }

        public Task<bool> Exists(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                // Fetch all records from the database first
                var allEntities = await _context.Set<T>().ToListAsync();

                // Check if the entity has the 'Status' property and filter accordingly
                var statusProperty = typeof(T).GetProperty("Status");

                if (statusProperty != null && statusProperty.PropertyType == typeof(int))
                {
                    // Filter the entities in memory if 'Status' exists and is of type 'int'
                    return allEntities.Where(entity => (int)statusProperty.GetValue(entity) != 0);
                }

                // If 'Status' property doesn't exist or is not of type 'int', return all records
                return allEntities;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw;
            }
        }


        public async Task<T> GetById(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }


        public async Task Update(T entity)
        {

            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public void DetachEntity(T entity)
        {
            var entry = _context.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Detached;
            }
        }


    }
}
