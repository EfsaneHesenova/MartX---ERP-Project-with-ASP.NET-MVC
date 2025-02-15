using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartX.Core.Models.Common;
using MartX.DAL.Contexts;
using MartX.DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace MartX.DAL.Repositories.Implementations
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity, new()
    {
        private readonly AppDbContext _context;

        public WriteRepository(AppDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task CreateAsync(T entity)
        {
            await Table.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            Table.Remove(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            int rows = await _context.SaveChangesAsync();
            return rows;
        }

        public void Update(T entity)
        {
            Table.Update(entity);
        }
    }
}
